using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class FileSystem
{
    // DOCTOR SERIALIZE AND DEserialize
    public void JsonSerializeMethod_DOCTOR(Admin admin, string FilePath)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true 
        };

        string jsonData = JsonSerializer.Serialize(admin.doctors, options);
        File.WriteAllText(FilePath, jsonData);
    }

    public void JsonDeserializeMethod_DOCTOR(Admin admin, string FilePath)
    {
        if (File.Exists(FilePath))
        {
            string jsonData = File.ReadAllText(FilePath);
            admin.doctors = JsonSerializer.Deserialize<List<Doctor>>(jsonData)!;
        }
    }

    // USER SERIALIZE AND DEserialize

    public void JsonSerializeMethod_USER(Admin admin, string FilePath)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string jsonData = JsonSerializer.Serialize(admin.users, options);
        File.WriteAllText(FilePath, jsonData);
    }

    public void JsonDeserializeMethod_USER(Admin admin, string FilePath)
    {
        if (File.Exists(FilePath))
        {
            string jsonData = File.ReadAllText(FilePath);
            admin.users = JsonSerializer.Deserialize<List<Patient>>(jsonData)!;
        }
    }
}
