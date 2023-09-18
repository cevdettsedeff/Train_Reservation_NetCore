using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Train_Reservation.Models;
using Train_Reservation.Services.Abstract;

namespace Train_Reservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService; //IoC kaydını program.cs içerisine yapıyoruz.

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost]
        public IActionResult RezervasyonYap([FromBody] RezervasyonIstegi istek)
        {
            var sonuc = _reservationService.RezervasyonYap(istek);

            if (sonuc is null)
            {
                return NotFound();
            }
            return Ok(sonuc);
        }
    }
        
}

