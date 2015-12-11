using MyProject.Website.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Website.Model.ResponseTypes
{
    public class AllData
    {
        public List<Client> AllClients { get; set; }
        public List<Product> AllProducts { get; set; }
    }
}