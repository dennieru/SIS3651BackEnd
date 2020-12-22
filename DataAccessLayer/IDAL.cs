using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IDAL<T>
    {
        List<T> GetList();

        T Get(string id);

        bool Update(ref T t);

        bool Delete(string id);

        bool Post(T t);
    }
}
