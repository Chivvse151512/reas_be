using BusinessObject;
using BusinessObject.DTO;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository
{
    public class PropertyRepository : IPropertyRepository
    {
        PropertyManagement propertyManagement = PropertyManagement.Instance;

        public IEnumerable<Property> get()
        {
            return propertyManagement.get();
        }

        public Property get(int id)
        {
            return propertyManagement.get(id);
        }

        public bool create(Property property)
        {
            return propertyManagement.create(property);
        }

        public bool delete(Property property)
        {
            return propertyManagement.delete(property);
        }

        

        public bool update(Property property)
        {
            return propertyManagement.update(property);
        }

        public IQueryable<Property> GetPropertiesByStatus(int status)
        {
            return propertyManagement.GetPropertiesByStatus(status);
        }

        public IQueryable<Property> GetPropertiesToVerify(int staffId)
        {
            return propertyManagement.GetPropertiesToVerify(staffId);
        }

        public IQueryable<Property> GetFinishedPropertiesByUser(int userId)
        {
            return propertyManagement.GetFinishedPropertiesByUser(userId);
        }

        public IQueryable<Property> GetPropertiesByUser(int userId)
        {
            return propertyManagement.GetPropertiesByUser(userId);
        }

        public IQueryable<Property> GetPropertyWithBids(int propertyId)
        {
            return propertyManagement.GetPropertyWithBids(propertyId);
        }
    }
}
