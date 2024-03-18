using BusinessObject;

namespace service
{
    public interface IBidService
    {
        List<Bid>? GetListByPropertyId(int id, int pageNumber, int pageSize);
        Bid? Create(Bid bid);
    }
}
