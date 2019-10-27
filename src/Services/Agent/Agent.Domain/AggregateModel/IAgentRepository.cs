using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Agent.Domain.AggregateModel
{
    public interface IAgentRepository
    {
        Task<Agent> AddAsync(Agent agent);
        Task<Agent> UpdateAgent(Agent agent);

        Task<Agent> FindById(string id,string companyRef);

        Task<IEnumerable<Agent>> FindFreelance();

        Task<IEnumerable<Agent>> FindByCompany(string companyRef);
    }
}
