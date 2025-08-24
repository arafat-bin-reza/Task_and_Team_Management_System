using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<User> Members { get; set; } 
    }
}
