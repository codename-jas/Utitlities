# Write some fancy output
$artText = @"
(                                                                  
    )\ )    )                               )     (     )              
   (()/( ( /(                (       (   ( /( (   )\ ( /( (     (      
    /(_)))\())  (    (      ))\      )\  )\()))\ ((_))\()))\   ))\ (   
   (_)) ((_)\   )\   )\ )  /((_)  _ ((_)(_))/((_) _ (_))/((_) /((_))\  
   | _ \| |(_) ((_) _(_/( (_))   | | | || |_  (_)| || |_  (_)(_)) ((_) 
   |  _/| ' \ / _ \| ' \))/ -_)  | |_| ||  _| | || ||  _| | |/ -_)(_-< 
   |_|  |_||_|\___/|_||_| \___|   \___/  \__| |_||_| \__| |_|\___|/__/ 
                                                                       
"@
Write-Host $artText
$myLocation = $PSScriptRoot
Push-Location $myLocation
$slnPath = "../../src/Phone"
Write-Host "Executing in $myLocation"
Push-Location $slnPath
Write-Host "Restoring packages for Phone.sln"
& dotnet clean
& dotnet restore
& dotnet build
