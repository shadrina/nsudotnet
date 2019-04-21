using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AlgoMe.Models.Repository;

namespace AlgoMe.Models.DataManager {
    public class RequestManager : IDataRepository<Request> {
        private readonly AlgoMeContext _algomeContext;
 
        public RequestManager(AlgoMeContext context) {
            _algomeContext = context;
        }
 
        public async Task<IEnumerable<Request>> GetAll() {
            return await _algomeContext.Requests
                .Include(r => r.Parameters)
                .ToListAsync();
        }
 
        public async Task<Request> Get(long id) {
            return await _algomeContext.Requests
                .Include(r => r.Parameters)
                .SingleOrDefaultAsync(e => e.RequestId == id);
        }

        public async Task<Request> GetWhere(Expression<Func<Request, bool>> predicate) {
            return await _algomeContext.Requests
                .Include(r => r.Parameters)
                .SingleOrDefaultAsync(predicate);
        }

        public async Task Add(Request entity) {
            await _algomeContext.Requests.AddAsync(entity);
            
            await _algomeContext.SaveChangesAsync();
        }
 
        public async Task Update(Request request, Request entity) {
            request.Name = entity.Name;
            request.Answer = entity.Answer;
            request.Status = entity.Status;
            request.Capacity = entity.Capacity;
            request.Parameters = entity.Parameters;
            request.Percentage = entity.Percentage;
            request.FullAnswer = entity.FullAnswer;
            request.ProcessTime = entity.ProcessTime;
 
            await _algomeContext.SaveChangesAsync();
        }
 
        public async Task DeleteWhere(Expression<Func<Request, bool>> predicate) {
            _algomeContext.Requests.RemoveRange(_algomeContext.Requests.Where(predicate));
            
            await _algomeContext.SaveChangesAsync();
        }

        public void Dispose() {
            _algomeContext?.Dispose();
        }
    }
}