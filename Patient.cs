public class Patient
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Gmail {  get; set; }
    public string Phone_Number { get; set; }

    public Patient(string name, string surname, string gmail, string phone_Number)
    {
        Name = name;
        Surname = surname;
        Gmail = gmail;
        Phone_Number = phone_Number;
    }

    public override string ToString()
    {
        return $" Name: {Name}\n Surname: {Surname}\n Gmail: {Gmail}\n Phone number: +994 {Phone_Number}\n---------------------------\n";
    }
}