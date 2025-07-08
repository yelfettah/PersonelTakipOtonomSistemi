using System.ComponentModel.DataAnnotations;

namespace PersonelTakipOtomasyonuApi.Models
{
    public class Personel
    {
        [Key]
        public int PersonelID { get; set; }
        
        [Required]
        public string Ad { get; set; } = string.Empty;
        
        [Required]
        public string Soyad { get; set; } = string.Empty;
        
        [Required]
        public DateTime DogumTarihi { get; set; }
        
        [Required]
        public string TelefonNo { get; set; } = string.Empty;
        
        [Required]
        public double Maas { get; set; }
        
        [Required]
        public string aktifMi { get; set; } = string.Empty;
        
        [Required]
        public int yillikIzinHakki { get; set; }
        
        [Required]
        public DateTime IseBaslamaTarihi { get; set; }
        
        [Required]
        public string TCKimlikNo { get; set; } = string.Empty;
        
        [Required]
        public string Departman { get; set; } = string.Empty;
        
        [Required]
        public string Pozisyon { get; set; } = string.Empty;
        
        [Required]
        public string Sifre { get; set; } = string.Empty;
        
        [Required]
        public string Eposta { get; set; } = string.Empty;
        
        public string? EkleyenKullanici { get; set; }
        public DateTime? EklenmeTarihi { get; set; }
        public string? GuncelleyenKullanici { get; set; }
        public DateTime? GuncellenmeTarihi { get; set; }
    }
} 