using System.Collections.Generic;
using System.Linq;
using AlgoMe.Models.Repository;

namespace AlgoMe.Models.DataManager {
    public class RequestManager : IDataRepository<Request> {
        readonly AlgoMeContext _algomeContext;
 
        public RequestManager(AlgoMeContext context) {
            _algomeContext = context;
        }
 
        public IEnumerable<Request> GetAll() {
            return _algomeContext.Requests.ToList();
        }
 
        public Request Get(long id) {
            return _algomeContext.Requests
                .FirstOrDefault(e => e.RequestId == id);
        }
 
        public void Add(Request entity) {
            _algomeContext.Requests.Add(entity);
            _algomeContext.SaveChanges();
        }
 
        public void Update(Request request, Request entity) {
            request.Name = entity.Name;
            request.Answer = entity.Answer;
            request.Status = entity.Status;
            request.Capacity = entity.Capacity;
            request.Parameters = entity.Parameters;
            request.Percentage = entity.Percentage;
            request.FullAnswer = entity.FullAnswer;
            request.ProcessTime = entity.ProcessTime;
 
            _algomeContext.SaveChanges();
        }
 
        public void Delete(Request request) {
            _algomeContext.Requests.Remove(request);
            _algomeContext.SaveChanges();
        }
    }
}