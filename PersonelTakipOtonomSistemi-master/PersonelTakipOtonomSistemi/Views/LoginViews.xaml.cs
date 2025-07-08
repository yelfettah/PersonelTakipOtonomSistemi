using PersonelTakipOtonomSistemi.Services;

namespace PersonelTakipOtonomSistemi.Views;

public partial class LoginViews : ContentPage
{
    private readonly IPersonelServices _personelServices;
    public LoginViews()
	{
		InitializeComponent();
        _personelServices = new PersonelServices();
        
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var gelenTcNo = tcEntry.Text;
        var gelenPassword = sifreEntry.Text;

        var personel = await _personelServices.Login(gelenTcNo, gelenPassword);

        if (personel == null)
        {
            await DisplayAlert("Hata", "TC veya şifre yanlış ya da kullanıcı aktif değil.", "Tamam");
            return;
        }

        if (personel.AktifMi != "Aktif")
        {
            await DisplayAlert("Hata", $"Personel durumu: {personel.AktifMi}", "Tamam");
            return;
        }

        await DisplayAlert($"{personel.Ad} {personel.Soyad}", "Hoş Geldiniz.", "Tamam");
        PersonelMenu personelMenu = new PersonelMenu();
        personelMenu.SetPersonel(personel);
        await Navigation.PushAsync(personelMenu);
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        PasswordForget sifreUnuttum = new PasswordForget();
        await Navigation.PushAsync(sifreUnuttum);
    }
}