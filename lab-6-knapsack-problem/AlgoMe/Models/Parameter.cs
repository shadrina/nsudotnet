using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlgoMe.Models {
    public class Parameter {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ParameterId { get; set; }

        public string Name { get; set; }
        public long Price { get; set; }
        public long Weight { get; set; }

        public Request Request { get; set; }
    }
}