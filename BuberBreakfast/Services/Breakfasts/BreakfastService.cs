using BuberBreakfast.Models;
using ErrorOr;
using BuberBreakfast.ServiceErrors;
using BuberBreakfast.Services.Breakfasts;

namespace BuberBreakfast.Services;

class BreakfastService : IBreakfastService
{
    private static readonly Dictionary<Guid, Breakfast> _breakfasts = new();
    public ErrorOr<Created> CreateBreakfast(Breakfast request)
    {
        _breakfasts.Add(request.Id, request);

        return Result.Created;
    }

 

    public ErrorOr<Breakfast> GetBreakfast(Guid id)
    {

        if(_breakfasts.TryGetValue(id, out var breakfast))

        {
            return breakfast;
        }



        return Errors.Breakfast.NotFound;
    }

    public ErrorOr<UpsertedBreakfastResult> UpsertBreakfast(Breakfast breakfast)
    {
        var IsNewlyCreated = !_breakfasts.ContainsKey(breakfast.Id);
        _breakfasts[breakfast.Id] = breakfast;

        return new UpsertedBreakfastResult(IsNewlyCreated);
    }

    public ErrorOr<Deleted> DeleteBreakfast(Guid id)
    {
        _breakfasts.Remove(id);

        return Result.Deleted;
    }
}