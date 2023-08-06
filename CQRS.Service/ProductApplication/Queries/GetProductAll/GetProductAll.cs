using CQRS.Entities.Read;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Core.ProductApplication.Queries.GetProductAll
{
    public class GetProductAll:IRequest<List<ProductModel>>
    {
    }
}
