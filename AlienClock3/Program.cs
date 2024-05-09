using System;
using System.Threading;

class AlienClock
{
    const double EarthToAlienTimeRatio = 2.0;
    const int SecondsInAlienMinute = 90;
    const int MinutesInAlienHour = 90;
    const int HoursInAlienDay = 36;
    const int MonthsInAlienYear = 18;
    static readonly int[] DaysInAlienMonths = { 44, 42, 48, 40, 48, 44, 40, 44, 42, 40, 40, 42, 44, 48, 42, 40, 44, 38 };

    const int SecondsInEarthMinute = 60;

    static int currentYear = 2804;
    static int currentMonth = 18;
    static int currentDay = 31;
    static int currentHour = 2;
    static int currentMinute = 2;
    static int currentSecond = 88;

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Alien Clock!");
        Console.WriteLine("Press Ctrl + C to exit.");

        Thread clockThread = new Thread(UpdateClock);
        clockThread.Start();

        while (true)
        {
            Console.WriteLine("\nCurrent Alien Time:");
            DisplayClock();
            Console.WriteLine("\n1. Set Date and Time");
            Console.WriteLine("2. Exit");

            int choice = GetChoice(1, 2);

            switch (choice)
            {
                case 1:
                    SetDateAndTime();
                    break;
                case 2:
                    Environment.Exit(0);
                    break;
            }
        }
    }

    static void UpdateClock()
    {
        while (true)
        {
            Thread.Sleep((int)(1000 / EarthToAlienTimeRatio));
            UpdateTime();
            DisplayClock();
        }
    }

    static void UpdateTime()
    {
        currentSecond++;
        if (currentSecond >= SecondsInAlienMinute)
        {
            currentSecond = 0;
            currentMinute++;
            if (currentMinute >= MinutesInAlienHour)
            {
                currentMinute = 0;
                currentHour++;
                if (currentHour >= HoursInAlienDay)
                {
                    currentHour = 0;
                    currentDay++;
                    if (currentDay > DaysInAlienMonths[currentMonth - 1])
                    {
                        currentDay = 1;
                        currentMonth++;
                        if (currentMonth > MonthsInAlienYear)
                        {
                            currentMonth = 1;
                            currentYear++;
                        }
                    }
                }
            }
        }
    }

    // Function to display the Alien clock
    static void DisplayClock()
    {
        Console.WriteLine($"Year {currentYear}, Month {currentMonth}, Day {currentDay}, Hour {currentHour}, Minute {currentMinute}, Second {currentSecond}");
    }

    // Function to set the date and time
    static void SetDateAndTime()
    {
        Console.Write("Enter Year: ");
        currentYear = GetIntInput();

        Console.Write("Enter Month (1-18): ");
        currentMonth = GetIntInput(1, 18);

        Console.Write($"Enter Day (1-{DaysInAlienMonths[currentMonth - 1]}): ");
        currentDay = GetIntInput(1, DaysInAlienMonths[currentMonth - 1]);

        Console.Write("Enter Hour (0-35): ");
        currentHour = GetIntInput(0, 35);

        Console.Write("Enter Minute (0-89): ");
        currentMinute = GetIntInput(0, 89);

        Console.Write("Enter Second (0-89): ");
        currentSecond = GetIntInput(0, 89);

        Console.WriteLine("Date and time set successfully.");
    }

    static int GetIntInput()
    {
        int input;
        while (!int.TryParse(Console.ReadLine(), out input))
        {
            Console.WriteLine("Invalid input. Please enter an integer.");
        }
        return input;
    }

    static int GetIntInput(int min, int max)
    {
        int input;
        while (!int.TryParse(Console.ReadLine(), out input) || input < min || input > max)
        {
            Console.WriteLine($"Invalid input. Please enter an integer between {min} and {max}.");
        }
        return input;
    }

    static int GetChoice(int min, int max)
    {
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < min || choice > max)
        {
            Console.WriteLine($"Invalid choice. Please enter a number between {min} and {max}.");
        }
        return choice;
    }
}
