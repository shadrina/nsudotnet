using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AlgoMe.Models {
    public class AlgoMeContext : DbContext {
        public AlgoMeContext(DbContextOptions<AlgoMeContext> options): base(options) { }

        public DbSet<Request> Requests { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
    }
}