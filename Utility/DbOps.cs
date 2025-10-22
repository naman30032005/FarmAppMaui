namespace Farm.Utility;

// DBOps Class to Handle Database Operations
public class DbOps
{
    // COnnection String
    private readonly SQLiteAsyncConnection _conn;


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
        _conn = new SQLiteAsyncConnection(dbPath);
        _conn.CreateTablesAsync<Animal, Cow, Sheep>().Wait();

    }

    // Reads the data and returns a list of all Animals
    public async Task<List<Animal>> ReadDataAsync()
    {
        var animals = new List<Animal>();
        animals.AddRange(await _conn.Table<Cow>().ToListAsync());
        animals.AddRange(await _conn.Table<Sheep>().ToListAsync());
        return animals;
    }

    // Insert operation for DataBase
    public Task<int> Insert(Animal animal) => _conn.InsertAsync(animal);

    // Delete Operations

    public Task<int> Delete(Animal animal) => _conn.DeleteAsync(animal);

    //Update Operations
    public Task<int> Update(Animal animal) => _conn.UpdateAsync(animal);


    public async Task<int> GetTotalAnimalsAsync()
    {
        var cows = await _conn.Table<Cow>().ToListAsync();
        var sheep = await _conn.Table<Sheep>().ToListAsync();
        return cows.Sum(x => x.Quantity) + sheep.Sum(x => x.Quantity);
    }

}
