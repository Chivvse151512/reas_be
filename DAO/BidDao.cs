using System.Data.Common;
using BusinessObject;

namespace DAO
{
    public class BidDao
	{
        private readonly ReasContext? context;
        private static BidDao? instance = null;
        public static BidDao Instance
        {
            get
            {
                instance ??= new BidDao();

                return instance;
            }
        }

        private BidDao()
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

        public IQueryable<Bid> GetListByPropertyId(int propertyId, int pageNumber, int pageSize)
        {
            try
            {
                if (context == null)
                    return new List<Bid>().AsQueryable();

                int skip = (pageNumber - 1) * pageSize;

                return context.Bids
                    .Where(b => b.PropertyId == propertyId)
                    .Skip(skip)
                    .Take(pageSize);
            }
            catch (DbException e)
            {
                Console.WriteLine($"Error when getting bid by propertyId: {e.Message}");
                return new List<Bid>().AsQueryable();
            }
        }

        public async Task<bool> PlaceBidAsync(Bid bid)
        {
            if (context == null)
            {
                throw new InvalidOperationException("Database context is not initialized.");
            }

            context.Bids.Add(bid);
            await context.SaveChangesAsync();

            return true;
        }
    }
}

