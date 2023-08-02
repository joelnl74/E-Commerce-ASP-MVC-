using e_commerce_application_web.Models;

namespace e_commerce_data_access.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category entity);
        void Save();
    }
}
