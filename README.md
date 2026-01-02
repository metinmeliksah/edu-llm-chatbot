# EduLlmChatbot - Gemini 2.5 Flash Chat Uygulaması

Google Gemini 2.5 Flash modeli ile entegre, modern ve hızlı bir sohbet uygulaması. C# .NET 9 ve Windows Forms ile geliştirilmiştir.

## Özellikler
*   **Google Gemini 2.5 Flash Entegrasyonu**: En yeni Gemini 2.5 Flash modeli ile sohbet edin.
*   **C# / Windows Forms**: Yerel Windows masaüstü deneyimi.
*   **Güvenli API Key Girişi**: API anahtarınızı doğrudan arayüzden girin (sağ üst köşe).
*   **Bağlam Farkındalığı**: Tutarlı yanıtlar için konuşma geçmişini korur.
*   **Modern Arayüz**: Karanlık tema ve duyarlı düzen.
*   **MVVM Pattern**: Temiz mimari ve bakımı kolay kod yapısı.

## Gereksinimler
*   .NET 9 SDK veya üzeri.
*   Windows işletim sistemi (Windows Forms gereksinimi).
*   Google Gemini API Anahtarı ([Google AI Studio](https://aistudio.google.com/) adresinden alabilirsiniz).

## Kurulum ve Çalıştırma

### Visual Studio ile:
1.  Solution dosyasını (`EduLlmChatbot.sln`) Visual Studio'da açın.
2.  Solution'ı derleyin (Build > Build Solution veya `Ctrl+Shift+B`).
3.  `EduChatbot.WinForms` projesini startup project olarak ayarlayın (Solution Explorer'da sağ tıklayıp "Set as Startup Project").
4.  Uygulamayı çalıştırın (F5 veya Debug > Start Debugging).

### Komut Satırı ile:
1.  Repository'yi klonlayın veya kaynak kodları indirin.
2.  Terminal'de proje klasörüne gidin:
    ```bash
    cd EduChatbot.WinForms
    ```
3.  Uygulamayı çalıştırın:
    ```bash
    dotnet run
    ```

## Kullanım
1.  Uygulamayı başlatın.
2.  **API Key Girişi**: 
   - Uygulamanın **sağ üst köşesindeki** "API Key:" alanına Google Gemini API anahtarınızı girin.
   - API anahtarınızı [Google AI Studio](https://aistudio.google.com/) adresinden ücretsiz olarak alabilirsiniz.
   - API anahtarı girildikten sonra otomatik olarak kaydedilir ve kullanıma hazır olur.
3.  Alt kısımdaki metin kutusuna mesajınızı yazın ve **Enter** tuşuna basın veya **Gönder** butonuna tıklayın.
4.  Gemini 2.5 Flash ile sohbet etmeye başlayın!

## Proje Yapısı
*   **EduLlmChatbot.Core**: Domain modelleri ve arayüzler (`ChatMessage`, `Role`, `ILlmService`).
*   **EduLlmChatbot.Services**: Gemini API entegrasyonu (`GeminiService`, HTTP client implementasyonu).
*   **EduChatbot.WinForms**: MVVM tabanlı Windows Forms kullanıcı arayüzü.

## Teknolojiler
*   .NET 9
*   Windows Forms
*   MVVM Pattern
*   Google Gemini 2.5 Flash API
*   HttpClient (REST API iletişimi için)

## Lisans
MIT
