rem delete existing
rmdir "ZipPackage" /Q /S

rem Create required folders
mkdir "ZipPackage"
mkdir "ZipPackage\data"
mkdir "ZipPackage\x64"
mkdir "ZipPackage\x86"

set "CONFIGURATION=bin\Release\net45"

rem Copy output files
xcopy "src\Jord.Game\%CONFIGURATION%\*.*" "ZipPackage\*.*" /s /e
copy "src\Jord.MapEditor\%CONFIGURATION%\Jord.MapEditor.exe" "ZipPackage" /Y
del "ZipPackage\data\Tilesets\ASCIIDroidSans\ASCIIDroidSans.bmfc"
del "ZipPackage\data\Tilesets\ASCIIDroidSans\ASCIIDroidSans.fnt"
del "ZipPackage\data\Tilesets\ASCIIDroidSans\DroidSans.ttf"