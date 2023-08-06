using e_commerce_data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_data_access.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product entity);
        void Save();
    }
}
