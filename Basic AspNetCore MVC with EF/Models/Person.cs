using System.Diagnostics.CodeAnalysis;

namespace StartUp_CORE.Models {
    public class Person {
        public string FirstName { get; set; }
        public string LastName {get; set;}
        public string Address {get; set;}
        public string Zip {get; set;}
        public string City {get; set;}
        private List<string> PhoneNumbers;
        private DateOnly birthDay;
        public DateOnly BirthDay {
            get { return birthDay; }
            set {
                if (CalcAge(birthDay) < 0 || CalcAge(birthDay) > 120) {
                    throw new Exception("Alder skal være mellem 0 og 120");
                }
                else {
                    birthDay = value;
                }
            }
        }
        public int Age {
            get; private set;
        }

        public Person(string firstnavn, string lastnavn, string address, string zip, string city, string phoneNumber, DateOnly birthDay) {
            FirstName = firstnavn;
            LastName = lastnavn;    
            Address = address;  
            Zip = zip;
            City = city;
            birthDay = birthDay;
            PhoneNumbers = new();
            PhoneNumbers.Add(phoneNumber);

            Age = CalcAge(birthDay);
        }

        private int CalcAge(DateOnly date) {
            TimeSpan age = DateTime.Now - date.ToDateTime(TimeOnly.MinValue);
            return age.Days / 365;
        }
        public void AddPhoneNumber(string phoneNumber) {
            PhoneNumbers.Add(phoneNumber);
        }

        public List<string> GetPhoneNumbers() {
            return PhoneNumbers;
        }
    }
}
