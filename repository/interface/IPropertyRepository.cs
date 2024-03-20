using BusinessObject;
using BusinessObject.DTO;

namespace repository
{
    public interface IPropertyRepository
    {
        IEnumerable<Property> get();
        Property get(int id);
        PropertyWithBidsDTO GetPropertyWithBids(int propertyId);
        bool create(Property property);
        bool update(Property property);
        bool delete(Property property);
        IQueryable<Property> GetPropertiesByStatus(int status);
        IQueryable<Property> GetPropertiesToVerify(int staffId);
        IQueryable<Property> GetFinishedPropertiesByUser(int userId);
        IQueryable<Property> GetPropertiesByUser(int userId);
    }
}
