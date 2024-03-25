using BusinessObject;

namespace DAO
{
    public class TransactionDao
    {
        private readonly ReasContext? context;
        private static TransactionDao? instance = null;
        public static TransactionDao Instance
        {
            get
            {
                instance ??= new TransactionDao();
                return instance;
            }
        }

        private TransactionDao()
        {
            if (context == null)
            {
                try
                {
                    context = new ReasContext();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Cannot connect to the database: {ex.Message}");
                }
            }
        }

        public async Task CreateTransactionAsync(Transaction transaction)
        {
            if (context == null)
                throw new InvalidOperationException("Database context is not initialized.");

            context.Transactions.Add(transaction);
            await context.SaveChangesAsync();
        }

        public IQueryable<Transaction> GetAllTransaction()
        {
            if (context == null)
                throw new InvalidOperationException("Database context is not initialized.");

            return context.Transactions;
        }

        public IQueryable<Transaction> GetTransactionsByUserId(int id)
        {
            if (context == null)
                throw new InvalidOperationException("Database context is not initialized.");

            return context.Transactions.Where(u => u.UserId == id);
        }

        public Transaction GetTransactionById(int id)
        {
            if (context == null)
                throw new InvalidOperationException("Database context is not initialized.");

            return context.Transactions.FirstOrDefault(x => x.Id == id);
        }

        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            if (context == null)
                throw new InvalidOperationException("Database context is not initialized.");

            context.Transactions.Update(transaction);
            await context.SaveChangesAsync();
        }

        public async Task DeleteTransactionAsync(int id)
        {
            if (context == null)
                throw new InvalidOperationException("Database context is not initialized.");

            var transaction = GetTransactionById(id);
            if (transaction != null)
            {
                context.Transactions.Remove(transaction);
                await context.SaveChangesAsync();
            }
        }
    }
}

