@echo off

set DBPATH=c:\data
set LOCKFILE=%DBPATH%\db\mongod.lock

@rem By default MongoDB will store data in \data\db, but it won't automatically create that folder
IF EXIST %DBPATH% (echo .) ELSE (mkdir %DBPATH%)
IF EXIST %DBPATH%\db (echo .) ELSE (mkdir %DBPATH%\db)

@rem MongoDB refuses to work after an unclean shutdown
IF EXIST %LOCKFILE% del %LOCKFILE%

@rem Start!
MongoDB\bin\mongod.exe --dbpath %DBPATH%\db --port 27017

pause