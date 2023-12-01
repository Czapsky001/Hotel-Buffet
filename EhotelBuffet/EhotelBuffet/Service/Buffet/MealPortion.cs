using EhotelBuffet.Model;

namespace EhotelBuffet.Service.Buffet
{
    public record MealPortion(GuestType type, MealType mealType, MealDurability durability, int count, DateTime timestamp);
}
