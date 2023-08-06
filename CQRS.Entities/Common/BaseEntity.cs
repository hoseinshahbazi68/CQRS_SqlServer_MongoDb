namespace CQRS.Entities.Common
{
    public interface IEntity
    {
    }

    public abstract class BaseEntity<TKey> : IEntity
    {
        public BaseEntity()
        {
            CreateDate = DateTime.Now;
        }
        public TKey Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? DateModified { get; set; }

    }

    public abstract class BaseEntity : BaseEntity<int>
    {
    }
}
