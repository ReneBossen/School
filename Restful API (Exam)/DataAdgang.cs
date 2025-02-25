using EksamensOpgaveCS.DTO;
using EksamensOpgaveCS.Model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Data {
	public static class DataAdgang {
		#region Opdater
		//Bliver ikke brugt, men er klar til at blive implementeret
		public static void OpdaterBil(EksamensOpgaveCS.DTO.Bil DataBil) 
			{
			using (Database db = new ())
			{
				EksamensOpgaveCS.Model.Bil bil = db.Biler.Find(DataBil.Nummerplade);
				bil.Mærke = DataBil.Mærke;
				bil.Farve = DataBil.Farve;
				bil.MaxPladser = DataBil.MaksPladser;
				db.SaveChanges();
			}
        }

		//Opdatere navn, prisliste og max gæster/biler på færgen
		public static void OpdaterFærge(EksamensOpgaveCS.DTO.Færge DTOFærge) 
		{
			using (Database db = new())
			{
				EksamensOpgaveCS.Model.Færge dataFærge = db.Færger.Find(DTOFærge.Id);

				dataFærge.Navn = DTOFærge.Navn;
				dataFærge.PrislisteId = DTOFærge.PrislisteId;
				dataFærge.MaxAntalGæster = DTOFærge.MaksAntalGæster;
				dataFærge.MaxAntalBiler = DTOFærge.MaksAntalBiler;
				db.SaveChanges();
			}
		}

		//Bliver ikke brugt, men er klar til at blive implementeret
		public static void OpdaterGæst(EksamensOpgaveCS.DTO.Gæst DataGæst) 
		{
			using (Database db = new())
			{
				EksamensOpgaveCS.Model.Gæst gæst = db.Gæster.Find(DataGæst.Cpr);
				gæst.Fører = DataGæst.Fører;
				gæst.Navn = DataGæst.Navn;
				gæst.Køn = DataGæst.Køn;
				gæst.Alder = DataGæst.Alder;
				db.SaveChanges();
			}
		}

		//Bliver ikke brugt men er klar til at blive implementeret
		public static void OpdaterPrisliste(EksamensOpgaveCS.DTO.Prisliste DataPrisliste) 
		{
			using (Database db = new())
			{
				EksamensOpgaveCS.Model.Prisliste prisliste = db.Prislister.Find(DataPrisliste.Id);
				prisliste.PrisBil = DataPrisliste.PrisBil;
				prisliste.PrisGæst = DataPrisliste.PrisGæst;
				db.SaveChanges();
			}
		}
		#endregion

		#region Hent
		//Færger
		//Henter alle færger i databasen
		public static List<EksamensOpgaveCS.DTO.Færge> HentFærger()
		{
			using (Database db = new())
			{
				List<EksamensOpgaveCS.Model.Færge> dataFærger = db.Færger.ToList();
				List<EksamensOpgaveCS.DTO.Færge> DTOFærger = new();
				dataFærger.ForEach(f => DTOFærger.Add(DTOKonverter.Data_Til_DTO_Færge(f)));
				return DTOFærger;
			} 
		}

		//Henter en færge samt alle dens gæster, biler og prislisten i databasen
		public static EksamensOpgaveCS.DTO.Færge HentFærge(int id)
		{
			using (Database db = new())
			{
				EksamensOpgaveCS.Model.Færge dataFærge = db.Færger.Include(f => f.Biler)
																	.Include(f => f.Gæster)
																	.Include(f => f.Prisliste)
																	.FirstOrDefault(f => f.Id == id);


				if (dataFærge == null) throw new Exception($"Færge med id '{id}' findes ikke");

                EksamensOpgaveCS.DTO.Færge DTOFærge = DTOKonverter.Data_Til_DTO_Færge(dataFærge);

				return DTOFærge;
			}
		}

		//Biler
		//Henter en bil og alle dens passagere i databasen ud fra bilens nummerplade
		public static EksamensOpgaveCS.DTO.Bil HentBil(string nummerplade)
		{
			using (Database db = new()) 
			{
                EksamensOpgaveCS.Model.Bil dataBil = db.Biler.Include(b => b.Passagere)
                                                                    .FirstOrDefault(b => b.Nummerplade == nummerplade);
                if (dataBil == null) throw new Exception($"Bil med nummerplade '{nummerplade}' findes ikke");

				EksamensOpgaveCS.DTO.Bil DTOBil = DTOKonverter.Data_Til_DTO_Bil(dataBil);

				return DTOBil;
			}
		}

		//Henter alle biler i databasen uden passagere
        public static List<EksamensOpgaveCS.DTO.Bil> HentBiler() {
            using(Database db = new()) {
                List<EksamensOpgaveCS.Model.Bil> dataBiler = db.Biler.ToList();
                List<EksamensOpgaveCS.DTO.Bil> DTOBiler = new();
                dataBiler.ForEach(b => DTOBiler.Add(DTOKonverter.Data_Til_DTO_Bil(b)));
                return DTOBiler;
            }
        }

        //Gæster
		//Henter alle gæster i en bil (Passagere inkl. fører)
		public static EksamensOpgaveCS.DTO.Bil HentGæsterIBil(string nummerplade) {
            using (Database db = new()) {
                EksamensOpgaveCS.Model.Bil dataBil = db.Biler.Include(b => b.Passagere)
															.FirstOrDefault(b => b.Nummerplade == nummerplade);

                EksamensOpgaveCS.DTO.Bil DTOBil = DTOKonverter.Data_Til_DTO_Bil(dataBil);
                return DTOBil;
            }
        }
		#endregion

		#region Opret
		//Opretter en færge i databasen
		public static void OpretFærge(EksamensOpgaveCS.DTO.Færge DTOFærge) {
			using (Database db = new())
			{
				EksamensOpgaveCS.Model.Færge dataFærge = DTOKonverter.DTO_Til_Data_Færge(DTOFærge);

				db.Færger.Add(dataFærge);
				db.SaveChanges();
			}
		}

		//Bliver ikke brugt, men er klar til at bliver implementeret
		public static void OpretPrisliste(EksamensOpgaveCS.DTO.Prisliste DTOPrisliste) {
			using (Database db = new()) {
				EksamensOpgaveCS.Model.Prisliste dataPrisliste = DTOKonverter.DTO_Til_Data_Prisliste(DTOPrisliste);

				db.Prislister.Add(dataPrisliste);
				db.SaveChanges();
			}
		}

		//Opretter en bil i databasen og tilknytter den færgen
		public static void OpretBil(EksamensOpgaveCS.DTO.Bil DTOBil) {
			using (Database db = new()) {
				EksamensOpgaveCS.Model.Bil dataBil = DTOKonverter.DTO_Til_Data_Bil(DTOBil);

				db.Biler.Add(dataBil);
				db.SaveChanges();
			}
		}

		//Opretter en gæst på en færge, i databasen
		public static void OpretGæstPåFærge(EksamensOpgaveCS.DTO.Gæst DTOGæst) {
			using (Database db = new()) {
                EksamensOpgaveCS.Model.Gæst dataGæst = DTOKonverter.DTO_Til_Data_Gæst(DTOGæst);

                db.Gæster.Add(dataGæst);
                db.SaveChanges();
            }
		}
		#endregion

		#region Fjern
		//Sletter en bil og alle dens passagere i databasen
        public static void FjernBil(string nummerplade) {
            using (Database db = new Database()) {
                EksamensOpgaveCS.Model.Bil bil = db.Biler.Include(b => b.Passagere).FirstOrDefault(b => b.Nummerplade == nummerplade);
                if (bil != null) {
                    // Fjern alle gæster som er tilknyttet bilen
                    foreach (var gæst in bil.Passagere.ToList()) {
                        db.Gæster.Remove(gæst);
                    }

                    db.Biler.Remove(bil);
                    db.SaveChanges();
                }
            }
        }

		//Sletter en gæst i databasen
        public static void FjernGæst(int cpr) {
            using (Database db = new()) {
                EksamensOpgaveCS.Model.Gæst gæst = db.Gæster.FirstOrDefault(g => g.Cpr == cpr);
                if (gæst != null) {
                    db.Gæster.Remove(gæst);
                    db.SaveChanges();
                }
            }
        }
		#endregion
    }
}
