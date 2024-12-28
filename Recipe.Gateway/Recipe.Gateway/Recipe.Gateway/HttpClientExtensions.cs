using System.Text;
using System.Text.Json;

namespace Recipe.Gateway.Extensions;

public static class HttpClientExtensions
{
    public static async Task<HttpResponseMessage> PostJsonAsync<T>(
        this HttpClient httpClient, string requestUri, T data)
    {
        var jsonContent = JsonSerializer.Serialize(data);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        return await httpClient.PostAsync(requestUri, content);
    }

    public static async Task<HttpResponseMessage> PutJsonAsync<T>(
        this HttpClient httpClient, string requestUri, T data)
    {
        var jsonContent = JsonSerializer.Serialize(data);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        return await httpClient.PutAsync(requestUri, content);
    }
}
/*
    public static HttpClient CreateClient(this IHttpClientFactory httpClientFactory, string clientName)
    {
        var client = httpClientFactory.CreateClient(clientName);
        return client;
    }
    */