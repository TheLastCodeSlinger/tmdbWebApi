﻿using System.Text.Json;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly HttpClient _httpClient;

    private const string BaseUrl = "https://api.themoviedb.org/3";

    private readonly string? ApiKey;


    public MovieController(IHttpClientFactory httpClientFactory, IConfiguration config)
    {
        _httpClient = httpClientFactory.CreateClient();
        if (string.IsNullOrEmpty(ApiKey))
        {
            ApiKey = config["TMDB_API_KEY"]
                   ?? throw new InvalidOperationException("TMDB_API_KEY not set.");
        }
    }

    [HttpGet("discover")]
    public async Task<IActionResult> DiscoverMovies(
        [FromQuery] string genreId,
        [FromQuery] int page = 1,
        [FromQuery] string sortBy = "popularity.desc",
        [FromQuery] string language = "en-US")
    {
        var url = $"{BaseUrl}/discover/movie?api_key={ApiKey}&language={language}&page={page}&with_genres={genreId}&sort_by={sortBy}";

        try
        {
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, "TMDB API Error");

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<MovieResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Server Error: {ex.Message}");
        }
    }
}