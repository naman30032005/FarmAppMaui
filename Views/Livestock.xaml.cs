namespace Farm.Views;

public partial class Livestock : ContentPage
{
	public ObservableCollection<Animal> animals { get; set; } = new ObservableCollection<Animal>();
	private readonly DbOps _dbs;
	public Livestock(DbOps dbs)
	{
		InitializeComponent();
		BindingContext = this;
		_dbs = dbs;
		_dbs.ReadData().ForEach(x => animals.Add(x));
	}
}