namespace MVC.Models.Movie
{
    public class IndexVM
    {
        public FilterVM Filter { get; set; }
        public List<MovieVM> Items { get; set; }
        public PagerVM Pager { get; set; }
    }
}
