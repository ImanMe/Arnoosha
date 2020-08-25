namespace Arnoosha.Core.Interfaces
{
    public class QueryObject
    {
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }

        private int _pageIndex = 1;
        public int PageIndex
        {
            get => _pageIndex;
            set
            {
                if (value > 0)
                {
                    _pageIndex = value;
                }
            }
        }

        private int _pageSize = 5;
        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (value > 0)
                {
                    _pageSize = value;
                }
            }
        }

        private string _search;
        public string Search {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}
