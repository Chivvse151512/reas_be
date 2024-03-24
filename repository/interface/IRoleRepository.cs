using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository
{
    public interface IRoleRepository
    {
        Role Create(Role role);
        bool Delete(int id);
        List<Role> Get();
        Role Get(int id);
        Role GetByName(string name);
        Role Update(Role role);
    }
}
