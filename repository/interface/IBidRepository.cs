using BusinessObject;

namespace repository
{
    public interface IBidRepository
    {
        IQueryable<Bid>? GetListByPropertyId(int id, int pageNumber, int pageSize);
        Task<bool> PlaceBidAsync(Bid bid);
    }
}