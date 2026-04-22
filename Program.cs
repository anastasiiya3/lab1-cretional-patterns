using System;
using System.Collections.Generic;

namespace Lab1_CreationalPatterns;

// 1. SINGLETON
public sealed class Logger
{
    private static Logger? _instance;
    private static readonly object _lock = new();
    private List<string> _logs = new();

    private Logger() 
    {
        Console.WriteLine("[Singleton] Логгер створено");
    }

    public static Logger Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new Logger();
                }
            }
            return _instance;
        }
    }

    public void Log(string msg)
    {
        _logs.Add(msg);
        Console.WriteLine($"[Singleton] Лог: {msg}");
    }
}

// 2. FACTORY METHOD
public interface ITransport
{
    void Deliver();
}

public class Truck : ITransport
{
    public void Deliver() => Console.WriteLine("  Доставка вантажівкою");
}

public class Ship : ITransport
{
    public void Deliver() => Console.WriteLine("  Доставка кораблем");
}

public abstract class Logistics
{
    public abstract ITransport CreateTransport();
    public void PlanDelivery() => CreateTransport().Deliver();
}

public class RoadLogistics : Logistics
{
    public override ITransport CreateTransport() => new Truck();
}

public class SeaLogistics : Logistics
{
    public override ITransport CreateTransport() => new Ship();
}

// 3. ABSTRACT FACTORY
public interface IButton { void Render(); }
public interface IWindow { void Render(); }

public class WinButton : IButton { public void Render() => Console.WriteLine("  Windows Button"); }
public class WinWindow : IWindow { public void Render() => Console.WriteLine("  Windows Window"); }

public class MacButton : IButton { public void Render() => Console.WriteLine("  Mac Button"); }
public class MacWindow : IWindow { public void Render() => Console.WriteLine("  Mac Window"); }

public interface IWidgetFactory
{
    IButton CreateButton();
    IWindow CreateWindow();
}

public class WinFactory : IWidgetFactory
{
    public IButton CreateButton() => new WinButton();
    public IWindow CreateWindow() => new WinWindow();
}

public class MacFactory : IWidgetFactory
{
    public IButton CreateButton() => new MacButton();
    public IWindow CreateWindow() => new MacWindow();
}

// 4. BUILDER
public class Burger
{
    public string Bun { get; set; } = "";
    public string Patty { get; set; } = "";
    public string Sauce { get; set; } = "";
    public bool HasCheese { get; set; }

    public void Show() => Console.WriteLine($"  Бургер: {Bun}, {Patty}, {Sauce}, Сир: {HasCheese}");
}

public interface IBurgerBuilder
{
    IBurgerBuilder SetBun(string bun);
    IBurgerBuilder SetPatty(string patty);
    IBurgerBuilder SetSauce(string sauce);
    IBurgerBuilder AddCheese();
    Burger Build();
}

public class BurgerBuilder : IBurgerBuilder
{
    private Burger _burger = new();
    public IBurgerBuilder SetBun(string bun) { _burger.Bun = bun; return this; }
    public IBurgerBuilder SetPatty(string patty) { _burger.Patty = patty; return this; }
    public IBurgerBuilder SetSauce(string sauce) { _burger.Sauce = sauce; return this; }
    public IBurgerBuilder AddCheese() { _burger.HasCheese = true; return this; }
    public Burger Build() => _burger;
}

// 5. PROTOTYPE
public class Book : ICloneable
{
    public string Title { get; set; } = "";
    public string Author { get; set; } = "";
    public object Clone() => new Book { Title = this.Title, Author = this.Author };
}

// 6. FLUENT INTERFACE
public class Email
{
    public string To { get; private set; } = "";
    public string Subject { get; private set; } = "";
    public string Body { get; private set; } = "";

    public class Builder
    {
        private Email _email = new();
        public Builder SetTo(string to) { _email.To = to; return this; }
        public Builder SetSubject(string subj) { _email.Subject = subj; return this; }
        public Builder SetBody(string body) { _email.Body = body; return this; }
        public Email Build() => _email;
    }
    public static Builder Create() => new Builder();
    public void Show() => Console.WriteLine($"  Email: До:{To}, Тема:{Subject}");
}

// MAIN
class Program
{
    static void Main()
    {
        Console.WriteLine("\n=== ЛАБОРАТОРНА РОБОТА №1 ===");
        Console.WriteLine("Породжувальні патерни\n");

        // 1. Singleton
        Console.WriteLine("1. SINGLETON");
        var l1 = Logger.Instance;
        var l2 = Logger.Instance;
        l1.Log("Тестовий лог");
        Console.WriteLine($"  l1 == l2: {ReferenceEquals(l1, l2)}\n");

        // 2. Factory Method
        Console.WriteLine("2. FACTORY METHOD");
        Logistics log = new RoadLogistics();
        log.PlanDelivery();
        log = new SeaLogistics();
        log.PlanDelivery();
        Console.WriteLine();

        // 3. Abstract Factory
        Console.WriteLine("3. ABSTRACT FACTORY");
        IWidgetFactory widget = new WinFactory();
        widget.CreateButton().Render();
        widget.CreateWindow().Render();
        widget = new MacFactory();
        widget.CreateButton().Render();
        widget.CreateWindow().Render();
        Console.WriteLine();

        // 4. Builder
        Console.WriteLine("4. BUILDER");
        var burger = new BurgerBuilder()
            .SetBun("Кунжутна")
            .SetPatty("Яловичина")
            .SetSauce("Барбекю")
            .AddCheese()
            .Build();
        burger.Show();
        Console.WriteLine();

        // 5. Prototype
        Console.WriteLine("5. PROTOTYPE");
        var book1 = new Book { Title = "C# Programming", Author = "John" };
        var book2 = (Book)book1.Clone();
        book2.Title = "Design Patterns";
        Console.WriteLine($"  Оригінал: {book1.Title}");
        Console.WriteLine($"  Клон: {book2.Title}\n");

        // 6. Fluent Interface
        Console.WriteLine("6. FLUENT INTERFACE");
        var email = Email.Create()
            .SetTo("student@mail.com")
            .SetSubject("Lab1")
            .SetBody("Hello!")
            .Build();
        email.Show();

        Console.WriteLine("\n✅ Всі 6 патернів продемонстровано!");
    }
}