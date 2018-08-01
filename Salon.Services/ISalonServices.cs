

namespace Salon.Services
{
    using Salon.Services.Models;
    using Salon.Data.Models;
    using System.Collections.Generic;
    using Salon.Services.Implementation;
    using System.Threading.Tasks;

    public interface ISalonServices
    {
        IEnumerable<SalonViewModel> AllSalon();

        void  Create(Salons salon ,string name);

        Salons FindSalon(int  id);

        void Edit(Salons Salon, int id);

        void Delete(int id);

        void Delete(int id, string str);

        Salons Details(int id);

        void AddProduct(AddProductView product, int id);

        IEnumerable<SalonViewModel> MySalons(string name );

        List<SearchByProductViewModel> SearchProduct(string product);

        ProductWithWorkers GetProductWithWorkers(int id);

        Product ProductDetails(int id);
        

        void Book();
    }
}
