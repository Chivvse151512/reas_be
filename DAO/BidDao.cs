using System;
using System.Data.Common;
using BusinessObject;
using Microsoft.EntityFrameworkCore;

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

        public List<Bid>? GetListByPropertyId(int id)
        {
            try
            {
                return context?.Bids.Where(b => b.PropertyId == id).ToList();
            }
            catch (DbException e)
            {
                Console.WriteLine("Error when getting bid by propertyid: " + e);
                return null;
            }
        }

        public Bid? Create(Bid bid)
        {
            try
            {
                context?.Bids.Add(bid);
                context?.SaveChanges();
                return bid;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error when creating new bid: " + e);
                return null;
            }
        }
    }
}

