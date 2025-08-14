using System;
using System.Collections.Generic;
using System.Linq;

namespace Question3_WarehouseInventory
{
    public interface IInventoryItem { int Id { get; } string Name { get; } int Quantity { get; set; } }

    public class ElectronicItem : IInventoryItem
    {
        public int Id { get; } public string Name { get; } public int Quantity { get; set; }
        public string Brand; public int WarrantyMonths;
        public ElectronicItem(int id, string name, int qty, string brand, int warrantyMonths)
        { Id = id; Name = name; Quantity = qty; Brand = brand; WarrantyMonths = warrantyMonths; }
        public override string ToString() => $"{Id} {Name} ({Brand})";
    }

    public class GroceryItem : IInventoryItem
    {
        public int Id { get; } public string Name { get; } public int Quantity { get; set; }
        public DateTime ExpiryDate;
        public GroceryItem(int id, string name, int qty, DateTime expiryDate)
        { Id = id; Name = name; Quantity = qty; ExpiryDate = expiryDate; }
        public override string ToString() => $"{Id} {Name} Exp:{ExpiryDate:yyyy-MM-dd}";
    }

    public class DuplicateItemException : Exception { public DuplicateItemException(string m) : base(m) { } }
    public class ItemNotFoundException : Exception { public ItemNotFoundException(string m) : base(m) { } }
    public class InvalidQuantityException : Exception { public InvalidQuantityException(string m) : base(m) { } }

    public class InventoryRepository<T> where T : IInventoryItem
    {
        private Dictionary<int, T> _items = new();
        public void AddItem(T item)
        {
            if (_items.ContainsKey(item.Id)) throw new DuplicateItemException("Duplicate ID");
            _items[item.Id] = item;
        }
        public T GetItemById(int id)
        {
            if (!_items.ContainsKey(id)) throw new ItemNotFoundException("Not found");
            return _items[id];
        }
        public void RemoveItem(int id)
        {
            if (!_items.Remove(id)) throw new ItemNotFoundException("Not found");
        }
        public List<T> GetAllItems() => _items.Values.ToList();
        public void UpdateQuantity(int id, int qty)
        {
            if (qty < 0) throw new InvalidQuantityException("Negative qty");
            var item = GetItemById(id); item.Quantity = qty;
        }
    }

    public class WareHouseManager
    {
        private InventoryRepository<ElectronicItem> _electronics = new();
        private InventoryRepository<GroceryItem> _groceries = new();

        public void SeedData()
        {
            _electronics.AddItem(new ElectronicItem(1, "Laptop", 10, "Acer", 12));
            _groceries.AddItem(new GroceryItem(101, "Rice", 50, DateTime.Today.AddMonths(6)));
        }

        public void PrintAllItems<T>(InventoryRepository<T> repo) where T : IInventoryItem
        {
            foreach (var i in repo.GetAllItems()) Console.WriteLine(i);
        }

        public static void Main()
        {
            var m = new WareHouseManager();
            m.SeedData();
            m.PrintAllItems(m._electronics);
            m.PrintAllItems(m._groceries);
        }
    }
}
