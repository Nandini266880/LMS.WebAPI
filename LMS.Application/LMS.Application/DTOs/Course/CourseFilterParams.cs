namespace LMS.Application.DTOs.Course
{

    public class CourseFilterParams : PaginationParams
    {
        public string? Search { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public string? SortBy { get; set; } = "CreatedAt";
        public string? SortOrder { get; set; } = "desc";
    }
    public class PaginationParams
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}
