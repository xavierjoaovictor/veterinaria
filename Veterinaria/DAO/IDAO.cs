using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;

namespace Veterinaria.DAO
{
    public interface IDAO<T> : IDisposable where T: class, new()
    {
        int Insert(T model);
        bool Update(T model);
        bool Delete(T model);
        T Search(T model);
        List<T> ListAll();
    }
}
