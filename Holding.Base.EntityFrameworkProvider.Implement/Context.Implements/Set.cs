using System;
using Holding.Base.EntityFrameworkProvider.Write.Context.Interfaces;
using Holding.Base.EntityFrameworkProvider.Context.Contracts;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Holding.Base.EntityFrameworkProvider.Implement.Context.Implements
{
    internal class Set<TEntity> : Write.Context.Interfaces.ISet<TEntity>,Read.Context.Interfaces.ISet<TEntity> where TEntity : DataTableBase
    {

        private readonly DbSet<TEntity> _dbSet;
        private IQueryable<TEntity> _query;

        

        public Set(DbSet<TEntity> dbSet)
        {
            _dbSet = dbSet;
            _query = null;
        }


        #region Shared ISet

        public TEntity Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public Task<TEntity> FindAsync(params object[] keyValues)
        {
            return _dbSet.FindAsync(keyValues);
        }

        #endregion


        #region Write ISet

        IQueryable<TEntity> Write.Context.Interfaces.ISet<TEntity>.AsQuery()
        {
            if (_query == null)
            {
                _query = _dbSet.AsQueryable();
            }

            return _query;
        }

        Write.Context.Interfaces.ISet<TEntity> Write.Context.Interfaces.ISet<TEntity>.WithInclude<TProperty>(Expression<Func<TEntity, TProperty>> include)
        {
            if (_query == null)
            {
                _query = _dbSet.Include(include);
            }
            else
            {
                _query = _query.Include(include);
            }
            return this;
        }

        Write.Context.Interfaces.ISet<TEntity> Write.Context.Interfaces.ISet<TEntity>.WithInclude(string include)
        {
            if (_query == null)
            {
                _query = _dbSet.Include(include);
            }
            else
            {
                _query = _query.Include(include);
            }
            return this;
        }


        public TEntity Attach(TEntity entity)
        {
            return _dbSet.Attach(entity);
        }

        public TEntity Add(TEntity entity)
        {
            return _dbSet.Add(entity);
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            return _dbSet.AddRange(entities);
        }

        public TEntity Remove(TEntity entity)
        {
            return _dbSet.Remove(entity);
        }

        public IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities)
        {
            return _dbSet.RemoveRange(entities);
        }


        #endregion


        #region Read ISet

        IQueryable<TEntity> Read.Context.Interfaces.ISet<TEntity>.AsQuery()
        {
            if (_query == null)
            {
                _query = _dbSet.AsQueryable();
            }

            return _query.AsNoTracking();
        }

        Read.Context.Interfaces.ISet<TEntity> Read.Context.Interfaces.ISet<TEntity>.WithInclude<TProperty>( Expression<Func<TEntity, TProperty>> include)
        {
            if (_query == null)
            {
                _query = _dbSet.Include(include);
            }
            else
            {
                _query = _query.Include(include);
            }
            return this;
        }

        Read.Context.Interfaces.ISet<TEntity> Read.Context.Interfaces.ISet<TEntity>.WithInclude(string include)
        {
            if (_query == null)
            {
                _query = _dbSet.Include(include);
            }
            else
            {
                _query = _query.Include(include);
            }
            return this;
        }


        #endregion



    }
}