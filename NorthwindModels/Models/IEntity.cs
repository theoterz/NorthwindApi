namespace NorthwindModels.Models
{
    public interface IEntity<IdType>
    {
        IdType Id { get; set; }
    }
}
