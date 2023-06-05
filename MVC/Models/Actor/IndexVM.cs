namespace MVC.Models.Actor
{
    public class IndexVM
    {
        public FilterVM Filter { get; set; }
        public List<ActorVM> Items { get; set; }
        public PagerVM Pager { get; set; }
    }
}
