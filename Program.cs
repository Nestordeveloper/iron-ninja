using System;
using System.Collections.Generic;

interface IConsumable
{
    string Name { get; set; }
    int Calories { get; set; }
    bool IsSpicy { get; set; }
    bool IsSweet { get; set; }
    string GetInfo();
}

class Food : IConsumable
{
    public string Name { get; set; }
    public int Calories { get; set; }
    public bool IsSpicy { get; set; }
    public bool IsSweet { get; set; }

    public string GetInfo()
    {
        return $"{Name} (Food).  Calories: {Calories}.  Spicy?: {IsSpicy}, Sweet?: {IsSweet}";
    }

    public Food(string name, int calories, bool spicy, bool sweet)
    {
        Name = name;
        Calories = calories;
        IsSpicy = spicy;
        IsSweet = sweet;
    }
}

class Drink : IConsumable
{
    public string Name { get; set; }
    public int Calories { get; set; }
    public bool IsSpicy { get; set; }
    public bool IsSweet { get; set; }

    public string GetInfo()
    {
        return $"{Name} (Drink).  Calories: {Calories}.  Spicy?: {IsSpicy}, Sweet?: {IsSweet}";
    }

    public Drink(string name, int calories)
    {
        Name = name;
        Calories = calories;
        IsSpicy = false;
        IsSweet = true;
    }
}

abstract class Ninja
{
    protected int calorieIntake;
    public List<IConsumable> ConsumptionHistory;

    public Ninja()
    {
        calorieIntake = 0;
        ConsumptionHistory = new List<IConsumable>();
    }

    public abstract bool IsFull { get; }
    public abstract void Consume(IConsumable item);
}

class SweetTooth : Ninja
{
    public override bool IsFull => calorieIntake >= 1500;

    public override void Consume(IConsumable item)
    {
        if (!IsFull)
        {
            calorieIntake += item.Calories + (item.IsSweet ? 10 : 0);
            ConsumptionHistory.Add(item);
            Console.WriteLine($"SweetTooth está comiendo {item.Name}. Picante: {item.IsSpicy}, Dulce: {item.IsSweet}");
            Console.WriteLine(item.GetInfo());
        }
        else
        {
            Console.WriteLine("SweetTooth está lleno y no puede comer más.");
        }
    }
}

class SpiceHound : Ninja
{
    public override bool IsFull => calorieIntake >= 1200;

    public override void Consume(IConsumable item)
    {
        if (!IsFull)
        {
            calorieIntake += item.Calories - (item.IsSpicy ? 5 : 0);
            ConsumptionHistory.Add(item);
            Console.WriteLine($"SpiceHound está comiendo {item.Name}. Picante: {item.IsSpicy}, Dulce: {item.IsSweet}");
            Console.WriteLine(item.GetInfo());
        }
        else
        {
            Console.WriteLine("SpiceHound está lleno y no puede comer más.");
        }
    }
}

class Buffet
{
    public List<IConsumable> Menu;

    public Buffet()
    {
        Menu = new List<IConsumable>
        {
            new Food("Pizza", 285, true, false),
            new Food("Chocolate Cake", 400, false, true),
            new Food("Sushi", 350, true, false),
            new Food("Salad", 150, false, false),
            new Drink("Cola", 150),
            new Drink("Orange Juice", 120),
            new Food("Taco", 200, true, false)
        };
    }

    public IConsumable Serve()
    {
        Random rand = new Random();
        int randomIndex = rand.Next(Menu.Count);
        return Menu[randomIndex];
    }
}

class Program
{
    static void Main(string[] args)
    {
        Buffet buffet = new Buffet();
        SweetTooth sweetTooth = new SweetTooth();
        SpiceHound spiceHound = new SpiceHound();

        while (!sweetTooth.IsFull || !spiceHound.IsFull)
        {
            IConsumable food = buffet.Serve();
            if (!sweetTooth.IsFull)
            {
                sweetTooth.Consume(food);
            }
            if (!spiceHound.IsFull)
            {
                spiceHound.Consume(food);
            }
        }

        Console.WriteLine($"SweetTooth ha consumido {sweetTooth.ConsumptionHistory.Count} elementos.");
        Console.WriteLine($"SpiceHound ha consumido {spiceHound.ConsumptionHistory.Count} elementos.");
    }
}
