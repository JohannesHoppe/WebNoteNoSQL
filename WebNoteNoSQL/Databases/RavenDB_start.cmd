@echo off

set DBPATH=c:\data
IF EXIST %DBPATH% (echo .) ELSE (mkdir %DBPATH%)

..\packages\RavenDB.Server.2.0.2330\tools\Raven.Server.exe

pause