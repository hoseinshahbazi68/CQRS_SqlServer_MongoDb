using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Sample.Core.ProductApplication.Commands.UpdateProduct
{
 
    public class UpdateProductCommand : IRequest<bool>
    {
        public int  Id { get; set; }

        [Required()]
        public string? Name { get; set; }
        public bool IsActive { get; set; }
        [Required()]
        public string Brand { get; set; }
        [Required()]
        public string Catgory { get; set; }
    }

}