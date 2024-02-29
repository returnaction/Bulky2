using BulkyWeb.Models;

namespace BulkyWeb.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }

        void Save();
        
    }
}
