

namespace Salon.Services
{
    using Salon.Services.Models;
    using Salon.Data.Models;
    using System.Collections.Generic;
    using Salon.Services.Implementation;

    public interface ISalonServices
    {
         IEnumerable<SalonViewModel> AllSalon();

        void Create(Salons salon);
        Salons FindSalon(int  id);
        void Edit(Salons Salon, int id);

        void Delete(int id);
        void Delete(int id, string str);
        Salons Details(int id);
        void AddSalon(Product product);

        List<SearchByProductViewModel> SearchProduct(string product);


    }
}
