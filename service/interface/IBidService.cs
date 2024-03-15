using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace service
{
    public interface IBidService
    {
        List<Bid>? GetListByPropertyId(int id);
        Bid? Create(Bid bid);
    }
}
