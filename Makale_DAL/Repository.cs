using ArticleWeb_Common;
using Article_Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Article_DAL
{
    //We have to be sure that T is a class(which will be provided to Set<T>), "where T : class" conforms that condition.
    //DbContext is initialized in DbSingleton for avoiding multiple referance error. (This could've been done in Constructor of repository too.
    public class Repository<T> : DbSingleton where T : class
    {
        //For avoiding calls to db in every function call, we initialize objSet at constructor.
        DbSet<T> objSet;
        public Repository()
        {
            objSet = db.Set<T>();
        }

        //Set<T> is a way of telling that T object will be of type dbsets on our Database Context.
        public int Create(T obj)
        {
            objSet.Add(obj);
            BaseEntity entObj = obj as BaseEntity;
            DateTime now = DateTime.Now;
            if (obj is BaseEntity)
            {
               
                entObj.RegisterationDate = now;
                entObj.UpdateDate = now;
                entObj.UpdatedBy = Application_UpdaterUser._UserName;
            }
            return db.SaveChanges();
        }

        public List<T> Read()
        {
            return objSet.ToList();
        }

        public List<T> Read(Expression<Func<T, bool>> condition)
        {
            return objSet.Where(condition).ToList();
        }
        public IQueryable<T> ReadQueryable()
        {
            return objSet.AsQueryable();
        }

        public T Find(Expression<Func<T, bool>> condition)
        {
            return objSet.FirstOrDefault(condition);
        }

        public int Update(T obj)
        {
            BaseEntity entObj = obj as BaseEntity;

            if (obj is BaseEntity)
            {
                entObj.UpdateDate = DateTime.Now;
                entObj.UpdatedBy = Application_UpdaterUser._UserName;
            }
            return db.SaveChanges();
        }

        public int Delete(T obj)
        {
            objSet.Remove(obj);
            return db.SaveChanges();
        }
    }
}
