namespace Train_Reservation.Models
{
    public class RezervasyonCevabi
    {
        public bool RezervasyonYapilabilir { get; set; }
        public List<YerlesimAyrinti> YerlesimAyrinti { get; set; }
    }
}
