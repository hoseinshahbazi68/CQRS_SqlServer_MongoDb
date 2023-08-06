using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Models.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required()]
        [StringLength(100)]
        public string? Name { get; set; }
        public bool IsActive { get; set; }
        [Required()]
        public int BrandId { get; set; }
        public string? Brand { get; set; }
        [Required()]
        public int CatgoryId { get; set; }
        public string? Catgory { get; set; }

    }
}
