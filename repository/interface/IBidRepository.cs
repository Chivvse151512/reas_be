using BusinessObject;

namespace repository
{
    public interface IBidRepository
    {
        IQueryable<Bid>? GetListByPropertyId(int id, int pageNumber, int pageSize);
        Bid? Create(Bid bid);
    }
}
