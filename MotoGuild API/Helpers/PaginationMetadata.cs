namespace MotoGuild_API.Helpers;

public class PaginationMetadata
{
    public PaginationMetadata(int totalCount, int currentPage, int itemsPerPage)
    {
        TotalCount = totalCount;
        CurrentPage = currentPage;
        TotalPages = (int) Math.Ceiling(totalCount / (double) itemsPerPage);
    }

    public int CurrentPage { get; }
    public int TotalCount { get; }
    public int TotalPages { get; }
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;
}