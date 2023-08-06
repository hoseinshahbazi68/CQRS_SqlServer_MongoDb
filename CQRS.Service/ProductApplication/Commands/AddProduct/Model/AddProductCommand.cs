using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace CQRS.Service.ProductApplication.Commands.AddProduct.Model
{
    public class AddProductCommand : IRequest<AddProductCommandResult>
    {
        [Required()]
        public string? Name { get; set; }
        public bool IsActive { get; set; }
        [Required()]
        public string Brand { get; set; }
        [Required()]
        public string Catgory { get; set; }
    }

}