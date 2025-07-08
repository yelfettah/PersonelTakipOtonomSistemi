using System.Text.Json.Serialization;

namespace PersonelTakipOtomasyonuApi.Dtos
{
    public class LoginDto
    {
        [JsonPropertyName("tckimlikNo")]
        public string TCKimlikNo { get; set; }
        [JsonPropertyName("sifre")]
        public string Sifre { get; set; }
    }
} 