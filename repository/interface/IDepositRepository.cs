using BusinessObject;

namespace repository
{
    public interface IDepositRepository
    {
        List<Deposit>? GetAll();
        Deposit? Insert(int userId, int propertyId, decimal amount);
        Deposit? UpdateStatus(int depositId, int newStatus);
        List<Deposit>? GetListByStatusNotZero();
        Deposit? GetById(int depositId);
        bool? CheckDeposit(int userId, int propertyId);
    }
}
