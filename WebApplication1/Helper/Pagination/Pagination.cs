namespace WebApplication1.Helper.Pagination
{
    public class Pagination<T>
    {
        public List<T> Datas { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public Pagination(List<T> datas,int currentPage,int totalPage)
        {
            Datas = datas;
            CurrentPage = currentPage;
            TotalPage = totalPage;
        }
    }
}
