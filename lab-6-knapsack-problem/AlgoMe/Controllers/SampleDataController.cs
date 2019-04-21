using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AlgoMe.Models;
using AlgoMe.Models.DataManager;
using AlgoMe.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlgoMe.Controllers {
    [Route("api/[controller]")]
    public class SampleDataController : Controller {
        
        private readonly IDataRepository<Parameter> _parameterRepository;
        private readonly IDataRepository<Request> _requestRepository;
        
        private readonly DbContextOptionsBuilder<AlgoMeContext> _optionsBuilder = new DbContextOptionsBuilder<AlgoMeContext>();
        

        public SampleDataController(IDataRepository<Parameter> parameterRepository, IDataRepository<Request> requestRepository) {
            _parameterRepository = parameterRepository;
            _requestRepository = requestRepository;
            _optionsBuilder.UseNpgsql(
                "User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=algome;Pooling=true;",
                b => b.MigrationsAssembly("AlgoMe")
                );
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
        
        private async void ProcessRequest(Request request) {
            // TODO: The direct use of DbContext here is a patch
            // TODO: It spoils the idea of using repository in controller
            // Btw it works ¯\_(ツ)_/¯
            using(var context = new AlgoMeContext(_optionsBuilder.Options)) {
                var requestRepository = new RequestManager(context);
                var realR = await requestRepository.Get(request.RequestId);
            
                var items = request.Parameters.Select(p => new Algorithm.Item {Value = p.Price, Weight = p.Weight}).ToArray();
                var answer = Algorithm.Knapsack(items, request.Capacity);
                request.Answer = answer;
                request.Status = true;
                await requestRepository.Update(realR, request);
            }
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