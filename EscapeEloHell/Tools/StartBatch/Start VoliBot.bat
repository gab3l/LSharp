SET path=C:\Riot Games\League of Legends
SET Volipath=C:\Users\NEWNICK\Desktop\MyVoliBots\Bot100
start "" "%Volipath%\VoliBot.exe"
xcopy  /R /Y "%path%\Config\game.cfg"  "%path%\Config\gameOrginal.cfg*" 
xcopy  /R /Y "%path%\Config\gameBot.cfg"  "%path%\Config\game.cfg*" 