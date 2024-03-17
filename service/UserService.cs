using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using repository;

namespace service
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public List<User> Get()
		{
			return _userRepository.Get();
		}

		public User Get(int id)
		{
			return _userRepository.Get(id);
		}

		public User FindByUserName(string userName)
		{
			return _userRepository.FindByUserName(userName);
		}

		public User FindByEmail(string email)
		{
			return _userRepository.FindByEmail(email);
		}

		public User Create(User user)
		{
			return _userRepository.Create(user);
		}

		public bool Update(User user)
		{
			return _userRepository.Update(user);
		}

		public bool Ban(int id)
		{
			return _userRepository.Ban(id);
		}

		public bool Delete(int id)
		{
			return _userRepository.Delete(id);
		}

		public bool CheckPassword(User user, string password)
		{
			return user.Password == password;
		}

		public Role GetRole(User user)
		{
			return Get(user.Id)
				.Role;
		}
	}
}
