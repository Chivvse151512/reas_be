using BusinessObject;
using Microsoft.Extensions.Logging;
using repository;

namespace service
{
    public class DepositService : IDepositService
    {
        private readonly IDepositRepository _depositRepository;
        private readonly ILogger<BidService> _logger;

        public DepositService(IDepositRepository depositRepository, ILogger<BidService> logger)
        {
            _depositRepository = depositRepository;
            _logger = logger;
        }

        public List<Deposit>? GetAll()
        {
            return ExecuteWithErrorHandling(() => _depositRepository.GetAll(),
                "An error occurred while retrieving all deposits.");
        }

        public Deposit? Insert(int userId, int propertyId, decimal amount)
        {
            return ExecuteWithErrorHandling(() => _depositRepository.Insert(userId, propertyId, amount),
                "An error occurred while inserting a new deposit.");
        }

        public Deposit? UpdateStatus(int depositId, int newStatus)
        {
            return ExecuteWithErrorHandling(() => _depositRepository.UpdateStatus(depositId, newStatus),
                "An error occurred while updating deposit status.");
        }

        public List<Deposit>? GetListByStatusNotZero()
        {
            return ExecuteWithErrorHandling(() => _depositRepository.GetListByStatusNotZero(),
                "An error occurred while retrieving deposits with status not zero.");
        }

        public Deposit? GetById(int depositId)
        {
            return ExecuteWithErrorHandling(() => _depositRepository.GetById(depositId),
                "An error occurred while retrieving a deposit by ID.");
        }

        public bool? CheckDeposit(int userId, int propertyId)
        {
            return ExecuteWithErrorHandling(() => _depositRepository.CheckDeposit(userId, propertyId),
                "An error occurred while checking for deposit existence.");
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
