using System;
using System.Collections.Generic;

class Event
{
    public delegate void EventHandler(string message);

    public event EventHandler CoffeeReadyEvent;
    public event EventHandler NotEnoughMoneyEvent;
    public event EventHandler NoWaterEvent; 

    public void OnCoffeeReady(string message)
    {
        CoffeeReadyEvent?.Invoke(message);
    }

    public void OnNotEnoughMoney(string message)
    {
        NotEnoughMoneyEvent?.Invoke(message);
    }

    public void OnNoWater(string message)
    {
        NoWaterEvent?.Invoke(message);
    }
}

class CoffeeMachine
{
    private Event events; 
    private int waterLevel = 100; 
    private int coffeePrice = 10;
    private int userBalance = 0;

    public CoffeeMachine(Event eventHandler)
    {
        events = eventHandler;
        events.CoffeeReadyEvent += HandleCoffeeReady;
        events.NotEnoughMoneyEvent += HandleNotEnoughMoney;
        events.NoWaterEvent += HandleNoWater;
    }

    public void InsertMoney(int amount)
    {
        userBalance += amount;
        MakeCoffee();
    }

    private void MakeCoffee()
    {
        if (userBalance >= coffeePrice && waterLevel > 0)
        {
            waterLevel -= 10;
            userBalance -= coffeePrice;
            events.OnCoffeeReady("Your coffee is ready!");
        }
        else if (userBalance < coffeePrice)
        {
            events.OnNotEnoughMoney("Not enough money to make coffee!");
        }
        else if (waterLevel <= 0)
        {
            events.OnNoWater("No water in the coffee machine!");
        }
    }

    private void HandleCoffeeReady(string message)
    {
        Console.WriteLine(message);
    }

    private void HandleNotEnoughMoney(string message)
    {
        Console.WriteLine(message);
    }

    private void HandleNoWater(string message)
    {
        Console.WriteLine(message);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Event events = new Event();

        CoffeeMachine coffeeMachine = new CoffeeMachine(events);

        coffeeMachine.InsertMoney(20);
        coffeeMachine.InsertMoney(10);

        for (int i = 0; i < 11; i++)
        {
            coffeeMachine.InsertMoney(10); 
        }
    }
}
