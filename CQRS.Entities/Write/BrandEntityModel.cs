using CQRS.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Entities.Write
{
    public class BrandEntityModel: BaseEntity
    {
        #region  Fields

        [Required()]
        [StringLength(50)]
        public string? Name { get; set; }
        public bool IsActive { get; set; }

        #endregion
        #region  relationships
        [InverseProperty(nameof(ProductEntityModel.Brand))]
        public virtual ICollection<ProductEntityModel> Products { get; set; } = new List<ProductEntityModel>();
        #endregion

    }
}
