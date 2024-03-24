using System;
using BusinessObject;

namespace repository
{
	public interface ITransactionRepository
	{
        Task CreateTransactionAsync(Transaction transaction);
        IQueryable<Transaction> GetAllTransaction();
        IQueryable<Transaction> GetTransactionsByUserId(int id);
        Transaction GetTransactionById(int id);
        Task UpdateTransactionAsync(Transaction transaction);
        Task DeleteTransactionAsync(int id);
    }
}

