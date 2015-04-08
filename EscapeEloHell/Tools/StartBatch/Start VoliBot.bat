SET path=C:\Users\NEWNICK\Desktop\MyVoliBots\Bot100
start "" "%path%\VoliBot.exe"
xcopy  /R /Y "%path%\Config\game.cfg"  "%path%\Config\gameOrginal.cfg*" 
xcopy  /R /Y "%path%\Config\gameBot.cfg"  "%path%\Config\game.cfg*" 