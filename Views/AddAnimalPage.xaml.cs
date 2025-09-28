
namespace Farm.Views;

public partial class AddAnimalPage : ContentPage
{
	public AddAnimalPage(AddAnimalPageVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}