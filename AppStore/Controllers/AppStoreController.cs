using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class AppStoreController : ControllerBase
{
    private const string iTunesApiUrl = "https://itunes.apple.com/us/rss/newapplications/limit=10/json";

    [HttpGet]
    public async Task<IActionResult> GetNewGames()
    {
        try
        {
            var newGames = await FetchNewGamesFromiTunes();
            return Ok(newGames);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetNewGames: {ex}");
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    public async Task<List<GameInfo>> FetchNewGamesFromiTunes()
    {
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetStringAsync(iTunesApiUrl);
            var result = JsonConvert.DeserializeObject<iTunesApiResponse>(response);

            var newGames = new List<GameInfo>();

            if (result != null && result.Feed != null && result.Feed.Entry != null)
            {
                foreach (var entry in result.Feed.Entry)
                {
                    try
                    {
                        var gameInfo = new GameInfo
                        {
                            Name = entry?.Name?.Label ?? "N/A",
                            ReleaseDate = ParseReleaseDate(entry?.ReleaseDate?.Label),
                            Genre = entry?.Category?.Attributes?.Term ?? "N/A",
                            AppStoreLink = entry?.Id?.Label ?? "N/A"
                        };
                        newGames.Add(gameInfo);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error parsing game info: {ex}");
                    }
                }
            }

            return newGames;
        }
    }

    private DateTime? ParseReleaseDate(string dateString)
    {
        if (DateTime.TryParse(dateString, out DateTime releaseDate))
        {
            return releaseDate;
        }

        return null;
    }
}
