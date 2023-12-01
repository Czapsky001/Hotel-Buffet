using EhotelBuffet.Model;

namespace EhotelBuffet.Service.Buffet;

public interface IBuffetService
{
    public void Refill(MealType mealType, int Count);
    bool ConsumeFreshest(MealType mealType);
    List<DateTime> MealsOnBuffet(MealType mealType);

    int CollectWaste();
}