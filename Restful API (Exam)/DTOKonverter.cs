namespace Data {
	internal static class DTOKonverter {
		//Denne klasse står for at konveretere objekter fra Datalaget til DTO objekter og omvendt.
		//Der er ikke så mange kommentare da alle metoder giver sig selv.

		#region Data_Til_DTO Konvertere
		public static EksamensOpgaveCS.DTO.Færge Data_Til_DTO_Færge(EksamensOpgaveCS.Model.Færge modelFærge)
		{
			//Konverter alle biler fra Data til DTO
			List<EksamensOpgaveCS.DTO.Bil> DTOBiler = new();
			modelFærge.Biler?.ForEach(b => DTOBiler.Add(Data_Til_DTO_Bil(b)));
			//Konverter alle gæster fra Data til DTO
			List<EksamensOpgaveCS.DTO.Gæst> DTOGæster = new();
			modelFærge.Gæster?.ForEach(g => DTOGæster.Add(Data_Til_DTO_Gæst(g)));

			EksamensOpgaveCS.DTO.Færge DTOFærge = new();
			DTOFærge.Id = modelFærge.Id;
			DTOFærge.Navn = modelFærge.Navn;
			DTOFærge.PrislisteId = modelFærge.PrislisteId;
			DTOFærge.MaksAntalGæster = modelFærge.MaxAntalGæster;
			DTOFærge.MaksAntalBiler = modelFærge.MaxAntalBiler;
			DTOFærge.Prisliste = modelFærge.Prisliste == null ? null : Data_Til_DTO_Prisliste(modelFærge.Prisliste);
			DTOFærge.Gæster = DTOGæster;
			DTOFærge.Biler = DTOBiler;

			return DTOFærge;
		}
		public static EksamensOpgaveCS.DTO.Bil Data_Til_DTO_Bil(EksamensOpgaveCS.Model.Bil modelBil) {
			//Konverter alle passagere fra Data til DTO
			List<EksamensOpgaveCS.DTO.Gæst> DTOPassagere = new();
			EksamensOpgaveCS.DTO.Bil DTOBil = new();
			modelBil.Passagere?.ForEach(p => DTOPassagere.Add(Data_Til_DTO_Gæst(p)));

			DTOBil.Nummerplade = modelBil.Nummerplade;
			DTOBil.Mærke = modelBil.Mærke;
			DTOBil.Farve = modelBil.Farve;
			DTOBil.MaksPladser = modelBil.MaxPladser;
			DTOBil.FærgeId = modelBil.FærgeId;
			DTOBil.Passagere = DTOPassagere;
            DTOBil.FærgeId = modelBil.FærgeId;

            return DTOBil;
		}
		public static EksamensOpgaveCS.DTO.Gæst Data_Til_DTO_Gæst(EksamensOpgaveCS.Model.Gæst modelGæst) {
			EksamensOpgaveCS.DTO.Gæst DTOGæst = new();
			DTOGæst.Cpr = modelGæst.Cpr;
			DTOGæst.Fører = modelGæst.Fører;
			DTOGæst.Navn = modelGæst.Navn;
			DTOGæst.Køn = modelGæst.Køn;
			DTOGæst.Alder = modelGæst.Alder;
            if (modelGæst.BilNummerplade != null) {
                DTOGæst.BilNummerplade = modelGæst.BilNummerplade;
            }
            if (modelGæst.FærgeId != null) {
                DTOGæst.FærgeId = modelGæst.FærgeId;
            }
            return DTOGæst;
		}
		public static EksamensOpgaveCS.DTO.Prisliste Data_Til_DTO_Prisliste(EksamensOpgaveCS.Model.Prisliste modelPrisliste) {
			EksamensOpgaveCS.DTO.Prisliste DTOPrisliste = new();
			DTOPrisliste.Id = modelPrisliste.Id;
			DTOPrisliste.PrisBil = modelPrisliste.PrisBil;
			DTOPrisliste.PrisGæst = modelPrisliste.PrisGæst;
			return DTOPrisliste;
		}
		#endregion

		#region DTO_Til_Data Konvertere
		public static EksamensOpgaveCS.Model.Færge DTO_Til_Data_Færge(EksamensOpgaveCS.DTO.Færge DTOFærge) {
			//Konverter alle biler fra DTO til Data
			List<EksamensOpgaveCS.Model.Bil> DataBiler = new();
			DTOFærge.Biler?.ForEach(b => DataBiler.Add(DTO_Til_Data_Bil(b)));
			//Konverter alle gæster fra Data til DTO
			List<EksamensOpgaveCS.Model.Gæst> DataGæster = new();
			DTOFærge.Gæster?.ForEach(g => DataGæster.Add(DTO_Til_Data_Gæst(g)));

			EksamensOpgaveCS.Model.Færge dataFærge = new();
			dataFærge.Id = DTOFærge.Id;
			dataFærge.Navn = DTOFærge.Navn;
			dataFærge.PrislisteId = DTOFærge.PrislisteId;
			dataFærge.MaxAntalGæster = DTOFærge.MaksAntalGæster;
			dataFærge.MaxAntalBiler = DTOFærge.MaksAntalBiler;
			dataFærge.Gæster = DataGæster;
			dataFærge.Biler = DataBiler;

			return dataFærge;
		}
		public static EksamensOpgaveCS.Model.Bil DTO_Til_Data_Bil(EksamensOpgaveCS.DTO.Bil DTOBil) {
			//Konverter alle passagere fra DTO til Data
			List<EksamensOpgaveCS.Model.Gæst> DataPassagere = new();
			EksamensOpgaveCS.Model.Bil dataBil = new();
			DTOBil.Passagere?.ForEach(p => DataPassagere.Add(DTO_Til_Data_Gæst(p)));

			dataBil.Nummerplade = DTOBil.Nummerplade;
			dataBil.Mærke = DTOBil.Mærke;
			dataBil.Farve = DTOBil.Farve;
			dataBil.MaxPladser = DTOBil.MaksPladser;
			dataBil.Passagere = DataPassagere;
			dataBil.FærgeId = DTOBil.FærgeId;

			return dataBil;
		}
		public static EksamensOpgaveCS.Model.Gæst DTO_Til_Data_Gæst(EksamensOpgaveCS.DTO.Gæst DTOGæst) {
			EksamensOpgaveCS.Model.Gæst dataGæst = new();
			dataGæst.Cpr = DTOGæst.Cpr;
			dataGæst.Fører = DTOGæst.Fører;
			dataGæst.Navn = DTOGæst.Navn;
			dataGæst.Køn = DTOGæst.Køn;
			dataGæst.Alder = DTOGæst.Alder;
			if (DTOGæst.BilNummerplade != null) {
				dataGæst.BilNummerplade = DTOGæst.BilNummerplade;
			}
			if (DTOGæst.FærgeId != null) {
				dataGæst.FærgeId = DTOGæst.FærgeId;
			}
            return dataGæst;
		}
		public static EksamensOpgaveCS.Model.Prisliste DTO_Til_Data_Prisliste(EksamensOpgaveCS.DTO.Prisliste DTOPrisliste) {
			EksamensOpgaveCS.Model.Prisliste dataPrisliste = new();
			dataPrisliste.Id = DTOPrisliste.Id;
			dataPrisliste.PrisBil = DTOPrisliste.PrisBil;
			dataPrisliste.PrisGæst = DTOPrisliste.PrisGæst;
			return dataPrisliste;
		}
		#endregion
	}
}
