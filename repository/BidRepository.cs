using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using DAO;

namespace repository
{
    public class BidRepository : IBidRepository
    {
        public IQueryable<Bid>? GetListByPropertyId(int id, int pageNumber, int pageSize) => BidDao.Instance.GetListByPropertyId(id, pageNumber, pageSize);

        public Task<bool> PlaceBidAsync(Bid bid) => BidDao.Instance.PlaceBidAsync(bid);
    }
}