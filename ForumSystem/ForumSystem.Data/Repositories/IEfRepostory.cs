using System.Linq;
using ForumSystem.Data.Model.Contracts;

namespace ForumSystem.Data.Repositories
{
    public interface IEfRepostory<T> where T : class, IDeletable
    {
        IQueryable<T> All { get; }
        IQueryable<T> AllAndDeleted { get; }

        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}