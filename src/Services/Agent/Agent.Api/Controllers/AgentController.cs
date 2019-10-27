using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agent.Domain.AggregateModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Agent.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgentController : ControllerBase
    {
        private readonly IAgentRepository _repository;

        public AgentController(IAgentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("{id}/{companyRef}")]
        public async Task<IActionResult> Get(string id,string companyRef)
        {
            var agent = await _repository.FindById(id, companyRef);

            if (agent == null)
                return NotFound();

            return Ok(agent);
        }

        
    }
}
