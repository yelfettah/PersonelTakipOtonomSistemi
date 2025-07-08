using PersonelTakipOtonomSistemi.Dtos;
using PersonelTakipOtonomSistemi.Services;

namespace PersonelTakipOtonomSistemi.Views;

public partial class YoneticiLogin : ContentPage
{
    private readonly IPersonelServices _personelServices;
    public YoneticiLogin()
	{
		InitializeComponent();
        _personelServices = new PersonelServices();
    }
    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        await GetPersoneller();
    }
    private async Task GetPersoneller()
    {
        var pesoneller = await _personelServices.GetTumPersoneller();
    }
    private async void Button_Clicked(object sender, EventArgs e)
    {
        var gelenTcNo = kullanıcıAdı.Text;
        var gelenKullaniciSifresi = sifre.Text;
        
        if(gelenTcNo != null && gelenKullaniciSifresi != null) 
        { 
            var personel = await _personelServices.Login(gelenTcNo, gelenKullaniciSifresi);

            if (personel != null && personel.AktifMi == "Aktif")
            {
                YoneticiMenu yoneticiMenu = new YoneticiMenu();
                yoneticiMenu.SetPersonel(personel);
                await Navigation.PushAsync(yoneticiMenu);
                await DisplayAlert($"{personel.Ad} {personel.Soyad}", $"Hoşgeldiniz.", "Tamam");
            }
            else
            {
                await DisplayAlert("Hata", "Eksik veya Hatalı Giriş", "Tamam");
            }
        }
        else
        {
            await DisplayAlert("Hata","Eksik Giriş Yaptınız","Tamam");
        }
    }
}
