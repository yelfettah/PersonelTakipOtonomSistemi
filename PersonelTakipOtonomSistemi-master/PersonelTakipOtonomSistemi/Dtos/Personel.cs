using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace PersonelTakipOtonomSistemi.Dtos
{
    public class Personel
    {
        [JsonPropertyName("personelID")]
        public int PersonelID { get; set; }
        [JsonPropertyName("ad")]
        public string Ad { get; set; }
        [JsonPropertyName("soyad")]
        public string Soyad { get; set; }
        [JsonPropertyName("dogumTarihi")]
        public DateTime DogumTarihi { get; set; }
        [JsonPropertyName("telefonNo")]
        public string TelefonNo { get; set; }
        [JsonPropertyName("maas")]
        public double Maas { get; set; }
        [JsonPropertyName("aktifMi")]
        public string AktifMi { get; set; }
        [JsonPropertyName("yillikIzinHakki")]
        public int YillikIzinHakki { get; set; }
        [JsonPropertyName("iseBaslamaTarihi")]
        public DateTime IseBaslamaTarihi { get; set; }
        [JsonPropertyName("tcKimlikNo")]
        public string TCKimlikNo { get; set; }
        [JsonPropertyName("departman")]
        public string Departman { get; set; }
        [JsonPropertyName("pozisyon")]
        public string Pozisyon { get; set; }
        [JsonPropertyName("sifre")]
        public string Sifre { get; set; }
        [JsonPropertyName("eposta")]
        public string Eposta { get; set; }
        [JsonPropertyName("ekleyenKullanici")]
        public string EkleyenKullanici { get; set; }
        [JsonPropertyName("eklenmeTarihi")]
        public DateTime? EklenmeTarihi { get; set; }
        [JsonPropertyName("guncelleyenKullanici")]
        public string GuncelleyenKullanici { get; set; }
        [JsonPropertyName("guncellenmeTarihi")]
        public DateTime? GuncellenmeTarihi { get; set; }
    }
}
