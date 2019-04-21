using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AlgoMe.Models;
using AlgoMe.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AlgoMe.Controllers {
    [Route("api/[controller]")]
    public class SampleDataController : Controller {
        
        private readonly IDataRepository<Parameter> _parameterRepository;
        private readonly IDataRepository<Request> _requestRepository;

        public SampleDataController(IDataRepository<Parameter> parameterRepository, IDataRepository<Request> requestRepository) {
            _parameterRepository = parameterRepository;
            _requestRepository = requestRepository;
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
            ThreadPool.QueueUserWorkItem(TaskCallback, request);
        }
        
        private async void TaskCallback(object request) {
            var r = (Request) request;
            
            // TODO: Exception goes here -- Cannot access a disposed object
            var realR = await _requestRepository.Get(r.RequestId);
            
            var items = r.Parameters.Select(p => new Algorithm.Item {Value = p.Price, Weight = p.Weight}).ToArray();
            var answer = Algorithm.Knapsack(items, r.Capacity);
            r.Answer = answer;
            r.Status = true;
            await _requestRepository.Update(realR, r);
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