onlive-core
==========
Tesi, creazione di una WebApp per la fruizione online di concerti a distanza

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

Jamulus Server
--------------
##### Dipendenze
- sudo apt install -y qt5-qmake
- sudo apt install qt5-default
- sudo apt install build-essential
- sudo apt install qttools5-dev-tools
- sudo apt install ffmpeg

##### Installazione
- git clone -b streamer3 https://github.com/dingodoppelt/jamulus
- cd jamulus
- qmake "CONFIG+=nosound headless" Jamulus.pro
- make clean
- make

##### Icecast
- Guida: https://mediarealm.com.au/articles/icecast-hosting-setup-guide/
- Aggiungere ENABLE=true alla fine del file /etc/icecast2/icecast.xml

##### Avvio
- Entrare nella cartella jamulus
- ./Jamulus -s -n -F -T --streamto "-f mp3 icecast://source:root@localhost:80/stream"
	- Nel comando precedente sostituire root con il valore di source-password in /etc/icecast2/icecast.xml

##### Service
- sudo adduser --system --no-create-home jamulus
- Spostare cartella root/jamulus/ in /usr/local/bin/jamulus/
- Creare script etc/systemed/system/jamulus.service
	- Script nel repo: jamulus-server/jamulus.service
- sudo chmod 644 /etc/systemd/system/jamulus.service
- Avvio: sudo systemctl start jamulus
