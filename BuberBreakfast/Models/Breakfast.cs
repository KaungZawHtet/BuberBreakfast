using BuberBreakfast.Contracts;
using BuberBreakfast.ServiceErrors;
using ErrorOr;

namespace BuberBreakfast.Models;

public class Breakfast
{
    private Breakfast(
        Guid id,
        string name,
        string description,
        DateTime startDateTime,
        DateTime endDateTime,
        DateTime lastModifiedDateTime,
        List<string> savory,
        List<string> sweet)
    {
        Id = id;
        Name = name;
        Description = description;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        LastModifiedDateTime = lastModifiedDateTime;
        Savory = savory;
        Sweet = sweet;
    }

    public const int MinNameLength = 3;
    public const int MaxNameLength = 50;
    public const int MinDescriptionLength = 30;
    public const int MaxDescriptionLength = 150;

    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public DateTime StartDateTime { get; }
    public DateTime EndDateTime { get; }
    public DateTime LastModifiedDateTime { get; }
    public List<string> Savory { get; }
    public List<string> Sweet { get; }


   


    public static ErrorOr<Breakfast> Create(
            string name,
            string description,
            DateTime startDateTime,
            DateTime endDateTime,
         
            List<string> savory,
            List<string> sweet,
            
            Guid? id = null
            )
    {
        List<Error> errors = new List<Error>();

        { //validation block  
            if ((name.Length < MinNameLength) || (name.Length > MaxNameLength)) errors.Add(Errors.Breakfast.InvalidName);
            if ((description.Length < MinNameLength) || (description.Length > MaxNameLength)) errors.Add(Errors.Breakfast.InvalidDescription);
            if (errors.Count > 0) return errors;
        }

        return new Breakfast(
            id?? Guid.NewGuid(),
            name,
            description,
            startDateTime,
            endDateTime,
            DateTime.UtcNow,
            savory,
            sweet
        );

    }


    public static ErrorOr<Breakfast> From (CreateBreakfastRequest request)
    
    {

        return Create(

            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,

            request.Savory,
            request.Sweet
        );

    }

    public static ErrorOr<Breakfast> From(Guid id, UpsertBreakfastRequest request)

    {

        return Create(

            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,

            request.Savory,
            request.Sweet,

            id
        );

    }


}