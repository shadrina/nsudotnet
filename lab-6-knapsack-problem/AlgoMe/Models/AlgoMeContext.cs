using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AlgoMe.Models {
    public class AlgoMeContext : DbContext {
        public AlgoMeContext(DbContextOptions<AlgoMeContext> options): base(options) { }

        public DbSet<Request> Requests { get; set; }
        public DbSet<Parameter> Parameters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Request>().HasData(new Request
            {
                RequestId = 1,
                Name = "Default",
                Status = true,
                Percentage = 0,
                ProcessTime = 0,
                Answer = 0,
                Capacity = 0,
                Parameters = new List<Parameter>()
            });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}