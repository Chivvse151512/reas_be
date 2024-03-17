using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using DAO;

namespace repository
{
	public class UserRepository : IUserRepository
	{
		UserDao _userDao = UserDao.Instance;
		public UserRepository() { }
		public bool Ban(int id)
		{
			return _userDao.Ban(id);
		}

		public User Create(User user)
		{
			return _userDao.Create(user);
		}

		public bool Delete(int id)
		{
			return _userDao.Delete(id);
		}

		public User FindByEmail(string email)
		{
			return _userDao.FindByEmail(email);
		}

		public User FindByUserName(string userName)
		{
			return _userDao.FindByUserName(userName);
		}

		public List<User> Get()
		{
			return _userDao.Get();
		}

		public User Get(int id)
		{
			return _userDao.Get(id);
		}

		public bool Update(User user)
		{
			return _userDao.Update(user);
		}
	}
}
