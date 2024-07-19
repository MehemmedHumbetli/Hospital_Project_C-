using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
class Program
{
    static Admin admin = new Admin("A", "A");
    static FileSystem fs = new FileSystem();
    static void UserAdmin_Menu()
    {
        Console.WriteLine(" [1] Patient");
        Console.WriteLine(" [2] Admin\n");
    }

    static void Admin_menu()
    {
        Console.Clear();
        Console.WriteLine(" [1] Add Doctor");
        Console.WriteLine(" [2] Delete Doctor");
        Console.WriteLine(" [3] Doctor's list");
        Console.WriteLine(" [4] Wiew Patients");
        Console.WriteLine(" [5] Delete Patient");
    }
    
    static void Department_Menu()
    {
        Console.WriteLine(" [1] Pediatrics");
        Console.WriteLine(" [2] Traumatology");
        Console.WriteLine(" [3] Stomatology");
    }

    static void Main()
    {
        admin.users.Clear();
        fs.JsonDeserializeMethod_USER(admin, "users.json");
        admin.doctors.Clear();
        fs.JsonDeserializeMethod_DOCTOR(admin, "doctors.json");
        Console.ResetColor();
        UserAdmin_Menu();
        Console.WriteLine("Choose the your option: ");
        int option;
        try
        {
            option = Convert.ToInt32(Console.ReadLine());
        }
        catch (FormatException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Only number enter!");
            Console.ReadKey();
            Console.Clear();
            Main();
            return;
        }
        switch (option)
        {
            case 1:
                {
                    SendEmailWithGoogleSMTP email_msg = new SendEmailWithGoogleSMTP();
                    string gmail;
                    string ph_number;

                    Console.WriteLine("Enter your Name: ");
                    string name = Console.ReadLine()!;
                    Console.WriteLine("Enter your Surname: ");
                    string surname = Console.ReadLine()!;
                    
                    while(true)
                    {
                        Console.WriteLine("Enter your gmail: ");
                        gmail = Console.ReadLine()!;
                        if (gmail.EndsWith("@gmail.com"))
                        {
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("The email address must end with @gmail.com!");
                            Console.ResetColor();
                        }
                    }
                    while(true)
                    {
                        Console.WriteLine("Enter your phone number: +994 ");
                        ph_number = Console.ReadLine()!;
                        bool numberDIGIT = ph_number.Any(char.IsDigit);
                        if (ph_number.Length < 10 && numberDIGIT)
                        {
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Enter only digits and the number length should be maximum 4!");
                            Console.ResetColor();
                        }
                    }

                    Patient patient = new Patient(name, surname, gmail, ph_number);
                    admin.users.Add(patient);
                    fs.JsonSerializeMethod_USER(admin, "users.json");
                    email_msg.Send_Email_Msg(patient);
                    
                    while (true)
                    {
                        int choice = -1;
                        Console.ResetColor();
                        Console.Clear();
                        Department_Menu();
                        Console.WriteLine("Choose the your option: ");
                        try
                        {
                            choice = Convert.ToInt32(Console.ReadLine())!;
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        switch (choice)
                        {
                            case 1:
                                {
                                    int dc_choice = -1;
                                    int dc_count = 1;
                                    Console.Clear();

                                    var pediatricDoctors = admin.doctors.Where(s => s.Specialty == "Pediatrics").ToList();

                                    pediatricDoctors.ForEach(s => Console.WriteLine($" [{dc_count++}] {s.Name} {s.Surname}\n"));

                                    Console.WriteLine("Choose the Doctor: ");
                                    try
                                    {
                                        dc_choice = Convert.ToInt32(Console.ReadLine());
                                    }
                                    catch(Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }

                                    if (dc_choice == 0)
                                    {
                                        break;
                                    }
                                    int select_time = 0;
                                    if (dc_choice > 0 && dc_choice <= pediatricDoctors.Count)
                                    {
                                        var selectedDoctor = pediatricDoctors[dc_choice - 1];
                                        Console.WriteLine(selectedDoctor.ToString());
                                        Console.WriteLine("Select time: ");
                                        try
                                        {
                                            select_time = Convert.ToInt32(Console.ReadLine());
                                        }
                                        catch(Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        if (select_time != 0 && select_time <= 3)
                                        {
                                            string timeKey = selectedDoctor.TimeIntervals.Keys.ElementAt(select_time - 1);
                                            if (selectedDoctor.TimeIntervals[timeKey])
                                            {
                                                selectedDoctor.TimeIntervals[timeKey] = false;
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine("Time successfully reserved");
                                                fs.JsonSerializeMethod_DOCTOR(admin, "doctors.json");
                                            }
                                            else
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("This time is already reserved! Please select another time");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Invalid choice.");
                                    }

                                    Console.ReadKey();
                                    break;
                                }
                            case 2:
                                {
                                    int dc_choice = -1;
                                    int dc_count = 1;
                                    Console.Clear();

                                    var traumatologyDoctors = admin.doctors.Where(s => s.Specialty == "Traumatology").ToList();

                                    traumatologyDoctors.ForEach(s => Console.WriteLine($" [{dc_count++}] {s.Name} {s.Surname}\n"));

                                    Console.WriteLine("Choose the Doctor: ");
                                    
                                    try
                                    {
                                        dc_choice = Convert.ToInt32(Console.ReadLine());
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }

                                    if (dc_choice == 0)
                                    {
                                        break;
                                    }

                                    if (dc_choice > 0 && dc_choice <= traumatologyDoctors.Count)
                                    {
                                        var selectedDoctor = traumatologyDoctors[dc_choice - 1];
                                        Console.WriteLine(selectedDoctor.ToString());
                                        Console.WriteLine("Select time: ");
                                        int select_time = Convert.ToInt32(Console.ReadLine());
                                        if (select_time != 0 && select_time <= 3)
                                        {
                                            string timeKey = selectedDoctor.TimeIntervals.Keys.ElementAt(select_time - 1);
                                            if (selectedDoctor.TimeIntervals[timeKey])
                                            {
                                                selectedDoctor.TimeIntervals[timeKey] = false;
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine("Time successfully reserved");
                                                fs.JsonSerializeMethod_DOCTOR(admin, "doctors.json");
                                            }
                                            else
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("This time is already reserved! Please select another time");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Invalid choice.");
                                    }

                                    Console.ReadKey();
                                    break;
                                }
                            case 3:
                                {
                                    int dc_choice = -1;
                                    int dc_count = 1;
                                    Console.Clear();

                                    var stomatologyDoctors = admin.doctors.Where(s => s.Specialty == "Stomatology").ToList();

                                    stomatologyDoctors.ForEach(s => Console.WriteLine($" [{dc_count++}] {s.Name} {s.Surname}\n"));

                                    Console.WriteLine("Choose the Doctor: ");
                                    
                                    try
                                    {
                                        dc_choice = Convert.ToInt32(Console.ReadLine());
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }

                                    if (dc_choice == 0)
                                    {
                                        break;
                                    }

                                    if (dc_choice > 0 && dc_choice <= stomatologyDoctors.Count)
                                    {
                                        var selectedDoctor = stomatologyDoctors[dc_choice - 1];
                                        Console.WriteLine(selectedDoctor.ToString());
                                        Console.WriteLine("Select time: ");
                                        int select_time = Convert.ToInt32(Console.ReadLine());
                                        if (select_time != 0 && select_time <= 3)
                                        {
                                            string timeKey = selectedDoctor.TimeIntervals.Keys.ElementAt(select_time - 1);
                                            if (selectedDoctor.TimeIntervals[timeKey])
                                            {
                                                selectedDoctor.TimeIntervals[timeKey] = false;
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine("Time successfully reserved");
                                                fs.JsonSerializeMethod_DOCTOR(admin, "doctors.json");
                                            }
                                            else
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("This time is already reserved! Please select another time");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Invalid choice.");
                                    }

                                    Console.ReadKey();
                                    break;
                                }
                            case 0:
                                {
                                    Main();
                                    return;
                                }
                        }

                        Console.Clear();
                    }
                    break;
                }
            case 2:
                {
                    Console.WriteLine("Enter Admin Name: ");
                    string name = Console.ReadLine()!;
                    Console.WriteLine("Enter Admin Password: ");
                    string surname = Console.ReadLine()!;
                    if (name == admin.Name)
                    {
                        if (surname == admin.Password)
                        {
                            while (true)
                            {
                                int choice = -1;
                                Admin_menu();
                                Console.WriteLine("Choose the your option: ");
                                try
                                {
                                    choice = Convert.ToInt32(Console.ReadLine());
                                }
                                catch(Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                if (choice == 0)
                                {
                                    Main();
                                    return;
                                }
                                switch (choice)
                                {
                                    case 1:
                                        {
                                            string dcName;
                                            string dcSurname;
                                            int experience = 0;
                                            string dept;
                                            Dictionary<string, bool> times = new Dictionary<string, bool>();
                                            while (true)
                                            {
                                                Console.WriteLine("Enter Doctor name: ");
                                                dcName = Console.ReadLine()!;
                                                Console.WriteLine("Enter Doctor surname: ");
                                                dcSurname = Console.ReadLine()!;

                                                while (true)
                                                {
                                                    Console.WriteLine("Enter Doctor's Department \n[1] Pediatrics [2] Traumatology [3] Stomatology: ");
                                                    dept = Console.ReadLine()!;
                                                    if (dept == "1" || dept == "Pediatrics")
                                                    {
                                                        dept = "Pediatrics";
                                                        break;
                                                    }
                                                    else if (dept == "2" || dept == "Traumatology")
                                                    {
                                                        dept = "Traumatology";
                                                        break;
                                                    }
                                                    else if (dept == "3" || dept == "Stomatology")
                                                    {
                                                        dept = "Stomatology";
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Department not found!");
                                                    }
                                                }
                                                while (true)
                                                {
                                                    Console.WriteLine("Enter Work experience with year: ");
                                                    try
                                                    {
                                                        experience = Convert.ToInt32(Console.ReadLine());
                                                    }
                                                    catch (FormatException)
                                                    {
                                                        Console.WriteLine("Invalid input. Please enter a valid number for experience.");
                                                    }
                                                    if (experience != 0)
                                                    {
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Experience cannot be '0'. Please enter a valid number.");
                                                    }
                                                }

                                                if (dcName != "" && dcSurname != "" && dept != "")
                                                {
                                                    break;
                                                }
                                                else
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine("The question cannot be skipped!");
                                                    Console.ReadKey();
                                                    Console.ResetColor();
                                                }
                                            }

                                            for (int i = 0; i < 3; i++)
                                            {
                                                Console.WriteLine($"Time Interval {i + 1}:");
                                                Console.WriteLine("Start time (HH:MM): ");
                                                string startTime = Console.ReadLine()!;
                                                string pattern = @"\b\d{2}:\d{2} / \d{2}:\d{2}\b";
                                                if (Regex.IsMatch(startTime,pattern))
                                                {
                                                    times.Add(startTime, true);
                                                }
                                                else
                                                {
                                                    i--;
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine("Please enter a valid time! This in format (HH:mm / HH:mm)");
                                                    Console.ResetColor();
                                                }
                                            }

                                            Doctor dc = new Doctor(dcName, dcSurname, dept, experience, times);
                                            admin.AddDoctor(dc);
                                            fs.JsonSerializeMethod_DOCTOR(admin, "doctors.json");
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("\nDoctor added to the system with time intervals");
                                            Console.ReadKey();
                                            Console.ResetColor();
                                            break;
                                        }
                                    case 2:
                                        {
                                            Console.WriteLine("Enter delete Name: ");
                                            string delName = Console.ReadLine()!;
                                            Console.WriteLine("enter delete surname: ");
                                            string delSurname = Console.ReadLine()!;
                                            if (admin.RemoveDoctor(delName, delSurname))
                                            {
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine("The doctor was removed from the system");
                                                fs.JsonSerializeMethod_DOCTOR(admin, "doctors.json");
                                                Console.ReadKey();
                                            }
                                            else
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("The doctor was not removed from the system!!");
                                                Console.ReadKey();
                                                Main();
                                                return;
                                            }
                                            break;
                                        }
                                    case 3:
                                        {
                                            Console.Clear();
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine("List of Doctors\n");
                                            Console.ResetColor();
                                            admin.ListDoctors();
                                            Console.ReadKey();
                                            Console.Clear();
                                            break;
                                        }
                                    case 4:
                                        {
                                            Console.Clear();
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine("List of Patients\n");
                                            Console.ResetColor();
                                            admin.ListPatients();
                                            Console.ReadKey();
                                            Console.Clear();
                                            break;
                                        }
                                    case 5:
                                        {
                                            Console.WriteLine("Enter patient name: ");
                                            string Pname = Console.ReadLine()!;
                                            Console.WriteLine("Enter patient gmail: ");
                                            string Pgmail = Console.ReadLine()!;
                                            if (admin.RemoveUser(Pname, Pgmail) == true)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine("The Patient was removed from the system");
                                                fs.JsonSerializeMethod_USER(admin, "users.json");
                                                Console.ReadKey();
                                            }
                                            else
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("The Patient was not removed from the system!!");
                                                Console.ReadKey();
                                            }
                                            Console.ResetColor();
                                            break;
                                        }
                                    default:
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("Wrong choice");
                                            Console.ReadKey();
                                            Console.ResetColor();
                                            break;
                                        }
                                }
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Admin not found");
                            ConsoleKeyInfo key = Console.ReadKey();
                            Main();
                            return;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Admin not found");
                        ConsoleKeyInfo key = Console.ReadKey();
                        Console.Clear();
                        Main();
                        return;
                    }
                }
            default:
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong choice");
                    ConsoleKeyInfo key = Console.ReadKey();
                    Console.Clear();
                    Console.ResetColor();
                    Main();
                    return;
                }
        }
    }
}