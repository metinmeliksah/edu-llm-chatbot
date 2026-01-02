using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using EduLlmChatbot.Core.Interfaces;
using EduLlmChatbot.Core.Models;
using EduLlmChatbot.Services;
using EduChatbot.WinForms.Helpers;
using System.Linq;

namespace EduChatbot.WinForms.ViewModels;

public class MainViewModel : ViewModelBase, IDisposable
{
    private string _apiKey = "";
    private string _currentMessage = "";
    private bool _isLoading = false;
    private string _modelId = "gemini-2.5-flash"; 
    private bool _disposed = false;

    public ObservableCollection<ChatMessage> Messages { get; } = new();

    public string ApiKey
    {
        get => _apiKey;
        set 
        { 
             if (SetProperty(ref _apiKey, value)) 
             {
                 _currentLlmService?.Dispose();
                 _currentLlmService = new GeminiService(_apiKey, _modelId);
             }
        }
    }

    public string CurrentMessage
    {
        get => _currentMessage;
        set 
        {
            SetProperty(ref _currentMessage, value);
        }
    }

    public bool IsLoading
    {
        get => _isLoading;
        set 
        {
            SetProperty(ref _isLoading, value);
        }
    }

    public ICommand SendCommand { get; }

    private ILlmService _currentLlmService;

    public MainViewModel()
    {
        _currentLlmService = new GeminiService(_apiKey, _modelId);
        SendCommand = new RelayCommand(async _ => await SendMessageAsync(), _ => !IsLoading && !string.IsNullOrWhiteSpace(CurrentMessage));
    }

    public async Task SendMessageAsync()
    {
        if (string.IsNullOrWhiteSpace(_apiKey))
        {
            Messages.Add(new ChatMessage { Role = Role.System, Content = "⚠️ Lütfen sağ üst köşeden geçerli bir API Anahtarı (Gemini API Key) giriniz." });
            return;
        }

        if (string.IsNullOrWhiteSpace(CurrentMessage))
        {
            return;
        }

        var userMsgText = CurrentMessage.Trim();
        if (string.IsNullOrEmpty(userMsgText))
        {
            return;
        }
        
        CurrentMessage = ""; 
        
        // Kullanıcı mesajını ekle
        var userMsg = new ChatMessage { Role = Role.User, Content = userMsgText };
        Messages.Add(userMsg);

        IsLoading = true;

        try
        {
            // API key güncellenmiş olabilir, service'i kontrol et
            if (_currentLlmService == null || string.IsNullOrEmpty(_apiKey))
            {
                _currentLlmService?.Dispose();
                _currentLlmService = new GeminiService(_apiKey, _modelId);
            }

            // Geçmiş mesajları hazırla (sistem mesajları hariç)
            var history = Messages
                .Where(m => m != userMsg && m.Role != Role.System)
                .ToList();

            var responseText = await _currentLlmService.SendMessageAsync(userMsgText, history);
            
            // Hata kontrolü
            if (string.IsNullOrEmpty(responseText))
            {
                Messages.Add(new ChatMessage { Role = Role.System, Content = "⚠️ Boş yanıt alındı. API key'inizi kontrol edin.", IsError = true });
            }
            else if (responseText.StartsWith("Error:") || responseText.StartsWith("API Error") || responseText.StartsWith("Connection Error"))
            {
                Messages.Add(new ChatMessage { Role = Role.System, Content = responseText, IsError = true });
            }
            else
            {
                Messages.Add(new ChatMessage { Role = Role.Model, Content = responseText });
            }
        }
        catch (Exception ex)
        {
            Messages.Add(new ChatMessage { Role = Role.System, Content = $"Hata: {ex.Message}", IsError = true });
        }
        finally
        {
            IsLoading = false;
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
                _currentLlmService?.Dispose();
            }
            _disposed = true;
        }
    }
}
