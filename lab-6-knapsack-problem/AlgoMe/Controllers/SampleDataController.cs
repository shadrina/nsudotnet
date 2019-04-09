using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AlgoMe.Models;
using AlgoMe.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AlgoMe.Controllers {
    [Route("api/[controller]")]
    public class SampleDataController : Controller {
        
        private readonly IDataRepository<Parameter> _parameterRepository;
        private IDataRepository<Request> _requestRepository;

        public SampleDataController(IDataRepository<Parameter> parameterRepository, IDataRepository<Request> requestRepository) {
            _parameterRepository = parameterRepository;
            _requestRepository = requestRepository;
        }

        [HttpGet("[action]")]
        public IEnumerable<Request> Requests() {
            return _requestRepository.GetAll();
        }

        [HttpDelete("[action]")]
        public void DeleteRequest([FromBody] long id) {
            var toDelete = _requestRepository.Get(id);
            if (toDelete == null) return;
            _parameterRepository.DeleteWhere(p => p.Request.RequestId == id);
            _requestRepository.DeleteWhere(r => r.RequestId == id);
        }
        
        [HttpPost("[action]")]
        public void PostRequest([FromBody] Request request) {
            _requestRepository.Add(request);
            object[] argsArray = new object[2];
            argsArray[0] = request;
            //argsArray[1] = _requestRepository;
            ThreadPool.QueueUserWorkItem(TaskCallback, argsArray);
        }
        
        
        
        
        private void TaskCallback(object args) {
            var argArray = (Array) args;
            var r = (Request) argArray.GetValue(0);
            var realR = _requestRepository.Get(r.RequestId);
            //IDataRepository<Request>) argArray.GetValue(1);
            var items = r.Parameters.Select(p => new Algorithm.Item {Value = p.Price, Weight = p.Weight}).ToArray();
            var answer = Algorithm.Knapsack(items, r.Capacity);
            r.Answer = answer;
            
            // TODO: нельзя так делать!
            _requestRepository.Update(realR, r);
        }
    }
}