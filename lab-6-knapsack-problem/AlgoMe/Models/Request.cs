using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlgoMe.Models {
    public class Request {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RequestId { get; set; }
        
        // Status info
        public string Name { get; set; }
        public bool Status { get; set; }
        public int Percentage { get; set; }
        public double ProcessTime { get; set; }
        
        // Task info
        public ICollection<Parameter> Parameters { get; set; }
        public long Capacity { get; set; }
        public long Answer { get; set; }
        public int[] FullAnswer { get; set; }
    }
}