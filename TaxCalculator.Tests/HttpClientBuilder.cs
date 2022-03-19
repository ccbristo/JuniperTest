using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace TaxCalculator.Tests;

internal class HttpClientBuilder
{
    private readonly InterceptingHandler _requestHandler = new();
    private readonly Uri _baseAddress;

    private HttpClientBuilder(Uri baseAddress)
    {
        _baseAddress = baseAddress;
    }
    
    public static HttpClient Build(string baseAddress, Action<HttpClientBuilder> config)
    {
        var baseUri = new Uri(baseAddress);
        var builder = new HttpClientBuilder(baseUri);
        config(builder);

        var client = new HttpClient(builder._requestHandler);
        client.BaseAddress = baseUri;
        return client;
    }
    
    public HttpResponseBuilder ForUrl(string url)
    {
        if (!Uri.TryCreate(_baseAddress, url, out var fullUri))
            throw new ArgumentOutOfRangeException(nameof(url), $"Could not create a URI from {url}");
        
        return new HttpResponseBuilder(fullUri, _requestHandler);
    }

    internal class InterceptingHandler : DelegatingHandler
    {
        private readonly Dictionary<Uri, (HttpStatusCode StatusCode, object? Content)> _results = new();

        public void AddHandler(Uri fullUri, HttpStatusCode resultStatusCode, object? content = null)
        {
            _results.Add(fullUri, (resultStatusCode, content));
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            if(!_results.TryGetValue(request.RequestUri!, out var result))
                throw new Exception($"No result was provided for {request.RequestUri}"); // this should really have it's own exception type

            var content = result.Content == null ? null : JsonContent.Create(result.Content);
            var response = new HttpResponseMessage(result.StatusCode)
            {
                Content = content
            };

            return Task.FromResult(response);

        }
    }

    internal class HttpResponseBuilder
    {
        private readonly Uri _fullUrl;
        private readonly InterceptingHandler _handler;

        public HttpResponseBuilder(Uri fullUri, InterceptingHandler handler)
        {
            _fullUrl = fullUri;
            _handler = handler;
        }
        
        public void ReturnsJson(HttpStatusCode statusCode, object? result = null)
        {
            _handler.AddHandler(_fullUrl, statusCode, result);
        }
    }
}