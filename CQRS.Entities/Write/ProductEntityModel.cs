using CQRS.Entities.Common;
using CQRS.Entities.Read;
using CQRS.Entities.Write;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Entities.Write
{
    [Table("Product")]
    public class ProductEntityModel : BaseEntity
    {
        #region  Fields
        [Required()]
        [StringLength(100)]
        public string? Name { get; set; }
        public bool IsActive { get; set; }
        [Required()]
        public int BrandId { get; set; }
        [Required()]
        public int CatgoryId { get; set; }

        #endregion
        #region relationships

        [InverseProperty(nameof(CatgoryEntityModel.Products))]
        [ForeignKey(nameof(CatgoryId))]
        public virtual CatgoryEntityModel? Catgory { get; set; }

        [InverseProperty(nameof(BrandEntityModel.Products))]
        [ForeignKey(nameof(BrandId))]
        public virtual BrandEntityModel? Brand { get; set; }
        #endregion
    }
}
