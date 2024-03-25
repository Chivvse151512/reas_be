using BusinessObject;
using repository;

namespace service
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task CreateTransactionAsync(Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));

            ValidateTransaction(transaction);
            await _transactionRepository.CreateTransactionAsync(transaction);
        }

        public IQueryable<Transaction> GetAllTransaction()
        {
            return _transactionRepository.GetAllTransaction();
        }

        public IQueryable<Transaction> GetTransactionsByUserId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID");
            return _transactionRepository.GetTransactionsByUserId(id);
        }
        
        public Transaction GetTransactionById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID");
            return _transactionRepository.GetTransactionById(id);
        }

        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));

            await _transactionRepository.UpdateTransactionAsync(transaction);
        }

        public async Task DeleteTransactionAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID");

            await _transactionRepository.DeleteTransactionAsync(id);
        }

        private static void ValidateTransaction(Transaction transaction)
        {
            if (string.IsNullOrWhiteSpace(transaction.PaymentMethod))
                throw new ArgumentException("Payment Method is required.", nameof(transaction.PaymentMethod));
            if (transaction.Amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(transaction.Amount), "Amount must be greater than zero.");
        }
    }
}

