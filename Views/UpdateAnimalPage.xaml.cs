namespace Farm.Views;

public partial class UpdateAnimalPage : ContentPage
{
	public UpdateAnimalPage(UpdateAnimalPageVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}