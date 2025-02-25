using System;
using System.Data;
using System.Data.SqlClient;

namespace EksamensopgaveDBRene
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // lav en connection
            string connStr;
            SqlConnection con;
            connStr = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=EksamensopgaveDBRene;user id=sa;password=renebossen123";
            con = new SqlConnection(connStr);


            SqlCommand cmd = con.CreateCommand();
            cmd.Connection = con;


            //Indtast virksomhedsid
            Console.WriteLine("Indtast id på virksomheden du ønsker at booke fra");
            int virksomhedsid = int.Parse(Console.ReadLine());

            //Indtasket ønsket dato
            Console.WriteLine("Indtast ønsket dato (YYYY/MM/DD");
            DateTime ønskeDato = DateTime.Parse(Console.ReadLine());

            //Indtast om forsikring ønskes eller ej
            Console.WriteLine("Ønskes der forsikring med? (Ja/Nej)");
            string forsikringSvar = Console.ReadLine();
            bool forsikringØnskes;
            switch (forsikringSvar.ToLower())
            {

                case "ja":
                    forsikringØnskes = true;
                    break;
                case "nej":
                    forsikringØnskes = false;
                    break;
                default:
                    Console.WriteLine("Forsikring ej tilvalgt");
                    forsikringØnskes = false;
                    break;
            }
            //Indtast kundens id
            Console.WriteLine("Indtast dit id");
            int kundeId = int.Parse(Console.ReadLine());
            //Ingen validering af indtastet

            //Indtast id på udstyr
            Console.WriteLine("Indtast id på det udstyr du ønsker at leje");
            int udstyrId = int.Parse(Console.ReadLine());
            //Der skal kun kunne bookes én slags udstyr ad gangen
            //Indtast antal der ønskes
            Console.WriteLine("Hvor mange stk. ønsker du at leje?");
            int antalStk = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("TjekUdstyrTilgængelighed", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@dato", ønskeDato);
                    command.Parameters.AddWithValue("@udstyrId", udstyrId);
                    command.Parameters.AddWithValue("@antal", antalStk);

                    SqlParameter statusParam = new SqlParameter("@status", SqlDbType.Bit);
                    statusParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(statusParam);

                    command.ExecuteNonQuery();

                    bool isAvailable = (bool)statusParam.Value;
                    if (isAvailable)
                    {
                        Console.WriteLine($"Det ønskede udstyr er tilgængeligt d. {ønskeDato.Date}. Ønsker du at foretage bookingen? (Ja/Nej)");
                        string input = Console.ReadLine();
                        if (input.ToLower() == "ja")
                        {
                            // Bekræft booking
                            using (SqlCommand confirmCommand = new SqlCommand("BookUdstyrBekræftet", connection))
                            {
                                confirmCommand.CommandType = CommandType.StoredProcedure;
                                confirmCommand.Parameters.AddWithValue("@virksomhedsid", virksomhedsid);
                                confirmCommand.Parameters.AddWithValue("@dato", ønskeDato);
                                confirmCommand.Parameters.AddWithValue("@forsikring", forsikringØnskes);
                                confirmCommand.Parameters.AddWithValue("@kundeId", kundeId);
                                confirmCommand.Parameters.AddWithValue("@udstyrId", udstyrId);
                                confirmCommand.Parameters.AddWithValue("@antal", antalStk);

                                confirmCommand.ExecuteNonQuery();
                                Console.WriteLine("Booking foretaget");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Bookingen er annulleret");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Der er desværre ikke nok udstyr ledigt på d. {ønskeDato.Date}...");
                    }
                }
            }
        }
    }
}
