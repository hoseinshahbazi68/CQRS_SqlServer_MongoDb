using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CQRS.Entities.Read
{
    public class ProductModel 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string? Id { get; set; }
        public int  ProductId { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }
        [Required()]
        public string? Brand { get; set; }
        [Required()]
        public string? Catgory { get; set; }
    }
}
