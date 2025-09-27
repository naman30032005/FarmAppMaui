using Farm.Utitlity;

namespace Farm.Views;

public partial class AddAnimalPage : ContentPage
{
	private readonly DbOps _dbs;
	public AddAnimalPage(DbOps dbs)
	{
		InitializeComponent();
		_dbs = dbs;
	}


}