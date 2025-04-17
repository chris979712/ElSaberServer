@echo off
cd /d %~dp0
sqlcmd -S LAPTOP-JLEQRMT1\SQLEXPRESS -d ElSaberDB -U AdminElSaber -P 123456 -i "actualizar_multas.sql"
