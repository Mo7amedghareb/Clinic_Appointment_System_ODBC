using System.Data.Odbc;
namespace ccharp_database_integration_odbc
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connection_string = "Driver={ODBC Driver 17 for SQL Server};" +
                "Server=Localhost;" +
                "Database=college_task_two;" +
                "Trusted_Connection=Yes;";
            using (OdbcConnection Conn =new OdbcConnection(connection_string))
            {
                Conn.Open();
                Console.WriteLine("database connection done successfully");
                string query = "select student_id ,name from students";
                //same as update , insert , delete , create table
                string insert_record = "insert into students (student_id,name,email,birth_date) " +
                    "values(201,'Mohamed Ghareb','Mo7amed.ghareb@gmail.com','2003-04-17')";
                using (OdbcCommand cmd =new OdbcCommand(query,Conn))
                {
                    OdbcDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int st_id = reader.GetInt32(0);
                        string st_name = reader.GetString(1);
                        Console.WriteLine("student id : "+st_id + " student name : "+st_name);
                    }
                }
                using (OdbcCommand cmd2=new OdbcCommand(insert_record,Conn))    
                {
                    int rows=cmd2.ExecuteNonQuery();
                    Console.WriteLine(rows + "row(s) inserted");
                }
            }
        }
    }
}