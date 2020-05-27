using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeitauswertungV2.Model
{
    [Table("tblZeitkontenAnpassung")]
    public class TimeAccountAdjustment
    {
        [Key]
        public int Id { get; set; }
        [Column("DatumAngepasst")]
        public DateTime DateOfAdjustment{ get; set; }
        [Column("ZeitkontoAngepasstStunden")]
        public int TimeAccountAdjustedHours{ get; set; }
        [Column("ZeitkontoAngepasstMinuten")]
        public int TimeAccountAdjustedMinutes { get; set; }
        [Column("Bearbeiter")]
        public string Employee { get; set; }

    }
}
