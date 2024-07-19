public class Doctor
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Specialty { get; set; }
    public int Experience { get; set; }
    public Dictionary<string, bool> TimeIntervals { get; set; }

    public Doctor(string name, string surname, string specialty, int experience, Dictionary<string, bool> timeIntervals)
    {
        Name = name;
        Surname = surname;
        Specialty = specialty;
        Experience = experience;
        TimeIntervals = timeIntervals ?? new Dictionary<string, bool>();
    }

    public override string ToString()
    {
        int counter = 1;
        return $"Name: {Name}\nSurname: {Surname}\nSpecialty: {Specialty}\nExperience: {Experience}\nTime Intervals:\n{string.Join("\n", TimeIntervals.Select(kv => $"[{counter++}] {kv.Key}  {(kv.Value ?  "[Not reserved]" : "[Reserved]")}"))}\n---------------------------\n";
    }

}
