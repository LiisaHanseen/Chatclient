using ChatClient_Exam;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChatClient
{
    class Program
    {
        // ----------------------------------------------------- Programmets start - Main ----------------------------------------------------------
        static async Task Main()
        {
            SocketManager chat = new();

            Console.WriteLine("Välkommen till chatten!");
            Console.WriteLine("Ange ett användarnamn: ");
            chat.Username = Console.ReadLine()?.Trim() ?? "username";

            while (string.IsNullOrWhiteSpace(chat.Username))
            {
                Console.WriteLine("Användarnamnet kan inte vara tomt. Vänligen ange ett giltigt användarnamn: ");
                chat.Username = Console.ReadLine()?.Trim() ?? "username";
            }

            await chat.Connect();

            chat.LoadChatHistory();
            if (chat.Messages.Count > 0)
            {
                Console.WriteLine("Tidigare meddelanden:");
                foreach (var message in chat.Messages)
                {
                    Console.WriteLine(message);
                }
            }

            Console.WriteLine("Skriv ett meddelande eller ett kommando (/help):");


            // ----------------------------------------------------- Huvuvdloop ----------------------------------------------------------
            while (true)
            {
                string input = Console.ReadLine() ?? "";

                if (input.StartsWith("/quit")) {
                    Console.WriteLine("Avslutar...");
                    chat.SaveChatHistory();
                    break;
                }
                else if (input.StartsWith("/help")) {
                    Console.WriteLine("""
                        Kommandon:
                        /help - Visa hjälp
                        /quit - Avsluta 
                        /history - Visa tidigare meddelanden
                        /dm <användarnamn> <meddelande> - Skicka privat meddelande till användare
                        """);
                        
                }
                else if (input.StartsWith("/history"))
                {
                    Console.WriteLine("Meddelandehistorik:");
                    foreach (var message in chat.Messages)
                    {
                        Console.WriteLine(message);
                    }
                }
                else if (input.StartsWith("/dm "))
                {
                    var parts = input.Split(' ', 3);
                    if (parts.Length >= 3)
                    {
                        string recipient = parts[1];
                        string message = parts[2];
                        await chat.SendDirectMessage(recipient, message);
                    }
                    else
                    {
                        Console.WriteLine("Felaktigt kommando. Använd formatet: /dm <användarnamn> <meddelande>");
                    }
                }
                else
                {
                    await chat.SendMessage(input);
                }
            }
        }
    }
}