using System;
using System.Collections.Generic;
using System.Linq;

namespace Question2_HealthcareSystem
{
    public class Repository<T>
    {
        private List<T> items = new();
        public void Add(T item) => items.Add(item);
        public List<T> GetAll() => new(items);
        public T? GetById(Func<T, bool> predicate) => items.FirstOrDefault(predicate);
        public bool Remove(Func<T, bool> predicate)
        {
            var item = items.FirstOrDefault(predicate);
            if (item == null) return false;
            return items.Remove(item);
        }
    }

    public class Patient
    {
        public int Id; public string Name; public int Age; public string Gender;
        public Patient(int id, string name, int age, string gender)
        { Id = id; Name = name; Age = age; Gender = gender; }
        public override string ToString() => $"{Id} {Name} ({Gender},{Age})";
    }

    public class Prescription
    {
        public int Id; public int PatientId; public string MedicationName; public DateTime DateIssued;
        public Prescription(int id, int patientId, string medicationName, DateTime dateIssued)
        { Id = id; PatientId = patientId; MedicationName = medicationName; DateIssued = dateIssued; }
        public override string ToString() => $"{Id} {MedicationName} ({DateIssued:yyyy-MM-dd})";
    }

    public class HealthSystemApp
    {
        private Repository<Patient> _patientRepo = new();
        private Repository<Prescription> _prescriptionRepo = new();
        private Dictionary<int, List<Prescription>> _prescriptionMap = new();

        public void SeedData()
        {
            _patientRepo.Add(new Patient(1, "Alice", 30, "F"));
            _patientRepo.Add(new Patient(2, "Bob", 45, "M"));
            _prescriptionRepo.Add(new Prescription(1, 1, "Drug A", DateTime.Today));
            _prescriptionRepo.Add(new Prescription(2, 2, "Drug B", DateTime.Today));
        }

        public void BuildPrescriptionMap()
        {
            _prescriptionMap = _prescriptionRepo.GetAll().GroupBy(p => p.PatientId)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        public List<Prescription> GetPrescriptionsByPatientId(int id)
            => _prescriptionMap.ContainsKey(id) ? _prescriptionMap[id] : new List<Prescription>();

        public void PrintAllPatients()
        {
            foreach (var p in _patientRepo.GetAll()) Console.WriteLine(p);
        }

        public void PrintPrescriptionsForPatient(int id)
        {
            foreach (var rx in GetPrescriptionsByPatientId(id)) Console.WriteLine(rx);
        }

        public static void Main()
        {
            var app = new HealthSystemApp();
            app.SeedData();
            app.BuildPrescriptionMap();
            app.PrintAllPatients();
            app.PrintPrescriptionsForPatient(1);
        }
    }
}
