using BusinessObject;

namespace repository
{
    public interface IPropertyRepository
    {
        IEnumerable<Property> get();
        Property get(int id);
        IQueryable<Property> GetPropertyWithBids(int propertyId);
        bool create(Property property);
        bool update(Property property);
        bool delete(Property property);
        IQueryable<Property> GetPropertiesByStatus(int status);
        IQueryable<Property> GetPropertiesToVerify(int staffId);
        IQueryable<Property> GetFinishedPropertiesByUser(int userId);
        IQueryable<Property> GetPropertiesByUser(int userId);
    }
}
