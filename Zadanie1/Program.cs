using MySqlConnector;

namespace Zadanie1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<FIO> fioS = new List<FIO>();  
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                Database = "sys",
                UserID = "Admin",
                Password = "Admin",
                SslMode = MySqlSslMode.Required,
            };

            using (var conn = new MySqlConnection(builder.ConnectionString))
            {
                Console.WriteLine("Opening connection");
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                   
                    command.CommandText = "SELECT * FROM fio WHERE age < @age";
                    int age = 40;
                    command.Parameters.AddWithValue("@age",age);
                    using (var reader = command.ExecuteReader())
                    {
                       
                        while (reader.Read())
                        {
                              fioS.Add(new FIO { Id = reader.GetInt32(0),LastName =  reader.GetString(1),
                                  FirstName = reader.GetString(2),Age = reader.GetInt32(3) });
                        }
                    }
                }
                    
             
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"ID|LastName|FirstName|Age");
            Console.ResetColor();   
            foreach (var s in fioS)
            {
                Console.WriteLine($"{s.Id}|{s.LastName}|{s.FirstName}|{s.Age}");
            }



        }
    }
}