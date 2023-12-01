using EhotelBuffet.Service.Buffet;

namespace EhotelBuffet.Model;

public record Buffet()
{
    public Dictionary<MealType, List<DateTime>> mealPortion = new Dictionary<MealType, List<DateTime>>();

    public Dictionary<MealType, List<DateTime>> GetMealPortion()
    {

        return mealPortion;
    }
    public void AddMeal(MealType mealType)
    {
        DateTime dateTime = DateTime.Now;
        if (mealPortion.TryGetValue(mealType, out var mealListDate))
        {
            mealListDate.Add(dateTime);
        }
        else
        {
            mealPortion[mealType] = new List<DateTime> { dateTime };
        }
    }

    public List<DateTime> FreshMeal(MealType mealType)
    {
        if (mealPortion.ContainsKey(mealType))
        {
            return mealPortion[mealType].OrderByDescending(date => date).ToList();
        }
        return new List<DateTime>();
    }

    public void DisplayWholeBuffet()
    {
        foreach (var kvp in mealPortion)
        {
            string dates = string.Join(", ", kvp.Value.Select(date => date.ToString()));
            Console.WriteLine("food is being prepared: ");
            Console.WriteLine($"Food on the table : {kvp.Key}\nTime of food being put on the table: {dates}");
            Console.WriteLine();
        }
    }
    public void DeleteWastedMeal(MealType mealType)
    {
        mealPortion.Remove(mealType);
    }


}