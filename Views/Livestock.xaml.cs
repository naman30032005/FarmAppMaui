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
		viewmodel.DeleteCommandEnabled = false;
		await viewmodel.FillList();
	}

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);
    }  
}