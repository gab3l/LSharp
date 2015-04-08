SET path=C:\Riot Games\League of Legends
start "" "%path%\lol.launcher.admin.exe"
xcopy  /R /Y "%path%\Config\game.cfg"  "%path%\Config\gameOrginal.cfg*" 
xcopy  /R /Y "%path%\Config\gameRanked.cfg"  "%path%\Config\game.cfg*" 