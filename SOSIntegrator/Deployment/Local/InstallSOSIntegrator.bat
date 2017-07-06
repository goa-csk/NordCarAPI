ECHO OFF
ECHO Installing SOS Reservation service updater
PAUSE
sc create SOSIntegrator binpath= "C:\projects\EuropCar\NCWebApi\SOSIntegrator\bin\Release\sosintegrator.exe" DisplayName= "SOSIntegrator" start= delayed-auto
PAUSE
