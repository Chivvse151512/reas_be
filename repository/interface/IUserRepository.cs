using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace repository
{
	public interface IUserRepository
	{
		User Create(User user);
		bool Ban(int id);
		bool Delete(int id);
		User FindByEmail(string email);
		User FindByUserName(string userName);
		List<User> Get();
		User Get(int id);
		bool Update(User user);
	}
}