# ChattKlient

Det här är en skoluppgift som är en enkel konsolbaserad chattklient byggd i C# so ansluter till en Socket.IO-server.
Programmet låter flera användare chatta i realtid, spara historik och använda kommandon.

## Funktioner

- Anslut med användarnamn (valideras så det inte är tomt i fältet)
- Skicka och ta emot meddelanden i realtid
- Visa tidsstämpel, avsändare och meddelandetext för varje meddelande  
- Visa händelser som när någon ansluter eller lämnar chatten 
- Spara och ladda meddelandehistorik mellan sessioner  

- Kommandon: 
  - /help - visar alla hjälpfunktioner 
  - /quit - avsluta chattklienten 
  - /history - visa tidigare meddelanden  
  - /dm <användarnamn> <meddelanden> - skicka privata meddelanden mellan användarna   