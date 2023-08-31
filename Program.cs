/*Создайте в SSMS таблицу инетренат магазина, которая будет хранить в себе поля по продукту. 
Наполните 5 продуктами. После чего установите отсоединенный режим подключения к данной БД и таблице. 
Добавьте 5 продуктов. Измените продукты которые уже были в бд. Изменить 5 продуктов(по одному свойству)
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Homework_31_08
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.ForegroundColor = ConsoleColor.Green;

            try
            {
                Console.WriteLine();
                Console.Write("Введите имя сервера: ");
                string server_name = Console.ReadLine();
                Console.Write("Введите имя базы данных: ");
                string database_name = Console.ReadLine();
                Console.Write("Введите имя таблицы: ");
                string table_name = Console.ReadLine();
                Console.WriteLine();

                Console.WriteLine("Подождите немного, выполняется подключение...");

                string connection_string = $"Data Source = {server_name}; Initial Catalog = {database_name}; Trusted_Connection=True; TrustServerCertificate = True";
                string sql_request = $"SELECT * FROM {table_name}";

               
                    using(SqlConnection connection = new SqlConnection(connection_string))
                    {
                        connection.Open();

                        while (true)
                        {
                            try
                            {
                                Console.WriteLine();
                                Console.WriteLine("Нажмите 1 чтобы отобразить содержимое таблицы.");
                                Console.WriteLine("Нажмите 2 чтобы редактировать таблицу.");
                                Console.WriteLine("Нажмите 3 чтобы выйти.");
                                ConsoleKeyInfo key = Console.ReadKey();
                                Console.WriteLine();

                                if (key.Key == ConsoleKey.D1)
                                {
                                    
                                    

                                    SqlDataAdapter adapter = new SqlDataAdapter(sql_request, connection_string);

                                    DataSet dataSet = new DataSet();

                                    adapter.Fill(dataSet);

                                    foreach (DataTable dataTable in dataSet.Tables)
                                    {
                                        
                                        foreach (DataColumn dataColumn in dataTable.Columns)
                                        {
                                            Console.Write($"|{dataColumn.ColumnName}|\t");
                                        }
                                        Console.WriteLine(); 

                                        
                                        foreach (DataRow dataRow in dataTable.Rows)
                                        {
                                            var cells = dataRow.ItemArray;
                                            foreach (var item in cells)
                                            {
                                                Console.Write($"{item}\t");
                                            }
                                            Console.WriteLine(); 
                                        }
                                    }

                                }

                                if (key.Key == ConsoleKey.D2)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Нажмите 1 чтобы добавить продукт в таблицу.");
                                    Console.WriteLine("Нажмите 2 чтобы изменить запись о продукте.");
                                    Console.WriteLine("Нажмите 3 чтобы удалить продукт из таблицы.");
                                    Console.WriteLine("Нажмите 4 чтобы вернуться в начальное меню.");
                                    ConsoleKeyInfo key_two = Console.ReadKey();
                                    Console.WriteLine();

                                    if (key_two.Key == ConsoleKey.D1)
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("Добавление продукта.");
                                        Console.Write("Введите название продукта: ");
                                        string prduct_name = Console.ReadLine();
                                        Console.WriteLine();
                                        Console.Write("Введите количество продукта: ");
                                        int prduct_count = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine();

                                       
                                            SqlDataAdapter adapter = new SqlDataAdapter(sql_request, connection);

                                            DataSet dataSet = new DataSet();

                                            adapter.Fill(dataSet);

                                            DataTable dataTable = dataSet.Tables[0];
                                            DataRow dataRow = dataTable.NewRow();

                                            dataRow["Name"] = $"{prduct_name}";
                                            dataRow["Count"] = $"{prduct_count}";

                                            dataTable.Rows.Add(dataRow);

                                            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);
                                            adapter.Update(dataSet);
                                            dataSet.Clear();
                                        
                                    }

                                    if (key_two.Key == ConsoleKey.D2)
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("Изменение записи о продукте.");
                                        Console.Write("Введите ID продукта которого нужно изменить: ");
                                        int prduct_id = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Нажмите 1 если требуется изменить только название.");
                                        Console.WriteLine("Нажмите 2 если требуется изменить только количество.");
                                        Console.WriteLine("Нажмите 3 если требуется изменить и название и количество.");
                                        ConsoleKeyInfo key_three = Console.ReadKey();
                                        Console.WriteLine();

                                        if (key_three.Key == ConsoleKey.D1)
                                        {
                                            Console.WriteLine();
                                            Console.WriteLine("Замена названия.");
                                            Console.Write("Введите новое название: ");
                                            string new_prduct_name = Console.ReadLine();
                                            Console.WriteLine();

                                            
                                                SqlDataAdapter adapter = new SqlDataAdapter(sql_request, connection);

                                                DataSet dataSet = new DataSet();

                                                adapter.Fill(dataSet);

                                                DataTable dataTable = dataSet.Tables[0];


                                                dataTable.Rows[prduct_id - 1]["Name"] = $"{new_prduct_name}";

                                                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);
                                                adapter.Update(dataSet);
                                                dataSet.Clear();
                                            
                                        }

                                        if (key_three.Key == ConsoleKey.D2)
                                        {
                                            Console.WriteLine();
                                            Console.WriteLine("Замена количества.");
                                            Console.Write("Введите новое количество: ");
                                            int new_product_count = Convert.ToInt32(Console.ReadLine());
                                            Console.WriteLine();

                                           
                                                SqlDataAdapter adapter = new SqlDataAdapter(sql_request, connection);

                                                DataSet dataSet = new DataSet();

                                                adapter.Fill(dataSet);

                                                DataTable dataTable = dataSet.Tables[0];


                                                dataTable.Rows[prduct_id - 1]["Count"] = $"{new_product_count}";

                                                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);
                                                adapter.Update(dataSet);
                                                dataSet.Clear();
                                           
                                        }

                                        if (key_three.Key == ConsoleKey.D3)
                                        {
                                            Console.WriteLine();
                                            Console.WriteLine("Замена названия и количества.");
                                            Console.Write("Введите новое название: ");
                                            string new_prduct_name = Console.ReadLine();
                                            Console.WriteLine();
                                            Console.Write("Введите новое количество: ");
                                            int new_product_count = Convert.ToInt32(Console.ReadLine());
                                            Console.WriteLine();

                                             SqlDataAdapter adapter = new SqlDataAdapter(sql_request, connection);

                                                DataSet dataSet = new DataSet();

                                                adapter.Fill(dataSet);

                                                DataTable dataTable = dataSet.Tables[0];

                                                dataTable.Rows[prduct_id - 1]["Name"] = $"{new_prduct_name}";
                                                dataTable.Rows[prduct_id - 1]["Count"] = $"{new_product_count}";

                                                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);
                                                adapter.Update(dataSet);
                                                dataSet.Clear();
                                           
                                        }
                                    }

                                    if (key_two.Key == ConsoleKey.D3)
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("Удаление продукта.");
                                        Console.Write("Введите ID продукта которого нужно удалить: ");
                                        int prduct_id = Convert.ToInt32(Console.ReadLine());

                                        
                                            SqlDataAdapter adapter = new SqlDataAdapter(sql_request, connection);

                                            DataSet dataSet = new DataSet();

                                            adapter.Fill(dataSet);

                                            DataTable dataTable = dataSet.Tables[0];


                                            dataTable.Rows[prduct_id - 1].Delete();

                                            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);
                                            adapter.Update(dataSet);
                                            dataSet.Clear();
                                       
                                    }

                                    if (key_two.Key == ConsoleKey.D4)
                                    {

                                    }
                                }

                                if (key.Key == ConsoleKey.D3)
                                {
                                    Environment.Exit(3);
                                }

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine();
                                Console.WriteLine($"Ошибка: {ex.Message}");
                                Console.WriteLine();
                            }
                        }
                    }
               
            }
            catch(Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine($"Ошибка: {ex.Message}");
                Console.WriteLine();
                Console.WriteLine("Нажмите любую клавишу для выхода.");
                Console.Read();
            }
        }
    }
}
