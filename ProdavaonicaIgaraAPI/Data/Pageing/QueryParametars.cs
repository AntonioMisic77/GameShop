namespace ProdavaonicaIgaraAPI.Data.Pageing
{
    public class QueryParametars
    {
        #region properties

        private int _pageSize = 10;

        public int StartIndex { get; set; }

        public int PageNumber { get; set; }

        public string? filterText { get; set; }


        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }
        
        #endregion
    }
}
