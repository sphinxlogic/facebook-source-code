@echo off

del *.zip
del *.vsi

for /d %%t in (ASP.NET*) do call package_template.bat "%%t"

copy *.zip ..\Facebook.Templates.Projects /Y

cd ..\Facebook.Templates.Projects

winrar a ..\_Templates\".NET Facebook API Client Templates.zip" -r -afzip

cd ..\_Templates

ren ".NET Facebook API Client Templates.zip" ".NET Facebook API Client Templates.vsi"