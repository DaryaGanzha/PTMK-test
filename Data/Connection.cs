using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMK_test.Data
{
    internal class Connection
    {
        private string datasource = @"DESKTOP-O98QPDO\SQLEXPRESS";
        private string database = "PTMK Test DB";
        private string username = "DESKTOP-O98QPDO\\User";
        private string connectionSrting;
        public SqlConnection connection;
        public Connection()
        {
        }

        public void OpenConnection()
        {
            //Console.WriteLine("Getting Connection ...");

            //var datasource = @"DESKTOP-O98QPDO\SQLEXPRESS";//your server
            //var database = "PTMK Test DB"; //your database name
            //var username = "DESKTOP-O98QPDO\\User"; //username of server to connect
            //var password = "password"; //password

            //your connection string 
            this.connectionSrting = @"Data Source=" + datasource + ";Initial Catalog="
                        + database + ";Persist Security Info=True;User ID=" + username + ";Trusted_Connection=True;" + ";TrustServerCertificate=True;" + "Encrypt=False;";

            //create instanace of database connection
            this.connection = new SqlConnection(connectionSrting);

            try
            {
                //Console.WriteLine("Openning Connection ...");

                //open connection
                this.connection.Open();

                //Console.WriteLine("Connection successful!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        public void CloseConnection()
        {
            this.connection.Close();
        }
    }
}
