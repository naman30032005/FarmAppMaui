namespace Farm.Views;

public partial class DashBoard : ContentPage
{
	private readonly DashboardVM _vm;
	public DashBoard(DashboardVM vm)
	{
		InitializeComponent();
		BindingContext = _vm = vm;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await _vm.CalculateField();
	}
}