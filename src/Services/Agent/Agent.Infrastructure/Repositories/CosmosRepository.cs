using Model = Agent.Domain.AggregateModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Agent.Infrastructure.Repositories
{
    public class CosmosRepository : Model.IAgentRepository
    {
        private readonly Container _container;
        private readonly ILogger<CosmosRepository> _logger;

        public CosmosRepository(IConfiguration configuration,
                                ILogger<CosmosRepository> logger)
        {
            var cosmosClient = new CosmosClient(configuration["Cosmos:ConnectionString"]);
            _container = cosmosClient.GetContainer(configuration["Cosmos:DatabaseId"], 
                                                   configuration["Cosmos:ContainerId"]);

            _logger = logger;
        }

        public async Task<Model.Agent> AddAsync(Model.Agent agent)
        {
            return await _container.CreateItemAsync(agent, new PartitionKey(agent.CompanyRef));
        }

        public async Task<IEnumerable<Model.Agent>> FindByCompany(string companyRef)
        {
            var query = new QueryDefinition($"select * from Agent a where a.companyRef = '{companyRef}'");

            return await ExtractResultQuery(query);
        }

        public async Task<Model.Agent> FindById(string id,string companyRef)
        {
            return await _container.ReadItemAsync<Model.Agent>(id, new PartitionKey(companyRef));
        }

        public async Task<IEnumerable<Model.Agent>> FindFreelance()
        {
            var query = new QueryDefinition("select * from Agent a where a.companyRef = 'Freelancer'");

            return await ExtractResultQuery(query);

        }

        public async Task<Model.Agent> UpdateAgent(Model.Agent agent)
        {
            return await _container.UpsertItemAsync<Model.Agent>(agent, new PartitionKey(agent.CompanyRef));
        }

        private async Task<IEnumerable<Model.Agent>> ExtractResultQuery(QueryDefinition query)
        {
            FeedIterator<Model.Agent> feedIterator = _container.GetItemQueryIterator<Model.Agent>(query);

            var agents = new List<Model.Agent>();

            while (feedIterator.HasMoreResults)
            {
                var response = await feedIterator.ReadNextAsync();
                agents.AddRange(response.ToList());
            }

            return agents;
        }
    }
}
