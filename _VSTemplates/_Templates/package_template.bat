@echo off

cd %1

svn export . ..\temp --force

cd ..\temp

winrar.exe a ..\%1.zip -r -afzip
cd..

rd temp /S /Q