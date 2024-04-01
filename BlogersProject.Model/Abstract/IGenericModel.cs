using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogersProject.Model.Abstract
{
    public interface IGenericModel<T> where T : class
    {
        bool Insert(T t);
        bool Delete(T t);
        bool Update(T t);
        T GetRecord(int id);
        List<T> GetList();
    }
}
