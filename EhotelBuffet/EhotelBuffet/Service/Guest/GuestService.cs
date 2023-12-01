namespace EhotelBuffet.Service.Guest
{
    using EhotelBuffet.Model;
    using EhotelBuffet.Service.Buffet;
    using RandomNameGeneratorLibrary;
    using System;
    using System.Collections.Generic;

    public class GuestService : IGuestService
    {
        private static readonly Random randoms = new Random();
        private static readonly PersonNameGenerator _personGenerator = new PersonNameGenerator();
        private static readonly BuffetService _buffetService = new BuffetService();
        private static GuestService instance;
        public List<Guest> guestList = new List<Guest>();
        HashSet<Guest> guestsForDay = new HashSet<Guest>();

        public static GuestService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GuestService();
                    instance.GenerateRandomGuest(DateTime.Now.AddDays(-360), DateTime.Now.AddDays(360));
                }
                return instance;
            }
        }
        public void NextCycle()
        {
            guestList.Clear();
            GenerateRandomGuest(DateTime.Now.AddDays(-360), DateTime.Now.AddDays(360));
            _buffetService.AddTime(TimeSpan.FromMinutes(30));
        }
        public List<Guest> GenerateRandomGuest (DateTime seasonStart, DateTime seasonEnd)
        {

            (int minGuests, int maxGuests) guests = (10001, 30000);

            int randomGuests = randoms.Next(guests.minGuests, guests.maxGuests);

            for (int i = 0; i < randomGuests; i++)
            {
                string randomName = _personGenerator.GenerateRandomFirstAndLastName();
                DateTime randomCheckIn = seasonStart.AddDays(randoms.Next((seasonEnd - seasonStart).Days));
                DateTime randomCheckOut = randomCheckIn.AddDays(randoms.Next(1, 7));
                GuestType randomType = (GuestType)randoms.Next(0, Enum.GetValues(typeof(GuestType)).Length);

                guestList.Add(new Guest(randomName, randomType, randomCheckIn, randomCheckOut));
            }
            return guestList;
        }

        public HashSet<Guest> GetGuestsForDay (List<Guest> guestsList, DateTime date)
        {
            foreach (Guest guest in guestsList)
            {
                if (guest is not null && guest.CheckIn <= date.Date && guest.CheckOut > date.Date)
                {
                    guestsForDay.Add(guest);
                }

            }
            return guestsForDay;
        }

        // YAGNI  <-------------

        //public void DisplayGuest ()
        //{
        //    (int minDays, int maxDays) days = (1, 7);

        //    DateTime seasonStart = DateTime.Now;
        //    DateTime seasonEnd = DateTime.Now.AddDays(randoms.Next(days.minDays, days.maxDays));
        //    List<Guest> randomGuests = GenerateRandomGuest(seasonStart, seasonEnd);

        //    foreach (var guest in randomGuests)
        //    {
        //        Console.WriteLine("Name: " + guest.Name);
        //        Console.WriteLine("Check-In: " + guest.CheckIn);
        //        Console.WriteLine("Check-Out: " + guest.CheckOut);
        //        Console.WriteLine("Type: " + guest.Type);
        //        Console.WriteLine();
        //    }
        //}
        public List<MealType> PreferencesMeal (GuestType guestType)
        {
            List<MealType> preferencesMeal = GuestTypeExtensions.GetMealPreferences(guestType);
            return preferencesMeal;
        }
        public HashSet<GuestType> GetGuestTypes (HashSet<Guest> lists)
        {
            HashSet<GuestType> guests = new HashSet<GuestType>();
            foreach (Guest guest in lists)
            {  
                guests.Add(guest.Type);
            }
            return guests;
        }
    }
}
