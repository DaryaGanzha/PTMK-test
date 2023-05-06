using PTMK_test.Data;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Data;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

namespace PTMK_test
{
    internal class DatabaseService
    {
        private Connection connection;
        public DatabaseService()
        {
            connection = new Connection();
        }
        public string CreateTable()
        {
            connection.OpenConnection();
            string createTableQuery = "CREATE TABLE Customer (FullName VARCHAR(50), DateOfBirth DATE, Gender VARCHAR(10))";
            var command = new SqlCommand(createTableQuery, connection.connection);
            command.ExecuteNonQuery();
            connection.CloseConnection();
            return "Таблица создана.";
        }
        public string CreateRecord(string fullName, string date, string gender)
        {
            var dateTime = DateTime.Parse(date);
            connection.OpenConnection();
            string insertQuery = "INSERT INTO Customer (FullName, DateOfBirth, Gender) VALUES (@Value1, @Value2, @Value3)";
            var command = new SqlCommand(insertQuery, connection.connection);
            command.Parameters.AddWithValue("@Value1", SqlDbType.NVarChar);
            command.Parameters.AddWithValue("@Value2", SqlDbType.Date);
            command.Parameters.AddWithValue("@Value3", SqlDbType.NVarChar);
            command.Parameters["@Value1"].Value = fullName;
            command.Parameters["@Value2"].Value = dateTime.ToShortDateString();
            command.Parameters["@Value3"].Value = gender;
            command.ExecuteNonQuery();
            connection.CloseConnection();
            return "Данные успешно добавлены.";
        }
        private void PrintTable(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn col in table.Columns)
                {
                    Console.Write($"{row[col]} \t");
                }
                Console.WriteLine();
            }
        }
        public string GetUniqueRows()
        {
            connection.OpenConnection();
            string query = "SELECT DISTINCT FullName, DateOfBirth, Gender, DATEDIFF(year, [DateOfBirth], GETDATE()) \r\nFROM Customer \r\nGROUP BY FullName, DateOfBirth, Gender\r\nORDER BY FullName";
            var command = new SqlCommand(query, connection.connection);
            var adapter = new SqlDataAdapter(command);
            var table = new DataTable();

            adapter.Fill(table);
            connection.CloseConnection();
            this.PrintTable(table);
            return "Таблица выведена успешно.";
        }
        private string GenerateName(string firstLetter)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();
            int length = random.Next(3, 10);
            string name = firstLetter + new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return name;
        }
        private DateTime GenerateDate()
        {
            var random = new Random();
            DateTime startDate = new DateTime(1950, 1, 1);
            DateTime endTime = DateTime.Now;
            int range = (endTime - startDate).Days;
            return startDate.AddDays(random.Next(range)).AddHours(random.Next(24)).AddMinutes(random.Next(60)).AddSeconds(random.Next(60));
        }
        private string GenereateGender()
        {
            var random = new Random();
            return random.Next(1, 3).ToString();
        }
        public string RandomLinesGeneration(int num, string firstLetter, string gen)
        {
            connection.OpenConnection();

            for (int i = 0; i < num; i++)
            {
                string fullName = this.GenerateName(firstLetter) + " " + this.GenerateName(firstLetter) + " " + this.GenerateName(firstLetter);
                DateTime dateOfBirth = GenerateDate();
                string gender;
                if (gen == "")
                {
                    gender = GenereateGender();
                }
                else
                {
                    gender = gen;
                }
                
                this.CreateRecord(fullName, dateOfBirth.ToString(), gender);
            }
            connection.CloseConnection();
            return "Данные успешно добавлены!";
        }
        public string QueryByCriteria()
        {
            connection.OpenConnection();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string query = "SELECT * FROM Customer WHERE Gender LIKE '2' AND FullName LIKE 'F%'";
            var command = new SqlCommand(query, connection.connection);
            stopwatch.Stop();

            var adapter = new SqlDataAdapter(command);
            var table = new DataTable();

            adapter.Fill(table);
            connection.CloseConnection();
            // this.PrintTable(table);
            Console.WriteLine($"Время выполнения запроса: {stopwatch.Elapsed}");
            return "Запрос выполнен успешно.";
        }
    }
}
