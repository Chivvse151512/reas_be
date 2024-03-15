using BusinessObject;
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

        public Bid? Create(Bid bid)
        {
            return ExecuteWithErrorHandling(() => _bidRepository.Create(bid),
                "An error occurred while creating a new bid.");
        }

        public List<Bid>? GetListByPropertyId(int id)
        {
            return ExecuteWithErrorHandling(() => _bidRepository.GetListByPropertyId(id),
                "An error occurred while retrieving all bids by propertyId.");
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
