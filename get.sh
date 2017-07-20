mv /home/jjxx_out/*.zip /home/inspect/ftp/get/
mv /home/jjxx_out/*.txt /home/inspect/ftp/get/
cd /home/inspect/ftp/get ;
unzip -o  *.zip
chown inspect /home/inspect/ftp/get/* -R
chgrp inspect /home/inspect/ftp/get/* -R
mv /home/inspect/ftp/get/photo/* /home/inspect/photo/
mv /home/inspect/ftp/get/*.zip /home/inspect/ftp/get/back
