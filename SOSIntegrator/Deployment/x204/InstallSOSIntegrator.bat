ECHO OFF
ECHO Installing SOS Reservation service updater on x204
PAUSE
sc create SOSIntegrator binpath= "C:\Goapplicate\SOSIntegrator\sosintegrator.exe" DisplayName= "SOSIntegrator" start= delayed-auto
PAUSE
