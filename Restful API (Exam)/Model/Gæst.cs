using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EksamensOpgaveCS.Model {
	internal class Gæst {
		[Key]public int Cpr {get; set;}
		public bool Fører {get; set;}
		public string Navn {get; set;}
		public string Køn {get; set;}
		public int Alder {get; set;}
		public string? BilNummerplade {get; set;}
        public Bil Bil {get;set;}
		public int? FærgeId {get; set;}
		public Færge Færge {get; set;}

		public Gæst() {
        }
		public Gæst (int cpr, bool fører, string navn, string køn, int alder, int? færgeId = null, string? nummerplade = null) {
			Cpr = cpr;
			Fører = fører;
			Navn = navn;
			Køn = køn;
			Alder = alder;
			FærgeId = færgeId;
            BilNummerplade = nummerplade;
        }

        public void AssignFærgeId(int? færgeId) {
            FærgeId = færgeId;
        }
    }
}
