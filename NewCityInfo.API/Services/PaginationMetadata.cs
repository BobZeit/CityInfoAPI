namespace NewCityInfo.API.Services
{
    public class PaginationMetadata
    {
        public int TotalPagenumber { get; set; }
        public int PageSize {  get; set; }
        public int TotalItem {  get; set; }
        public int CurrentPage {  get; set; }

        public PaginationMetadata(int pageSize, int totalItem, int currentPage)
        {
            PageSize = pageSize;
            TotalItem = totalItem;
            CurrentPage = currentPage;
            TotalPagenumber = (int)Math.Ceiling(totalItem / (double)pageSize);

        }
    }
}
