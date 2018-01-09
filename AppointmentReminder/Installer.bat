@ECHO OFF

REM The following directory is for .NET 2.0
REM This must be run as Administator 

set DOTNETFX2=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319
set PATH=%PATH%;%DOTNETFX2%

sc delete AppReminder
InstallUtil /i "C:\Users\Dariel\Documents\Visual Studio 2015\Projects\_GIT\AppointmentReminder\bin\Release\AppointmentReminder.exe"
sc start AppReminder

pause