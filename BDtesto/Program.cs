using System;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BDtesto
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string connectionString = "Server=localhost;Database=testoAdjale;Trusted_Connection=True;";
            SqlCommand sqlCommand = new SqlCommand();
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                // Открываем подключение
                await connection.OpenAsync();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM COMMON", connection);
                Console.WriteLine("Подключение открыто");
                DataSet ds = new DataSet();
                // Заполняем Dataset
                adapter.Fill(ds);
                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataColumn column in dt.Columns)
                        Console.Write($"{column.ColumnName}\t");
                    Console.WriteLine();
                    // перебор всех строк таблицы
                    foreach (DataRow row in dt.Rows)
                    {
                        // получаем все ячейки строки
                        var cells = row.ItemArray;
                        foreach (object cell in cells)
                            Console.Write($"{cell} ");
                        Console.WriteLine();
                    }
                }


            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
