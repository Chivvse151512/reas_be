using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace service
{
    public interface IUserService
    {
		bool CheckPassword(User user, string password);
		User Create(User user);
		bool Ban(int id);
		bool Delete(int id);
		User FindByEmail(string email);
		User FindByUserName(string userName);
		List<User> Get();
		User Get(int id);
		Role GetRole(User user);
		bool Update(User user);
	}
}
