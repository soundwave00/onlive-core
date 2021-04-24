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
- Verificare processi jamulus aperti con: ps aux | grep jamulus

##### Service
- sudo adduser --system --no-create-home jamulus
- Spostare cartella root/jamulus/ in /usr/local/bin/jamulus/
- Creare script etc/systemed/system/jamulus.service
	- Script nel repo: jamulus-server/jamulus.service
- sudo chmod 644 /etc/systemd/system/jamulus.service
- Avvio: sudo systemctl start jamulus
- Stop: sudo systemctl stop jamulus
