namespace EksamensOpgaveCS.Model {
	internal class Færge {
		public int Id {get; set; }
		public string Navn {get;  set;}
		public int MinAntalGæster {get;  set;}
		public int MaxAntalGæster {get; set;}
		public int MinAntalBiler {get;  set;}
		public int MaxAntalBiler {get; set;}
		public int PrislisteId {get; set;}
		public Prisliste Prisliste {get; set;}
		public List<Gæst> Gæster {get; set;}
		public List<Bil> Biler {get; set;}
		
		public Færge() {
			Gæster = new();
			Biler = new();
		}
		public Færge(int id, string navn, int prislisteid, int minAntalGæster, int maxAntalGæster, int minAntalBiler, int maxAntalBiler) {
			Id = id;
			Navn = navn;
			PrislisteId = prislisteid;
			MinAntalGæster = minAntalGæster;
			MaxAntalGæster = maxAntalGæster;
			MinAntalBiler = minAntalBiler;
			MaxAntalBiler = maxAntalBiler;
			Gæster = new();
			Biler = new();
		}

		#region Tilføj/Fjern
		public void TilføjGæst(Gæst gæst) {
			Gæster.Add(gæst);
		}

		public void FjernGæst(Gæst gæst) {
			Gæster.Remove(gæst);
		}

		public void TilføjBil(Bil bil) {
			Biler.Add(bil);
		}

		public void FjernBil(Bil bil) {
			Biler.Remove(bil);
		}
		#endregion
	}
}
