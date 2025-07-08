using Microsoft.EntityFrameworkCore;
using PersonelTakipOtomasyonuApi.Models;

namespace PersonelTakipOtomasyonuApi.Data
{
    public class PersonelDbContext : DbContext
    {
        public PersonelDbContext(DbContextOptions<PersonelDbContext> options) : base(options)
        {
        }

        public DbSet<Personel> Personeller { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Personel>().HasData(
                new Personel
                {
                    PersonelID = 1,
                    Ad = "Admin",
                    Soyad = "User",
                    DogumTarihi = new DateTime(1990, 1, 1),
                    TelefonNo = "5551234567",
                    Maas = 15000,
                    aktifMi = "Aktif",
                    yillikIzinHakki = 20,
                    IseBaslamaTarihi = new DateTime(2020, 1, 1),
                    TCKimlikNo = "12345678901",
                    Departman = "IT",
                    Pozisyon = "Yonetici",
                    Sifre = "123456",
                    Eposta = "admin@company.com"
                },
                new Personel
                {
                    PersonelID = 2,
                    Ad = "Test",
                    Soyad = "Personel",
                    DogumTarihi = new DateTime(1995, 5, 15),
                    TelefonNo = "5559876543",
                    Maas = 8000,
                    aktifMi = "Aktif",
                    yillikIzinHakki = 15,
                    IseBaslamaTarihi = new DateTime(2021, 3, 1),
                    TCKimlikNo = "11111111111",
                    Departman = "Satış",
                    Pozisyon = "Personel",
                    Sifre = "123456",
                    Eposta = "test@company.com"
                }
            );
        }
    }
} 