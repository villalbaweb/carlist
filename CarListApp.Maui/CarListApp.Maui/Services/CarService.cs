using CarListApp.Maui.Models;
using SQLite;

namespace CarListApp.Maui.Services;

// Daniel: this will be abstracted away behind an interface
public class CarService
{
    private SQLiteConnection _conn;
    string _dbPath;

    public string StatusMessage;

    public CarService(string dbPath)
    {
        _dbPath = dbPath;
    }

    public List<Car> GetCars()
    {
        try
        {
            Init();

            return _conn.Table<Car>().ToList();
        }
        catch (Exception ex)
        {
            StatusMessage = "Failed to retrieve data.";
        }

        return new List<Car>();
    }

    private void Init()
    {
        if (_conn is not null) return;

        _conn = new SQLiteConnection(_dbPath);
        _conn.CreateTable<Car>();
    }
}
