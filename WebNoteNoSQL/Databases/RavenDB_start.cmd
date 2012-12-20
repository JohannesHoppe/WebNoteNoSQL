@echo off

set DBPATH=c:\data
IF EXIST %DBPATH% (echo .) ELSE (mkdir %DBPATH%)

..\packages\RavenDB.1.0.888\server\Raven.Server.exe

pause