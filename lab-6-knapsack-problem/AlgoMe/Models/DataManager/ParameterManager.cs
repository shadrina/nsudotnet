using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AlgoMe.Models.Repository;

namespace AlgoMe.Models.DataManager {
    public class ParameterManager : IDataRepository<Parameter> {
        private readonly AlgoMeContext _algomeContext;
 
        public ParameterManager(AlgoMeContext context) {
            _algomeContext = context;
        }
 
        public async Task<IEnumerable<Parameter>> GetAll() {
            return await _algomeContext.Parameters
                .Include(p => p.Request)
                .ToListAsync();
        }
 
        public async Task<Parameter> Get(long id) {
            return await _algomeContext.Parameters
                .Include(p => p.Request)
                .FirstOrDefaultAsync(e => e.ParameterId == id);
        }

        public async Task<Parameter> GetWhere(Expression<Func<Parameter, bool>> predicate) {
            return await _algomeContext.Parameters
                .Include(p => p.Request)
                .FirstOrDefaultAsync(predicate);
        }

        public async Task Add(Parameter entity) {
            _algomeContext.Parameters.Add(entity);
            
            await _algomeContext.SaveChangesAsync();
        }
 
        public async Task Update(Parameter parameter, Parameter entity) {
            parameter.Name = entity.Name;
            parameter.Price = entity.Price;
            parameter.Weight = entity.Weight;
            parameter.Request = entity.Request;
 
            await _algomeContext.SaveChangesAsync();
        }
 
        public async Task DeleteWhere(Expression<Func<Parameter, bool>> predicate) {
            _algomeContext.Parameters.RemoveRange(_algomeContext.Parameters.Where(predicate));
            
            await _algomeContext.SaveChangesAsync();
        }

        public void Dispose() {
            _algomeContext?.Dispose();
        }
    }
}