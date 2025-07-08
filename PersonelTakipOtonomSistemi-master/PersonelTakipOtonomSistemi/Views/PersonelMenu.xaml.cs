using PersonelTakipOtonomSistemi.Dtos;
using PersonelTakipOtonomSistemi.Dtos;
using PersonelTakipOtonomSistemi.Services;

namespace PersonelTakipOtonomSistemi.Views;

public partial class PersonelMenu : ContentPage
{

    private Personel _personel;
    private readonly IPersonelServices _personelServices;

    public void SetPersonel(Personel personel)
    {
        _personel = personel;
    }
    public PersonelMenu()
    {
        InitializeComponent();
        _personelServices = new PersonelServices();
    }

    private async void getir_Clicked(object sender, EventArgs e)
    {

        var personeller = await _personelServices.GetTumPersoneller();
        var personel = personeller.FirstOrDefault(x => x.PersonelID == _personel.PersonelID);
        _personel = personel;

        Ad.Text = _personel.Ad.ToString();
        Soyad.Text = _personel.Soyad.ToString();
        TcNo.Text = _personel.TCKimlikNo.ToString();
        DogumTarihi.Text = _personel.DogumTarihi.ToString();
        Eposta.Text = _personel.Eposta.ToString();
        IseBaslamaTarihi.Text = _personel.IseBaslamaTarihi.ToString();
        TelefonNo.Text = _personel.TelefonNo.ToString();
        Pozisyon.Text = _personel.Pozisyon.ToString();
        Depertman.Text = _personel.Departman.ToString();
        maas.Text = _personel.Maas.ToString();
        aktif.Text = _personel.AktifMi.ToString();
        izin.Text = _personel.YillikIzinHakki.ToString();
    }

    private async void SifreDegistir_Clicked(object sender, EventArgs e)
    {
        var EskiSifre = eskiSifre.Text;
        var YeniSifre = yeniSifre.Text;
        var TekrardanYeniSifre = yeniSifre.Text;

        var Select = await DisplayAlert("", "Degistirmek istediginize Emin misin.", "Evet", "Hayir");
        if (Select == true)
        {
            if (EskiSifre != null && YeniSifre != null && TekrardanYeniSifre != null && YeniSifre == TekrardanYeniSifre && _personel.Sifre.ToString() == EskiSifre)
            {
                var personelGuncelleDto = new PersonelGuncelleDto
                {
                    PersonelID = _personel.PersonelID,
                    Ad = _personel.Ad,
                    Soyad = _personel.Soyad,
                    TCKimlikNo = _personel.TCKimlikNo,
                    DogumTarihi = _personel.DogumTarihi,
                    Eposta = _personel.Eposta,
                    TelefonNo = _personel.TelefonNo,
                    Pozisyon = _personel.Pozisyon,
                    Departman = _personel.Departman,
                    IseBaslamaTarihi = _personel.IseBaslamaTarihi,
                    Sifre = YeniSifre,
                    aktifMi = _personel.AktifMi,
                    Maas = _personel.Maas,
                    yillikIzinHakki = _personel.YillikIzinHakki,

                };

                await _personelServices.PersonelGuncelle(personelGuncelleDto);
                DisplayAlert("", "Basarili Sifre Degistirme", "Tamam");
            }
            else
            {
                DisplayAlert("Uyari", "Hatali Sifre Girisimi", "Tamam");
            }

        }

    }

    private async void SifreDegistir2_Clicked(object sender, EventArgs e)
    {
        var passwordForget = new PasswordForget();
        await Navigation.PushAsync(passwordForget);
    }

    private async void talepEt_Clicked(object sender, EventArgs e)
    {
        var personeller = await _personelServices.GetTumPersoneller();
        var personel = personeller.FirstOrDefault(x => x.PersonelID == x.PersonelID);

        var gunceldurum = "Izinli";
        if (istenilenMiktar.Text != null && personel.AktifMi != "Ayrildi") { 
            var istenilenGunMiktari = int.Parse(istenilenMiktar.Text.Trim());

        var guncelDurum = _personel.YillikIzinHakki - istenilenGunMiktari;
        if (_personel.YillikIzinHakki > 0 && istenilenGunMiktari > 0)
        {
            if (_personel.YillikIzinHakki >= istenilenGunMiktari)
            {
                var guncellenecek = new PersonelGuncelleDto
                {
                    PersonelID = _personel.PersonelID,
                    Eposta = _personel.Eposta,
                    Pozisyon = _personel.Pozisyon,
                    Ad = _personel.Ad,
                    Sifre = _personel.Sifre,
                    Soyad = _personel.Soyad,
                    aktifMi = gunceldurum,
                    Departman = _personel.Departman,
                    DogumTarihi = _personel.DogumTarihi,
                    IseBaslamaTarihi = _personel.IseBaslamaTarihi,
                    Maas = _personel.Maas,
                    TCKimlikNo = _personel.TCKimlikNo,
                    TelefonNo = _personel.TelefonNo,
                    yillikIzinHakki = guncelDurum,
                };

                await _personelServices.PersonelGuncelle(guncellenecek);
                DisplayAlert("Islem Basarili", $"{istenilenGunMiktari} Gun Izin Alinmistir. Geriye Kalan Gun Miktari {guncelDurum}", "Tamam");
                izin.Text = guncelDurum.ToString();
                aktif.Text = gunceldurum.ToString();
            }
            else
            {
                await DisplayAlert("", $"Istenilen Miktar {_personel.YillikIzinHakki} ustunde Oldugundan Islem Saglanamiyor.", "Tamam");
            }
        }
        else
        {
            await DisplayAlert("", "Yillik Izin Hakkiniz Bulunmamaktadir", "Tamam");
        }
     }
        else
        {
            DisplayAlert("", "Herhangi Bir Deger Girilmemistir ya da Isten Ayrilma Durumu Vardir", "Tamam");
        }
       
    }

    private async void aktifOl_Clicked(object sender, EventArgs e)
    {
        var personeller = await _personelServices.GetTumPersoneller();
        var personel = personeller.FirstOrDefault(x => x.PersonelID == _personel.PersonelID);
        bool select = await DisplayAlert("UYARI","Izinden Donus Yapmak istediginize Emin Misiniz","Evet","Hayir");

        if ( select == true && personel.AktifMi.Trim() != "Ayrildi")
        {
            var GuncelDurum = "Aktif";
            var guncellenecek = new PersonelGuncelleDto
            {
                PersonelID = _personel.PersonelID,
                Eposta = _personel.Eposta,
                Pozisyon = _personel.Pozisyon,
                Ad = _personel.Ad,
                Sifre = _personel.Sifre,
                Soyad = _personel.Soyad,
                aktifMi = GuncelDurum,
                Departman = _personel.Departman,
                DogumTarihi = _personel.DogumTarihi,
                IseBaslamaTarihi = _personel.IseBaslamaTarihi,
                Maas = _personel.Maas,
                TCKimlikNo = _personel.TCKimlikNo,
                TelefonNo = _personel.TelefonNo,
                yillikIzinHakki = _personel.YillikIzinHakki,
            };
            aktif.Text = GuncelDurum.ToString();

            await _personelServices.PersonelGuncelle(guncellenecek);
            await DisplayAlert("","Basariliyla Ise Donus Gerceklestirdiniz","Tamam");
        }
        else
        {
            DisplayAlert("","Isten Ayrildiginiz icin Gerceklestiremiyoruz","Tamam");
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var personeller = await _personelServices.GetTumPersoneller();
        var personel = personeller.FirstOrDefault(x => x.PersonelID == _personel.PersonelID);

        var istenAyrilma = new IstenAyrılma();
        if(personel != null) { 
            istenAyrilma.setPersonel(personel);
            await Navigation.PushAsync(istenAyrilma);
        }
        else
        {
            DisplayAlert("","Bilgiler Aktarilamadi","Tamam");
        }
    }

    private async void CikisYap_Clicked(object sender, EventArgs e)
    {
        bool cikisOnay = await DisplayAlert("Çıkış Onayı", 
            "Çıkış yapmak istediğinizden emin misiniz?", 
            "Evet", "Hayır");
            
        if (cikisOnay)
        {
            // Ana ekrana dön
            await Navigation.PopToRootAsync();
            await DisplayAlert("Çıkış Başarılı", 
                "Başarıyla çıkış yaptınız.", 
                "Tamam");
        }
    }
}
