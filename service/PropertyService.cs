using System.Net.Http;
using System.Security.Claims;
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
        private readonly IBidService bidService;
        public PropertyService(IPropertyRepository propertyRepository, IUserRepository userRepository, IBidService bidService)
        {
            this.propertyRepository = propertyRepository;
            _userRepository = userRepository;
            this.bidService = bidService;
        }

        public void create(int userId, CreatePropertyRequest request)
        {
            if (request == null)
            {
                throw new Exception("Invalid request");
            }
            Property property = new Property
            {
                Title = request.Title,
                Description = request.Description,
                Address = request.Address,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                StartingPrice = request.StartingPrice,
                StepPrice = request.StepPrice,
                CurrentPrice = request.StartingPrice,
                
                Status = 1,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,

                SellerId = userId
            };
            
            List<PropertyFile> files = new List<PropertyFile>();
            if (request.Files != null)
            {
                foreach (var item in request.Files)
                {
                    files.Add(new PropertyFile { File = item, CreatedAt = DateTime.Now, Status = 1});
                }
            }
            

            List<PropertyImage> images = new List<PropertyImage>();
            if (request.Images != null)
            {
                foreach (var item in request.Images)
                {
                    images.Add(new PropertyImage { Image = item });
                }
            }
            property.PropertyFiles = files;
            property.PropertyImages = images;
            

            propertyRepository.create(property);
            
        }

        public void update()
        {

        }

        public void updateStatus(UpdateStatusPropertyRequest request, int userId)
        { 
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
                    case 0:
                        // Cho phép đổi trạng thái từ 5 về 0 nếu người dùng là staff hoặc admin
                        if (property.Status == 5 && (isStaff || isAdmin))
                        {
                            property.Status = status;
                        }
                        break;
                    case 1:
                        // Cho phép staff đổi trạng thái từ 2 về 1
                        if (isStaff && property.Status == 2)
                        {
                            property.Status = status;
                        }
                        break;
                    case 2:
                        // Cho phép staff đặt trạng thái từ 1 sang 2 và ghi nhận staff đã xác minh
                        if (isStaff && property.Status == 1)
                        {
                            property.Status = status;
                            property.StaffVerifyId = userId;
                        }
                        break;
                    case 3:
                        // Cho phép staff đổi trạng thái từ 2 sang 3 nếu chính họ đã xác minh trạng thái 2
                        if (isStaff && property.Status == 2 && property.StaffVerifyId == userId)
                        {
                            property.Status = status;
                        }
                        break;
                    case 4:
                        // Cho phép admin đổi trạng thái từ 3 sang 4
                        if (isAdmin && property.Status == 3)
                        {
                            property.Status = status;
                        }
                        break;
                    case 5:
                        // Không cho phép staff hoặc admin đổi trạng thái sang 5 từ bất kỳ trạng thái nào khác
                        if (property.Status != 5 && !isStaff && !isAdmin)
                        {
                            property.Status = status;
                        }
                        break;
                    case 6:
                        // Cho phép staff đổi trạng thái từ 2 sang 6, hoặc admin đổi từ 4 sang 6
                        if ((isStaff && property.Status == 2) || (isAdmin && property.Status == 4))
                        {
                            property.Status = status;
                        }
                        break;
                    default:
                        // Nếu trạng thái không hợp lệ, ném ra ngoại lệ
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
            return user != null && user.RoleId == 2;
        }

        private bool IsAdmin(int userId)
        {
            User user = _userRepository.Get(userId);
            return user != null && user.RoleId == 1;
        }


        public void updatePrice(int userId,UpdatePricePropertyRequest request)
        {

            if (request == null || request.Id <= 0 || request.Price <= 0 || request.Price > 10_000_000_000.00m)
            {
                throw new Exception("Invalid params request");
            } 

            Property property = propertyRepository.get(request.Id);
            if (property == null)
            {
                throw new Exception("Property Id is not exist");
            }

            if (userId == property.SellerId)
            {
                throw new Exception("You can not bid your property!");
            }


            //check is in time bid

            //if (userId == property.CurrentWinnerId)
            //{
            //    throw new Exception("You cannot bid when you are highest bid!");
            //}

            if (request.Price < property.StepPrice + property.CurrentPrice)
            {
                throw new Exception("Bid Price must larger current price + step price");
            }

            property.CurrentWinnerId = userId;
            property.CurrentPrice = request.Price;
            property.UpdatedAt = DateTime.Now;

            propertyRepository.update(property);

            //create bid
            bidService.PlaceBidAsync(userId, property.Id, request.Price);
        }

        public Property get(int id)
        {
            return propertyRepository.get(id);
        }

        public IQueryable<Property> GetPropertyWithBids(int propertyId)
        {
            return propertyRepository.GetPropertyWithBids(propertyId);
        }

        public IEnumerable<Property> get()
        {
            return propertyRepository.get();
        }

        private int currentUserId()
        {
            return 1;
        }

        public IQueryable<dynamic> GetPropertiesByStatus(int status)
        {
            var query = propertyRepository.GetPropertiesByStatus(status)
                .Select(p => new
                {
                    p.Id,
                    p.Title,
                    p.Description,
                    p.Seller,
                    Winner = p.CurrentWinner,
                    p.Address,
                    p.StartDate,
                    p.EndDate,
                    p.StartingPrice,
                    p.StepPrice,
                    p.Status,
                    p.CreatedAt,
                    p.UpdatedAt,
                    StaffVerifyId = p.StaffVerify.FullName,
                    p.Note,
                    Files = p.PropertyFiles.Select(pf => pf.File),
                    Images = p.PropertyImages.Select(pi => pi.Image)
                });

            return query; // This will return an IQueryable<dynamic>
        }

        public IQueryable<Property> GetPropertiesToVerify(int staffId)
        {
            return propertyRepository.GetPropertiesToVerify(staffId);
        }

        public IQueryable<Property> GetFinishedPropertiesByUser(int userId)
        {
            return propertyRepository.GetFinishedPropertiesByUser(userId);
        }

        public IQueryable<Property> GetPropertiesByUser(int userId)
        {
            return propertyRepository.GetPropertiesByUser(userId);
        }

    }
}
