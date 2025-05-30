namespace WebApi.Data;

public class Course
{
    public string Id { get; set; }
    public string Slug { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public int MaxCapacity { get; set; }
    public decimal Price { get; set; }
}