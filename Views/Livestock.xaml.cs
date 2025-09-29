namespace Farm.Views;

public partial class Livestock : ContentPage
{
	LivestockPageVM viewmodel;
	public Livestock(LivestockPageVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
		viewmodel = vm;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await viewmodel.FillList();
	}

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);
    }  
}