using Train_Reservation.Models;
using Train_Reservation.Services.Abstract;

namespace Train_Reservation.Services.Concrete
{
    public class ReservationService : IReservationService
    {
        // Rezervasyon yapılması için gerekli olan kuralları burada tanımlıyoruz. İmza metodu ise IReservationService içerisinde oluşturuyoruz.
        public RezervasyonCevabi RezervasyonYap(RezervasyonIstegi istek)
        {
            var yerlesimAyrinti = new List<YerlesimAyrinti>(); //Yerlesim ayrıntılarını yazabilmek için bir liste oluşturuyoruz.
            var rezervasyonYapilabilir = false; //default olarak false değerini atıyoruz.

            if (istek.KisilerFarkliVagonlaraYerlestirilebilir)
            {
                foreach (var vagon in istek.Tren.Vagonlar)
                {
                    // Birden fazla vagon olabileceği için döngü oluşturuyoruz. Döngü sonucunda boş koltuk sayısını ve yüzde 70 dolu olup olmadığını hesaplıyoruz.
                    var bosKoltukSayisi = vagon.Kapasite - vagon.DoluKoltukAdet; // Öncelikle boş koltuk sayısını hesaplıyoruz.
                    var rezervasyonYapilabilecekKoltukSayisi = (int)(vagon.Kapasite * 0.7) - vagon.DoluKoltukAdet; // Doluluk oranını hesaplıyoruz. Hesaplanan değeri int değişkene dönüştürüyoruz.

                    if (rezervasyonYapilabilecekKoltukSayisi >= istek.RezervasyonYapilacakKisiSayisi)
                    {
                        rezervasyonYapilabilir = true;
                        yerlesimAyrinti.Add(new YerlesimAyrinti { VagonAd = vagon.Ad, KisiSayisi = istek.RezervasyonYapilacakKisiSayisi });
                        break;
                    }
                    else if (rezervasyonYapilabilecekKoltukSayisi > 0)
                    {
                        yerlesimAyrinti.Add(new YerlesimAyrinti { VagonAd = vagon.Ad, KisiSayisi = rezervasyonYapilabilecekKoltukSayisi });
                        istek.RezervasyonYapilacakKisiSayisi -= rezervasyonYapilabilecekKoltukSayisi; 
                    }
                }
            }
            else
            {
                var uygunVagon = istek.Tren.Vagonlar.FirstOrDefault(v => (int)(v.Kapasite * 0.7) - v.DoluKoltukAdet >= istek.RezervasyonYapilacakKisiSayisi);

                if (uygunVagon is not null)
                {
                    rezervasyonYapilabilir = true;
                    yerlesimAyrinti.Add(new YerlesimAyrinti { VagonAd = uygunVagon.Ad, KisiSayisi = istek.RezervasyonYapilacakKisiSayisi });
                }
            }

            var sonuc = new RezervasyonCevabi
            {
                RezervasyonYapilabilir = rezervasyonYapilabilir,

                YerlesimAyrinti = rezervasyonYapilabilir ? yerlesimAyrinti : new List<YerlesimAyrinti>()
                // Eğer rezervasyon yapılabilir değeri false ise boş bir array elde ediyoruz.
            };
            
            return sonuc;
        }
    }
}
