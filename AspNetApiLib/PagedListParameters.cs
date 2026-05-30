namespace MyAspNetApiLib
{
    public class PagedListParameters
    {
        const int maxPageSize = 50; // Limite de segurança

        public int PageNumber { get; set; } = 1; // Página padrão é a 1

        private int _pageSize = 10; // Tamanho padrão da página

        public bool OrderByDesc { get; set; }

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > maxPageSize ? maxPageSize : value; }
        }
    }
}
