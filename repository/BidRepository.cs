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
        public Bid? Create(Bid bid) => BidDao.Instance.Create(bid);

        public List<Bid>? GetListByPropertyId(int id) => BidDao.Instance.GetListByPropertyId(id);
    }
}
