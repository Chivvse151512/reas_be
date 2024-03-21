using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace repository
{
    public interface IBidRepository
    {
        IQueryable<Bid>? GetListByPropertyId(int id, int pageNumber, int pageSize);
        Bid? Create(Bid bid);
    }
}
