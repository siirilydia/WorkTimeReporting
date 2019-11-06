using System;
using System.Data;
using System.Data.SqlClient;

namespace Raportointi
{
    class Program
    {
        public const string connString = "server=localhost;database=Tuntiseuranta;trusted_connection=true;MultipleActiveResultSets=True";

        static LueTietoKannasta ltk = new LueTietoKannasta();
        static void Main(string[] args)
        {


            char[] jakajat = { ' ', ',', '.', ':', '\t', '\\', '/', '-' };

            Console.WriteLine("Syötä aikaväli jolta haluat hakea tietoa:");
            Console.WriteLine("Alkupvm:");
            string input = Console.ReadLine();
            string[] alkaen = input.Split(jakajat);

            Console.WriteLine("Loppupvm:");
            input = Console.ReadLine();
            string[] päättyen = input.Split(jakajat);

            DateTime alku = new DateTime(int.Parse(alkaen[2]), int.Parse(alkaen[1]), int.Parse(alkaen[0]));
            DateTime loppu = new DateTime(int.Parse(päättyen[2]), int.Parse(päättyen[1]), int.Parse(päättyen[0]));

            Console.WriteLine("Haluatko hakea kaikki tunnit, vai yksittäisen työntekijän?");
            Console.WriteLine(" 1 = KAIKKI 2 = YKSITTÄINEN");


            string vastaus = Console.ReadLine();

            //Console.WriteLine();
            //Console.WriteLine(alku);
            //Console.WriteLine(loppu);

            ltk.HaePäiväMäärällä(alku, loppu, vastaus);

        }
    }
}
