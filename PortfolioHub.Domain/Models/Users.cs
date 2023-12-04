using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioHub.Domain.Models
{
    public class Users
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public virtual ICollection<UserPlants> UserPlantsDetails { get; set; }
    }
}
