
$file= $MyInvocation.MyCommand.Definition
$parent = Split-Path $file
copy  "$parent\ZhuoYue.Components.Core.dll" C:\Github\SSAB.Framework\SourceCode\SSAB.Framework\Refs\
copy  "$parent\ZhuoYue.Components.Core.dll" C:\Github\SSAB.APAC.TDS\SourceCode\Refs\

