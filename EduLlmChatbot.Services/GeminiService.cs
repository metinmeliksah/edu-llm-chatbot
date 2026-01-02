using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Linq;
using EduLlmChatbot.Core.Interfaces;
using EduLlmChatbot.Core.Models;
using EduLlmChatbot.Services.Gemini.Models;

namespace EduLlmChatbot.Services;

public class GeminiService : ILlmService, IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _modelId;
    private const string BaseUrl = "https://generativelanguage.googleapis.com/v1beta/models/";
    private bool _disposed = false;

    public GeminiService(string apiKey, string modelId = "gemini-2.5-flash")
    {
        _apiKey = apiKey ?? string.Empty;
        _modelId = modelId;
        _httpClient = new HttpClient();
    }

    public async Task<string> SendMessageAsync(string message, List<ChatMessage> history)
    {
        if (string.IsNullOrEmpty(_apiKey))
        {
             return "Error: API Key is missing. Please configure it in settings.";
        }

        var url = $"{BaseUrl}{_modelId}:generateContent?key={_apiKey}";

        var request = new GeminiRequest();
        
        // Convert history to Gemini format (excluding the very latest message if it's not in history list yet, 
        // typically the history passed here is PAST history)
        if (history != null) 
        {
            foreach (var msg in history)
            {
                request.Contents.Add(new GeminiContent
                {
                    Role = msg.Role == Role.User ? "user" : "model",
                    Parts = new List<GeminiPart> { new GeminiPart { Text = msg.Content } }
                });
            }
        }

        // Add current message
        request.Contents.Add(new GeminiContent
        {
            Role = "user",
            Parts = new List<GeminiPart> { new GeminiPart { Text = message } }
        });

        try
        {
            var response = await _httpClient.PostAsJsonAsync(url, request);
            var responseContent = await response.Content.ReadAsStringAsync();
            
            if (!response.IsSuccessStatusCode)
            {
                return $"API Error ({response.StatusCode}): {responseContent}";
            }

            var geminiResponse = await response.Content.ReadFromJsonAsync<GeminiResponse>();
            
            if (geminiResponse == null)
            {
                return $"Error: Could not parse response. Raw: {responseContent}";
            }

            if (geminiResponse.Candidates == null || geminiResponse.Candidates.Count == 0)
            {
                return $"Error: No candidates in response. Raw: {responseContent}";
            }

            var candidate = geminiResponse.Candidates.FirstOrDefault();
            if (candidate?.Content == null)
            {
                return $"Error: Candidate has no content. FinishReason: {candidate?.FinishReason}";
            }

            if (candidate.Content.Parts == null || candidate.Content.Parts.Count == 0)
            {
                return $"Error: Content has no parts. FinishReason: {candidate.FinishReason}";
            }

            var text = candidate.Content.Parts.FirstOrDefault()?.Text;
            
            if (string.IsNullOrWhiteSpace(text))
            {
                return $"Error: Empty text in response. FinishReason: {candidate.FinishReason}";
            }
            
            return text;
        }
        catch (HttpRequestException ex)
        {
            return $"Connection Error: {ex.Message}";
        }
        catch (Exception ex)
        {
            return $"Error: {ex.GetType().Name} - {ex.Message}";
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _httpClient?.Dispose();
            }
            _disposed = true;
        }
    }
}
