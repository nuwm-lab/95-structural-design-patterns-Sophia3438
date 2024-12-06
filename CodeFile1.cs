using System;

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

// Абстракція, що використовує конкретну реалізацію
public abstract class Transformation
{
    protected ITransformation transformation;

    public Transformation(ITransformation transformation)
    {
        this.transformation = transformation;
    }

    public abstract void ApplyTransformation(int x, int y);
    public abstract void ApplyTransformation(int x, int y, int z);
}

// Абстракція для 2D трансформації
public class Transformation2DAdapter : Transformation
{
    public Transformation2DAdapter(ITransformation transformation) : base(transformation)
    {
    }

    public override void ApplyTransformation(int x, int y)
    {
        transformation.ApplyTransformation(x, y);
    }

    public override void ApplyTransformation(int x, int y, int z)
    {
        throw new NotImplementedException("2D адаптер не підтримує Z-координату");
    }
}

// Абстракція для 3D трансформації
public class Transformation3DAdapter : Transformation
{
    public Transformation3DAdapter(ITransformation transformation) : base(transformation)
    {
    }

    public override void ApplyTransformation(int x, int y)
    {
        throw new NotImplementedException("3D адаптер не підтримує 2D трансформацію");
    }

    public override void ApplyTransformation(int x, int y, int z)
    {
        transformation.ApplyTransformation(x, y, z);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Вибір трансформації 2D
        ITransformation t2d = new Transformation2D(1, 2, 3, 4, 5, 6);
        Transformation transformation2D = new Transformation2DAdapter(t2d);

        Console.WriteLine("Введіть координати для 2D:");
        int x2d = int.Parse(Console.ReadLine());
        int y2d = int.Parse(Console.ReadLine());

        transformation2D.ApplyTransformation(x2d, y2d);

        // Вибір трансформації 3D
        ITransformation t3d = new Transformation3D(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
        Transformation transformation3D = new Transformation3DAdapter(t3d);

        Console.WriteLine("Введіть координати для 3D:");
        int x3d = int.Parse(Console.ReadLine());
        int y3d = int.Parse(Console.ReadLine());
        int z3d = int.Parse(Console.ReadLine());

        transformation3D.ApplyTransformation(x3d, y3d, z3d);
    }
}
