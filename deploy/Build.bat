REM MSBuild EXE path
SET MSBuildPath="C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe"
SET NuGetPath="C:\Program Files (x86)\NuGet\nuget.exe"

ECHO Did you adjust the version number (if this is a release)? Press ENTER to continue...
PAUSE

REM change to the source root directory
pushd ..


REM ======================= clean =======================================

REM ensure any previously created package is deleted
del deploy\*.nupkg

REM ======================= build =======================================


REM build Any CPU
%MSBuildPath% libsignalservice-uwp\libsignalservice-uwp.csproj /property:Configuration=Release /property:Platform="AnyCPU"

REM create NuGet package
pushd deploy
%NuGetPath% pack ..\libsignalservice-uwp\libsignalservice-uwp.nuspec -Prop Configuration=Release -outputdirectory .
popd


REM ============================ done ==================================


REM go back to the build dir
popd
