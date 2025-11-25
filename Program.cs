using System.Data.Odbc;
namespace HF_Session_11_task_Clinic_Appointment_System
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string connection_string = "Driver={ODBC Driver 17 for SQL Server};" +
                "Server=Localhost;" +
                "Database=Clinic_Appointment_System;" +
                "Trusted_Connection=Yes;";
            using (OdbcConnection Conn = new OdbcConnection(connection_string))
            {
                Conn.Open();
                Console.WriteLine("database connection done successfully");
                // add and create
                List<string> Add_records = new List<string>  {"insert into doctor (doctor_id,D_name,speciality)  values(1,'Mohamed Ghareb','Cardiology')",
                "insert into patient (patient_id,P_name,P_Phone_num,P_gender,Date_of_birth) values(1,'Moamen Ragab','01155598632','male','2004-10-17')",
                "insert into appointment (appointment_id,date_and_time,doctor_id,patient_id) values(1,'2025-11-20 14:30:00',1,1)",
                 "insert into Prescription (Prescription_id,issue_Date,doctor_id,appointment_id) values(1,'2025-07-20',1,1)",
                 "insert into medicine (Medicine_id,M_name,M_description,Dosage) values (1,'Panadol Extra','Panadol Extra is a pain relief','Adults: 1–2 tablets every 4–6 hours as needed.')"
                };
                foreach (string record in Add_records)
                {
                    using (OdbcCommand cmd2 = new OdbcCommand(record, Conn))
                    {
                        int rows = cmd2.ExecuteNonQuery();
                        Console.WriteLine(rows + "row(s) inserted");
                    }
                }

                // query
                List<string> queries = new List<string> { "select * from doctor",
                "select * from patient",
                "select appointment_id from appointment where patient_id=1",
                "select appointment_id from appointment where doctor_id=1",
                "select * from medicine",
                "update medicine set M_name='Fenadon'",
                "update appointment set date_and_time='2025-10-10 17:15:00'",
                //"delete from appointment where appointment_id=1", don't work due to relation
                //"delete from doctor where doctor_id=1" don't work due to relation
                };
     
                foreach (string query in queries)
                {
                    using (OdbcCommand cmd = new OdbcCommand(query, Conn))
                    {
                        if (query.TrimStart().StartsWith("select", StringComparison.OrdinalIgnoreCase))
                        {
                            // SELECT query → use reader
                            using (OdbcDataReader reader = cmd.ExecuteReader())
                            {
                                // Print all columns dynamically
                                int fieldCount = reader.FieldCount;
                                while (reader.Read())
                                {
                                    for (int i = 0; i < fieldCount; i++)
                                    {
                                        Console.Write(reader.GetName(i) + ": " + reader[i] + " | ");
                                    }
                                    Console.WriteLine();
                                }
                                Console.WriteLine("-------------------------------------");
                            }
                        }
                        else
                        {
                            // UPDATE / DELETE → use ExecuteNonQuery
                            int rows = cmd.ExecuteNonQuery();
                            Console.WriteLine("Query executed. Affected rows: " + rows);
                            Console.WriteLine("-------------------------------------");
                        }
                    }
                }
            };
        }
    }
}
