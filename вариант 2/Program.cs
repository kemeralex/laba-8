using System;

public abstract class BitStringBase
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
        // Get bit string data from user
        Console.WriteLine("Enter the high and low 64-bit values for the bit string (separated by spaces):");
        string[] input = Console.ReadLine().Split(' ');
        ulong high = ulong.Parse(input[0], System.Globalization.NumberStyles.HexNumber);
        ulong low = ulong.Parse(input[1], System.Globalization.NumberStyles.HexNumber);

        // Create a bit string object
        BitString bs = new BitString(high, low);

        // Print the bit string
        Console.WriteLine("Bit string:");
        Console.WriteLine($"High: {bs.High:X16}");
        Console.WriteLine($"Low: {bs.Low:X16}");

        // Get shift direction and amount from user
        Console.WriteLine("Enter 'l' to shift left or 'r' to shift right:");
        string direction = Console.ReadLine();

        Console.WriteLine("Enter the number of bits to shift:");
        int count = int.Parse(Console.ReadLine());

        // Perform the shift operation
        BitStringBase result;
        if (direction == "l")
        {
            result = bs.ShiftLeft(count);
        }
        else if (direction == "r")
        {
            result = bs.ShiftRight(count);
        }
        else
        {
            Console.WriteLine("Invalid direction");
            return;
        }

        // Print the result
        Console.WriteLine("Result:");
        Console.WriteLine($"High: {result.High:X16}");
        Console.WriteLine($"Low: {result.Low:X16}");
    }
}