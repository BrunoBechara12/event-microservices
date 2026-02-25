using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Adapters.Secondary.WhatsApp.DTOs;
using Domain.Ports.Output;
using Microsoft.Extensions.Configuration;

namespace Adapters.Secondary.WhatsApp;

public class EvolutionApiService : IWhatsAppService
{
    private readonly HttpClient _httpClient;
    private readonly string _instanceName;
    private readonly JsonSerializerOptions _jsonOptions;

    public EvolutionApiService(
        HttpClient httpClient,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _instanceName = configuration["EvolutionApi:InstanceName"] ?? "default";
        
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
    }

    public async Task<WhatsAppSendResult> SendTextMessageAsync(string phoneNumber, string message)
    {
        var normalizedPhone = NormalizePhoneNumber(phoneNumber);
        
        var payload = new SendTextRequest
        {
            Number = normalizedPhone,
            Text = message
        };

        var response = await _httpClient.PostAsJsonAsync(
            $"/message/sendText/{_instanceName}",
            payload,
            _jsonOptions);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<SendMessageResponse>(_jsonOptions);
            return new WhatsAppSendResult(true, result?.Key?.Id);
        }

        var errorContent = await response.Content.ReadAsStringAsync();
        return new WhatsAppSendResult(false, ErrorMessage: $"HTTP {response.StatusCode}: {errorContent}");
    }

    public async Task<WhatsAppSendResult> SendMediaMessageAsync(string phoneNumber, string mediaUrl, string? caption = null)
    {
        var normalizedPhone = NormalizePhoneNumber(phoneNumber);
        
        var payload = new SendMediaRequest
        {
            Number = normalizedPhone,
            Media = mediaUrl,
            Caption = caption
        };

        var response = await _httpClient.PostAsJsonAsync(
            $"/message/sendMedia/{_instanceName}",
            payload,
            _jsonOptions);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<SendMessageResponse>(_jsonOptions);
            return new WhatsAppSendResult(true, result?.Key?.Id);
        }

        var errorContent = await response.Content.ReadAsStringAsync();
        return new WhatsAppSendResult(false, ErrorMessage: $"HTTP {response.StatusCode}: {errorContent}");
    }

    public async Task<bool> IsConnectedAsync()
    {
        var response = await _httpClient.GetAsync($"/instance/connectionState/{_instanceName}");
        
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<ConnectionStateResponse>(_jsonOptions);
            return result?.Instance?.State == "open";
        }

        return false;
    }

    public async Task<string?> GetQrCodeAsync()
    {
        var response = await _httpClient.GetAsync($"/instance/connect/{_instanceName}");
        
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<QrCodeResponse>(_jsonOptions);
            return result?.Base64;
        }

        return null;
    }

    private static string NormalizePhoneNumber(string phoneNumber)
    {
        var normalized = new string(phoneNumber.Where(char.IsDigit).ToArray());
        
        if (!normalized.StartsWith("55") && normalized.Length <= 11)
        {
            normalized = "55" + normalized;
        }

        return normalized;
    }
}
