%~d0
CD "%~dp0"

.\..\..\..\Databases\MongoDB\bin\mongoimport --type csv --drop --db logs -collection logs --headerline --file footbag-shop.de-2013-06-22.csv