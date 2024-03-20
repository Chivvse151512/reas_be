using BusinessObject;
using BusinessObject.DTO;
using Microsoft.EntityFrameworkCore;

namespace DAO
{
    public class PropertyManagement
    {
        private readonly ReasContext? context;
        private static PropertyManagement? instance = null;
        public static PropertyManagement Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PropertyManagement();
                }

                return instance;
            }
        }

        private PropertyManagement()
        {
            if (context == null)
            {
                try
                {
                    context = new ReasContext();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Cannot connect to the database: {ex.Message}");
                }
            }
        }




        public IEnumerable<Property> get()
        {
            return context.Properties.ToList();
        }
        public Property get(int id)
        {
            return context.Properties.FirstOrDefault(p => p.Id == id);

        }
        public bool create(Property property)
        {
            context.Properties.Add(property);
            context.SaveChanges();

            return true;
        }

        public bool update(Property property)
        {
            context.Properties.Update(property);
            context.SaveChanges();

            return true;
        }
        public bool delete(Property property)
        {
            property.Status = 0;
            context.Properties.Update(property);
            context.SaveChanges();

            return true;
        }

        public IQueryable<Property> GetPropertiesByStatus(int status) // dùng cho 1,3 và 4
        {
            return context.Properties.Where(p => p.Status == status);
        }
        public IQueryable<Property> GetPropertiesToVerify(int staffId)
        {
            return context.Properties.Where(p => p.Status == 2 && p.VerifyBy == staffId);
        }
        public IQueryable<Property> GetFinishedPropertiesByUser(int userId)
        {
            return context.Properties.Where(p => p.Status == 5 && p.CurrentWinner == userId);
        }
        public IQueryable<Property> GetPropertiesByUser(int userId)
        {
            return context.Properties.Where(p => p.SellerId == userId);
        }
        public PropertyWithBidsDTO GetPropertyWithBids(int propertyId)
        {
            var propertyWithBids = context.Properties
                                          .Where(p => p.Id == propertyId)
                                          .Include(p => p.Bids)
                                          .Select(property => new PropertyWithBidsDTO
                                          {
                                              Id = property.Id,
                                              SellerId = property.SellerId,
                                              CurrentWinner = property.CurrentWinner,
                                              VerifyBy = property.VerifyBy,
                                              Bids = property.Bids.ToList()
                                          })
                                          .FirstOrDefault();
            return propertyWithBids;
        }
    }
}
