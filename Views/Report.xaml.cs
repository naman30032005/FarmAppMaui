namespace Farm.Views;

public partial class Report : ContentPage
{
	ReportVM viewmodel;
	public Report(ReportVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
		viewmodel = vm;
		vm.PredictedPorl = 0;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await viewmodel.CalculateFields();
    }
}