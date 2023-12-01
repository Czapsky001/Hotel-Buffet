namespace EhotelBuffet.Service.Guest;

using EhotelBuffet.Model;

public interface IGuestService
{
    public HashSet<Guest> GetGuestsForDay(List<Guest> guests, DateTime date);
}