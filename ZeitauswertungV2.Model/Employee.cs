namespace ZeitauswertungV2.Model
{
    public class Employee
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public string LastName{ get; set; }
        public string FirstName { get; set; }
        public int MandatoryHours { get; set; }
    }
}