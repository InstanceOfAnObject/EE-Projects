using MyProject.Website.Model;
using MyProject.Website.Model.ResponseTypes;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace MyProject.Website.Data
{
    public class DumpMethods
    {

        public AllData GetAllData()
        {
            AllData result = new AllData()
            {
                AllClients = new List<Model.Client>(),
                AllProducts = new List<Model.Product>()
            };

            using (var db = new EFMultipleResultSetEntities())
            {
                // If using Code First we need to make sure the model is built before we open the connection 
                // This isn't required for models created with the EF Designer 
                db.Database.Initialize(force: false);

                // Create a SQL command to execute the sproc 
                var cmd = db.Database.Connection.CreateCommand();
                cmd.CommandText = "[dbo].[GetAllData]";

                try
                {
                    db.Database.Connection.Open();

                    // Run the sproc  
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result.AllClients.Add(
                            new Model.Client()
                            {
                                ID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Age = reader.GetInt32(2)
                            });
                    }

                    reader.NextResult();
                    while (reader.Read())
                    {
                        result.AllProducts.Add(
                            new Model.Product()
                            {
                                ID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Quantity = reader.GetDecimal(2)
                            });
                    }

                }
                finally
                {
                    db.Database.Connection.Close();
                }
            }

            return result;
        }

public AllData GetAllData_2()
{
    AllData result = new AllData()
    {
        AllClients = new List<Model.Client>(),
        AllProducts = new List<Model.Product>()
    };

    using (var db = new EFMultipleResultSetEntities())
    {
        // Create a SQL command to execute the sproc 
        var cmd = db.Database.Connection.CreateCommand();
        cmd.CommandText = "[dbo].[GetAllData]";

        try
        {
            db.Database.Connection.Open();

            // Run the sproc  
            var reader = cmd.ExecuteReader();
            // Read Blogs from the first result set 
            var customers = ((IObjectContextAdapter)db)
                .ObjectContext
                .Translate<Client>(reader, "Clients", MergeOption.AppendOnly);


            foreach (var item in customers)
            {
                result.AllClients.Add(
                    new Model.Client()
                    {
                        ID = item.ID,
                        Name = item.Name,
                        Age = item.Age
                    });
            }

            // Move to second result set and read Posts 
            reader.NextResult();
            var products = ((IObjectContextAdapter)db)
                .ObjectContext
                .Translate<Product>(reader, "Products", MergeOption.AppendOnly);


            foreach (var item in products)
            {
                result.AllProducts.Add(
                    new Model.Product()
                    {
                        ID = item.ID,
                        Name = item.Name,
                        Quantity = item.Quantity
                    });
            }
        }
        finally
        {
            db.Database.Connection.Close();
        }
    }

    return result;
}


    }
}