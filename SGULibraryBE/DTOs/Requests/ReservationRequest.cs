using SGULibraryBE.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGULibraryBE.DTOs.Requests
{
    public class ReservationRequest
    {
        public int Quantity { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateBorrow { get; set; }
        public DateTime DateReturn { get; set; }
        public long UserId { get; set; }
        public long DeviceId { get; set; }
    }
}
