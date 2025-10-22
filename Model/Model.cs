namespace Farm.Model;

// Base class To Store Common Animal Data
public class Animal
{
    [PrimaryKey, AutoIncrement] public int ID { get; set; }

    public string AnimalType => GetType().Name;
    public float Expense { get; set; } // the cost for raising the animal

    public float Weight { get; set; }

    public string Colour{ get; set; }

    public int Quantity { get; set; } //Total of animals
}

// Cow Class To store Data Unique to Cows
[Table("Cow")]
public class Cow : Animal
{
    public float Milk { get; set; }// in kg per Day

    public override string ToString()
    {
        return $"{base.GetType().Name,-6}{ID,-6} {Expense,-8} {Weight,-7} {Colour,-7} {Milk,-5}";
    }
}

// Sheep Class to Store Data Unique to Sheeps
[Table("Sheep")] public class Sheep:Animal
{
    public float Wool { get; set; } // in kg per Day

    public override string ToString()
    {
        return $"{base.GetType().Name,-6}{ID,-6} {Expense,-8} {Weight,-7} {Colour,-7} {Wool,-5}";
    }
}

public class typeTotal
{
    public string category { get; set; } = string.Empty;
    public int total { get; set; }
}
