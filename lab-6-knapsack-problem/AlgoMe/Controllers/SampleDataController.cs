using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlgoMe.Models;
using AlgoMe.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlgoMe.Controllers {
    [Route("api/[controller]")]
    public class SampleDataController : Controller {
        
        private readonly IDataRepository<Parameter> _parameterRepository;
        private readonly IDataRepository<Request> _requestRepository;
        
        private readonly DbContextOptionsBuilder<AlgoMeContext> _optionsBuilder 
            = new DbContextOptionsBuilder<AlgoMeContext>();
        

        public SampleDataController(
            IDataRepository<Parameter> parameterRepository, 
            IDataRepository<Request> requestRepository) {
            _parameterRepository = parameterRepository;
            _requestRepository = requestRepository;
            _optionsBuilder.UseNpgsql(
                "User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=algome;Pooling=true;",
                b => b.MigrationsAssembly("AlgoMe")
                );
        }
        
        [HttpPost("[action]")]
        public async Task<ActionResult> SignInRequest(Object _) {
            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<Request>> Requests() {
            return await _requestRepository.GetAll();
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult> DeleteRequest([FromBody] long id) {
            var toDelete = await _requestRepository.Get(id);
            if (toDelete == null) return BadRequest();;
            await _parameterRepository.DeleteWhere(p => p.Request.RequestId == id);
            await _requestRepository.DeleteWhere(r => r.RequestId == id);
            return Ok();
        }
        
        [HttpPost("[action]")]
        public async Task PostRequest([FromBody] Request request) {
            await _requestRepository.Add(request);
            await Task.Run(() => ProcessRequest(request));
        }
        
        [HttpPost("[action]")]
        public async Task<ActionResult> RenewRequest([FromBody] long id) {
            var toRenew = await _requestRepository.Get(id);
            if (toRenew == null) return BadRequest();
            toRenew.Answer = 0;
            toRenew.Status = false;
            await Task.Run(() => ProcessRequest(toRenew));
            return Ok();
        }
        
        private async void ProcessRequest(Request request) {
            await Algorithm.Knapsack(_optionsBuilder.Options, request);
        }
        
        protected override void Dispose(bool disposing) {
            if (disposing) {
                _requestRepository.Dispose();
                _parameterRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}