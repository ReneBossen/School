using System.ComponentModel.DataAnnotations;

namespace EksamensOpgaveCS.Model {
	internal class Bil {
		[Key]public string Nummerplade {get; set;}
		public string Mærke {get; set;}
		public string Farve {get; set;}
		public int MaxPladser {get; set;}
		public List<Gæst> Passagere {get; set;}
        public int FærgeId {get; set;} // Foreign key
		public Færge Færge {get; set;} // Navigation property

        public Bil() {
            Passagere = new();
        }
		public Bil(string nummerplade, string mærke, string farve, int maxPladser, int færgeId) {
			Nummerplade = nummerplade;
			Mærke = mærke;
			Farve = farve;
			MaxPladser = maxPladser;
			FærgeId = færgeId;
			Passagere = new();
		}

		//Fungerer kun hvis der er ledige pladser i bilen
		//Ellers kast exception
		public void TilføjPassager(Gæst gæst) {
			Passagere.Add(gæst);
		}
	}
}
