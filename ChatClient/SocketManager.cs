using System;
using System.Collections.Generic;
using System.IO;
using SocketIOClient;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using ChatClient;
using System.Text.Json.Serialization;

namespace ChatClient_Exam
{
    // ----------------------------------------------------- SocketManager klass ----------------------------------------------------------
    public class SocketManager : ChatCore
    {
        private static SocketIOClient.SocketIO _chatSocket;
        private static readonly string Path = "/sys25d";
        private static readonly string ChatEvent = "lisa123_message";

        // ----------------------------------------------------- Anslut till Socket.IO servern ----------------------------------------------------------
        public async Task Connect()
        {
            _chatSocket = new SocketIOClient.SocketIO("wss://api.leetcode.se", new SocketIOOptions { Path = Path });

            _chatSocket.On(ChatEvent, response =>
            {
                var element = response.GetValue<JsonElement>();

                string sender = "";
                string text = "";

                if (element.ValueKind == JsonValueKind.Object)
                {
                    sender = element.TryGetProperty("Sender", out var senderProp) ? senderProp.GetString() ?? "Okänd" : "Okänd";
                    text = element.TryGetProperty("Text", out var textProp) ? textProp.GetString() ?? "" : "";
                }
                else if (element.ValueKind == JsonValueKind.String)
                {
                    string fullText = element.GetString() ?? "";
                    int separatorIndex = fullText.IndexOf(": ");
                    if (separatorIndex > 0)
                    {
                        sender = fullText.Substring(0, separatorIndex).Trim();
                        text = fullText.Substring(separatorIndex + 1).Trim();
                    }
                    else
                    {
                        sender = "Okänd";
                        text = fullText;
                    }
                }
                if (string.IsNullOrEmpty(text))
                {
                    return;
                }
                if (sender == Username)
                {
                    ShowMessage($"You: {text}");
                }
                else
                {
                    ShowMessage($"{sender}: {text}");
                }
            });

            _chatSocket.OnConnected += (sender, args) =>
            {
                Console.WriteLine("Ansluten till chatt servern.");
                _chatSocket.EmitAsync(ChatEvent, new { Sender = Username, Text = $"{Username} har anslutit till chatten."});
            };

            _chatSocket.OnDisconnected += (sender, args) =>
            {
                Console.WriteLine("Frånkopplad från chatt servern.");
            };

            await _chatSocket.ConnectAsync();
            await Task.Delay(1000);
        }

        // ----------------------------------------------------- Skicka meddelande ----------------------------------------------------------
        public async Task SendMessage(string message)
        {
            string formattedMessage = $"[{DateTime.Now:HH:mm}] {Username}: {message}";
            await _chatSocket.EmitAsync(ChatEvent, formattedMessage);
            ShowMessage($"You: {message}");
            SaveToHistory(formattedMessage);
        }

        public async Task SendDirectMessage(string recipient, string message)
        {
            string dm = $"[DM till {recipient}] {Username}: {message}";
            await _chatSocket.EmitAsync(ChatEvent, dm);
            ShowMessage(dm);
            SaveToHistory(dm);
        }

        //----------------------------------------------------- Visa meddelande med tidsstämpel ----------------------------------------------------------
        public override void ShowMessage(string message)
        {
            Console.WriteLine($"{DateTime.Now:HH:mm} | {message}");
        }
    }
}
