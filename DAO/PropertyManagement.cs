using BusinessObject;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class PropertyManagement
    {
        ReasContext context = new ReasContext();
        private static PropertyManagement instance = null;
        public static PropertyManagement Instance
        {
            get {
                if (instance == null)
                {
                    instance = new PropertyManagement();
                }

                return instance; 
            }
        }

        private PropertyManagement() { }




        public IEnumerable<Property> get()
        {
            return context.Properties.ToList();
        }
        public Property get(int id)
        {
            return context.Properties.FirstOrDefault(p => p.Id == id);

        }
        public bool create(Property property)
        {
            context.Properties.Add(property);
            context.SaveChanges();

            return true;
        }

        public bool update(Property property)
        {
            context.Properties.Update(property);
            context.SaveChanges();

            return true;
        }
        public bool delete(Property property) 
        {
            property.Status = 0;
            context.Properties.Update(property);
            context.SaveChanges();

            return true;
        }

        

        
    }
}
