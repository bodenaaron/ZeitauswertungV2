using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZeitauswertungV2.Model
{
    [Table("tblBearbeiter")]
    public class Employee
    {
        [Key]
        [Column("idBearbeiter")]
        public string Id { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("Telefon")]
        public string Phonenumber { get; set; }
        [Column("Bearbeiter_Name")]
        public string LastName{ get; set; }
        [Column("Bearbeiter_Vorname")]
        public string FirstName { get; set; }
        [Column("SollStunden")]
        public Single MandatoryHours { get; set; }        
    }
}