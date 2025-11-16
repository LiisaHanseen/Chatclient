# ChatApp

Det här är en skoluppgift som är en enkel konsolbaserad chattklient byggd i C# som ansluter till en Socket.IO-server. 
Programmet låter flera användare chatta i realtid, spara historik och använda kommandon.

## Funktioner

- Anslut med användarnamn (valideras så det inte är tomt)  
- Skicka och ta emot meddelanden i realtid  
- Visa tidsstämpel, avsändare och meddelandetext för varje meddelande  
- Visa händelser som när någon ansluter eller lämnar chatten  
- Spara och ladda meddelandehistorik mellan sessioner  
- Kommandon:  
  - `/help` – visa hjälp  
  - `/quit` – avsluta chatten  
  - `/history` – visa tidigare meddelanden  
  - `/dm <användarnamn> <meddelande>` – skicka privat meddelande  