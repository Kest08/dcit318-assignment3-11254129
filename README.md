
---

 Question Summaries
 
Q1 – Finance Management System
- Implements `record` types for transactions.
- Uses interfaces for transaction processors (`MobileMoneyProcessor`, `BankTransferProcessor`, `CryptoWalletProcessor`).
- Demonstrates inheritance with a `SavingsAccount` (sealed) that overrides transaction behavior.
- Simulates processing multiple transactions and updating account balance.

Q2 – Healthcare System
- Uses a **generic repository** to manage `Patient` and `Prescription` records.
- Stores prescriptions in a `Dictionary<int, List<Prescription>>` grouped by patient ID.
- Includes methods to fetch and display prescriptions for a specific patient.
---
Q3 – Warehouse Inventory Management
- Uses a **marker interface** `IInventoryItem` for different product types.
- Implements two product types: `ElectronicItem` and `GroceryItem`.
- Uses a **generic inventory repository** with **custom exceptions**:
  - `DuplicateItemException`
  - `ItemNotFoundException`
  - `InvalidQuantityException`
- Demonstrates adding, removing, and updating stock.

Q4 – Grading System
- Reads student records from a `.txt` file.
- Validates data with custom exceptions:
  - `InvalidScoreFormatException`
  - `MissingFieldException`
- Assigns grades based on score and writes a summary report to a new file.

Q5 – Inventory Records with File Storage
- Uses a **C# record** `InventoryItem` to store immutable data.
- Implements a **generic inventory logger** for saving and loading data to/from JSON.
- Demonstrates seeding, saving, loading, and printing inventory items.

