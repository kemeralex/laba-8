using System;

public interface IString
{
    BitStringBase And(BitStringBase other);
    BitStringBase Or(BitStringBase other);
    BitStringBase Xor(BitStringBase other);
    BitStringBase Not();
    BitStringBase ShiftLeft(int count);
    BitStringBase ShiftRight(int count);
}
public abstract class BitStringBase : IString
{
    public abstract ulong High { get; set; }
    public abstract ulong Low { get; set; }

    public abstract BitStringBase And(BitStringBase other);
    public abstract BitStringBase Or(BitStringBase other);
    public abstract BitStringBase Xor(BitStringBase other);
    public abstract BitStringBase Not();
    public abstract BitStringBase ShiftLeft(int count);
    public abstract BitStringBase ShiftRight(int count);

    public override string ToString()
    {
        return $"{High:X16}{Low:X16}";
    }
}


public class BitString : BitStringBase
{
    private ulong _high;
    private ulong _low;

    public BitString(ulong high, ulong low)
    {
        _high = high;
        _low = low;
    }

    public override ulong High { get { return _high; } set { _high = value; } }
    public override ulong Low { get { return _low; } set { _low = value; } }

    public override BitStringBase And(BitStringBase other)
    {
        BitString otherBitString = (BitString)other;
        return new BitString(_high & otherBitString._high, _low & otherBitString._low);
    }

    public override BitStringBase Or(BitStringBase other)
    {
        BitString otherBitString = (BitString)other;
        return new BitString(_high | otherBitString._high, _low | otherBitString._low);
    }

    public override BitStringBase Xor(BitStringBase other)
    {
        BitString otherBitString = (BitString)other;
        return new BitString(_high ^ otherBitString._high, _low ^ otherBitString._low);
    }

    public override BitStringBase Not()
    {
        return new BitString(~_high, ~_low);
    }

    public override BitStringBase ShiftLeft(int count)
    {
        if (count < 64)
        {
            return new BitString(_high << count | _low >> (64 - count), _low << count);
        }
        else if (count == 64)
        {
            return new BitString(_low, 0);
        }
        else
        {
            return new BitString(0, 0);
        }
    }

    public override BitStringBase ShiftRight(int count)
    {
        if (count < 64)
        {
            return new BitString(_high >> count, _low >> count | _high << (64 - count));
        }
        else if (count == 64)
        {
            return new BitString(0, _high);
        }
        else
        {
            return new BitString(0, 0);
        }
    }
}


public class Program
{
    public static void Main(string[] args)
    {
        // Ввод строки
        Console.WriteLine("Введите первую и вторую часть через пробел:");
        string[] input = Console.ReadLine().Split(' ');
        ulong high = ulong.Parse(input[0], System.Globalization.NumberStyles.HexNumber);
        ulong low = ulong.Parse(input[1], System.Globalization.NumberStyles.HexNumber);

        //Вывод строки
        BitString bs = new BitString(high, low);

        Console.WriteLine("Строка:");
        Console.WriteLine($"Первая: {bs.High:X16}");
        Console.WriteLine($"Вторая: {bs.Low:X16}");

        Console.WriteLine("Введите 'a' для операции AND, 'o' для операции OR, 'x' для операции XOR, 'n' для операции NOT, 'l' для операции 'Сдвиг влево', 'r' для операции 'Сдвиг вправо':");
        string operation = Console.ReadLine();

        //дополнения к операциям
        BitStringBase other = null;
        int count = 0;
        switch (operation)
        {
            case "a":
            case "o":
            case "x":
                Console.WriteLine("Введите другие две строки через пробел:");
                input = Console.ReadLine().Split(' ');
                high = ulong.Parse(input[0], System.Globalization.NumberStyles.HexNumber);
                low = ulong.Parse(input[1], System.Globalization.NumberStyles.HexNumber);
                other = new BitString(high, low);
                break;
            case "l":
            case "r":
                Console.WriteLine("На сколько битов вы хотите сместить?");
                count = int.Parse(Console.ReadLine());
                break;
            case "n":
                break; //ничего не нужно делать для операции not
            default:
                Console.WriteLine("Неизвестная операция");
                return;
        }

        //Сами операции
        BitStringBase result;
        switch (operation)
        {
            case "a":
                result = bs.And(other);
                break;
            case "o":
                result = bs.Or(other);
                break;
            case "x":
                result = bs.Xor(other);
                break;
            case "n":
                result = bs.Not();
                break;
            case "l":
                result = bs.ShiftLeft(count);
                break;
            case "r":
                result = bs.ShiftRight(count);
                break;
            default:
                Console.WriteLine("Неизвестная операция");
                return;
        }

        Console.WriteLine("Результат:");
        Console.WriteLine($"Первая строка: {result.High:X16}");
        Console.WriteLine($"Вторая строка: {result.Low:X16}");
    }
}