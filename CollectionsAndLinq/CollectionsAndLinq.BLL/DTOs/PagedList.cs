namespace CollectionsAndLinq.BLL.DTOs;

public record PagedList<T>(
    List<T> Items,
    int TotalCount)
{

}
