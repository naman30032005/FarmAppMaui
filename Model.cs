namespace Farm;
public class Animal
{
    [PrimaryKey, AutoIncrement] public int ID { get; set; }
    public float Expense { get; set; }

    public float Weight { get; set; }

    public float Colour { get; set; }
}

[Table("Cow")]
public class Cow : Animal
{
    public float Milk { get; set; }

    public override string ToString()
    {
        return $"{ID,-6} {Expense,-8} {Weight,-7} {Colour,-7} {Milk,-5}";
    }
}

[Table("Sheep")] public class Sheep:Animal
{
    public float Wool { get; set; }

    public override string ToString()
    {
        return $"{ID,-6} {Expense,-8} {Weight,-7} {Colour,-7} {Wool,-5}";
    }
}
