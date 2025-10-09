namespace Farm.Views;

public partial class QueryPage : ContentPage
{
	public QueryPage(QueryPageVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}