namespace PersonelTakipOtomasyonuApi.Dtos
{
    public class PersonelGuncelleDto
    {
        public int PersonelID { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string TCKimlikNo { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string TelefonNo { get; set; }
        public string Eposta { get; set; }
        public DateTime IseBaslamaTarihi { get; set; }
        public double Maas { get; set; }
        public string Pozisyon { get; set; }
        public string Departman { get; set; }
        public string Sifre { get; set; }
        public bool aktifMi { get; set; }
        public int yillikIzinHakki { get; set; }
        public string? GuncelleyenKullanici { get; set; }
        public DateTime? GuncellenmeTarihi { get; set; }
    }
} 