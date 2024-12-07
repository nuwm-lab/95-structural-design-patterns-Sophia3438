using System;
using System.Diagnostics;

// Інтерфейс для реалізації математичних операцій
public interface ITransformation
{
    void ApplyTransformation(int x, int y);
    void ApplyTransformation(int x, int y, int z);
}

// Конкретна реалізація для 2D трансформації
public class Transformation2D : ITransformation
{
    private int a11, a12, a13, a21, a22, a23;

    public Transformation2D(int a11, int a12, int a13, int a21, int a22, int a23)
    {
        this.a11 = a11;
        this.a12 = a12;
        this.a13 = a13;
        this.a21 = a21;
        this.a22 = a22;
        this.a23 = a23;
    }

    public void ApplyTransformation(int x, int y)
    {
        int tempx = a11 * x + a12 * y + a13;
        int tempy = a21 * x + a22 * y + a23;
        Console.WriteLine($"\nРезультат 2D:\nx = {tempx}\ny = {tempy}\n");
    }

    public void ApplyTransformation(int x, int y, int z)
    {
        throw new NotImplementedException("2D трансформація не підтримує Z-координату");
    }
}

// Конкретна реалізація для 3D трансформації
public class Transformation3D : ITransformation
{
    private int a11, a12, a13, a14, a21, a22, a23, a24, a31, a32, a33, a34;

    public Transformation3D(int a11, int a12, int a13, int a14, int a21, int a22, int a23, int a24, int a31, int a32, int a33, int a34)
    {
        this.a11 = a11;
        this.a12 = a12;
        this.a13 = a13;
        this.a14 = a14;
        this.a21 = a21;
        this.a22 = a22;
        this.a23 = a23;
        this.a24 = a24;
        this.a31 = a31;
        this.a32 = a32;
        this.a33 = a33;
        this.a34 = a34;
    }

    public void ApplyTransformation(int x, int y)
    {
        throw new NotImplementedException("3D трансформація не підтримує 2D перетворення");
    }

    public void ApplyTransformation(int x, int y, int z)
    {
        int tempx = a11 * x + a12 * y + a13 * z + a14;
        int tempy = a21 * x + a22 * y + a23 * z + a24;
        int tempz = a31 * x + a32 * y + a33 * z + a34;
        Console.WriteLine($"\nРезультат 3D:\nx = {tempx}\ny = {tempy}\nz = {tempz}");
    }
}

// Декоратор для вимірювання часу виконання методів
public class TimerDecorator : ITransformation
{
    private readonly ITransformation _transformation;

    public TimerDecorator(ITransformation transformation)
    {
        _transformation = transformation;
    }

    public void ApplyTransformation(int x, int y)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        _transformation.ApplyTransformation(x, y);
        stopwatch.Stop();
        Console.WriteLine($"Час виконання 2D трансформації: {stopwatch.ElapsedMilliseconds} мс");
    }

    public void ApplyTransformation(int x, int y, int z)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        _transformation.ApplyTransformation(x, y, z);
        stopwatch.Stop();
        Console.WriteLine($"Час виконання 3D трансформації: {stopwatch.ElapsedMilliseconds} мс");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Вибір трансформації 2D
        ITransformation t2d = new Transformation2D(1, 2, 3, 4, 5, 6);
        ITransformation decoratedT2D = new TimerDecorator(t2d);

        Console.WriteLine("Введіть координати для 2D:");
        int x2d, y2d;
        if (int.TryParse(Console.ReadLine(), out x2d) && int.TryParse(Console.ReadLine(), out y2d))
        {
            decoratedT2D.ApplyTransformation(x2d, y2d);
        }
        else
        {
            Console.WriteLine("Невірний ввід для 2D координат!");
        }

        // Вибір трансформації 3D
        ITransformation t3d = new Transformation3D(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
        ITransformation decoratedT3D = new TimerDecorator(t3d);

        Console.WriteLine("Введіть координати для 3D:");
        int x3d, y3d, z3d;
        if (int.TryParse(Console.ReadLine(), out x3d) && int.TryParse(Console.ReadLine(), out y3d) && int.TryParse(Console.ReadLine(), out z3d))
        {
            decoratedT3D.ApplyTransformation(x3d, y3d, z3d);
        }
        else
        {
            Console.WriteLine("Невірний ввід для 3D координат!");
        }
    }
}
