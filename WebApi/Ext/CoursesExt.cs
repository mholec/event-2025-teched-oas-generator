
using WebApi.Contracts;
using WebApi.Data;

namespace WebApi.Ext;

/// <summary>
/// Extensions pro urychení dema na TechEd
/// </summary>
public static  class CoursesExt
{
    public static IQueryable<GetWorkshopResult> ToContract(this IQueryable<Course> courses)
    {
        return courses.Select(x => new GetWorkshopResult()
        {
            Id = x.Id,
            Capacity = x.MaxCapacity,
            Price = x.Price,
            StartDate = x.StartDate,
            Slug = x.Slug,
            Title = x.Name
        });
    }
}