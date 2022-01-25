using Funda.Data.Infrastructure;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Funda.Data.Clients
{
    public class APIClientBase
    {
        protected readonly HttpClient _client;
        private readonly ILogger _logger = Log.Logger;

        public APIClientBase(IOptions<APIClientConfiguration> option)
        {
            _client = new HttpClient { BaseAddress = new Uri(option.Value.BaseUrl) };
            _client.DefaultRequestHeaders.Accept.Clear();
        }

        public async Task<string> GetAsync(string url, string customErrorMessage)
        {
            try
            {
                using var httpResponse = await _client.GetAsync(url).ConfigureAwait(false);
                if (httpResponse.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new HttpRequestException(await BuildHttpException(httpResponse));

                return await httpResponse.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                LogError(ex, customErrorMessage);
                throw;
            }
        }

        private static async Task<string> BuildHttpException(HttpResponseMessage response)
        {
            var body = await response.Content.ReadAsStringAsync();
            var code = response.StatusCode.GetHashCode().ToString();
            return $"API thrown an exception. Response code: {code}, Response body: {body}.";
        }

        private void LogError(Exception ex, string message)
        {
            _logger.Error(ex, "{Message}", message);
        }

        protected static string BuildStringForURL(List<string> filter)
        {
            var filterString = string.Empty;

            if (filter != null)
            {
                foreach (var prop in filter)
                {
                    filterString += (prop == filter.FirstOrDefault())
                        ? string.Concat($"/{prop}/").ToLower()
                        : string.Concat($"{prop}/").ToLower();
                }
            }

            return filterString.Trim(); ;
        }
    }
}