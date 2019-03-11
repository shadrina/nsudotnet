using System.Collections.Generic;
using System.Linq;
using AlgoMe.Models.Repository;

namespace AlgoMe.Models.DataManager {
    public class ParameterManager : IDataRepository<Parameter> {
        readonly AlgoMeContext _algomeContext;
 
        public ParameterManager(AlgoMeContext context) {
            _algomeContext = context;
        }
 
        public IEnumerable<Parameter> GetAll() {
            return _algomeContext.Parameters.ToList();
        }
 
        public Parameter Get(long id) {
            return _algomeContext.Parameters
                .FirstOrDefault(e => e.ParameterId == id);
        }
 
        public void Add(Parameter entity) {
            _algomeContext.Parameters.Add(entity);
            _algomeContext.SaveChanges();
        }
 
        public void Update(Parameter parameter, Parameter entity) {
            parameter.Name = entity.Name;
            parameter.Price = entity.Price;
            parameter.Weight = entity.Weight;
            parameter.Request = entity.Request;
 
            _algomeContext.SaveChanges();
        }
 
        public void Delete(Parameter parameter) {
            _algomeContext.Parameters.Remove(parameter);
            _algomeContext.SaveChanges();
        }
    }
}