# Role Message Tracker
This is a prof of concept for a reddit bot request. The images used in the tutorial for setup are the same for my other bot prof of concepts, while the names in the images for things might not correspond to what this bot is, the steps are the same.

TLDR; Images are for reference only.

## Discord Commands
```
Commands:
rmt! <- Prefix

Command: (you must NOT include the "[]" brackets.)

No Commands.
```

## Setup

- [Click Here](https://discordapp.com/developers/applications/) to create a bot and invite it to your server as follows
   - Click the 'New Application' button
         
   ![New Application](https://i.imgur.com/2OQwdyk.png)
         
   - Enter a name for this bot and click create (you can name this anything, but it will show in discord this way)
         
   ![Name Bot](https://imgur.com/2giEnTq.png)
         
   - Click the 'Bot' tab under settings on the left toolbar
         
   ![Bot Tab](https://imgur.com/vJNHZnm.png)
         
   - Click the 'Add Bot' button
         
   ![Add Bot](https://imgur.com/8AlIHjo.png)
         
   - Click 'Yes do it' in the pop-up
       
   ![Yes do it](https://imgur.com/HWg5AZ8.png)
     
   - Select 'OAuth2' tab under settings on the left toolbar, select 'Bot' from the checkbox, then 'Administrator'. Copy the link from the bottom and paste it into a new tab / window.
         
   ![OAuth2](https://imgur.com/tBLw2Vj.png)
                                
   - Select the server you want to invite the bot to and click 'Authorize'
     
   ![Authorize Bot](https://imgur.com/Bnr5vdP.png)
   
   - Go back to the developer portal and select the Bot tab again. Then enable to privledged gateway intents
   
   ![Gateway Intents](https://imgur.com/RNewfJB.png)
   
   - Click 'Reset Token' button located under the token, then select 'Copy'.
   
   ![Reset Token](https://imgur.com/ScjmbL9.png)
   ![Copy Token](https://imgur.com/ImHZxNG.png)
        
   - Make sure your discord desktop client is running *(this does not work with discord on webclients like chrome etc)*
   
   - Download the latest version of the bot
      - > [v0.1](https://github.com/OniSensei/Discord-Reactions-to-CSV-Pastebin/releases/download/v0.1/Export.Reactions.rar)
    
   - Extract the zip to the desktop or my documents, somewhere you can find it easily.
   
   - Navigate to the folder and open settings.ini with a text editor like notepad, it will look like this:
```
[SETTINGS]
Token = ""
Name = "Role Message Tracker"
IconURL = "https://imgur.com/ZRfxJja.png"
Prefix = "rmt!"
Roles = "Developer,Moderator"
Channels = "1087462681608462377,1087462728798584913"
```
   - Edit the settings with the token we copied in the steps above. Change the name, iconURL, prefix, add roles/channels to the list seperated by a comma as you please. 
   
   It is important to note that the order the roles are listed will be the same order for channels, so the first role will be sent to the first channel ID, the second role to the second channel.


   - Once your settings.ini file is saved, you can start 'Simple Poll And Extract.exe'
