using System;

Tamagocha tamagocha = new Tamagocha { Name = "Тимур" };
tamagocha.HungryChanged += Tamagocha_HungryChanged;
tamagocha.HungryChanged += Tamagocha_ThirstyChanged;
tamagocha.HungryChanged += Tamagocha_DirtyChanged;
Random random = new Random();
ConsoleKeyInfo command;
do
{
    command = Console.ReadKey();
    if (command.Key == ConsoleKey.F)
        tamagocha.Feed();
    else if (command.Key == ConsoleKey.I)
        tamagocha.PrintInfo();
    else if (command.Key == ConsoleKey.W)
        tamagocha.Washy();
    else if (command.Key == ConsoleKey.D)
        tamagocha.Piiiiit();
    else if (command.Key == ConsoleKey.P)
    {
        int press = random.Next(0, 100);
        IPresent present = null;
        if (press >= 66)
            present = new Food();
        else if (press < 66 && press >= 33)
            present = new Toy();
        else present = new Book();
        tamagocha.GivePresent(present);
    }
}
while (command.Key != ConsoleKey.Escape);
tamagocha.Stop();

void Tamagocha_HungryChanged(object? sender, EventArgs e)
{
    Console.SetCursorPosition(0, 0);
    Console.Write($"{tamagocha.Name} голодает! Показатель голода растет: {tamagocha.Hungry}        ");
    Console.SetCursorPosition(0, 5); // возвращаем курсор для ввода команды!
}

void Tamagocha_ThirstyChanged(object? sender, EventArgs e)
{
    Console.SetCursorPosition(0, 1);
    Console.Write($"{tamagocha.Name} обезвоживается! Показатель жажды растет: {tamagocha.Thirsty}        ");
    Console.SetCursorPosition(0, 5); // возвращаем курсор для ввода команды!
}

void Tamagocha_DirtyChanged(object? sender, EventArgs e)
{
    Console.SetCursorPosition(0, 2);
    Console.Write($"{tamagocha.Name} становиться поросенком! Показатель грязи растет: {tamagocha.Dirty}        ");
    Console.SetCursorPosition(0, 5); // возвращаем курсор для ввода команды!
}

class Tamagocha
{
    public string Name { get; set; }
    public int Health { get; set; } = 100;
    public int Hungry
    {
        get => hungry;
        set
        {
            hungry = value;
            HungryChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    public int Dirty
    {
        get => dirty;
        set
        {
            dirty = value;
            DirtyChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    public int Thirsty
    {
        get => thirsty;
        set
        {
            thirsty = value;
            ThirstyChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    public bool IsDead { get; set; } = false;

    public event EventHandler HungryChanged;
    public event EventHandler DirtyChanged;
    public event EventHandler ThirstyChanged;

    public Tamagocha()
    {
        Thread thread = new Thread(LifeCircle);
        thread.Start();
    }

    private int hungry = 0;
    private int dirty = 0;
    private int thirsty = 0;
    Random random = new Random();

    private void LifeCircle(object? obj)
    {
        while (!IsDead)
        {
            Thread.Sleep(1000);
            int rnd = random.Next(0, 6);
            switch (rnd)
            {
                case 0: JumpMinute(); break;
                case 1: FallSleep(); break;
                case 2: Coffe(); break;
                case 3: HotGod(); break;
                case 4: Pilps(); break;
                case 5: Drugs(); break;
                default: break;
            }
        }
    }

    private void FallSleep()
    {
        WriteMessageToConsole($"{Name} внезапно начинает спать как угорелый. Это продолжается целую минуту. Показатели голода, жажды и чистоты повышены!");
        Thirsty += random.Next(5, 10);
        Hungry += random.Next(5, 10);
        Dirty += random.Next(5, 10);
        
    }

    private void Coffe()
    {
        WriteMessageToConsole($"{Name} внезапно начинает пить кофе. Какой Ужас! Это продолжается целую минуту. Показатели голода повышены, жажда понижена!");
        Thirsty -= random.Next(5, 10);
        Hungry += random.Next(5, 10);
    }

    private void HotGod()
    {
        WriteMessageToConsole($"{Name} внезапно начинает есть горячего Бога. Какой Ужас! Это продолжается целую минуту. Показатели голода понижены, жажда повышена!");
        Thirsty += random.Next(5, 10);
        Hungry -= random.Next(5, 10);
    }

    private void Pilps()
    {
        WriteMessageToConsole($"{Name} внезапно начинает есть таблетки. Еблан! Это продолжается целую минуту. Показатели голода и жажды понижены, жизнь повышена!");
        Hungry += random.Next(5, 10);
        Thirsty += random.Next(5, 10);
        Health += random.Next(5, 10);
    }

    private void Drugs()
    {
        WriteMessageToConsole($"{Name} внезапно начинает курить. Неужели трава!? Это продолжается целую минуту. Показатели голода, жажды и жизни понижены!");
        Thirsty += random.Next(5, 10);
        Hungry += random.Next(5, 10);
        Health -= random.Next(5, 10);
    }

    private void JumpMinute()
    {
        WriteMessageToConsole($"{Name} внезапно начинает прыгать как угорелый. Это продолжается целую минуту. Показатели голода, жажды и чистоты повышены!");
        Thirsty += random.Next(5, 10);
        Hungry += random.Next(5, 10);
        Dirty += random.Next(5, 10);
    }

    private void WriteMessageToConsole(string message)
    {
        Console.SetCursorPosition(0, 10);
        Console.Write(message);
        Console.SetCursorPosition(0, 5); // возвращаем курсор для ввода команды!
    }

    public void PrintInfo()
    {
        Console.SetCursorPosition(0, 8);
        Console.WriteLine($"{Name}: Health:{Health} Hungry:{Hungry} Dirty:{Dirty} Thirsty:{Thirsty} IsDead:{IsDead}");
    }

    public void Stop()
    {
        IsDead = true;
    }

    internal void Feed()
    {
        WriteMessageToConsole($"{Name} внезапно начинает ЖРАТЬ как угорелый. Это продолжается целую минуту. Показатели голода, жажды и чистоты повышены!");

        Hungry -= random.Next(5, 10);
    }

    internal void Washy()
    {
        WriteMessageToConsole($"{Name} внезапно начинает МЫТЬСЯ как угорелый. Это продолжается целую минуту. Показатели голода, жажды и чистоты повышены!");
        Dirty -= random.Next(5, 10);
    }

    internal void Piiiiit()
    {
        if (Thirsty >= -100)
        {
            WriteMessageToConsole($"{Name} внезапно начинает ЖЁСТКО ПИТЬ ВОДЯРЫ как угорелый. Это продолжается целую минуту. Показатели жажды понижены!");
            Thirsty -= random.Next(5, 10);
        }
        else
        {
            WriteMessageToConsole($"{Name} внезапно начинает ЖЁСТКО БЛЕВАТЬ как угорелый. Это продолжается целую минуту. Показатели жизни понижены!");
            Health -= random.Next(5, 10);
        }
    }








    internal void GivePresent(IPresent present)
    {
        Random random = new Random();
        int action = random.Next(1, 4);

        if (action == 1)
        {
            present.Open();
        }
        else if (action == 2)
        {
            present.Gnaw();
        }
        else
        {
            present.Smash();
        }
    }
}

public interface IPresent
{
    void Open();
    void Gnaw();
    void Smash();
}

public class Toy : IPresent
{
    public void Open()
    {
        Console.WriteLine("Игрушка открыта, и тамагочи с удовольствием играет с ней!");
    }
    public void Gnaw()
    {
        Console.WriteLine("Тамагочи радостно грызет игрушку.");
    }
    public void Smash()
    {
        Console.WriteLine("Ой, тамагочи случайно разбил игрушку!");
    }
}

public class Food : IPresent
{
    public void Open()
    {
        Console.WriteLine("Блюдо открыто, и тамагочи готов к употреблению!");
    }
    public void Gnaw()
    {
        Console.WriteLine("Тамагочи с удовольствием ест эту еду.");
    }
    public void Smash()
    {
        Console.WriteLine("Тамагочи не хочет разбивать еду, он хочет ее съесть!");
    }
}

public class Book : IPresent
{
    public void Open()
    {
        Console.WriteLine("Книга открыта, и тамагочи любопытно ее прочесть!");
    }
    public void Gnaw()
    {
        Console.WriteLine("Тамагочи пытается разгрызть книгу, но у него это не очень получается.");
    }
    public void Smash()
    {
        Console.WriteLine("Тамагочи случайно вырвал страницу из книги!");
    }
}

