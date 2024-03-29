﻿using CarListApp.Maui.Models;
using SQLite;

namespace CarListApp.Maui.Services;

// Daniel: this will be abstracted away behind an interface
public class CarServiceDatabase
{
    private SQLiteConnection _conn;
    string _dbPath;
    int result;

    public string StatusMessage;

    public CarServiceDatabase(string dbPath)
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

    public Car GetCar(int id) 
    {
        try
        {
            Init();

            return _conn.Table<Car>().FirstOrDefault(x => x.Id == id);
        }
        catch (Exception ex)
        {
            StatusMessage = "Failed to Retrieve Data.";
        }

        return null;
    }

    public void AddCar(Car car)
    {
        try
        {
            Init();

            if (car is null) throw new Exception("Null Car Record");

            result = _conn.Insert(car);

            StatusMessage = result == 0 ? "Insert Failed" : "Insert Successful";
        }
        catch (Exception ex)
        {
            StatusMessage = "Failed to Insert Data.";
        }
    }

    public void UpdateCar(Car car)
    {
        try
        {
            Init();

            if (car is null) throw new Exception("Null Car Record");

            result = _conn.Update(car);

            StatusMessage = result == 0 ? "Update Failed" : "Update Successful";
        }
        catch (Exception ex)
        {
            StatusMessage = "Failed to Update Data.";
        }
    }

    public int DeleteCar(int id)
    {
        try
        {
            Init();

            return _conn.Table<Car>().Delete(x => x.Id == id);
        }
        catch (Exception ex)
        {
            StatusMessage = "Failed to Delete Data.";

            return 0;
        }
    }

    private void Init()
    {
        if (_conn is not null) return;

        _conn = new SQLiteConnection(_dbPath);
        _conn.CreateTable<Car>();
    }
}
