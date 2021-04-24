onlive-core
==========
Tesi, creazione di una WebApp per la fruizione online di concerti a distanza.

---

.NET SDK
--------
##### Dipendenze
- wget https://packages.microsoft.com/config/ubuntu/20.10/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
- sudo dpkg -i packages-microsoft-prod.deb

##### Installazione
- sudo apt update
- sudo apt install -y apt-transport-https
- sudo apt update
- sudo apt install -y dotnet-sdk-3.1

Database
--------
##### Installazione
- Installare MySql (guida da scrivere)

##### Configurazione
- Eseguire query nel repo: db/create.sql
- Seguire guida nel repo: db/EntityRelationship.md

##### Info
- Server: localhost
- Database: onlive
- Uid: root
- Password: root

Jamulus Server
--------------
##### Configurazione
- Seguire guida nel repo: jamulus-server/JamulusServer.md
