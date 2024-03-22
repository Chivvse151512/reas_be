using BusinessObject;

namespace service
{
    public interface IBidService
    {
        IQueryable<Bid>? GetListByPropertyId(int id, int pageNumber, int pageSize);
        Task<bool> PlaceBidAsync(int userId, int propertyId, decimal amount);
    }
}