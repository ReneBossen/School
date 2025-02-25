using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ORM_EF
{
    internal class Program
    {
        //Opgave 15.1
        static void Main(string[] args)
        {
            ORMdbEntities DB = new ORMdbEntities();
            person pers = (from p in DB.people select p).First();
            Console.WriteLine(pers.navn);
            Console.ReadLine();

            //Opgave 15.1.1
            //Insert person
            person p1 = new person();
            p1.navn = "Karl Sørensen";
            p1.cpr = "1818181818";
            p1.stilling = "systemudvikler";
            p1.loen = 503178;
            p1.postnr = "8000";
            DB.people.Add(p1);
            DB.SaveChanges();
            Console.WriteLine("Bo Hansen er tilføjet. \nSkriv CPR nr på den person du ønsker at slette");
            string inputCPR = Console.ReadLine();

            //Opgave 15.1.2
            //Delete person
            //Slet Bo Hansen igen
            DB.people.Remove((from p in DB.people where p.cpr == inputCPR select p).First());
            Console.WriteLine((from p in DB.people where p.cpr == inputCPR select p.navn).First() + " er blevet slettet fra databasen");
            DB.SaveChanges();

            //Opgave 15.1.3
            //Update salary
            //Opgave 15.1.4 put it inside try/catch
            Console.WriteLine("Skriv CPR nr på den person du ønsker at opdaterer lønnen på");
            string updateSalaryCPR = Console.ReadLine();
            try
            {
                Console.WriteLine("Hvad skal " + (from p in DB.people where p.cpr == updateSalaryCPR select p.navn).First() + " nye løn være?");
                int newSalary = int.Parse(Console.ReadLine());
                person salaryUpdatePerson = (from p in DB.people where p.cpr == updateSalaryCPR select p).First();
                salaryUpdatePerson.loen = newSalary;
                Console.WriteLine((from p in DB.people where p.cpr == updateSalaryCPR select p.navn).First() + "'s løn er nu opdateret");
                DB.SaveChanges();
            }
            catch
            {
                Console.WriteLine("CPR nr findes ikke");
            }

            //Opgave 15.1.5
            //Print name and cpr of all persons
            Console.WriteLine("Printer alle personers navn og cpr");
            foreach (person p in (from p in DB.people select p))
            {
                Console.WriteLine("navn: " + p.navn + " cpr: " + p.cpr);
            }
            Console.ReadLine();

            //Opgave 15.1.6
            //Print name, cpr and postaldistrict of all persons (the SQL-Like way)
            Console.WriteLine("Navn, cpr og postnr på alle personer med SQL");
            var allePersoner = from p in DB.people
                               from pnr in DB.postnummers
                               where p.postnr == pnr.postnr
                               select new { p.navn, p.cpr, pnr.postnr };

            foreach (var p in allePersoner)
            {
                Console.WriteLine("Navn: " + p.navn + " Cpr: " + p.cpr + " Postnr: " + p.postnr);
            }
            Console.ReadLine();

            //Opgave 15.1.7
            Console.WriteLine("Navn, cpr og postnr på alle personer med LINQ");
            var allePersonerMedLINQ = from p in DB.people select new { p.navn, p.cpr, p.postnummer.postdistrikt };
            foreach (var p in allePersonerMedLINQ)
            {
                Console.WriteLine("Navn: " + p.navn + " Cpr: " + p.cpr + " Postnr: " + p.postdistrikt);
            }
            Console.ReadLine();
        }



        //Opgave 15.3
        static void Main(string[] args)
        {
            ORMdbEntities DB = new ORMdbEntities();
            person pers = (from p in DB.people select p).First();


            Console.WriteLine("Skriv CPR nr på personen du ønsker at flytte postnr på");
            string flytCPR = Console.ReadLine();

            //Print alle postnumre ud kommasepareret
            List<postnummer> pnr = (from p in DB.postnummers select p).ToList();
            var postnrStrings = pnr.Select(p => p.postnr.ToString());
            string result = String.Join(", ", postnrStrings);
            Console.WriteLine("\nAlle Postnumre der kan flyttes til");
            Console.WriteLine(result);

            person flytPerson = (from p in DB.people where p.cpr == flytCPR select p).First();

            Console.WriteLine("\n" + flytPerson.navn + " bor i postnr " + flytPerson.postnr);
            Console.WriteLine("Skriv postNr " + flytPerson.navn + " skal flytte til");
            string flytPostNR = Console.ReadLine();

            flytPerson.postnr = flytPostNR;
            DB.SaveChanges();
        }

        //Opgave 15.4
        static void Main(string[] args)
        {
            ORMdbEntities DB = new ORMdbEntities();
            person flytPerson = null;
            //Flyt en person til et ikke eksisterende postnr
            try
            {
                person pers = (from p in DB.people select p).First();

                Console.WriteLine("Skriv CPR nr på personen du ønsker at flytte postnr på");
                string flytCPR = Console.ReadLine();

                //Print alle postnumre ud kommasepareret
                List<postnummer> pnr = (from p in DB.postnummers select p).ToList();
                var postnrStrings = pnr.Select(p => p.postnr.ToString());
                string result = String.Join(", ", postnrStrings);
                Console.WriteLine("\nAlle Postnumre der kan flyttes til");
                Console.WriteLine(result);

                flytPerson = (from p in DB.people where p.cpr == flytCPR select p).First();

                Console.WriteLine("\n" + flytPerson.navn + " bor i postnr " + flytPerson.postnr);
                Console.WriteLine("Skriv postNr " + flytPerson.navn + " skal flytte til");
                string flytPostNR = Console.ReadLine();

                flytPerson.postnr = flytPostNR;
                DB.SaveChanges();
            }
            catch (Exception ex)
            {
                //Løsning på problemet
                DB.Entry(flytPerson).Reload();
                Console.WriteLine(ex.ToString());
            }

            //Flyt en person til et eksisterende postnr
            try
            {
                person pers = (from p in DB.people select p).First();


                Console.WriteLine("Skriv CPR nr på personen du ønsker at flytte postnr på");
                string flytCPR = Console.ReadLine();

                //Print alle postnumre ud kommasepareret
                List<postnummer> pnr = (from p in DB.postnummers select p).ToList();
                var postnrStrings = pnr.Select(p => p.postnr.ToString());
                string result = String.Join(", ", postnrStrings);
                Console.WriteLine("\nAlle Postnumre der kan flyttes til");
                Console.WriteLine(result);

                flytPerson = (from p in DB.people where p.cpr == flytCPR select p).First();

                Console.WriteLine("\n" + flytPerson.navn + " bor i postnr " + flytPerson.postnr);
                Console.WriteLine("Skriv postNr " + flytPerson.navn + " skal flytte til");
                string flytPostNR = Console.ReadLine();

                flytPerson.postnr = flytPostNR;
                DB.SaveChanges();
            }
            catch (Exception ex)
            {
                //Løsning hvis man skriver forkert postnr
                DB.Entry(flytPerson).Reload();
                Console.WriteLine(ex.ToString());
            }
        }

        static void Main(string[] args)
        {
            ORMdbEntities DB = new ORMdbEntities();
            //Opgave 16.1.1
            //Indlæs et beløb fra tastatur og udskriv de personer, der tjener mere end det pågældende beløb
            Console.WriteLine("Opgave 16.1.1");
            Console.WriteLine("Indtast beløb du ønsker at søge efter");
            int søgeLoen = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Printer alle personers navn og cpr som tjener mere end det givet beløb");
            IQueryable<person> personer = (from p in DB.people
                                           where p.loen > søgeLoen
                                           select p);
            foreach (person p in personer)
            {
                Console.WriteLine("navn: " + p.navn + " tjener: " + p.loen);
            }
            Console.ReadLine();

            //Opgave 16.1.2
            //Man kunne måske ønske at få personerne fra svaret på spørgsmål 1 over i et array i stedet
            //for ud på skærmen. Kunne du forestille dig, hvorledes kommandoen ser ud
            Console.WriteLine("Opgave 16.1.2");
            Console.WriteLine("Indtast beløb du ønsker at søge efter");
            int søgeLoen1 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Printer alle personers navn og cpr som tjener mere end det givet beløb");
            IQueryable<person> personer1 = (from p in DB.people
                                            where p.loen > søgeLoen1
                                            select p);
            List<person> personList = personer1.ToList();

            foreach (person p in personList)
            {
                Console.WriteLine("navn: " + p.navn + " tjener: " + p.loen);
            }
            Console.ReadLine();

            //Opgave 16.1.3
            //Lav en udskrift med navne på personer og hvilket firma, de er ansat i
            //(glem i første omgang de arbejdsløse)
            Console.WriteLine("Opgave 16.1.3");
            IQueryable<person> personer2 = (from p in DB.people
                                            select p);

            foreach (person p in personer2)
            {
                Console.WriteLine(p.navn);
                foreach (firma f in p.firmas)
                {
                    Console.WriteLine("      " + f.firmanavn);
                }
            }
            Console.ReadLine();

            //Opgave 16.1.4
            //Ansæt en person i et firma, f.eks. cpr 1212121212 i firma med firmanr 1
            Console.WriteLine("Opgave 16.1.14");
            Console.WriteLine("Indtast cpr på person du ønsker at ansætte i et firma");
            string personCpr = Console.ReadLine();
            Console.WriteLine("Indtast firmaNr personen skal ansættes i");
            int firmaNr = Convert.ToInt32(Console.ReadLine());
            person person = (from p in DB.people
                             where p.cpr == personCpr
                             select p).FirstOrDefault();
            firma firma = (from f in DB.firmas
                           where f.firmanr == firmaNr
                           select f).FirstOrDefault();
            person.firmas.Add(firma);

            DB.SaveChanges();
            Console.ReadLine();

            //Opgave 16.1.5
            //Lav en udskrift, der viser antal firmaer i hvert postnummer. Kun postnumre med firmaer medtages
            Console.WriteLine("Opgave 16.1.5");
            IQueryable<postnummer> postnr = (from pn in DB.postnummers
                                             where pn.firmas.Count() > 0
                                             select pn);

            foreach (postnummer post in postnr)
            {
                Console.WriteLine(post.postnr + " har " + post.firmas.Count() + " firmaer");
            }


            Console.ReadLine();

            //Opgave 16.1.6
            //Vis gennemsnitslønnen per stilling.
            Console.WriteLine("Opgave 16.1.6");

            var gennemsnitsLønPerStilling = (from p in DB.people
                                             group p by p.stilling into gruppe
                                             select new
                                             {
                                                 Stilling = gruppe.Key,
                                                 Gennemsnitsløn = gruppe.Average(p => p.loen)
                                             });

            foreach (var item in gennemsnitsLønPerStilling)
            {
                Console.WriteLine($"Stilling: {item.Stilling}, Gennemsnitsløn: {item.Gennemsnitsløn}");
            }
            Console.ReadLine();

            //Opgave 16.1.7
            //Prøv at lave en stored procedure i SQL Server. Den skal returnere de personer, der er ansat i
            //et firma. Tilføj proceduren i dit projekt og prøv at kalde den
            Console.WriteLine("Opgave 16.1.7");
        }
    }
}