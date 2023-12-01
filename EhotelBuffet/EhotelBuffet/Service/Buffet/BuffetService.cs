

namespace EhotelBuffet.Service.Buffet;
using EhotelBuffet.Model;
using EhotelBuffet.Service.Guest;
using System;
using System.Collections.Generic;


public class BuffetService : IBuffetService
{
    private Buffet _buffet = new();
    private GuestService _guestService = new GuestService();
    private DateTime _time = DateTime.Now;

    public List<DateTime> MealsOnBuffet (MealType mealType)
    {
        return _buffet.FreshMeal(mealType);
    }

    public void AddTime (TimeSpan timeToAdd)
    {
        _time = _time.Add(timeToAdd);
    }

    public void Refill (MealType mealType, int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            _buffet.AddMeal(mealType);
        }
    }
    public void PrepareBuffet ()
    {
        var guestService = GuestService.Instance;
        var guestsForToday = _guestService.GetGuestsForDay(guestService.guestList, DateTime.Now);
        HashSet<GuestType> guestTypes = _guestService.GetGuestTypes(guestsForToday);
        foreach (GuestType guestType in guestTypes)
        {
            foreach (MealType mealType in _guestService.PreferencesMeal(guestType))
            {
                _buffet.AddMeal(mealType);
            }
        }
        _buffet.DisplayWholeBuffet();
    }

    public bool ConsumeFreshest (MealType mealType)
    {
        var mealBuffet = _buffet.GetMealPortion();
        if (mealBuffet.ContainsKey(mealType))
        {
            var freshestMeal = mealBuffet[mealType].OrderByDescending(date => date).FirstOrDefault();

            if (freshestMeal != default(DateTime))
            {
                mealBuffet[mealType].Remove(freshestMeal);
                if (mealBuffet[mealType].Count == 0)
                {
                    mealBuffet.Remove(mealType);
                }
                return true;
            }
        }

        return false;
    }

    public int CollectWaste ()
    {
        List<MealType> wastedMeal = new List<MealType>();
        int costWastedMeal = 0;
        var getBuffet = _buffet.GetMealPortion();
        foreach (var meal in getBuffet)
        {
            MealDurability mealDurability = MealTypeExtensions.GetDurability(meal.Key);
            if (mealDurability == MealDurability.Short)
            {
                foreach (DateTime dateTime in _buffet.FreshMeal(meal.Key))
                {
                    //DateTime currentTime = DateTime.Now;
                    //DateTime newTime = currentTime.AddMinutes(112);
                    var differenceTime = (_time - dateTime);
                    int differenceInMinutes = (int)differenceTime.TotalMinutes;
                    TimeSpan maxValidTime = TimeSpan.FromMinutes(30);
                    if (differenceInMinutes > (int)maxValidTime.TotalMinutes)
                    {
                        wastedMeal.Add(meal.Key);

                        _buffet.DeleteWastedMeal(meal.Key);
                    }
                }

            }
            if (mealDurability == MealDurability.Medium)
            {
                foreach (DateTime dateTime in _buffet.FreshMeal(meal.Key))
                {
                    var differenceTime = (_time - dateTime);
                    int differenceInMinutes = (int)differenceTime.TotalMinutes;
                    TimeSpan maxValidTime = TimeSpan.FromMinutes(90);
                    if (differenceInMinutes > (int)maxValidTime.TotalMinutes)
                    {
                        wastedMeal.Add(meal.Key);
                        costWastedMeal += MealTypeExtensions.GetCost(meal.Key);

                        _buffet.DeleteWastedMeal(meal.Key);
                    }
                }
            }
        }
        Console.WriteLine($"Zmarnowało sie {costWastedMeal} hiszpańskich pesos");
        return costWastedMeal;
    }
}

