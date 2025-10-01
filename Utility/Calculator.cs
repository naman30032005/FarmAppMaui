namespace Farm.Utility;

public class Calculator
{
    public static float MilkSellingPrice{ get; private set; }
    public static float WoolSellingPrice { get; private set; }
    public static float GovernmentTax { get; private set; }

    // null constructor with default values
    public Calculator()
    {
        // al the given values are per kg
        MilkSellingPrice = 9.4f;
        WoolSellingPrice = 6.2f;
        GovernmentTax = 0.02f; 
    }

    // value constructore
    public Calculator(float MilkSellingPrice, float WoolSellingPrice, float GovernmentTax)
    {
        Calculator.MilkSellingPrice = MilkSellingPrice;
        Calculator.WoolSellingPrice = WoolSellingPrice;
        Calculator.GovernmentTax = GovernmentTax;
    }

    public static void UpdatePrice(float MilkSellingPrice, float WoolSellingPrice, float GovernmentTax)
    {
        Calculator.MilkSellingPrice = MilkSellingPrice;
        Calculator.WoolSellingPrice = WoolSellingPrice;
        Calculator.GovernmentTax = GovernmentTax;
    }

    // takes the list of all the animals and calculates different amounts to return the total income per day
    public float IncomePerDay(List<AnimalVM> animals)
    {
        int cCount = 0, sCount = 0;
        foreach (var animal in animals)
        {
            if (animal.AnimalType == nameof(Cow)) cCount++;
            else if (animal.AnimalType == nameof(Sheep)) sCount++;
        }

        return cCount * MilkSellingPrice + sCount * WoolSellingPrice;
    }

    // takes the list of the animals to calculate different amounts and return the total expenses per day
    public float ExpensePerDay(List<AnimalVM> animals)
    {
        float totalExpense = 0;
        float totalWeight = 0;
        foreach(var animal in animals)
        {
            totalExpense += animal.Expense;
            totalWeight += animal.Weight;
        }

        return totalExpense + totalWeight * 0.02f;
    }
}
