@echo on
title Run all Tests!
echo AutoFrame!
CD "C:\Subrata\Subrata\FSE_260153\01_Middle_Tier_Layer\ProjectManager\packages\NUnit.ConsoleRunner.3.10.0\tools"
start nunit3-console.exe
nunit3-console ..\..\..\ProjectManager.Tests\bin\Debug\ProjectManager.Tests.dll

