using PersonelTakipOtonomSistemi.Dtos;
using PersonelTakipOtonomSistemi.Services;
using static System.Net.Mime.MediaTypeNames;

namespace PersonelTakipOtonomSistemi.Views;

public partial class YoneticiMenu : ContentPage
{
    private readonly IPersonelServices _personelServices;
    private Personel _personel;

    public void SetPersonel(Personel personel)
    {
        _personel = personel;
    }
    public YoneticiMenu()
	{
        InitializeComponent();
        _personelServices = new PersonelServices();
        
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await GetPersoneller();
        await UpdateStatistics();
    }
    
    private async Task GetPersoneller()
    {
        var pesoneller = await _personelServices.GetTumPersoneller();
        
    }
    
    private async Task UpdateStatistics()
    {
        try
        {
            var personeller = await _personelServices.GetTumPersoneller();
            await DisplayAlert("DEBUG", $"Personel listesi: {personeller.Count} kişi. İlk kişi: {(personeller.Count > 0 ? personeller[0].Ad : "yok")}", "Tamam");
            ToplamPersonelLabel.Text = personeller.Count.ToString();

            var aktifPersoneller = personeller.Where(p => p.AktifMi != null && p.AktifMi.ToLower().Contains("aktif")).ToList();
            AktifPersonelLabel.Text = aktifPersoneller.Count.ToString();

            var pasifPersoneller = personeller.Where(p => p.AktifMi != null && p.AktifMi.ToLower().Contains("pasif")).ToList();
            PasifPersonelLabel.Text = pasifPersoneller.Count.ToString();

            var izinAlanlar = personeller.Where(p => p.YillikIzinHakki < 0).ToList();
            IzinAlanlarLabel.Text = izinAlanlar.Count.ToString();

            var departmanlar = personeller.Where(p => !string.IsNullOrWhiteSpace(p.Departman)).Select(p => p.Departman.Trim()).Distinct().ToList();
            DepartmanLabel.Text = departmanlar.Count.ToString();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", $"İstatistikler güncellenirken hata oluştu: {ex.Message}", "Tamam");
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        PersonelGoruntule personelGoruntule = new PersonelGoruntule();
        await Navigation.PushAsync(personelGoruntule);
        
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        try
        {
        DateTime dogumTarihi = DateTime.Parse(DogumTarihi.Text.Trim());
            DateTime iseBaslamaTarihi = DateTime.Parse(IseBaslamaTarihi.Text.Trim());
        double maas = double.Parse(Maas.Text.Trim());
        int izin = int.Parse(izinHakki.Text.Trim());
            string aktifMi = aktif.SelectedItem?.ToString() ?? "Pasif";

        var eklencek = new PersonelOlusturDto()
        {
            Ad = Ad.Text.Trim(),
            Soyad = Soyad.Text.Trim(),
            TCKimlikNo = TcNo.Text.Trim(),
            Departman = Depertman.Text.Trim(),
            DogumTarihi = dogumTarihi,
                IseBaslamaTarihi = iseBaslamaTarihi,
            Pozisyon = Pozisyon.Text.Trim(),
            TelefonNo = TelefonNo.Text.Trim(),
            Eposta = Eposta.Text.Trim(),
            Sifre = Sifre.Text.Trim(),
            Maas = maas,
                aktifMi = aktifMi,
            yillikIzinHakki = izin,
        };
        await _personelServices.PersonelEkle(eklencek);

            await DisplayAlert("İşlem Başarılı", "Personel başarıyla kaydedildi", "Tamam");

        await GetPersoneller();
        await UpdateStatistics();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", $"Personel eklenirken hata oluştu: {ex.Message}", "Tamam");
        }
    }

    private async void Button_Clicked_2(object sender, EventArgs e)
    {
        int personelId;
        if (!int.TryParse(PersonelId.Text.Trim(), out personelId))
        {
            await DisplayAlert("Hata", "Lutfen gecerli bir personel ID'si girin.", "Tamam");
            return;
        }

        string ad = Ad.Text.Trim();
        string soyad = Soyad.Text.Trim();
        string tcKimlikNo = TcNo.Text.Trim();
        DateTime dogumTarihi;
        if (!DateTime.TryParse(DogumTarihi.Text.Trim(), out dogumTarihi))
        {
            await DisplayAlert("Hata", "Lutfen gecerli bir dogum tarihi girin.", "Tamam");
            return;
        }
        string eposta = Eposta.Text.Trim();
        string telefonNo = TelefonNo.Text.Trim();
        string pozisyon = Pozisyon.Text.Trim();
        string departman = Depertman.Text.Trim();
        DateTime iseBaslamaTarihi;
        if (!DateTime.TryParse(IseBaslamaTarihi.Text.Trim(), out iseBaslamaTarihi))
        {
            await DisplayAlert("Hata", "Lutfen gecerli bir ise baslama tarihi girin.", "Tamam");
            return;
        }
        double maas = double.Parse(Maas.Text.Trim());
        string aktifmi = aktif.SelectedItem?.ToString() ?? "Pasif";
        int yillikIzin = int.Parse(izinHakki.Text.Trim());

        var personelGuncelleDto = new PersonelGuncelleDto
        {
            PersonelID = personelId,
            Ad = ad,
            Soyad = soyad,
            TCKimlikNo = tcKimlikNo,
            DogumTarihi = dogumTarihi,
            Eposta = eposta,
            TelefonNo = telefonNo,
            Pozisyon = pozisyon,
            Departman = departman,
            IseBaslamaTarihi = iseBaslamaTarihi,
            Sifre = Sifre.Text,
            Maas = maas,
            aktifMi = aktifmi,
            yillikIzinHakki = yillikIzin,
        };

        await _personelServices.PersonelGuncelle(personelGuncelleDto);
        await DisplayAlert("Bilgi", "Personel basariyla guncellendi.", "Tamam");
        await UpdateStatistics();
    }

    private async void Button_Clicked_3(object sender, EventArgs e)
    {
        int personelId;
        if (!int.TryParse(PersonelId.Text.Trim(), out personelId))
        {
            await DisplayAlert("Hata", "Lutfen gecerli bir personel ID'si girin.", "Tamam");
            return;
        }

        var personeller = await _personelServices.GetTumPersoneller();
        var personel = personeller.FirstOrDefault(p => p.PersonelID == personelId);
        if (personel == null)
        {
            await DisplayAlert("Bilgi", "Bu ID'ye sahip bir personel bulunamadi.", "Tamam");
            return;
        }
        await _personelServices.PersonelSil(personelId);
        await DisplayAlert("Bilgi", "Personel basariyla silindi.", "Tamam");
        await UpdateStatistics();
    }

    private async void Button_Clicked_4(object sender, EventArgs e)
    {
        int personelId;
        if (!int.TryParse(PersonelId.Text.Trim(), out personelId))
        {
            await DisplayAlert("Hata", "Lutfen gecerli bir personel ID'si girin.", "Tamam");
            return;
        }
        var personeller = await _personelServices.GetTumPersoneller();
        var personel = personeller.FirstOrDefault(p => p.PersonelID == personelId);
        if (personel == null)
        {
            await DisplayAlert("Bilgi", "Bu ID'ye sahip bir personel bulunamadi.", "Tamam");
            return;
        }
        Ad.Text = personel.Ad;
        Soyad.Text = personel.Soyad;
        TcNo.Text = personel.TCKimlikNo;
        Depertman.Text = personel.Departman;
        DogumTarihi.Text = personel.DogumTarihi.ToString();
        Pozisyon.Text = personel.Pozisyon;
        TelefonNo.Text = personel.TelefonNo.ToString();
        Eposta.Text = personel.Eposta;
        IseBaslamaTarihi.Text = personel.IseBaslamaTarihi.ToString();
        Sifre.Text = personel.Sifre;
        Maas.Text = personel.Maas.ToString();
        aktif.SelectedItem = personel.AktifMi == "Aktif" ? "Aktif" : "Pasif";
        izinHakki.Text = personel.YillikIzinHakki.ToString();
    }
    
    private async void IzinAlanlar_Clicked(object sender, EventArgs e)
    {
        try
        {
            var personeller = await _personelServices.GetTumPersoneller();
            var izinAlanlar = personeller.Where(p => p.YillikIzinHakki < 0).ToList();
            
            if (izinAlanlar.Count == 0)
            {
                await DisplayAlert("Bilgi", "Şu anda izin alan personel bulunmamaktadır.", "Tamam");
                return;
            }
            
            string izinAlanlarListesi = "İZİN ALAN PERSONELLER:\n\n";
            
            foreach (var personel in izinAlanlar)
            {
                izinAlanlarListesi += $"👤 {personel.Ad} {personel.Soyad}\n";
                izinAlanlarListesi += $"   📋 Pozisyon: {personel.Pozisyon}\n";
                izinAlanlarListesi += $"   🏢 Departman: {personel.Departman}\n";
                izinAlanlarListesi += $"   📞 Telefon: {personel.TelefonNo}\n";
                izinAlanlarListesi += $"   📧 E-posta: {personel.Eposta}\n";
                izinAlanlarListesi += $"   🏖️ Kalan İzin: {personel.YillikIzinHakki} gün\n";
                izinAlanlarListesi += $"   📅 İşe Başlama: {personel.IseBaslamaTarihi:dd.MM.yyyy}\n";
                izinAlanlarListesi += "─────────────────────────────\n\n";
            }
            
            await DisplayAlert("İzin Alan Personeller", izinAlanlarListesi, "Tamam");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", $"İzin alan personeller listelenirken hata oluştu: {ex.Message}", "Tamam");
        }
    }

    private async void CikisYap_Clicked(object sender, EventArgs e)
    {
        bool cikisOnay = await DisplayAlert("Çıkış Onayı", 
            "Çıkış yapmak istediğinizden emin misiniz?", 
            "Evet", "Hayır");
            
        if (cikisOnay)
        {
            await Navigation.PopToRootAsync();
            await DisplayAlert("Çıkış Başarılı", 
                "Başarıyla çıkış yaptınız.", 
                "Tamam");
        }
    }
}