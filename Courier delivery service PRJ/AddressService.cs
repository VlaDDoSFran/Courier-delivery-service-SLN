using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using System.Windows.Forms;

public class AddressService
{
    private Random random = new Random();

    public async Task<string> GetRandomRussianAddress()
    {
        try
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(30);

                string[] streetQueries = {
                "москва ленина", "санкт-петербург невский", "новосибирск советская",
                "екатеринбург луначарского", "казань баумана", "краснодар красная",
                "сочи курортный", "воронеж ленина", "самара ленина", "ростов-на-дону садовая"
            };

                var randomQuery = streetQueries[random.Next(streetQueries.Length)];

                var request = new
                {
                    query = randomQuery,
                    count = 20
                };

                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Add("Authorization", "Token 48f929135ca576e8aa17882e79ad341438b73895");

                var response = await client.PostAsync(
                    "https://suggestions.dadata.ru/suggestions/api/4_1/rs/suggest/address",
                    content).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var data = JsonConvert.DeserializeObject<DaDataResponse>(responseJson);

                if (data.suggestions != null && data.suggestions.Length > 0)
                {
                    var validAddresses = data.suggestions
                        .Where(s => s.data != null &&
                                   s.data.house != null &&
                                   s.data.street != null &&
                                   s.data.city != null)
                        .ToArray();

                    //MessageBox.Show($"Найдено адресов с домами: {validAddresses.Length}");

                    if (validAddresses.Length > 0)
                    {
                        var randomIndex = random.Next(validAddresses.Length);
                        var address = validAddresses[randomIndex];

                        string apartment = !string.IsNullOrEmpty(address.data.flat) ? $", кв. {address.data.flat}" : "";
                        return $"Россия, г. {address.data.city}, ул. {address.data.street}, д. {address.data.house}{apartment}";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }

        return GenerateFallbackAddress();
    }

    private string GenerateFallbackAddress()
    {
        string[] cities = {
            "Москва", "Санкт-Петербург", "Новосибирск", "Екатеринбург", "Казань",
            "Нижний Новгород", "Челябинск", "Самара", "Омск", "Ростов-на-Дону",
            "Уфа", "Красноярск", "Воронеж", "Пермь", "Волгоград", "Краснодар"
        };

        string[] streets = {
            "Ленина", "Пушкина", "Гагарина", "Советская", "Мира", "Кирова",
            "Садовоя", "Центральная", "Молодежная", "Школьная", "Лесная", "Заречная"
        };

        var city = cities[random.Next(cities.Length)];
        var street = streets[random.Next(streets.Length)];
        var building = random.Next(1, 100);
        var apartment = random.Next(1, 200);

        return $"Россия, г. {city}, ул. {street}, д. {building}, кв. {apartment}";
    }
}

public class DaDataResponse
{
    public Suggestion[] suggestions { get; set; }
}

public class Suggestion
{
    public string value { get; set; }
    public Data data { get; set; }
}

public class Data
{
    public string city { get; set; }
    public string street { get; set; }
    public string house { get; set; }
    public string flat { get; set; }
}