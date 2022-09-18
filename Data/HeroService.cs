using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace dotnet6_maui_blazor_hero_client.Data;

public class HeroService
{
  readonly HttpClient _client;
  readonly JsonSerializerOptions _serializerOptions;

  public List<Hero> Heroes { get; private set; }

  // TODO: Læg denne i constants i en json konfigurationsfil
  private const string constRestUrl = "https://simple-hero-api-service.azurewebsites.net/api/Heroes/{0}";

  public HeroService()
  {
    _client = new HttpClient();
    _serializerOptions = new JsonSerializerOptions
    {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
      WriteIndented = true
    };
  }

  public async Task<IEnumerable<Hero>> RefreshDataAsync()
  {
    Heroes = new List<Hero>();

    Uri uri = new Uri(string.Format(constRestUrl, string.Empty));
    try
    {
      HttpResponseMessage response = await _client.GetAsync(uri);
      if (response.IsSuccessStatusCode)
      {
        string content = await response.Content.ReadAsStringAsync();
        Heroes = JsonSerializer.Deserialize<List<Hero>>(content, _serializerOptions);
      }
    }
    catch (Exception ex)
    {
      Debug.WriteLine(@"\tERROR {0}", ex.Message);
    }

    return Heroes;
  }

  public async Task DeleteHeroAsync(Hero hero)
  {
    Uri uri = new Uri(string.Format(constRestUrl, hero.Name));

    try
    {
      HttpResponseMessage response = await _client.DeleteAsync(uri);
      if (response.IsSuccessStatusCode)
        Debug.WriteLine(@"\tHero successfully deleted.");
    }
    catch (Exception ex)
    {
      Debug.WriteLine(@"\tERROR {0}", ex.Message);
    }
  }

  public async Task CreateHeroAsync(Hero hero)
  {
    Uri uri = new Uri(string.Format(constRestUrl, string.Empty));

    try
    {
      string json = JsonSerializer.Serialize(hero, _serializerOptions);
      StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

      HttpResponseMessage response = null;
      response = await _client.PostAsync(uri, content);

      if (response.IsSuccessStatusCode)
        Debug.WriteLine(@"\tHero successfully saved.");
    }
    catch (Exception ex)
    {
      Debug.WriteLine(@"\tERROR {0}", ex.Message);
    }
  }

  public async Task UpdateHeroAsync(Hero hero)
  {
    Uri uri = new Uri(string.Format(constRestUrl, string.Empty));

    try
    {
      string json = JsonSerializer.Serialize(hero, _serializerOptions);
      StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

      HttpResponseMessage response = null;
      response = await _client.PutAsync(uri, content);

      if (response.IsSuccessStatusCode)
        Debug.WriteLine(@"\tHero successfully saved.");
    }
    catch (Exception ex)
    {
      Debug.WriteLine(@"\tERROR {0}", ex.Message);
    }
  }

  public async Task<Hero> GetHeroAsync(string Name)
  {
    Uri uri = new Uri(string.Format(constRestUrl, Name));

    try
    {
      HttpResponseMessage response = await _client.GetAsync(uri);
      if (response.IsSuccessStatusCode)
      {
        string content = await response.Content.ReadAsStringAsync();
        var hero = JsonSerializer.Deserialize<Hero>(content, _serializerOptions);
        return hero;
      }
    }
    catch (Exception ex)
    {
      Debug.WriteLine(@"\tERROR {0}", ex.Message);
    }

    return null;
  }
}
