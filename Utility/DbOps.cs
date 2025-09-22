namespace Farm.Utility;

// DBOps Class to Handle Database Operations
public class DbOps
{
    // COnnection String
    private readonly SQLiteConnection _conn;

    public DbOps()
    {
        // get the Database name and path
        string dbName = "FarmData.db";
        string dbPath = Path.Combine(Current.AppDataDirectory, dbName);

        // create a file at that path is doesn't already exist
        if (!File.Exists(dbPath))
        {
            using Stream s = Current.OpenAppPackageFileAsync(dbName).Result;
            using MemoryStream ms = new MemoryStream();
            s.CopyTo(ms);
            File.WriteAllBytes(dbPath, ms.ToArray());
        }
        // open the connection and create the tables if does not exist
        _conn = new SQLiteConnection(dbPath);
        _conn.CreateTables<Cow, Sheep>();
    }

    // Reads the data and returns a list of all Animals
    public List<Animal> ReadData()
    {
        var animals = new List<Animal>();
        animals.AddRange(_conn.Table<Cow>().ToList());
        animals.AddRange(_conn.Table<Sheep>().ToList());
        return animals;
    }

    // Insert operation for DataBase
    public int Insert(Animal animal)
    {
        return _conn.Insert(animal);
    }

    // Delete Operations
    public int Delete(Animal animal)
    {
        return _conn.Delete(animal);
    }

    //Update Operations
    public int Update(Animal animal)
    {
        return _conn.Update(animal);
    }

}
