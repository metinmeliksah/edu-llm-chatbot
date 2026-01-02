using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EduLlmChatbot.Services.Gemini.Models;

public class GeminiRequest
{
    [JsonPropertyName("contents")]
    public List<GeminiContent> Contents { get; set; } = new();
}

public class GeminiContent
{
    [JsonPropertyName("role")]
    public string Role { get; set; } = "user"; // "user" or "model"

    [JsonPropertyName("parts")]
    public List<GeminiPart> Parts { get; set; } = new();
}

public class GeminiPart
{
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;
}
