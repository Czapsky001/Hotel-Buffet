namespace EhotelBuffet.Service.BreakfastManager
{
    using EhotelBuffet.Model;
    using EhotelBuffet.Service.Buffet;
    using EhotelBuffet.Service.Guest;

    public class BreakfastManager
    {
        private Random random = new Random();
        private BuffetService _buffetService;
        public BreakfastManager(BuffetService buffetService)
        {
            _buffetService = buffetService;

        }

        public void Serve()
        {
            var guestService = GuestService.Instance;
            var guestsForToday = guestService.GetGuestsForDay(guestService.guestList, DateTime.Now);
            var guestTypes = guestService.GetGuestTypes(guestsForToday);
            foreach (GuestType type in guestTypes)
            {
                List<MealType> preferencesMeal = guestService.PreferencesMeal(type);
                var randomMeal = preferencesMeal[random.Next(0, preferencesMeal.Count)];
                var stillOnBuffet = _buffetService.MealsOnBuffet(randomMeal);
                _buffetService.ConsumeFreshest(randomMeal);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Food has been eaten: {randomMeal}");
            }
            _buffetService.CollectWaste();

        }
    }
}
