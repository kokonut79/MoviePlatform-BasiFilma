namespace MVC.Models.Studio
{
    public class IndexVM
    {
        public FilterVM Filter { get; set; }

        public List<StudioVM> Items { get; set; }
        public PagerVM Pager { get; set; }
    }
}
