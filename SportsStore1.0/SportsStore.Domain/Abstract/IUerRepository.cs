using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Abstract
{
   public interface IUerRepository
    {
        IEnumerable<UserAccount> User { get; }

        void SaveUser(Product product);

        UserAccount DeleteUser(int UserID);
    }
}
