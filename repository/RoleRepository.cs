using BusinessObject;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository
{
    public class RoleRepository : IRoleRepository
    {
        RoleDao _roleDao = RoleDao.Instance;

        public RoleRepository(){}

        public List<Role> Get()
        {
            return _roleDao.Get();
        }

        public Role Get(int id)
        {
            return _roleDao.Get(id);
        }

        public Role GetByName(string name)
        {
			return _roleDao.GetByName(name);
        }

        public Role Create(Role role)
        {
            return _roleDao.Create(role);
        }

        public Role Update(Role role)
        {
            return _roleDao.Update(role);
        }

        public bool Delete(int id)
        {
            return _roleDao.Delete(id);
        }
    }
}
