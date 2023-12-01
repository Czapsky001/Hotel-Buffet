using EhotelBuffet.Model;
using EhotelBuffet.Service.BreakfastManager;
using EhotelBuffet.Service.Buffet;
using EhotelBuffet.Service.Guest;
using System;

namespace EhotelBuffet.RunUi
{
    public class Run
    {
        public static void RunUi()
        {
            BuffetService buffetService = new BuffetService();
            GuestService guestService = new GuestService();
            Buffet buffet = new Buffet();
            BreakfastManager breakfastManager = new BreakfastManager(buffetService);

            int userInput;

            int newCycle = 0;
            do
            {
                Console.WriteLine(
                    "0. Wyjście\n"
                    + "1. Preparing dishes for breakfast\n"
                    + "2. Start simulating breakfast.\n"
                    + "3. Next cycle."
                );

                userInput = int.Parse(Console.ReadLine());

                switch (userInput)
                {
                    case 1:
                        buffetService.PrepareBuffet();
                        break;
                    case 2:
                        breakfastManager.Serve();
                        break;
                    case 3:
                        guestService.NextCycle();
                        newCycle++;
                        Console.WriteLine($"We are starting: {newCycle} breakfast");
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            } while (newCycle < 8 && userInput != 0);
            if(newCycle == 8)
            {
                Console.WriteLine("all of our guest's have been fed");
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }
    }
}
