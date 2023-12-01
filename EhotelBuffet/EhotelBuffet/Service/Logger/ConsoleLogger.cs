
namespace EhotelBuffet.Service.Logger;

public class ConsoleLogger : ILogger
{
    public void Error(string message)
    {
        Console.WriteLine("Error" + message);
    }

    public void Info(string message)
    {
        Console.WriteLine("Info" + message);
    }
}
