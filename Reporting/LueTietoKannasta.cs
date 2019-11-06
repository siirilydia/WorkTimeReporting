using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Raportointi
{
    class LueTietoKannasta
    {

        public const string connString = "server=localhost;database=Tuntiseuranta;trusted_connection=true;MultipleActiveResultSets=True";

        public void HaePäiväMäärällä(DateTime alku, DateTime loppu, string vastaus)
        {
            int laskutettavat = default;

            SqlConnection connection = new SqlConnection();
            SqlCommand cmd = new SqlCommand();

            connection.ConnectionString = connString;
            cmd.Connection = connection;

            connection.Open();

            cmd.Parameters.Add("@alku", SqlDbType.DateTime, 0);
            cmd.Parameters.Add("@loppu", SqlDbType.DateTime, 1);
            cmd.Parameters.Add("@id", SqlDbType.Int, 2);


            cmd.Parameters[0].Value = alku;
            cmd.Parameters[1].Value = loppu;
            cmd.Parameters[2].Value = 0;


            if (vastaus == "1")
            {
                cmd.CommandText = "select * from tunnit where pvm >= @alku and pvm <= @loppu";
            }

            else if (vastaus == "2")
            {
                int id = KenenTunnitHaetaan();
                cmd.Parameters[2].Value = id;

                cmd.CommandText = "select * from tunnit where pvm >= @alku and pvm <= @loppu " +
                    "AND työntekijä_id = @id; ";

            }

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                if (reader[4].ToString() == "k")
                {
                    laskutettavat += int.Parse(reader[2].ToString());
                }
                Console.WriteLine($"työntekijä: {reader[0].ToString()} tehtävä: {reader[3]} tunnit: {reader[2]}, päivämäärä: {reader[5]}");
            }

            Console.WriteLine();
            Console.WriteLine($"Laskutettavia tunteja on {laskutettavat}.");
            laskutettavat = default;

            reader.Close();
            connection.Close();

        }

        public int KenenTunnitHaetaan()
        {
            SqlConnection connection2 = new SqlConnection();
            SqlCommand cmd2 = new SqlCommand();

            connection2.ConnectionString = connString;
            cmd2.Connection = connection2;

            connection2.Open();

            cmd2.CommandText = "select * from työntekijä";

            SqlDataReader reader2 = cmd2.ExecuteReader();

            Console.WriteLine("Tässä on kaikki työntekijät ja heidät id:t");
            Console.WriteLine();

            while (reader2.Read())
            {
                Console.WriteLine($"{reader2[0]} {reader2[1]} {reader2[2]} osasto: {reader2[3]}, tehtävä: {reader2[4]}");
            }

            Console.WriteLine();
            Console.WriteLine("Kenen id:llä haluat hakea tunnit? Syötä id:");

            int id = int.Parse(Console.ReadLine());

            reader2.Close();
            connection2.Close();

            return id;
        }
    }
}
