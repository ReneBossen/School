using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opgave7EF {
    internal class Program {
        static void Main(string[] args) {
            EksamensopgaveDBReneEntities db = new EksamensopgaveDBReneEntities();

            Console.WriteLine("Skriv navn på udstyr du ønsker at søge på");
            string udstyrsNavn = Console.ReadLine();
            Console.WriteLine("Skriv id på virksomheden du ønsker at søge på");
            int virksomhedsId = int.Parse(Console.ReadLine());

            //Deferred/Lazy
            IQueryable<Booking> bookingQuery = (from b in db.Bookings
                                            from bu in b.Bookudstyrs
                                            where b.Virksomhed.id == virksomhedsId
                                            && bu.Udstyr.navn == udstyrsNavn
                                            select b);
            Console.WriteLine("\nDeffered/Lasy");
            foreach (Booking b in bookingQuery) {
                Console.WriteLine(b.Lejer.navn + "; " + b.dato);
            }

            //Immediate/Eager
            List<Booking> bookingList = (from b in db.Bookings
                                            from bu in b.Bookudstyrs
                                            where b.Virksomhed.id == virksomhedsId
                                            && bu.Udstyr.navn == udstyrsNavn
                                            select b).Include(b => b.Lejer).ToList();

            Console.WriteLine("\nImmediate/Eager");
            foreach (Booking b in bookingList) {
                Console.WriteLine(b.Lejer.navn + "; " + b.dato);
            }
            Console.ReadLine();
        }
    }
}
