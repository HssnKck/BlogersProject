using BlogersProject.Model.Abstract;
using BlogersProject.Model.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogersProject.Model.Repositories
{
    public class GenericRepository<T> : IGenericModel<T> where T : class
    {
        public bool Delete(T t)
        {
            try
            {
                using Context context = new Context();
                context.Remove(t);
                return context.SaveChanges() > 0 ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public T GetRecord(int id)
        {
            using Context context = new Context();
            return context.Set<T>().Find(id);

        }

        public List<T> GetList()
        {
            using Context context = new Context();
            return context.Set<T>().ToList();
        }

        public bool Insert(T t)
        {
            try
            {
                using Context context = new Context();
                context.Add(t);
                return context.SaveChanges() > 0 ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(T t)
        {
            try
            {
                using Context context = new Context();
                context.Update(t);
                return context.SaveChanges() > 0 ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
