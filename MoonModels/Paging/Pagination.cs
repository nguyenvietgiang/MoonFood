﻿
namespace MoonModels.Paging
{
    public class Pagination<T> : IPagination
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecords { get; set; }
        public int TotalRecords { get; set; }
        public IEnumerable<T> Content { get; set; }
        IEnumerable<object> IPagination.Content { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Pagination()
        {
            PageSize = 20;
            CurrentPage = 1;
        }

        public Pagination(int currentPage, int pageSize)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
        }

        public Pagination(int totalRecords, int currentPage, int pageSize)
        {
            TotalRecords = totalRecords;
            CurrentPage = currentPage;
            PageSize = pageSize;
            NumberOfRecords = totalRecords;
            double totalPages = Math.Ceiling((double)totalRecords / pageSize);
            TotalPages = (int)totalPages;
        }

        public Pagination(IEnumerable<T> content, int totalRecords, int currentPage, int pageSize)
        {
            Content = content;
            TotalRecords = totalRecords;
            CurrentPage = currentPage;
            PageSize = pageSize;
            NumberOfRecords = totalRecords;
            double totalPages = Math.Ceiling((double)totalRecords / pageSize);
            TotalPages = (int)totalPages;
        }
    }
}
