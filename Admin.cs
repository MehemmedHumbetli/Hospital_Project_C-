class Admin
{
    public List<Doctor> doctors = new List<Doctor>();
    public List<Patient> users = new List<Patient>();
    public string Name { get; set; }
    public string Password { get; set; }

    public Admin(string name, string password)
    {
        Name = name;
        Password = password;
    }

    public void AddDoctor(Doctor doctor)
    {
        doctors.Add(doctor);
    }

    public bool RemoveDoctor(string name, string surname)
    {
        Doctor doctorToRemove = doctors.Find(d => d.Name == name && d.Surname == surname)!;
        if (doctorToRemove != null)
        {
            doctors.Remove(doctorToRemove);
            return true;
        }
        return false;
    }

    public bool RemoveUser(string name, string gmail)
    {
        Patient patientToRemove = users.Find(p => p.Name == name && p.Gmail == gmail)!;
        if (patientToRemove != null)
        {
            users.Remove(patientToRemove);
            return true;
        }
        return false;
    }

    public void ListDoctors()
    {
        foreach (var doctor in doctors)
        {
            Console.WriteLine(doctor);
        }
    }

    public void ListPatients()
    {
        foreach (var patient in users)
        {
            Console.WriteLine(patient);
        }
    }
}
