using Farm.Utitlity;

namespace Farm.ViewModels;

public class AddAnimalPageVM
{
    private readonly DbOps _dbs;
    public AddAnimalPageVM(DbOps dbs)
    {
        _dbs = dbs;
    }
}
