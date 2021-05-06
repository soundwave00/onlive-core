Entity Relationship
-------------------
##### Configurazione
- Entrare nella cartella del progetto
- Creazione modello:
	- dotnet ef dbcontext scaffold "SERVER=localhost;DATABASE=ONSTAGE;UID=root;PASSWORD=root;" MySql.Data.EntityFrameworkCore -o DbModels
- Aggiornamento modello
	- dotnet ef dbcontext scaffold "SERVER=localhost;DATABASE=ONSTAGE;UID=root;PASSWORD=root;" MySql.Data.EntityFrameworkCore -o DbModels -f

##### Note
- Le colonne di tipo DATETIME non vengono gestite, da aggiungere a mano.

###### Esempio
- Aggiungere le date nel modello
	- public DateTime DateSet { get; set; }
	- public DateTime? DateStart { get; set; }

- Aggiungere le date nel contesto
	- entity.Property(e => e.DateSet)
		.IsRequired()
		.HasColumnName("DATE_SET")
		.HasColumnType("datetime");

	- entity.Property(e => e.DateStart)
		.HasColumnName("DATE_START")
		.HasColumnType("datetime");
