using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using AlgoMe.Models.Repository;

namespace AlgoMe.Models.DataManager {
    public class RequestManager : IDataRepository<Request> {
        private readonly AlgoMeContext _algomeContext;
 
        public RequestManager(AlgoMeContext context) {
            _algomeContext = context;
        }
 
        public IEnumerable<Request> GetAll() {
            return _algomeContext.Requests.Include(r => r.Parameters).ToList();
        }
 
        public Request Get(long id) {
            return _algomeContext.Requests
                .Include(r => r.Parameters)
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
 
        public void DeleteWhere(Expression<Func<Request, bool>> predicate) {
            _algomeContext.Requests.RemoveRange(_algomeContext.Requests.Where(predicate));
            _algomeContext.SaveChanges();
        }
    }
}