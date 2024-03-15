using System;
using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DAO
{
	public class DepositDao
	{
        private readonly ReasContext? context;
        private static DepositDao? instance = null;
        public static DepositDao Instance
        {
            get
            {
                instance ??= new DepositDao();

                return instance;
            }
        }

        private DepositDao()
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

        public List<Deposit>? GetAll()
        {
            try
            {
                return context?.Deposits.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error when getting all eposits: " + e);
                return null;
            }
        }

        public Deposit? Insert(int userId, int propertyId, decimal amount)
        {
            try
            {
                var newDeposit = new Deposit
                {
                    UserId = userId,
                    PropertyId = propertyId,
                    Amount = amount,
                    Status = 1,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = null
                };
                context?.Deposits.Add(newDeposit);
                context?.SaveChanges();

                return newDeposit;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error when inserting new deposit: " + e);
                return null;
            }
        }

        public Deposit? UpdateStatus(int depositId, int newStatus)
        {
            try
            {
                var depositToUpdate = GetById(depositId);

                if (depositToUpdate == null)
                {
                    Console.WriteLine($"Deposit with Id {depositId} not found.");
                    return null;
                }
                depositToUpdate.Status = newStatus;
                depositToUpdate.UpdatedAt = DateTime.Now;
                context?.SaveChanges();

                return depositToUpdate;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error when updating deposit status: " + e);
                return null;
            }
        }

        public List<Deposit>? GetListByStatusNotZero()
        {
            try
            {
                var deposits = context?.Deposits.Where(d => d.Status != 0).ToList();

                return deposits;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error when getting deposits by status not zero: " + e);
                return null;
            }
        }

        public Deposit? GetById(int depositId)
        {
            try
            {
                var deposit = context?.Deposits.FirstOrDefault(d => d.Id == depositId);

                return deposit;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error when getting deposit by ID: " + e);
                return null;
            }
        }

        public bool? CheckDeposit(int userId, int propertyId)
        {
            try
            {
                bool depositExists = context?.Deposits.Any(d => d.UserId == userId && d.PropertyId == propertyId) ?? false;

                return depositExists;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error when checking deposit: " + e);
                return null;
            }
        }
    }
}

