using System;
using BusinessObject;
using DAO;

namespace repository
{
    public class TransactionRepository : ITransactionRepository
    {
        public Task CreateTransactionAsync(Transaction transaction) => TransactionDao.Instance.CreateTransactionAsync(transaction);

        public Task DeleteTransactionAsync(int id) => TransactionDao.Instance.DeleteTransactionAsync(id);

        public IQueryable<Transaction> GetAllTransaction() => TransactionDao.Instance.GetAllTransaction();

        public Transaction GetTransactionById(int id) => TransactionDao.Instance.GetTransactionById(id);

        public IQueryable<Transaction> GetTransactionsByUserId(int id) => TransactionDao.Instance.GetTransactionsByUserId(id);

        public Task UpdateTransactionAsync(Transaction transaction) => TransactionDao.Instance.UpdateTransactionAsync(transaction);
    }
}

