using PersonelTakipOtonomSistemi.Dtos;
using PersonelTakipOtonomSistemi.Services;

namespace PersonelTakipOtonomSistemi.Views;

public partial class PersonelGoruntule : ContentPage
{
    private readonly IPersonelServices _personelServices;
    public PersonelGoruntule()
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
        var personeller = await _personelServices.GetTumPersoneller();
        
        collectionViewPersonel.ItemsSource = personeller;
        
        var aktifPersonelSayisi = personeller.Count(p => p.AktifMi == "Aktif");
        var pasifPersonelSayisi = personeller.Count(p => p.AktifMi == "Pasif");
        
        await DisplayAlert("Personel Durumu", 
            $"Toplam Personel: {personeller.Count}\n" +
            $"Aktif Personel: {aktifPersonelSayisi}\n" +
            $"Pasif Personel: {pasifPersonelSayisi}", "Tamam");
    }
}