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
            return context.Properties.Include(p => p.PropertyFiles)
                                     .Include(i => i.PropertyImages)
                                     .Include(b => b.Bids)
                                     .ToList();
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
        public IQueryable<Property> GetPropertiesByStatus(int status)
        {
            return context.Properties.Where(p => p.Status == status)
                                     .Include(i => i.PropertyImages)
                                     .Include(f => f.PropertyFiles)
                                     .Include(s => s.Seller)
                                     .Include(v => v.StaffVerify)
                                     .Include(c => c.CurrentWinner);
        }
        public IQueryable<Property> GetPropertiesToVerify(int staffId)
        {
            return context.Properties.Where(p => p.Status == 2 && p.StaffVerifyId == staffId);
        }
        public IQueryable<Property> GetFinishedPropertiesByUser(int userId)
        {
            return context.Properties.Where(p => p.Status == 5 && p.CurrentWinnerId == userId);
        }
        public IQueryable<Property> GetPropertiesByUser(int userId)
        {
            return context.Properties.Where(p => p.SellerId == userId);
        }
        public IQueryable<Property> GetPropertyWithBids(int propertyId)
        {
            return context.Properties.Where(p => p.Id == propertyId)
                                     .Include(p => p.Bids)
                                     .Include(i => i.PropertyImages);
        }
    }
}
