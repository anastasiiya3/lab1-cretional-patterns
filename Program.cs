using System;
using System.Collections.Generic;

namespace Lab1_CreationalPatterns;

// ============================================================
// СТРУКТУРНІ ПАТЕРНИ (STRUCTURAL PATTERNS) - 7 штук
// ============================================================

// ============================================================
// 1. ADAPTER (Адаптер)
// ============================================================
// Цільовий інтерфейс (очікує клієнт)
public interface ITarget
{
    string GetRequest();
}

// Сумісний клас (стара система)
public class Adaptee
{
    public string GetSpecificRequest() => "Specific request from Adaptee";
}

// Адаптер
public class Adapter : ITarget
{
    private readonly Adaptee _adaptee;
    
    public Adapter(Adaptee adaptee)
    {
        _adaptee = adaptee;
    }
    
    public string GetRequest()
    {
        return $"Adapter: {_adaptee.GetSpecificRequest()}";
    }
}

// ============================================================
// 2. BRIDGE (Міст)
// ============================================================
// Реалізація (Implementor)
public interface IDevice
{
    void TurnOn();
    void TurnOff();
    void SetVolume(int volume);
}

// Конкретні реалізації
public class TV : IDevice
{
    public void TurnOn() => Console.WriteLine("  TV: Увімкнено");
    public void TurnOff() => Console.WriteLine("  TV: Вимкнено");
    public void SetVolume(int volume) => Console.WriteLine($"  TV: Гучність = {volume}");
}

public class Radio : IDevice
{
    public void TurnOn() => Console.WriteLine("  Radio: Увімкнено");
    public void TurnOff() => Console.WriteLine("  Radio: Вимкнено");
    public void SetVolume(int volume) => Console.WriteLine($"  Radio: Гучність = {volume}");
}

// Абстракція
public abstract class RemoteControl
{
    protected IDevice _device;
    
    protected RemoteControl(IDevice device)
    {
        _device = device;
    }
    
    public abstract void TurnOn();
    public abstract void TurnOff();
    public abstract void SetVolume(int volume);
}

// Розширена абстракція
public class AdvancedRemote : RemoteControl
{
    public AdvancedRemote(IDevice device) : base(device) { }
    
    public override void TurnOn() => _device.TurnOn();
    public override void TurnOff() => _device.TurnOff();
    public override void SetVolume(int volume) => _device.SetVolume(volume);
    
    public void Mute() => Console.WriteLine("  AdvancedRemote: Режим mute");
}

// ============================================================
// 3. COMPOSITE (Композит)
// ============================================================
// Компонент
public interface IGraphic
{
    void Draw();
}

// Листок (простий елемент)
public class Circle : IGraphic
{
    public void Draw() => Console.WriteLine("  ○ Коло");
}

public class Square : IGraphic
{
    public void Draw() => Console.WriteLine("  □ Квадрат");
}

// Композит (складний елемент)
public class CompositeGraphic : IGraphic
{
    private readonly List<IGraphic> _children = new();
    
    public void Add(IGraphic graphic) => _children.Add(graphic);
    public void Remove(IGraphic graphic) => _children.Remove(graphic);
    
    public void Draw()
    {
        Console.WriteLine("  Фігура (група):");
        foreach (var child in _children)
        {
            child.Draw();
        }
    }
}

// ============================================================
// 4. DECORATOR (Декоратор)
// ============================================================
// Компонент
public interface ICoffee
{
    string GetDescription();
    double GetCost();
}

// Конкретний компонент
public class SimpleCoffee : ICoffee
{
    public string GetDescription() => "Кава";
    public double GetCost() => 50.0;
}

// Базовий декоратор
public abstract class CoffeeDecorator : ICoffee
{
    protected ICoffee _coffee;
    
    protected CoffeeDecorator(ICoffee coffee)
    {
        _coffee = coffee;
    }
    
    public virtual string GetDescription() => _coffee.GetDescription();
    public virtual double GetCost() => _coffee.GetCost();
}

// Конкретні декоратори
public class MilkDecorator : CoffeeDecorator
{
    public MilkDecorator(ICoffee coffee) : base(coffee) { }
    
    public override string GetDescription() => _coffee.GetDescription() + ", Молоко";
    public override double GetCost() => _coffee.GetCost() + 15.0;
}

public class SugarDecorator : CoffeeDecorator
{
    public SugarDecorator(ICoffee coffee) : base(coffee) { }
    
    public override string GetDescription() => _coffee.GetDescription() + ", Цукор";
    public override double GetCost() => _coffee.GetCost() + 5.0;
}

public class ChocolateDecorator : CoffeeDecorator
{
    public ChocolateDecorator(ICoffee coffee) : base(coffee) { }
    
    public override string GetDescription() => _coffee.GetDescription() + ", Шоколад";
    public override double GetCost() => _coffee.GetCost() + 20.0;
}

// ============================================================
// 5. FACADE (Фасад)
// ============================================================
// Підсистеми
public class CPU
{
    public void Start() => Console.WriteLine("  CPU: Запуск процесора");
    public void Execute() => Console.WriteLine("  CPU: Виконання інструкцій");
    public void Shutdown() => Console.WriteLine("  CPU: Вимкнення");
}

public class Memory
{
    public void Load() => Console.WriteLine("  Memory: Завантаження даних");
    public void Free() => Console.WriteLine("  Memory: Очищення пам'яті");
}

public class HardDrive
{
    public void Read() => Console.WriteLine("  HDD: Читання даних");
    public void Write() => Console.WriteLine("  HDD: Запис даних");
}

// Фасад
public class ComputerFacade
{
    private readonly CPU _cpu;
    private readonly Memory _memory;
    private readonly HardDrive _hardDrive;
    
    public ComputerFacade()
    {
        _cpu = new CPU();
        _memory = new Memory();
        _hardDrive = new HardDrive();
    }
    
    public void Start()
    {
        Console.WriteLine("  Комп'ютер запускається...");
        _cpu.Start();
        _memory.Load();
        _hardDrive.Read();
        _cpu.Execute();
        Console.WriteLine("  Комп'ютер готовий до роботи!");
    }
    
    public void Shutdown()
    {
        Console.WriteLine("  Комп'ютер вимикається...");
        _cpu.Shutdown();
        _memory.Free();
        _hardDrive.Write();
        Console.WriteLine("  Комп'ютер вимкнено!");
    }
}

// ============================================================
// 6. FLYWEIGHT (Легковаговик)
// ============================================================
// Легковаговик (внутрішній стан)
public class TreeType
{
    private readonly string _name;
    private readonly string _color;
    private readonly string _texture;
    
    public TreeType(string name, string color, string texture)
    {
        _name = name;
        _color = color;
        _texture = texture;
    }
    
    public void Draw(int x, int y)
    {
        Console.WriteLine($"  Дерево {_name} ({_color}) на позиції ({x},{y})");
    }
}

// Фабрика легковаговиків
public class TreeFactory
{
    private static readonly Dictionary<string, TreeType> _treeTypes = new();
    
    public static TreeType GetTreeType(string name, string color, string texture)
    {
        string key = $"{name}_{color}_{texture}";
        if (!_treeTypes.ContainsKey(key))
        {
            _treeTypes[key] = new TreeType(name, color, texture);
            Console.WriteLine($"  Створено новий тип дерева: {name}");
        }
        return _treeTypes[key];
    }
    
    public static int GetTypesCount() => _treeTypes.Count;
}

// Клієнт
public class Forest
{
    private readonly List<(TreeType type, int x, int y)> _trees = new();
    
    public void PlantTree(int x, int y, string name, string color, string texture)
    {
        var type = TreeFactory.GetTreeType(name, color, texture);
        _trees.Add((type, x, y));
    }
    
    public void Draw()
    {
        Console.WriteLine($"  Малюємо {_trees.Count} дерев:");
        foreach (var tree in _trees)
        {
            tree.type.Draw(tree.x, tree.y);
        }
    }
}

// ============================================================
// 7. PROXY (Замісник)
// ============================================================
// Суб'єкт
public interface IImage
{
    void Display();
}

// Реальний суб'єкт
public class RealImage : IImage
{
    private readonly string _fileName;
    
    public RealImage(string fileName)
    {
        _fileName = fileName;
        LoadFromDisk();
    }
    
    private void LoadFromDisk()
    {
        Console.WriteLine($"  Завантаження зображення: {_fileName}");
    }
    
    public void Display()
    {
        Console.WriteLine($"  Відображення: {_fileName}");
    }
}

// Проксі
public class ProxyImage : IImage
{
    private readonly string _fileName;
    private RealImage? _realImage;
    
    public ProxyImage(string fileName)
    {
        _fileName = fileName;
    }
    
    public void Display()
    {
        if (_realImage == null)
        {
            _realImage = new RealImage(_fileName);
        }
        _realImage.Display();
    }
}

// ============================================================
// ГОЛОВНА ПРОГРАМА (демонстрація всіх патернів)
// ============================================================
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║        ЛАБОРАТОРНА РОБОТА №3 - СТРУКТУРНІ ПАТЕРНИ             ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝\n");

        // ============================================================
        // 1. ADAPTER
        // ============================================================
        Console.WriteLine("┌─────────────────────────────────────────────────────────────┐");
        Console.WriteLine("│ 1. ADAPTER (Адаптер)                                       │");
        Console.WriteLine("└─────────────────────────────────────────────────────────────┘");
        
        Adaptee adaptee = new Adaptee();
        ITarget target = new Adapter(adaptee);
        Console.WriteLine($"  Результат: {target.GetRequest()}\n");

        // ============================================================
        // 2. BRIDGE
        // ============================================================
        Console.WriteLine("┌─────────────────────────────────────────────────────────────┐");
        Console.WriteLine("│ 2. BRIDGE (Міст)                                           │");
        Console.WriteLine("└─────────────────────────────────────────────────────────────┘");
        
        IDevice tv = new TV();
        RemoteControl remote = new AdvancedRemote(tv);
        remote.TurnOn();
        remote.SetVolume(15);
        remote.TurnOff();
        
        IDevice radio = new Radio();
        remote = new AdvancedRemote(radio);
        remote.TurnOn();
        remote.SetVolume(8);
        remote.TurnOff();
        Console.WriteLine();

        // ============================================================
        // 3. COMPOSITE
        // ============================================================
        Console.WriteLine("┌─────────────────────────────────────────────────────────────┐");
        Console.WriteLine("│ 3. COMPOSITE (Композит)                                    │");
        Console.WriteLine("└─────────────────────────────────────────────────────────────┘");
        
        Circle circle = new Circle();
        Square square1 = new Square();
        Square square2 = new Square();
        
        CompositeGraphic group = new CompositeGraphic();
        group.Add(circle);
        group.Add(square1);
        group.Add(square2);
        
        group.Draw();
        Console.WriteLine();

        // ============================================================
        // 4. DECORATOR
        // ============================================================
        Console.WriteLine("┌─────────────────────────────────────────────────────────────┐");
        Console.WriteLine("│ 4. DECORATOR (Декоратор)                                   │");
        Console.WriteLine("└─────────────────────────────────────────────────────────────┘");
        
        ICoffee coffee = new SimpleCoffee();
        Console.WriteLine($"  {coffee.GetDescription()}: {coffee.GetCost():C}");
        
        coffee = new MilkDecorator(coffee);
        Console.WriteLine($"  {coffee.GetDescription()}: {coffee.GetCost():C}");
        
        coffee = new SugarDecorator(coffee);
        Console.WriteLine($"  {coffee.GetDescription()}: {coffee.GetCost():C}");
        
        coffee = new ChocolateDecorator(coffee);
        Console.WriteLine($"  {coffee.GetDescription()}: {coffee.GetCost():C}\n");

        // ============================================================
        // 5. FACADE
        // ============================================================
        Console.WriteLine("┌─────────────────────────────────────────────────────────────┐");
        Console.WriteLine("│ 5. FACADE (Фасад)                                          │");
        Console.WriteLine("└─────────────────────────────────────────────────────────────┘");
        
        ComputerFacade computer = new ComputerFacade();
        computer.Start();
        computer.Shutdown();
        Console.WriteLine();

        // ============================================================
        // 6. FLYWEIGHT
        // ============================================================
        Console.WriteLine("┌─────────────────────────────────────────────────────────────┐");
        Console.WriteLine("│ 6. FLYWEIGHT (Легковаговик)                                │");
        Console.WriteLine("└─────────────────────────────────────────────────────────────┘");
        
        Forest forest = new Forest();
        forest.PlantTree(1, 1, "Дуб", "Зелений", "Текстура_дуба");
        forest.PlantTree(2, 3, "Сосна", "Зелений", "Текстура_сосни");
        forest.PlantTree(3, 5, "Дуб", "Зелений", "Текстура_дуба");
        forest.PlantTree(4, 7, "Сосна", "Зелений", "Текстура_сосни");
        forest.PlantTree(5, 9, "Береза", "Білий", "Текстура_берези");
        forest.Draw();
        Console.WriteLine($"  Кількість унікальних типів дерев: {TreeFactory.GetTypesCount()}\n");

        // ============================================================
        // 7. PROXY
        // ============================================================
        Console.WriteLine("┌─────────────────────────────────────────────────────────────┐");
        Console.WriteLine("│ 7. PROXY (Замісник)                                        │");
        Console.WriteLine("└─────────────────────────────────────────────────────────────┘");
        
        IImage image1 = new ProxyImage("photo1.jpg");
        IImage image2 = new ProxyImage("photo2.jpg");
        
        Console.WriteLine("  Перший виклик Display (завантажує з диску):");
        image1.Display();
        
        Console.WriteLine("  Другий виклик Display (з кешу):");
        image1.Display();
        
        Console.WriteLine("  Інше зображення:");
        image2.Display();
        Console.WriteLine();

        // ============================================================
        // ПІДСУМОК
        // ============================================================
        Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║      ✅ ВСІ 7 СТРУКТУРНИХ ПАТЕРНІВ ПРОДЕМОНСТРОВАНО!          ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝\n");
        
        Console.WriteLine("Список структурних патернів:");
        Console.WriteLine("  1️⃣  Adapter    - адаптує несумісний інтерфейс");
        Console.WriteLine("  2️⃣  Bridge     - розділяє абстракцію та реалізацію");
        Console.WriteLine("  3️⃣  Composite  - дереволодібна структура об'єктів");
        Console.WriteLine("  4️⃣  Decorator  - динамічно додає функціональність");
        Console.WriteLine("  5️⃣  Facade     - спрощений інтерфейс до підсистеми");
        Console.WriteLine("  6️⃣  Flyweight  - економить пам'ять (спільні дані)");
        Console.WriteLine("  7️⃣  Proxy      - замісник для контролю доступу");
    }
}