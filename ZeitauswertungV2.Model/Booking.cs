using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZeitauswertungV2.Model
{
    [Table("tblAuftragStunden")]
    public class Booking
    {
        [Key]
        [Column("idAuftragStunden")]
        public int Id { get; set; }
        [Column("idBearbeiter")]
        public string Employee { get; set; }
        [Column("idAuftrag")]
        public int? Assignment { get; set; }//todo: in auftrag umwandeln
        [Column("Datum")]
        public DateTime Date { get; set; }
        [Column("Bemerkung")]
        public string Comment { get; set; }
        [Column("Von")]
        public DateTime BookingFrom { get; set; }
        [Column("Bis")]
        public DateTime BookingTill { get; set; }
        [Column("Gelöscht")]
        
        public bool Deleted { get; set;}
        [Column("idZeittyp")]
        public string BookingTyp { get; set; }//todo: in Zeittyp umwandeln
        [NotMapped]
        public TimeSpan Duration { get
            {
                return (BookingTill - BookingFrom);
            } }

    }
}
