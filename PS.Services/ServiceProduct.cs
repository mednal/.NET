using PS.Data;
using PS.Data.Infrastructure;
using PS.Domain;
using ServicePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Services
{
    public class ServiceProduct : Service<Product>, IServiceProduct
    {
        public void DeleteOldProds()
        { // on peut aussi utiliser la methode compare compare(t1,t2) 
            Delete(p => (DateTime.Now - p.DateProd).TotalDays > 365);
        }

        public IEnumerable<Product> FindMostExpensiveFiveProds()
        {
          return  GetMany().OrderByDescending(p => p.Price).Take(5);
        }

        public IEnumerable<Product> GetProdsByClient(Client c)
        {
            ServiceAchat sa = new ServiceAchat();
            return sa.GetMany(a => a.ClientFK == c.CIN).Select(a => a.Product);
        }

        public float UnavailableProductsPercentage()
        {
            int unavailableproduct = GetMany(p => p.Quantity == 0).Count();
            int totalPoroduct = GetMany().Count();
            return ((float) unavailableproduct/totalPoroduct)*100;
        }
    }
}
