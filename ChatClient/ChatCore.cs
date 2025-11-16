using ChatClient_Exam;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChatClient
{
    public class ChatCore
    {
        // ----------------------------------------------------- Egenskaper ----------------------------------------------------------
        public string Username { get; set; } = "Username";

        public List<string> Messages { get; set; } = new();

        // ----------------------------------------------------- Metoder ----------------------------------------------------------
        public virtual void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        // ----------------------------------------------------- Spara meddelande till historik ----------------------------------------------------------
        public void SaveToHistory(string message)
        {
            Messages.Add(message);
        }

        public void SaveChatHistory()
        {
            string fileName = $"{Username}_history.txt";
            File.WriteAllLines(fileName, Messages);
            Console.WriteLine($"Chatt historik sparad till {fileName}");
        }

        public void LoadChatHistory()
        {
            string fileName = $"{Username}_history.txt";
            if (File.Exists(fileName))
            {
                Messages = File.ReadAllLines(fileName).ToList();
                Console.WriteLine($"Chatt historik laddad från {fileName}");
            }
        }
    }
}
