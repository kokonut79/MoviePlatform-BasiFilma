namespace MVC.Models.Actor
{
    public class ActorVM
    {
        public int ActorId { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public int? MovieId { get; set; }
    }
}
