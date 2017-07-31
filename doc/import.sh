cd /home/inspect/import;
log=/home/inspect/log/`date -Ihours`.import
/usr/local/bin/dotnet importdata.dll 2>>$log 1>>$log 0>>$log 
/home/inspect/bin/sms.sh 2>>$log 1>>$log 0>>$log 
