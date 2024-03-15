using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using DAO;

namespace repository
{
    public class DepositRepository : IDepositRepository
    {
        public bool? CheckDeposit(int userId, int propertyId) => DepositDao.Instance.CheckDeposit(userId, propertyId);

        public List<Deposit>? GetAll() => DepositDao.Instance.GetAll();

        public Deposit? GetById(int depositId) => DepositDao.Instance.GetById(depositId);

        public List<Deposit>? GetListByStatusNotZero() => DepositDao.Instance.GetListByStatusNotZero();

        public Deposit? Insert(int userId, int propertyId, decimal amount) => DepositDao.Instance.Insert(userId, propertyId, amount);

        public Deposit? UpdateStatus(int depositId, int newStatus) => DepositDao.Instance.UpdateStatus(depositId, newStatus);
    }
}
