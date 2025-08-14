using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Question5_InventoryRecords
{
    public interface IInventoryEntity { int Id { get; } }
    public record InventoryItem(int Id, string Name, int Quantity, DateTime DateAdded) : IInventoryEntity;

    public class InventoryLogger<T> where T : IInventoryEntity
    {
        private List<T> _log = new();
        private string _filePath;
        public InventoryLogger(string path) { _filePath = path; }
        public void Add(T item) => _log.Add(item);
        public List<T> GetAll() => new(_log);
        public void SaveToFile()
        {
            var json = JsonSerializer.Serialize(_log);
            File.WriteAllText(_filePath, json);
        }
        public void LoadFromFile()
        {
            if (File.Exists(_filePath))
                _log = JsonSerializer.Deserialize<List<T>>(File.ReadAllText(_filePath));
        }
    }

    public class InventoryApp
    {
        private InventoryLogger<InventoryItem> _logger;
        public InventoryApp(string path) { _logger = new InventoryLogger<InventoryItem>(path); }
        public void SeedSampleData()
        {
            _logger.Add(new InventoryItem(1, "USB Cable", 10, DateTime.Today));
            _logger.Add(new InventoryItem(2, "HDMI Adapter", 5, DateTime.Today));
        }
        public void SaveData() => _logger.SaveToFile();
        public void LoadData() => _logger.LoadFromFile();
        public void PrintAllItems()
        {
            foreach (var i in _logger.GetAll()) Console.WriteLine(i);
        }
        public static void Main()
        {
            var app = new InventoryApp("inventory.json");
            app.SeedSampleData();
            app.SaveData();
            var app2 = new InventoryApp("inventory.json");
            app2.LoadData();
            app2.PrintAllItems();
        }
    }
}
