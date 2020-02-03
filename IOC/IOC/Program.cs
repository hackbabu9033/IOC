using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.IO;

namespace IOC
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .Build();

            string connectionString = config.GetConnectionString("AdventureWorks2017");
           
           
        }

        public class CustomerBusinessLogic
        {
            ICustomerDataAccess _dataAccess;
            public CustomerBusinessLogic()
            {
                //邏輯上這裡的_dataAccess不再依賴實體的class，而是由抽象的介面代替
                //DataAccess and CustomerBusinessLogic is now loosely coupled
                _dataAccess = DataAccessFactory.GetDataAccessObj();
            }

            public string GetCustomerName(int id)
            {
                return _dataAccess.GetCustomerName(id);
            }
        }

        public interface ICustomerDataAccess
        {
            string GetCustomerName(int id);
        }

        //產生任何能存取到data的模組
        public class DataAccessFactory
        {
            public static ICustomerDataAccess GetDataAccessObj()
            {
                return new DataAccess();
            }
        }

        public class DataAccess: ICustomerDataAccess
        {
            public DataAccess()
            {
            }

            public string GetCustomerName(int id)
            {
                return "Dummy Customer Name"; // get it from DB in real app
            }
        }

        public class CarDataAccess {

            public static string GetCarInfos()
            {

                return null;
            }
        }


    }
}
