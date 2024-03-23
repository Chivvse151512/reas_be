using System.Security.Cryptography;
using BusinessObject;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using repository;

namespace service
{
    public class BidService : IBidService
    {
        private readonly IBidRepository _bidRepository;
        private readonly ILogger<BidService> _logger;

        public BidService(IBidRepository bidRepository, ILogger<BidService> logger)
        {
            _bidRepository = bidRepository ?? throw new ArgumentNullException(nameof(bidRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IQueryable<Bid>? GetListByPropertyId(int id, int pageNumber, int pageSize)
        {
            return ExecuteWithErrorHandling(() => _bidRepository.GetListByPropertyId(id, pageNumber, pageSize),
                "An error occurred while retrieving all bids by propertyId.");
        }

        public async Task<bool> PlaceBidAsync(int userId, int propertyId, decimal amount)
        {
            var bid = new Bid
            {
                UserId = userId,
                PropertyId = propertyId,
                Amount = amount,
                Status = 1,
                CreatedAt = DateTime.UtcNow
            };

            return await ExecuteWithErrorHandling(async () =>
                await _bidRepository.PlaceBidAsync(bid),
                "An error occurred while creating a new bid.");
        }

        private T ExecuteWithErrorHandling<T>(Func<T> func, string errorMessage)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, errorMessage);
                throw new Exception(errorMessage, ex);
            }
        }
    }
}