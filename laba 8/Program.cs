using System;

public interface ITriangle
{
    double GetArea();
    double GetPerimeter();
}

public abstract class Polygon : ITriangle
{
    public List<double> Sides { get; set; }

    public Polygon(params double[] sides)
    {
        Sides = new List<double>(sides);
    }

    public abstract double GetArea();

    public double GetPerimeter()
    {
        return Sides.Sum();
    }
}

public class Triangle : Polygon
{
    public double Angle1 { get; set; }
    public double Angle2 { get; set; }
    public double Angle3 { get; set; }

    public Triangle(double side1, double side2, double side3, double angle1, double angle2, double angle3) :
        base(side1, side2, side3)
    {
        Angle1 = angle1;
        Angle2 = angle2;
        Angle3 = angle3;
    }

    public override double GetArea()
    {
        return 0.5 * Sides[0] * Sides[1] * Math.Sin(Angle1 * Math.PI / 180);
    }

    public double GetHeight(int sideIndex)
    {
        return 2 * GetArea() / Sides[sideIndex];
    }

    public string GetTriangleType()
    {
        if (Angle1 == 90 || Angle2 == 90 || Angle3 == 90)
        {
            return "Прямоугольный";
        }
        else if (Sides[0] == Sides[1] && Sides[1] == Sides[2])
        {
            return "Равносторонний";
        }
        else if (Sides[0] == Sides[1] || Sides[1] == Sides[2] || Sides[0] == Sides[2])
        {
            return "Равнобедренный";
        }
        else
        {
            return "Произвольный";
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine(" Введите значения сторон и углов: ");

        double AB = (double)Convert.ToDouble(Console.ReadLine());
        double BC = (double)Convert.ToDouble(Console.ReadLine());
        double CA = (double)Convert.ToDouble(Console.ReadLine());
        double angle1 = (double)Convert.ToDouble(Console.ReadLine());
        double angle2 = (double)Convert.ToDouble(Console.ReadLine());
        double angle3 = (double)Convert.ToDouble(Console.ReadLine());

        // Данные о треугольнике
        Triangle triangle = new Triangle(AB, BC, CA, angle1, angle2, angle3);
        Console.WriteLine("Данные о треугольнике:");
        Console.WriteLine($"Площадь: {triangle.GetArea()}");
        Console.WriteLine($"Периметр: {triangle.GetPerimeter()}");
        Console.WriteLine($"Длина, проведенная с первое стороны: {triangle.GetHeight(0)}");
        Console.WriteLine($"Длина, проведенная со второй стороны: {triangle.GetHeight(1)}");
        Console.WriteLine($"Длина, проведенная с третьей стороны: {triangle.GetHeight(2)}");
        Console.WriteLine($"Тип треугольника: {triangle.GetTriangleType()}");

        // Изменение значений или конец программы
        while (true)
        {
            Console.WriteLine("Введите 'с', чтобы изменить значения сторон, 'у' - изменить значения углов, 'в' - выйти из программы:");
            string input = Console.ReadLine();

            if (input == "с")
            {
                Console.WriteLine("Введите новые значения сторон (через пробел):");
                double[] newSides = Array.ConvertAll(Console.ReadLine().Split(' '), double.Parse);
                triangle.Sides = new List<double>(newSides); 
            }
            else if (input == "у")
            {
                Console.WriteLine("Введите новый значения углов (через пробел):");
                double[] newAngles = Array.ConvertAll(Console.ReadLine().Split(' '), double.Parse);
                triangle.Angle1 = newAngles[0];
                triangle.Angle2 = newAngles[1];
                triangle.Angle3 = newAngles[2];
            }
            else if (input == "в")
            {
                break;
            }

            // Измененные данные о треугольнике
            Console.WriteLine("Данные о треугольнике:");
            Console.WriteLine($"Площадь: {triangle.GetArea()}");
            Console.WriteLine($"Периметр: {triangle.GetPerimeter()}");
            Console.WriteLine($"Длина, проведенная с первое стороны: {triangle.GetHeight(0)}");
            Console.WriteLine($"Длина, проведенная со второй стороны: {triangle.GetHeight(1)}");
            Console.WriteLine($"Длина, проведенная с третьей стороны: {triangle.GetHeight(2)}");
            Console.WriteLine($"Тип треугольника: {triangle.GetTriangleType()}");
        }
    }
}
