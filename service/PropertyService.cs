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
    public class PropertyService : IPropertyService
    {
        private IPropertyRepository propertyRepository;
        public PropertyService(IPropertyRepository propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }

        public void create(CreatePropertyRequest request)
        {
            //check input

            // check start date, end date

            Property property = new Property();
            property.Title = request.Title;
            property.Description = request.Description;
            property.Address = request.Address;
            property.StartDate = request.StartDate;
            property.EndDate = request.EndDate;
            property.StartingPrice = request.StartingPrice;
            property.StepPrice = request.StepPrice;

            property.Status = 2;
            property.CreatedAt = DateTime.Now;
            property.UpdatedAt = DateTime.Now;

            property.SellerId = currentUserId();

            propertyRepository.create(property);

        }

        public void update()
        {

        }

        public void updateStatus(UpdateStatusPropertyRequest request)
        {
            int user = currentUserId();
            int status = request.Status;

            if (request == null || request.Id <= 0 || status < 0)
            {
                return;
            }

            Property property = propertyRepository.get(request.Id);
            if (property == null)
            {
                return;
            }

            if (status == 2)
            {

            }
            if (status == 3)
            {

            }
            if (status == 4)
            {

            }
            if (status == 5)
            {

            }
            if (status == 6)
            {

            }

            property.Status = request.Status;
            propertyRepository.update(property);

        }

        public void updatePrice(UpdatePricePropertyRequest request)
        {
            int useId = currentUserId();

            if (request == null || request.Id <= 0 || request.Price <= 0)
            {
                return; //todo: return message error here;
            } 

            Property property = propertyRepository.get(request.Id);
            if (property == null)
            {
                return;
            }

            if (request.Price < property.StepPrice + property.StartingPrice) 
            {
                return;
            }

            property.CurrentWinner = useId;
            property.StartingPrice = request.Price;

            propertyRepository.update(property);
        }

        public Property get(int id)
        {
            return propertyRepository.get(id);
        }

        public IEnumerable<Property> get()
        {
            return propertyRepository.get();
        }

        private int currentUserId()
        {
            return 1;
        }
        
    }
}
