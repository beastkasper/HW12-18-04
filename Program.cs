using System;
using System.Data;
using System.Data.SqlClient;

namespace Project55
{
    class Program
    {
        static void Main(string[] args)
        {
        const string ConnetoinString = @"Data Source=localhost;Initial Catalog=Person;Integrated Security=True;";
            SqlConnection connection = new SqlConnection(ConnetoinString);
            bool work = true;
            while (work == true)
            {
                System.Console.WriteLine("Choose Operation");
                System.Console.WriteLine("1.Insert");
                System.Console.WriteLine("2.SelectAll");
                System.Console.WriteLine("3.SelectBy");
                System.Console.WriteLine("4.Delete");
                System.Console.WriteLine("5.Update");
                System.Console.WriteLine("6.Quit");
                int operation = int.Parse(Console.ReadLine());
                switch (operation)
                {
                    case 1:
                        {
                            System.Console.Write("Name:");
                            string Name = Console.ReadLine();
                            System.Console.Write("SurName:");
                            string SurName = Console.ReadLine();
                            System.Console.Write("MiddleName:");
                            string MiddleName = Console.ReadLine();
                            Insert(Name, SurName, MiddleName, connection);
                        }
                        break;
                    case 2:
                        {
                            SelectAll(connection);
                        }
                        break;
                    case 3:
                        {
                            System.Console.Write("ID:");
                            int ID = int.Parse(Console.ReadLine());
                            SelectBy(connection, ID);
                        }break;
                        
                    case 4:
                        {
                            System.Console.Write("ID:");
                            int ID = int.Parse(Console.ReadLine());
                            DeleteBy(connection, ID);
                        }break;
                        
                    case 5:
                        {
                            System.Console.Write("Name:");
                            string Name = Console.ReadLine();
                            System.Console.Write("SurName:");
                            string SurName = Console.ReadLine();
                            System.Console.Write("MiddleName:");
                            string MiddleName = Console.ReadLine();
                            System.Console.Write("ID:");
                            int ID = int.Parse(Console.ReadLine());
                            UpdateBy(connection, Name, SurName, MiddleName, ID);
                        }
                        break;
                    case 6: work = false;connection.Close();break;
                }
            }
        }
        static void SelectAll(SqlConnection connection)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("select * from Person", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                System.Console.WriteLine($"ID:{reader.GetValue(0)},Name:{reader.GetValue(1)},SurName:{reader.GetValue(2)},MiddleName:{reader.GetValue(3)},BirthDate:{reader.GetValue(4)}");
            }
            connection.Close();
        }
        static void Insert(string Name, string SurName, string MiddleName, SqlConnection connection)
        {
            string DateTimeDB = $"{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}";
            connection.Open();
            SqlCommand command = new SqlCommand($"insert into Person(Name,SurName,MiddleName,BirthDate)Values('{Name}','{SurName}','{MiddleName}','{DateTimeDB}')", connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader != null)
            {
                System.Console.WriteLine("Added Succsesfully");
            }
            else
            {
                System.Console.WriteLine("Something went wrong");
            }
            connection.Close();
        }
        static void SelectBy(SqlConnection connection, int ID)
        {
            connection.Open();
            SqlCommand command = new SqlCommand($"select * from Person where ID={ID}", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                System.Console.WriteLine($"ID:{reader.GetValue(0)},Name:{reader.GetValue(1)},SurName:{reader.GetValue(2)},MiddleName:{reader.GetValue(3)},BirthDate:{reader.GetValue(4)}");
            }
            connection.Close();
        }
        static void UpdateBy(SqlConnection connection, string Name, string SurName, string MiddleName, int ID)
        {
            connection.Open();
            string DateTimeDB = $"{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}";
            if (Name != null && SurName != null && MiddleName != null)
            {
                SqlCommand Testcommand = new SqlCommand($"select * from Person where ID={ID}", connection);
                SqlDataReader Testreader = Testcommand.ExecuteReader();
                if (Testreader.Read())
                {
                    connection.Close();
                    connection.Open();
                    SqlCommand command = new SqlCommand($"Update Person set Name='{Name}',SurName='{SurName}',MiddleName='{MiddleName}',BirthDate={DateTimeDB} where ID={ID}", connection);
                    SqlDataReader reader = command.ExecuteReader();
                    System.Console.WriteLine("Updated Succesfully");
                }
                else
                {
                    System.Console.WriteLine("This ID doesnt exists");
                }
            }
            else
            {
                System.Console.WriteLine("Please Fill all fields");
            }
            connection.Close();
        }
        static void DeleteBy(SqlConnection connection, int ID)
        {
            connection.Open();
            SqlCommand Testcommand = new SqlCommand($"select * from Person where ID={ID}", connection);
            SqlDataReader Testreader = Testcommand.ExecuteReader();
            if (Testreader.Read())
            {
                connection.Close();
                connection.Open();
                SqlCommand command = new SqlCommand($"Delete from Person where ID={ID}", connection);
                SqlDataReader reader = command.ExecuteReader();
                System.Console.WriteLine("Deleted Succesfully");
            }
            else
            {
                System.Console.WriteLine("This ID doesnt exist");
            }
            connection.Close();
        }
    }
}

