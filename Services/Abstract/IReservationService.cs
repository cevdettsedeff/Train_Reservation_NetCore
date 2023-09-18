using Train_Reservation.Models;

namespace Train_Reservation.Services.Abstract
{
    public interface IReservationService
    {
        public RezervasyonCevabi RezervasyonYap(RezervasyonIstegi istek);
    }
}
