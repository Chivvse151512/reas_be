using BusinessObject;
using BusinessObject.DTO;
using repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service
{
    public interface IPropertyService
    {
        void create(CreatePropertyRequest request);
        void update();
        void updateStatus(UpdateStatusPropertyRequest request);
        void updatePrice(UpdatePricePropertyRequest request);
        Property get(int id);
        IEnumerable<Property> get();
       
    }
}
