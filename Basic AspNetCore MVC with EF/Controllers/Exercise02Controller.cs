using Microsoft.AspNetCore.Mvc;
using StartUp_CORE.Models;

namespace StartUp_CORE.Controllers {
    public class Exercise02Controller : Controller {
        public IActionResult Index() {

            Person person1 = new Person("Hans", "Jensen", "Hovedgade 1", "1234", "København", "45364363", new DateOnly(1999, 9, 14));
            Person person2 = new Person("Anna", "Andersen", "Bakkevej 5", "5678", "Aarhus", "32522232", new DateOnly(2001, 2, 14));
            Person person3 = new Person("Lars", "Pedersen", "Strandvejen 10", "9012", "Odense", "57875676", new DateOnly(1995, 9, 23));
            Person person4 = new Person("Maria", "Nielsen", "Skovgårdsvej 3", "3456", "Aalborg", "45642412", new DateOnly(2010, 4, 17));
            person1.AddPhoneNumber("12312312");

            List<Person> personer = [person1, person2, person3, person4];

            ViewBag.Personer = personer;
            return View();
        }
    }
}
