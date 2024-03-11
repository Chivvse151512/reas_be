using BusinessObject;
using repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service
{
    public class PropertyService : IPropertyService
    {
        private PropertyRepository propertyRepository;
        public PropertyService(PropertyRepository propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }

        public void create()
        {

        }

        public void update()
        {

        }

        public void updateStatus()
        {

        }

        public void updatePrice()
        {

        }

        public Property get(int id)
        {
            return propertyRepository.get(id);
        }
        
    }
}
