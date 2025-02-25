using EksamensOpgaveCS.Model;
using Microsoft.EntityFrameworkCore;

namespace Data {
	internal class Database : DbContext {
        public Database() {
			Database.EnsureCreated();
		}
		public DbSet<Bil> Biler  {get; set;}
		public DbSet<Færge> Færger  {get; set;}
		public DbSet<Gæst> Gæster  {get; set;}
		public DbSet<Prisliste> Prislister {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            // Define composite key for Bil
            modelBuilder.Entity<Bil>(entity => {
                entity.HasKey(b => b.Nummerplade);

                entity.HasMany(b => b.Passagere)
                    .WithOne(g => g.Bil)
                    .HasForeignKey(g => g.BilNummerplade);
            });

            modelBuilder.Entity<Gæst>(entity =>
            {
                entity.HasKey(g => g.Cpr);
                entity.Property(g => g.Cpr)
                      .ValueGeneratedNever(); // Angiver, at CprNumber ikke er en identitetskolonne
            });


            // Define one-to-many relationship between Færge og Bil
            modelBuilder.Entity<Færge>()
                .HasMany(f => f.Biler)
                .WithOne(b => b.Færge)
                .HasForeignKey(b => b.FærgeId);



            // Seed Prisliste
            Prisliste prisliste1 = new Prisliste(1, 197, 99);
            Prisliste prisliste2 = new Prisliste(2, 220, 127);
            Prisliste prisliste3 = new Prisliste(3, 178, 90);
            modelBuilder.Entity<Prisliste>().HasData(
                prisliste1, prisliste2, prisliste3
            );

            // Seed Færger
            var færger = new List<Færge> {
                new Færge(1, "BefriKatten", prisliste1.Id, 10, 66, 10, 25),
                new Færge(2, "Den Syngende Svane", prisliste2.Id, 10, 120, 10, 48),
                new Færge(3, "Skipper Skræks Drøm", prisliste3.Id, 10, 40, 10, 10),
            };
            modelBuilder.Entity<Færge>().HasData(færger);

            // Seed Biler with foreign keys
            var biler = new List<Bil> {
                new Bil("HD127662", "Peugeot", "Grå", 5, færger[1].Id),
                new Bil("BG727340", "Volvo", "Blå", 5, færger[1].Id),
                new Bil("HH191284", "VW", "Sort", 4, færger[1].Id),
                new Bil("OA834583", "Ferrari", "Gul", 2, færger[2].Id),
                new Bil("AZ759271", "BMW", "Grøn", 4, færger[0].Id),
                new Bil("XY123456", "Audi", "Hvid", 2, færger[1].Id),
                new Bil("KL789012", "Mercedes", "Sølv", 5, færger[1].Id),
                new Bil("MN345678", "Toyota", "Rød", 5, færger[1].Id),
                new Bil("PQ901234", "Honda", "Blå", 4, færger[2].Id),
                new Bil("RS567890", "Ford", "Sort", 4, færger[2].Id),
                new Bil("TU345678", "Chevrolet", "Grøn", 2, færger[0].Id),
                new Bil("VW901234", "Tesla", "Grå", 2, færger[0].Id),
                new Bil("XY567890", "Nissan", "Hvid", 5, færger[0].Id),
                new Bil("AB123456", "Mazda", "Blå", 5, færger[1].Id),
                new Bil("CD789012", "Hyundai", "Rød", 4, færger[1].Id),
            };

            modelBuilder.Entity<Bil>().HasData(biler);


            var gæster = new List<Gæst>
           {
                new Gæst(10000001, true, "Hans Erik", "Mand", 30, nummerplade: biler[1].Nummerplade),
                new Gæst(10000002, false, "Anne Marie", "Kvinde", 25, nummerplade: biler[1].Nummerplade),
                new Gæst(10000003, false, "Jens Peter", "Mand", 40, færgeId: færger[1].Id),
                new Gæst(10000004, true, "Birgitte", "Kvinde", 35, nummerplade: biler[2].Nummerplade),
                new Gæst(10000005, false, "John Doe", "Mand", 50, nummerplade: biler[2].Nummerplade),
                new Gæst(10000006, false, "Jane Doe", "Kvinde", 45, nummerplade: biler[2].Nummerplade),
                new Gæst(10000007, true, "Lars Larsen", "Mand", 28, nummerplade: biler[3].Nummerplade),
                new Gæst(10000008, false, "Mette Madsen", "Kvinde", 32, nummerplade: biler[3].Nummerplade),
                new Gæst(10000009, false, "Poul Pedersen", "Mand", 55, nummerplade: biler[1].Nummerplade),
                new Gæst(10000010, true, "Karin Kristensen", "Kvinde", 40, nummerplade: biler[4].Nummerplade),
                new Gæst(10000011, false, "Søren Sørensen", "Mand", 36, nummerplade: biler[4].Nummerplade),
                new Gæst(10000012, false, "Pia Hansen", "Kvinde", 29, færgeId: færger[1].Id),
                new Gæst(10000013, true, "Ole Olsen", "Mand", 45, nummerplade: biler[5].Nummerplade),
                new Gæst(10000014, false, "Anna Andersen", "Kvinde", 22, færgeId: færger[0].Id),
                new Gæst(10000015, false, "Bjørn Berg", "Mand", 37, færgeId: færger[0].Id),
                new Gæst(10000016, false, "Clara Clausen", "Kvinde", 31, færgeId: færger[1].Id),
                new Gæst(10000017, false, "David Davidsen", "Mand", 29, færgeId: færger[1].Id),
                new Gæst(10000018, true, "Eva Eriksen", "Kvinde", 27, nummerplade: biler[6].Nummerplade),
                new Gæst(10000019, false, "Frank Frederiksen", "Mand", 52, nummerplade: biler[6].Nummerplade),
                new Gæst(10000020, false, "Greta Gregersen", "Kvinde", 24, færgeId: færger[2].Id),
                new Gæst(10000021, false, "Henrik Hansen", "Mand", 39, færgeId: færger[2].Id),
                new Gæst(10000022, false, "Iben Iversen", "Kvinde", 26, færgeId: færger[0].Id),
                new Gæst(10000023, true, "Johan Jensen", "Mand", 35, nummerplade: biler[7].Nummerplade),
                new Gæst(10000024, false, "Katrine Kristensen", "Kvinde", 33, færgeId: færger[0].Id),
                new Gæst(10000025, false, "Lars Larsen", "Mand", 40, færgeId: færger[1].Id),
                new Gæst(10000026, false, "Mona Mortensen", "Kvinde", 29, færgeId: færger[1].Id),
                new Gæst(10000027, false, "Niels Nielsen", "Mand", 34, færgeId: færger[2].Id),
                new Gæst(10000028, true, "Oscar Olesen", "Mand", 38, nummerplade: biler[8].Nummerplade),
                new Gæst(10000029, false, "Pernille Petersen", "Kvinde", 30, færgeId: færger[2].Id),
                new Gæst(10000030, false, "Rasmus Rasmussen", "Mand", 32, færgeId: færger[0].Id),
                new Gæst(10000031, false, "Signe Sørensen", "Kvinde", 28, færgeId: færger[0].Id),
                new Gæst(10000032, true, "Thomas Thomsen", "Mand", 42, nummerplade: biler[9].Nummerplade),
                new Gæst(10000033, false, "Ulla Ulriksen", "Kvinde", 25, færgeId: færger[1].Id),
                new Gæst(10000034, false, "Viggo Villadsen", "Mand", 47, færgeId: færger[1].Id),
                new Gæst(10000035, false, "Winnie Winther", "Kvinde", 29, færgeId: færger[2].Id),
                new Gæst(10000036, true, "Xander Xensen", "Mand", 36, nummerplade: biler[10].Nummerplade),
                new Gæst(10000037, false, "Yvonne Ydergård", "Kvinde", 34, færgeId: færger[2].Id),
                new Gæst(10000038, false, "Zacharias Ziegler", "Mand", 45, færgeId: færger[0].Id),
                new Gæst(10000039, true, "Alice Andersen", "Kvinde", 39, nummerplade: biler[11].Nummerplade),
                new Gæst(10000040, false, "Brian Bertelsen", "Mand", 31, færgeId: færger[0].Id),
                new Gæst(10000041, false, "Cecilie Carlsen", "Kvinde", 28, færgeId: færger[1].Id),
                new Gæst(10000042, false, "Dennis Davidsen", "Mand", 32, færgeId: færger[1].Id),
                new Gæst(10000043, true, "Emil Eriksen", "Mand", 26, nummerplade: biler[12].Nummerplade),
                new Gæst(10000044, false, "Freja Frederiksen", "Kvinde", 33, færgeId: færger[2].Id),
                new Gæst(10000045, false, "Gustav Gregersen", "Mand", 37, færgeId: færger[2].Id),
                new Gæst(10000046, false, "Hannah Hansen", "Kvinde", 29, færgeId: færger[0].Id),
                new Gæst(10000047, true, "Ivan Iversen", "Mand", 34, nummerplade: biler[13].Nummerplade),
                new Gæst(10000048, false, "Julie Jensen", "Kvinde", 27, færgeId: færger[0].Id),
                new Gæst(10000049, false, "Kasper Kristensen", "Mand", 31, færgeId: færger[1].Id),
                new Gæst(10000050, false, "Louise Larsen", "Kvinde", 35, færgeId: færger[1].Id),
                new Gæst(10000051, true, "Mads Mortensen", "Mand", 38, nummerplade: biler[14].Nummerplade),
                new Gæst(10000052, false, "Nina Nielsen", "Kvinde", 32, færgeId: færger[2].Id),
                new Gæst(10000053, false, "Oliver Olesen", "Mand", 36, færgeId: færger[2].Id),
                new Gæst(10000054, false, "Pia Petersen", "Kvinde", 29, færgeId: færger[0].Id),
                new Gæst(10000055, true, "Rasmus Rasmussen", "Mand", 34, nummerplade: biler[0].Nummerplade),
                new Gæst(10000056, false, "Sara Sørensen", "Kvinde", 28, færgeId: færger[1].Id),
                new Gæst(10000057, false, "Thomas Thomsen", "Mand", 32, færgeId: færger[1].Id),
                new Gæst(10000058, false, "Ursula Ulriksen", "Kvinde", 26, færgeId: færger[2].Id),
                new Gæst(10000059, true, "Victor Villadsen", "Mand", 41, nummerplade: biler[1].Nummerplade),
                new Gæst(10000060, false, "Winnie Winther", "Kvinde", 34, færgeId: færger[2].Id),
                new Gæst(10000061, false, "Xenia Xensen", "Kvinde", 30, færgeId: færger[0].Id),
                new Gæst(10000062, false, "Yannik Ydergård", "Mand", 28, færgeId: færger[0].Id),
                new Gæst(10000063, true, "Zara Ziegler", "Kvinde", 36, nummerplade: biler[2].Nummerplade),
                new Gæst(10000064, false, "Arne Andersen", "Mand", 32, færgeId: færger[1].Id),
                new Gæst(10000065, false, "Bente Bertelsen", "Kvinde", 29, færgeId: færger[1].Id),
                new Gæst(10000066, false, "Carl Carlsen", "Mand", 38, færgeId: færger[2].Id),
                new Gæst(10000067, true, "Dorte Davidsen", "Kvinde", 33, nummerplade: biler[3].Nummerplade),
                new Gæst(10000068, false, "Erik Eriksen", "Mand", 31, færgeId: færger[0].Id),
                new Gæst(10000069, false, "Fie Frederiksen", "Kvinde", 27, færgeId: færger[0].Id),
                new Gæst(10000070, false, "Gunnar Gregersen", "Mand", 35, færgeId: færger[1].Id),
            };



            //Tilknyt gæsternes FærgeId baseret på deres BilNummerplade
            foreach (var gæst in gæster) {
                if (gæst.BilNummerplade != null) {
                    gæst.FærgeId = biler.FirstOrDefault(b => b.Nummerplade == gæst.BilNummerplade)?.FærgeId;
                }
            }

            modelBuilder.Entity<Gæst>().HasData(gæster);

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            //optionsBuilder.LogTo(message => Debug.WriteLine(message));
			optionsBuilder.UseSqlServer(
				"Data Source=RENES_BAERBAR\\SQLEXPRESS;" +
				"Initial Catalog = EksamensopgaveCSRene;" +
				"user id=sa;" +
				"password=renebossen123;" +
				"Integrated Security = SSPI;" +
				"TrustServerCertificate = true"
				);
		}
	}
}
