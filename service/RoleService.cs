using BusinessObject;
using DAO;
using repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public List<Role> Get()
        {
            return _roleRepository.Get();
        }

        public Role Get(int id)
        {
            return _roleRepository.Get(id);
        }

        public Role GetByName(string name)
        {
            return _roleRepository.GetByName(name);
        }
        public Role Create(Role role)
        {
            return _roleRepository.Create(role);
        }

        public Role Update(Role role)
        {
            return _roleRepository.Update(role);
        }

        public bool Delete(int id)
        {
            return _roleRepository.Delete(id);
        }
    }
}
