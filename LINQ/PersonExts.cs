using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise5
{
    internal static class PersonExts
    {
        public static void SetAccepted(this List<Person> persons, Predicate<Person> cond)
        {
            persons.FindAll(cond).ForEach(p=>p.Accepted = true);
        }
        public static void Reset(this List<Person> persons)
        {
            persons.ForEach(p => p.Accepted = false);
        }
    }
}
