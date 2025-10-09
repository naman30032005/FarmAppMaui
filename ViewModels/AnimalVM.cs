namespace Farm.ViewModels;

// Animal Vm For Auto Updating the Observable Collection When an animal is updated
public partial class AnimalVM : ObservableObject
{
    public Animal animal { get; set; }

    public AnimalVM(Animal animal)
    {
        this.animal = animal;

        expense = animal.Expense;
        weight = animal.Weight;
        colour = animal.Colour;

        // for derived classes
        if (animal is Cow cow) milk = cow.Milk; 
        if (animal is Sheep sheep) wool = sheep.Wool; 
    }

    public int ID => animal.ID;

    public string AnimalType => animal.AnimalType;

    [ObservableProperty]
    private float expense;
    
    [ObservableProperty]
    private float weight;
    
    [ObservableProperty]
    private string colour;

    [ObservableProperty]
    private float milk;

    [ObservableProperty]
    private float wool;

    public string MilkOrWoolValue => animal switch { Cow c => $"{c.Milk:0.##}", Sheep s => $"{s.Wool:0.##}", _ => "-" };

    // syncing back to the model;
    partial void OnExpenseChanged(float value) => animal.Expense = value;
    partial void OnWeightChanged(float value) => animal.Weight = value;
    partial void OnColourChanged(string value) => animal.Colour = Utils.ConvertInputColor(value);
    partial void OnMilkChanged(float value) { if (animal is Cow cow) cow.Milk = value; }
    partial void OnWoolChanged(float value) { if (animal is Sheep sheep) sheep.Wool = value; }
}
