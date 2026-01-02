using System;

namespace EduLlmChatbot.Core.Models;

public class ChatMessage
{
    public Role Role { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.Now;
    public bool IsError { get; set; } = false;
}
