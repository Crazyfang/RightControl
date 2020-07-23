using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using FreeSql;

namespace RightControl.Repository
{
    public abstract class RepositoryBase<TEntity, TKey> : BaseRepository<TEntity, TKey> where TEntity : class, new()
    {
        protected RepositoryBase(IFreeSql fsql) : base(fsql, null, null)
        {

        }

        public virtual Task<TDto> GetAsync<TDto>(TKey id)
        {
            return Select.WhereDynamic(id).ToOneAsync<TDto>();
        }

        public virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp)
        {
            return Select.Where(exp).ToOneAsync();
        }

        public virtual Task<TDto> GetAsync<TDto>(Expression<Func<TEntity, bool>> exp)
        {
            return Select.Where(exp).ToOneAsync<TDto>();
        }

        public async Task<bool> SoftDeleteAsync(TKey id)
        {
            await UpdateDiy
                .SetDto(new
                {
                    IsDeleted = true
                })
                .WhereDynamic(id)
                .ExecuteAffrowsAsync();
            return true;
        }

        public async Task<bool> SoftDeleteAsync(TKey[] ids)
        {
            await UpdateDiy
                .SetDto(new
                {
                    IsDeleted = true
                })
                .WhereDynamic(ids)
                .ExecuteAffrowsAsync();
            return true;
        }
    }

    public abstract class RepositoryBase<TEntity> : RepositoryBase<TEntity, long> where TEntity : class, new()
    {
        protected RepositoryBase(IFreeSql fsql) : base(fsql)
        {
        }
    }
}
