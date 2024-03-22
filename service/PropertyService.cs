using BusinessObject;
using BusinessObject.DTO;
using DAO;
using repository;

namespace service
{
    public class PropertyService : IPropertyService
    {
        private readonly IUserRepository _userRepository;
        private IPropertyRepository propertyRepository;
        public PropertyService(IPropertyRepository propertyRepository, IUserRepository userRepository)
        {
            this.propertyRepository = propertyRepository;
            _userRepository = userRepository;
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
            property.UpdatedAt = null;

            property.SellerId = currentUserId();

            propertyRepository.create(property);


        }

        public void update()
        {

        }

        public void updateStatus(UpdateStatusPropertyRequest request)
        {
            int userId = currentUserId();
            int status = request.Status;

            if (request == null || request.Id <= 0 || status < 0)
            {
                throw new ArgumentException("Invalid request parameters.");
            }

            Property property = propertyRepository.get(request.Id);
            if (property != null)
            {
                bool isStaff = IsStaff(userId);
                bool isAdmin = IsAdmin(userId);

                switch (status)
                {
                    case 2:
                        if (isStaff && property.Status == 1)
                        {
                            property.Status = status;
                            property.VerifyBy = userId;
                        }
                        break;
                    case 3:
                        if (isStaff && property.Status == 2 && property.VerifyBy == userId)
                        {
                            property.Status = status;
                        }
                        break;
                    case 4:
                        if (isAdmin && property.Status == 3)
                        {
                            property.Status = status;
                        }
                        break;
                    case 5:
                        if ((isStaff && property.Status == 2) || (isAdmin && property.Status == 4))
                        {
                            property.Status = status;
                        }
                        break;
                    default:
                        throw new InvalidOperationException("Invalid status value.");
                }

                if (property.Status != request.Status)
                {
                    throw new InvalidOperationException("Status transition is not allowed.");
                }

                property.UpdatedAt = DateTime.Now;
                propertyRepository.update(property);
            }
            else
            {
                throw new ArgumentException("Property not found.");
            }
        }

        private bool IsStaff(int userId)
        {
            User user = _userRepository.Get(userId); 
            Role userRole = RoleDao.Instance.Get(user.RoleId);
            return userRole.Name.Equals("Staff", StringComparison.OrdinalIgnoreCase);
        }

        private bool IsAdmin(int userId)
        {
            User user = _userRepository.Get(userId); 
            Role userRole = RoleDao.Instance.Get(user.RoleId); 
            return userRole.Name.Equals("Admin", StringComparison.OrdinalIgnoreCase);
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
            property.UpdatedAt = DateTime.Now;

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

        public IQueryable<Property> GetPropertiesByStatus(int status)
        {
            return propertyRepository.GetPropertiesByStatus(status);
        }

        public IQueryable<Property> GetPropertiesToVerify(int staffId)
        {
            return propertyRepository.GetPropertiesToVerify(staffId);
        }

        public IQueryable<Property> GetFinishedPropertiesByUser(int userId)
        {
            return propertyRepository.GetPropertiesByUser(userId);
        }

        public IQueryable<Property> GetPropertiesByUser(int userId)
        {
            return propertyRepository.GetPropertiesByUser(userId);
        }
    }
}
