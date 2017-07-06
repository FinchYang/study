mv /home/jjxx_out/* /home/inspect/ftp/get/
cd /home/inspect/ftp/get ;
unzip -o  *.zip
chown inspect /home/inspect/ftp/get/* -R
chgrp inspect /home/inspect/ftp/get/* -R
mv /home/inspect/ftp/get/photo/* /home/inspect/photo/
