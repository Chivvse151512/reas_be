using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
	public class RoleDao
	{
		private readonly ReasContext? context;
		private static RoleDao? instance = null;
		public static RoleDao Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new RoleDao();
				}

				return instance;
			}
		}

		private RoleDao()
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

		public List<Role> Get()
		{
			return context.Roles.ToList();
		}

		public Role Get(int id)
		{
			return context.Roles.Find(id);
		}

		public Role Create(Role role)
		{
			context.Roles.Add(role);
			context.SaveChanges();
			return role;
		}

		public Role Update(Role role)
		{
			context.Roles.Update(role);
			context.SaveChanges();
			return role;
		}

		public bool Delete(int id)
		{
			var obj = context.Roles.Find(id);

			if (obj != null)
			{
				context.Roles.Remove(obj);
				context.SaveChanges();
				return true;
			}

			return false;
		}

		public Role GetByName(string name)
		{
			return context
				.Roles
				.Where(x => x.Name == name)
				.FirstOrDefault();
		}
	}
}
