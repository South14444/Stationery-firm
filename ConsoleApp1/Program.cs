using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        SqlConnection conn = null;
        Program()
        {
            string sql = ConfigurationManager.ConnectionStrings["MssqlCoonWarehouse"].ConnectionString;
            conn = new SqlConnection();
            conn.ConnectionString = sql;
        }
        static void Main(string[] args)
        {
            Program pr = new Program();
            pr.SQlOpen();

            while (true)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1  - Выполнить задания 1-3");
                Console.WriteLine("2  - Выполнить задания 4-5");
                Console.WriteLine("3  - Вставить новый продукт");
                Console.WriteLine("4  - Вставить новый тип продукта");
                Console.WriteLine("5  - Вставить нового менеджера");
                Console.WriteLine("6  - Вставить новую фирму покупателя");
                Console.WriteLine("7  - Обновить продукт");
                Console.WriteLine("8  - Обновить фирму покупателя");
                Console.WriteLine("9  - Обновить менеджера");
                Console.WriteLine("10 - Обновить тип продукта");
                Console.WriteLine("11 - Удалить продукт");
                Console.WriteLine("12 - Удалить менеджера");
                Console.WriteLine("13 - Удалить тип продукта");
                Console.WriteLine("14 - Удалить фирму покупателя");
                Console.WriteLine("15 - Показать менеджера с наибольшим количеством продаж по единицам");
                Console.WriteLine("16 - Показать менеджера с наибольшей прибылью");
                Console.WriteLine("17 - Показать менеджера с наибольшей прибылью за указанный период");
                Console.WriteLine("18 - Показать фирму покупателя с самой большой суммой покупок");
                Console.WriteLine("19 - Показать тип продукта с наибольшим количеством продаж по единицам");
                Console.WriteLine("20 - Показать самые популярные продукты");
                Console.WriteLine("21 - Показать продукты, не продававшиеся заданное количество дней");
                Console.WriteLine("0  - Выход");
                Console.Write("\nВведите номер действия: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        pr.tasks();
                        break;
                    case "2":
                        pr.tasks2();
                        break;
                    case "3":
                        pr.InsertProduct();
                        break;
                    case "4":
                        pr.InsertTypeOfProduct();
                        break;
                    case "5":
                        pr.InsertManager();
                        break;
                    case "6":
                        pr.InsertBuyerFirm();
                        break;
                    case "7":
                        pr.UpdateProduct();
                        break;
                    case "8":
                        pr.UpdateBuyerFirm();
                        break;
                    case "9":
                        pr.UpdateManager();
                        break;
                    case "10":
                        pr.UpdateTypeOfProduct();
                        break;
                    case "11":
                        pr.DeleteProduct();
                        break;
                    case "12":
                        pr.DeleteManager();
                        break;
                    case "13":
                        pr.DeleteTypeOfProduct();
                        break;
                    case "14":
                        pr.DeleteBuyerFirm();
                        break;
                    case "15":
                        pr.ShowTopManagerBySales();
                        break;
                    case "16":
                        pr.ShowTopManagerByProfit();
                        break;
                    case "17":
                        pr.ShowTopManagerByProfitForPeriod();
                        break;
                    case "18":
                        pr.ShowTopBuyerFirmByPurchases();
                        break;
                    case "19":
                        pr.ShowTopProductTypeBySales();
                        break;
                    case "20":
                        pr.ShowMostPopularProducts();
                        break;
                    case "21":
                        pr.ShowUnsoldProductsForPeriod();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                        break;
                }
            }
        }
        public void DeleteBuyerFirm()
        {
            SqlDataReader reader = null;
            try
            {
                Console.WriteLine("Введите ID фирмы покупателя для удаления:");
                int firmId = int.Parse(Console.ReadLine());


                conn.Open();


                string archiveQuery = @"INSERT INTO Buyer_Firm_archive (ID, Name, Address, Phone)
                                SELECT ID, Name, Address, Phone FROM Buyer_Firm WHERE ID = @id";
                SqlCommand archiveCmd = new SqlCommand(archiveQuery, conn);
                archiveCmd.Parameters.AddWithValue("@id", firmId);
                archiveCmd.ExecuteNonQuery();


                string deleteQuery = "DELETE FROM Buyer_Firm WHERE ID = @id";
                SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn);
                deleteCmd.Parameters.AddWithValue("@id", firmId);
                int rowsAffected = deleteCmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Фирма покупателя успешно удалена и перенесена в архив.");
                }
                else
                {
                    Console.WriteLine("Фирма покупателя с таким ID не найдена.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: " + e.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        public void DeleteTypeOfProduct()
        {
            SqlDataReader reader = null;
            try
            {
                Console.WriteLine("Введите ID типа продукта для удаления:");
                int typeId = int.Parse(Console.ReadLine());

                conn.Open();

                string archiveQuery = @"INSERT INTO Type_of_product_archive (ID, Name)
                                SELECT ID, Name FROM Type_of_product WHERE ID = @id";
                SqlCommand archiveCmd = new SqlCommand(archiveQuery, conn);
                archiveCmd.Parameters.AddWithValue("@id", typeId);
                archiveCmd.ExecuteNonQuery();

                string deleteQuery = "DELETE FROM Type_of_product WHERE ID = @id";
                SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn);
                deleteCmd.Parameters.AddWithValue("@id", typeId);
                int rowsAffected = deleteCmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Тип продукта успешно удалён и перенесён в архив.");
                }
                else
                {
                    Console.WriteLine("Тип продукта с таким ID не найден.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: " + e.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        public void ShowUnsoldProductsForPeriod()
        {
            try
            {
                conn.Open();
                Console.Write("Введите количество дней: ");
                int days = int.Parse(Console.ReadLine());

                string query = @"SELECT P.Name
                         FROM Product P
                         WHERE NOT EXISTS (
                            SELECT 1
                            FROM Sale S
                            WHERE S.ProductId = P.ID
                            AND S.Date >= DATEADD(DAY, -@days, GETDATE())
                         )";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@days", days);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Продукт: {reader["Name"]} не продавался последние {days} дней.");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public void ShowMostPopularProducts()
        {
            try
            {
                conn.Open();
                string query = @"SELECT TOP 1 P.Name, SUM(S.Quantity) AS TotalSold
                         FROM Sale S
                         JOIN Product P ON S.ProductId = P.ID
                         GROUP BY P.Name
                         ORDER BY TotalSold DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Продукт: {reader["Name"]}, Продано единиц: {reader["TotalSold"]}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public void ShowTopProductTypeBySales()
        {
            try
            {
                conn.Open();
                string query = @"SELECT TOP 1 T.Name, SUM(S.Quantity) AS TotalSales
                         FROM Sale S
                         JOIN Product P ON S.ProductId = P.ID
                         JOIN Type_of_product T ON P.Type_of_productId = T.ID
                         GROUP BY T.Name
                         ORDER BY TotalSales DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Тип продукта: {reader["Name"]}, Количество продаж: {reader["TotalSales"]}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public void ShowTopBuyerFirmByPurchases()
        {
            try
            {
                conn.Open();
                string query = @"SELECT TOP 1 BF.Name, SUM(S.Quantity * P.Price) AS TotalPurchase
                         FROM Sale S
                         JOIN Buyer_firm BF ON S.Buyer_firmId = BF.ID
                         JOIN Product P ON S.ProductId = P.ID
                         GROUP BY BF.Name
                         ORDER BY TotalPurchase DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Фирма: {reader["Name"]}, Сумма покупок: {reader["TotalPurchase"]}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public void ShowTopManagerByProfitForPeriod()
        {
            try
            {
                conn.Open();
                Console.Write("Введите начальную дату (yyyy-mm-dd): ");
                string startDate = Console.ReadLine();
                Console.Write("Введите конечную дату (yyyy-mm-dd): ");
                string endDate = Console.ReadLine();

                string query = @"SELECT TOP 1 M.Name, SUM(S.Quantity * P.Price) AS TotalProfit
                         FROM Sale S
                         JOIN Manager M ON S.ManagerId = M.ID
                         JOIN Product P ON S.ProductId = P.ID
                         WHERE S.Date BETWEEN @startDate AND @endDate
                         GROUP BY M.Name
                         ORDER BY TotalProfit DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Менеджер: {reader["Name"]}, Прибыль: {reader["TotalProfit"]}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public void ShowTopManagerByProfit()
        {
            try
            {
                conn.Open();
                string query = @"SELECT TOP 1 M.Name, SUM(S.Quantity * P.Price) AS TotalProfit
                         FROM Sale S
                         JOIN Manager M ON S.ManagerId = M.ID
                         JOIN Product P ON S.ProductId = P.ID
                         GROUP BY M.Name
                         ORDER BY TotalProfit DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Менеджер: {reader["Name"]}, Прибыль: {reader["TotalProfit"]}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public void ShowTopManagerBySales()
        {
            try
            {
                conn.Open();
                string query = @"SELECT TOP 1 M.Name, SUM(S.Quantity) AS TotalSales
                         FROM Sale S
                         JOIN Manager M ON S.ManagerId = M.ID
                         GROUP BY M.Name
                         ORDER BY TotalSales DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Менеджер: {reader["Name"]}, Продажи: {reader["TotalSales"]}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public void DeleteManager()
        {
            try
            {
                conn.Open();
                Console.Write("Введите ID менеджера для удаления: ");
                int managerId = int.Parse(Console.ReadLine());

                string archiveQuery = @"INSERT INTO Archive_Manager SELECT * FROM Manager WHERE ID = @id";
                string deleteQuery = @"DELETE FROM Manager WHERE ID = @id";

                SqlCommand archiveCmd = new SqlCommand(archiveQuery, conn);
                archiveCmd.Parameters.AddWithValue("@id", managerId);
                archiveCmd.ExecuteNonQuery();

                SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn);
                deleteCmd.Parameters.AddWithValue("@id", managerId);
                int rowsAffected = deleteCmd.ExecuteNonQuery();

                Console.WriteLine($"{rowsAffected} менеджер(ов) удалено и архивировано.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public void DeleteProduct()
        {
            try
            {
                conn.Open();
                Console.Write("Введите ID продукта для удаления: ");
                int productId = int.Parse(Console.ReadLine());

                string archiveQuery = @"INSERT INTO Archive_Product SELECT * FROM Product WHERE ID = @id";
                string deleteQuery = @"DELETE FROM Product WHERE ID = @id";

                SqlCommand archiveCmd = new SqlCommand(archiveQuery, conn);
                archiveCmd.Parameters.AddWithValue("@id", productId);
                archiveCmd.ExecuteNonQuery();

                SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn);
                deleteCmd.Parameters.AddWithValue("@id", productId);
                int rowsAffected = deleteCmd.ExecuteNonQuery();

                Console.WriteLine($"{rowsAffected} продукт(ов) удалено и архивировано.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public void UpdateTypeOfProduct()
        {
            try
            {
                conn.Open();
                Console.Write("Введите ID типа для обновления: ");
                int typeId = int.Parse(Console.ReadLine());

                Console.Write("Введите новое название типа: ");
                string newName = Console.ReadLine();

                string query = @"UPDATE Type_of_product SET Name = @name WHERE ID = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", newName);
                cmd.Parameters.AddWithValue("@id", typeId);

                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} тип(ов) канцтоваров обновлено.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public void UpdateManager()
        {
            try
            {
                conn.Open();
                Console.Write("Введите ID менеджера для обновления: ");
                int managerId = int.Parse(Console.ReadLine());

                Console.Write("Введите новое имя менеджера: ");
                string newName = Console.ReadLine();

                string query = @"UPDATE Manager SET Name = @name WHERE ID = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", newName);
                cmd.Parameters.AddWithValue("@id", managerId);

                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} менеджер(ов) обновлено.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public void UpdateBuyerFirm()
        {
            try
            {
                conn.Open();
                Console.Write("Введите ID фирмы для обновления: ");
                int firmId = int.Parse(Console.ReadLine());

                Console.Write("Введите новое название фирмы: ");
                string newName = Console.ReadLine();

                string query = @"UPDATE Buyer_firm SET Name = @name WHERE ID = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", newName);
                cmd.Parameters.AddWithValue("@id", firmId);

                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} фирм(ы) обновлено.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public void UpdateProduct()
        {
            try
            {
                conn.Open();
                Console.Write("Введите ID продукта для обновления: ");
                int productId = int.Parse(Console.ReadLine());

                Console.Write("Введите новое название продукта: ");
                string newName = Console.ReadLine();

                Console.Write("Введите новое количество: ");
                int newQuantity = int.Parse(Console.ReadLine());

                Console.Write("Введите новую цену: ");
                decimal newCostPrice = decimal.Parse(Console.ReadLine());

                string query = @"UPDATE Product SET Name = @name, Quantity = @quantity, Cost_price = @costPrice WHERE ID = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", newName);
                cmd.Parameters.AddWithValue("@quantity", newQuantity);
                cmd.Parameters.AddWithValue("@costPrice", newCostPrice);
                cmd.Parameters.AddWithValue("@id", productId);

                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} продукт(ов) обновлено.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public void InsertBuyerFirm()
        {
            try
            {
                conn.Open();
                Console.Write("Введите название фирмы: ");
                string firmName = Console.ReadLine();

                string query = @"INSERT INTO Buyer_firm (Name) VALUES (@name)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", firmName);

                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} фирм(ы) покупателей добавлено.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public void InsertManager()
        {
            try
            {
                conn.Open();
                Console.Write("Введите имя менеджера: ");
                string managerName = Console.ReadLine();

                string query = @"INSERT INTO Manager (Name) VALUES (@name)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", managerName);

                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} менеджер(ов) добавлено.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public void InsertTypeOfProduct()
        {
            try
            {
                conn.Open();
                Console.Write("Введите название типа продукта: ");
                string typeName = Console.ReadLine();

                string query = @"INSERT INTO Type_of_product (Name) VALUES (@name)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", typeName);

                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} тип(ов) продукта добавлено.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public void InsertProduct()
        {
            try
            {
                conn.Open();
                Console.Write("Введите название продукта: ");
                string productName = Console.ReadLine();

                Console.Write("Введите количество: ");
                int quantity = int.Parse(Console.ReadLine());

                Console.Write("Введите цену: ");
                decimal costPrice = decimal.Parse(Console.ReadLine());

                Console.Write("Введите ID типа продукта: ");
                int typeOfProductId = int.Parse(Console.ReadLine());

                string query = @"INSERT INTO Product (Name, Quantity, Cost_price, Type_of_productId)
                         VALUES (@name, @quantity, @costPrice, @typeOfProductId)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", productName);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@costPrice", costPrice);
                cmd.Parameters.AddWithValue("@typeOfProductId", typeOfProductId);

                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} продукт(ов) добавлено.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        public void tasks()
        {
            SqlDataReader reader = null;
            try
            {
                conn.Open();
                string str = Console.ReadLine();
                string query = string.Empty;
                bool flag = true;
                switch (str)
                {
                    case "1":
                        query = @"select *
                            from Sale as S
                            join Manager as M On S.ManagerId=S.ID
                            join Product as P ON S.ProductId=P.ID
                            join Type_of_product as T ON P.Type_of_productId=T.ID";
                        break;
                    case "2":
                        query = @"select * from Type_of_product;";
                        break;
                    case "3":
                        query = @"select * from Manager;";
                        break;
                    case "4":
                        query = @"select Max(Quantity) as 'Max(Quantity)' from Product;";
                        flag = false;
                        break;
                    case "5":
                        flag = false;
                        query = @"select Min(Quantity) as 'Min(Quantity)'from Product;";
                        break;
                    case "6":
                        flag = false;
                        query = @"select Max(Cost_price) as 'Max(Cost_price)' from Product;";
                        break;
                    case "7":
                        flag = false;
                        query = @"select Min(Cost_price) as 'Min(Cost_price)' from Product;";
                        break;
                }
                SqlCommand cmd = new SqlCommand(query, conn);
                if (flag)
                {
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        //Названия Столбцов
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write($"{reader.GetName(i)}\t");
                        }
                        Console.WriteLine();
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Console.Write($"{reader.GetValue(i)}\t");
                            }
                            Console.WriteLine();
                        }
                    }
                }
                else
                {
                    Console.WriteLine(cmd.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (!reader.IsClosed) reader.Close();
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }
        public void tasks2()
        {
            SqlDataReader reader = null;
            try
            {
                conn.Open();
                string str = Console.ReadLine();
                string query = string.Empty;
                string stre2 = string.Empty;
                switch (str)
                {
                    case "1":
                        stre2 = Console.ReadLine();
                        query = @"select P.name,T.name
                            from Product as P
                            join Type_of_product as T On P.Type_of_productId=t.ID
                            where T.Name like @p1;";
                        break;
                    case "2":
                        stre2 = Console.ReadLine();
                        query = @"select M.Name,T.name,P.name
                            from Sale as S
                            join Manager as M On S.ManagerId=S.ID
                            join Product as P ON S.SupplierId=P.ID
                            join Type_of_product as T ON P.Type_of_productId=T.ID
                            where M.Name like @p1;";
                        break;
                    case "3":
                        stre2 = Console.ReadLine();
                        query = @"select P.Name,T.name,S.name,Quantity,Cost_price,Min(Date)
                            from Sale as S
                            join Manager as M On S.ManagerId=S.ID
                            join Product as P ON S.SupplierId=P.ID
                            join Type_of_product as T ON P.Type_of_productId=T.ID
                            where S.Name_buyer like @p1;";
                        break;
                    case "4":
                        query = @"select Max(S.Date)
                            from Sale as S
                            join Manager as M On S.ManagerId=S.ID
                            join Product as P ON S.SupplierId=P.ID
                            join Type_of_product as T ON P.Type_of_productId=T.ID;";
                        break;
                    case "5":
                        query = @"select AVG(P.Quantity),P.Type_of_productId
                        from Product as P
                        join Type_of_product as T ON P.Type_of_productId=T.ID
                        group by P.Type_of_productId;";
                        break;
                }

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@p1", SqlDbType.NVarChar).Value = stre2;
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    //Названия Столбцов
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write($"{reader.GetName(i)}\t");
                    }
                    Console.WriteLine();
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write($"{reader.GetValue(i)}\t");
                        }
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        public void SQlOpen()
        {          
            try
            {
                conn.Open();
                Console.WriteLine("Успешно открыто");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
    }
}
