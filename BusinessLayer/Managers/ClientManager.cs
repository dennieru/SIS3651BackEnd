using BusinessLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Managers
{
    public static class ClientManager
    {
        public static Client GetClient(string userName) 
        {
            return new Client
            {
                Id = 123,
                Name = userName,
            };
        }
    }
}
