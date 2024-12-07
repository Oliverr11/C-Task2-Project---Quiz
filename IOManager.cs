using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Task2Exam;

public class IOManager
{
    public IOManager() { } 
    //quiz
    public void WriteJson(string path, object obj)
    {
        StreamWriter streamWriter = new StreamWriter(path);
        string jsonContent = JsonConvert.SerializeObject(obj); 
        streamWriter.WriteLine(jsonContent);
        streamWriter.Close();

    }
    //quiz
    public T ReadJson<T>(string path)
    {
        StreamReader reader = new StreamReader(path);
        string jsonContent = reader.ReadToEnd();
        reader.Close();
        return JsonConvert.DeserializeObject<T>(jsonContent);
    }
    private string userDataFolder = "UserData";


    //for user
    public void WriteUserToFile(string userName, User user)
    {
        if (!Directory.Exists(userDataFolder))
        {
            Directory.CreateDirectory(userDataFolder);
        }
        string filePath = Path.Combine(userDataFolder, $"{userName}.json");

        StreamWriter streamWriter = new StreamWriter(filePath);
        
        string jsonContent = JsonConvert.SerializeObject(user); 
        streamWriter.WriteLine(jsonContent);
        streamWriter.Close();
        
    }


    public User ReadUserFromFile(string userName)
    {
        string filePath = Path.Combine(userDataFolder, $"{userName}.json");

        if (File.Exists(filePath))
        {
            StreamReader reader = new StreamReader(filePath);
            
            string jsonContent = reader.ReadToEnd();
            reader.Close () ;
            return JsonConvert.DeserializeObject<User>(jsonContent);
            
        }   

        return null;  
    }

    public List<User> ReadAllUsers()
    {
        List<User> users = new List<User>();

        if (Directory.Exists(userDataFolder))
        {
            string[] userFiles = Directory.GetFiles(userDataFolder, "*.json");

            foreach (string file in userFiles)
            {
                StreamReader reader = new StreamReader(file);

                string jsonContent = reader.ReadToEnd();
                User user = JsonConvert.DeserializeObject<User>(jsonContent);
                users.Add(user);
                reader.Close ();
            }
        }

        return users;
    }
    public void DeleteUserFile(string userName)
    {
        string filePath = Path.Combine(userDataFolder, $"{userName}.json");
        File.Delete(filePath);
    }

}