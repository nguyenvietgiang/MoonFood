
namespace MoonModels.Paging
{
    public interface IPagination
    {
        int CurrentPage { get; set; }
        int TotalPages { get; set; }
        int PageSize { get; set; }
        int NumberOfRecords { get; set; }
        int TotalRecords { get; set; }
        IEnumerable<object> Content { get; set; }
    }
}
