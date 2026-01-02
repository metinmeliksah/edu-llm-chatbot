using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EduLlmChatbot.Core.Models;

namespace EduLlmChatbot.Core.Interfaces;

public interface ILlmService : IDisposable
{
    Task<string> SendMessageAsync(string message, List<ChatMessage> history);
}
