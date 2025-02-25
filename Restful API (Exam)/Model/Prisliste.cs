namespace EksamensOpgaveCS.Model {
	internal class Prisliste {
		public int Id {get; set;}
		public float PrisBil {get; set;}
		public float PrisGæst {get; set;}

		public Prisliste() {}
		public Prisliste(int id, float prisBil, float prisGæst) {
			Id = id;
			PrisBil = prisBil;
			PrisGæst = prisGæst;
		}
	}
}
