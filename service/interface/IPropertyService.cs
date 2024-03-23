using BusinessObject;
using BusinessObject.DTO;

namespace service
{
    public interface IPropertyService
    {
        void create(int userId,CreatePropertyRequest request);
        void update();
        void updateStatus(UpdateStatusPropertyRequest request, int userId);
        void updatePrice(int userId, UpdatePricePropertyRequest request);
        Property get(int id);
        IQueryable<Property> GetPropertyWithBids(int propertyId);
        IEnumerable<Property> get();
        IQueryable<dynamic> GetPropertiesByStatus(int status);
        IQueryable<Property> GetPropertiesToVerify(int staffId);
        IQueryable<Property> GetFinishedPropertiesByUser(int userId);
        IQueryable<Property> GetPropertiesByUser(int userId);
        
    }
}
