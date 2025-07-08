using PersonelTakipOtonomSistemi.Dtos;
using System.Net.Http.Json;
using System.Text.Json;
using System.Net;
   using Microsoft.Maui.Controls;

namespace PersonelTakipOtonomSistemi.Services
{

    public static class UrlHelper
    {
        private static string BaseUrl = "http://localhost:5072";
        public static string PersonelUrl = $"{BaseUrl}/Personel";
    }

    public abstract class BaseService
    {
        protected HttpClient _client;
        protected JsonSerializerOptions _serializerOptions;

        public BaseService()
        {
#if DEBUG && ANDROID
            HttpsClientHandlerService handler = new HttpsClientHandlerService();
            _client = new HttpClient(handler.GetPlatformMessageHandler());
#else
            _client = new HttpClient();
#endif

            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }
    }

    public class HttpsClientHandlerService
    {

        public HttpMessageHandler GetPlatformMessageHandler()
        {
#if ANDROID
            var handler = new Xamarin.Android.Net.AndroidMessageHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert != null && cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
#elif IOS
        var handler = new NSUrlSessionHandler
        {
            TrustOverrideForUrl = IsHttpsLocalhost
        };
        return handler;
#else
     throw new PlatformNotSupportedException("Only Android and iOS supported.");
#endif
        }

#if IOS
    public bool IsHttpsLocalhost(NSUrlSessionHandler sender, string url, Security.SecTrust trust)
    {
        return url.StartsWith("https://localhost");
    }
#endif
    }

    public class PersonelServices : BaseService , IPersonelServices
    {


        public async Task<List<Personel>> GetTumPersoneller()
        {
            try
            {
                var response = await _client.GetAsync("http://localhost:5072/personeller");
                var json = await response.Content.ReadAsStringAsync();
                var personeller = JsonSerializer.Deserialize<List<Personel>>(json, _serializerOptions);
                return personeller ?? new List<Personel>();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("HATA", ex.Message, "Tamam");
                return new List<Personel>();
            }
        }

         async Task IPersonelServices.PersonelEkle(PersonelOlusturDto personel)
        {
            try
            {
                personel.EkleyenKullanici = "MobilKullanıcı";
                personel.EklenmeTarihi = DateTime.Now;
                Uri uri = new Uri(UrlHelper.PersonelUrl);
                JsonContent content = JsonContent.Create(personel);

                HttpResponseMessage response = await _client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"API Hatası: {response.StatusCode} - {errorContent}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Personel ekleme hatası: {ex.Message}");
            }
        }

        async Task IPersonelServices.PersonelGuncelle(PersonelGuncelleDto personel)
        {
            personel.GuncelleyenKullanici = "MobilKullanıcı";
            personel.GuncellenmeTarihi = DateTime.Now;
            JsonContent jsonContent = JsonContent.Create(personel);

            await _client.PutAsync(UrlHelper.PersonelUrl, jsonContent);
        }
        async public Task PersonelSil(int id)
        {
            Uri uri = new Uri($"{UrlHelper.PersonelUrl}?id={id}");

            HttpResponseMessage response = await _client.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
            {

            }
        }

        public async Task<Personel?> Login(string tckimlikNo, string sifre)
        {
            var loginDto = new { TCKimlikNo = tckimlikNo, Sifre = sifre };
            var uri = new Uri("http://localhost:5072/login");
            var content = JsonContent.Create(loginDto);
            var response = await _client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var personel = JsonSerializer.Deserialize<Personel>(json, _serializerOptions);
                return personel;
            }
            return null;
        }

    }
}
