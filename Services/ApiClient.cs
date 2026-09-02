using System.Net.Http.Headers;
using System.Text.Json;
using Hospital_Clinic_Appointment_System.Helpers;

namespace Hospital_Clinic_Appointment_System.Services;

public interface IApiClient
    {
        Task<ApiResult<T>> GetAsync<T>(string url);
        Task<ApiResult<T>> PostAsync<T>(string url, object payload);
        Task<ApiResult> PostAsync(string url, object payload);
        Task<ApiResult> PutAsync(string url, object payload);
        Task<ApiResult> DeleteAsync(string url);
    }

    public class ApiResult
    {
        public bool Success { get; init; }
        public string? Error { get; init; }
    }

    public class ApiResult<T> : ApiResult
    {
        public T? Data { get; init; }
    }

    public class ApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor) : IApiClient
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public Task<ApiResult<T>> GetAsync<T>(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            AddAuthHeader(request);
            return SendAsync<T>(request);
        }

        public Task<ApiResult<T>> PostAsync<T>(string url, object payload)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = JsonContent.Create(payload)
            };
            AddAuthHeader(request);
            return SendAsync<T>(request);
        }

        public Task<ApiResult> PostAsync(string url, object payload)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = JsonContent.Create(payload)
            };
            AddAuthHeader(request);
            return SendAsync(request);
        }

        public Task<ApiResult> PutAsync(string url, object payload)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = JsonContent.Create(payload)
            };
            AddAuthHeader(request);
            return SendAsync(request);
        }

        public Task<ApiResult> DeleteAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            AddAuthHeader(request);
            return SendAsync(request);
        }

        // Attach JWT token to the individual request (thread-safe)
        private void AddAuthHeader(HttpRequestMessage request)
        {
            var token = httpContextAccessor.HttpContext?.Session.GetString(SessionKeys.AuthToken);
            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        private async Task<ApiResult<T>> SendAsync<T>(HttpRequestMessage request)
        {
            var client = CreateClient();
            using var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                if (response.Content.Headers.ContentLength == 0)
                {
                    return new ApiResult<T> { Success = true };
                }

                var data = await response.Content.ReadFromJsonAsync<T>(JsonOptions);
                return new ApiResult<T> { Success = true, Data = data };
            }

            var error = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(error))
            {
                error = response.ReasonPhrase;
            }

            return new ApiResult<T> { Success = false, Error = error };
        }

        private async Task<ApiResult> SendAsync(HttpRequestMessage request)
        {
            var client = CreateClient();
            using var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return new ApiResult { Success = true };
            }

            var error = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(error))
            {
                error = response.ReasonPhrase;
            }

            return new ApiResult { Success = false, Error = error };
        }

        private HttpClient CreateClient()
        {
            var client = httpClientFactory.CreateClient("default");
            var request = httpContextAccessor.HttpContext?.Request;
            if (request != null)
            {
                client.BaseAddress = new Uri($"{request.Scheme}://{request.Host}");
            }
            return client;
        }
    }
