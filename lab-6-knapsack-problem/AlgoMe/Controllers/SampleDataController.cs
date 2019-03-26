using System;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<Request> Requests() {
            return _requestRepository.GetAll();
        }

        [HttpDelete("[action]")]
        public void DeleteRequest([FromBody] long id) {
            var toDelete = _requestRepository.Get(id);
            if (toDelete != null) {
                _parameterRepository.DeleteAll(toDelete.Parameters);
                _requestRepository.Delete(toDelete);
            }
        }
        
        [HttpPost("[action]")]
        public void PostRequest([FromBody] Request request) {
            _requestRepository.Add(request);
        }
    }
}