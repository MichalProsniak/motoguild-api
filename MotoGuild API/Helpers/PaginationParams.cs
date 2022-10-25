namespace MotoGuild_API.Helpers;

public class PaginationParams
{
    private readonly int _maxItemsPerPage = 10;
    private int itemsPerPage;


    public int Page { get; set; } = 1;

    public int ItemsPerPage
    {
        get => itemsPerPage;
        set => itemsPerPage = value > _maxItemsPerPage ? _maxItemsPerPage : value;
    }
}