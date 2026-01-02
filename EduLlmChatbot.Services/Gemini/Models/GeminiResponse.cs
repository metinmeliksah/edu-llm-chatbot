using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EduLlmChatbot.Services.Gemini.Models;

public class GeminiResponse
{
    [JsonPropertyName("candidates")]
    public List<GeminiCandidate>? Candidates { get; set; }
}

public class GeminiCandidate
{
    [JsonPropertyName("content")]
    public GeminiContent? Content { get; set; }
    
    [JsonPropertyName("finishReason")]
    public string? FinishReason { get; set; }
}
