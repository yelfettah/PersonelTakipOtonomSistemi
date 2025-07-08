using PersonelTakipOtonomSistemi.Dtos;
using PersonelTakipOtonomSistemi.Services;

namespace PersonelTakipOtonomSistemi.Views;

public partial class PasswordForget : ContentPage
{
	private readonly IPersonelServices _personelServices;
	public PasswordForget()
	{
        InitializeComponent();
		_personelServices = new PersonelServices();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {

		
        
		if (isim.Text != null && soyad.Text != null && personelId.Text != null && tcNo.Text != null && yeniSifre.Text != null && tekradanYeniSifre != null)
		{
			var kullaniciAdi = isim.Text.Trim();
			var kullaniciSoyadi = soyad.Text.Trim();
			var tcno = tcNo.Text.Trim();
			var yenisifre = tekradanYeniSifre.Text.Trim();
            var DogumTarihi = dogumTarihi.Date;


            if (int.TryParse(personelId.Text.Trim(), out int parsedPersonelId))
			{
			if ( kullaniciAdi != null && kullaniciSoyadi != null && yeniSifre.Text == tekradanYeniSifre.Text)
			{
                    var personeller = await _personelServices.GetTumPersoneller();
                    var personel = personeller.FirstOrDefault(x => x.TCKimlikNo == tcno);
					if (personel != null)
					{
						if (personel.Sifre != yenisifre) {
						if (personel.Ad.Trim() == kullaniciAdi && personel.Soyad.Trim() == kullaniciSoyadi && personel.PersonelID == parsedPersonelId && personel.DogumTarihi == DogumTarihi)
						{

							var guncellencek = new PersonelGuncelleDto
							{
								Ad = personel.Ad,
								Soyad = personel.Soyad,
								aktifMi = personel.AktifMi,
								Departman = personel.Departman,
								DogumTarihi = personel.DogumTarihi,
								Sifre = yenisifre,
								Eposta = personel.Eposta,
								IseBaslamaTarihi = personel.IseBaslamaTarihi,
								Maas = personel.Maas,
								PersonelID = personel.PersonelID,
								Pozisyon = personel.Pozisyon,
								TCKimlikNo = personel.TCKimlikNo,
								TelefonNo = personel.TelefonNo,
								yillikIzinHakki = personel.YillikIzinHakki
							};

							await _personelServices.PersonelGuncelle(guncellencek);
							await DisplayAlert("", "Sifre Basarili Sekilde Degistirildi.", "Tamam");
						}
						else
						{

							await DisplayAlert("", "Yanlis Giris Yaptiniz", "Tamam");

						}
					}
					else
					{

							await DisplayAlert("","Ayni Sifreyi Tekrar Kullanamazsiniz","Tamam");

					}
					}
					else
					{
						await DisplayAlert("","Yanlis Giris Yaptiniz","Tamam");
					}
			}
			else
			{
				await DisplayAlert("", "Eksik Ya da Hatali Giris Yaptiniz", "Tamam");

			}
			}
            else
            {
                
                await DisplayAlert("Hata", "Personel ID gecerli bir tam sayi degil.", "Tamam");
            }
        }
		else
		{
			await DisplayAlert("", "Eksik Giris Yaptiniz", "Tamam");
		}
    }

    private async void Cikis2_Clicked(object sender, EventArgs e)
    {
        // Ana menüye dön
        await Navigation.PopAsync();
        await DisplayAlert("Çıkış", "Şifre değiştirme sayfasından çıkıldı.", "Tamam");
    }
}