using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
	public class UserDao
	{
		private readonly ReasContext? context;

		private static UserDao? instance = null;

		public static UserDao Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new UserDao();
				}

				return instance;
			}
		}

		private UserDao()
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

		public List<User> Get()
		{
			return context
				.Users
				.Include(x => x.Role)
				.ToList();
		}

		public User Get(int id)
		{
			return context
				.Users
				.Include(x => x.Role)
				.FirstOrDefault(x => x.Id == id);
		}

		public User FindByUserName(string userName)
		{
			return context
				.Users
				.Include(x => x.Role)
				.FirstOrDefault(x => x.UserName == userName);
		}

		public User FindByEmail(string email)
		{
			return context
				.Users
				.Include(x => x.Role)
				.FirstOrDefault(x => x.Email == email);
		}

		public User Create(User user)
		{
			context.Users.Add(user);
			context.SaveChanges();
			return user;
		}

		public bool Update(User user)
		{
			context.Users.Update(user);
			return context.SaveChanges() > 0;
		}

		public bool Ban(int id)
		{
			var user = context.Users.Find(id);

			if (user != null)
			{
				user.Status = 0;
				context.Users.Update(user);
				return context.SaveChanges() > 0;
			}

			return false;
		}

		public bool Delete(int id)
		{
			var user = context.Users.Find(id);

			if (user != null)
			{
				context.Users.Remove(user);
				return context.SaveChanges() > 0;
			}

			return false;
		}
	}
}
