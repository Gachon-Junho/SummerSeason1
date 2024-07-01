using System;
using System.Collections.Generic;
using System.IO;

public class CSVReader
{
    public IEnumerable<Person> Get(string file, bool createIfNotExists = true)
    {
        List<Person> people = new List<Person>();
        
        using (StreamReader sr = new StreamReader(File.Open(file, FileMode.OpenOrCreate, FileAccess.Read)))
        {
            var table = sr.ReadLine();

            if (table == null)
                return null;

            var propertiesName = table.Split(',');

            var nameIdx = Array.IndexOf(propertiesName, nameof(Person.Name));
            var ageIdx = Array.IndexOf(propertiesName, nameof(Person.Age));
            var sexIdx = Array.IndexOf(propertiesName, nameof(Person.Sex));
            var hobbyIdx = Array.IndexOf(propertiesName, nameof(Person.Hobby));
            var jobTypeIdx = Array.IndexOf(propertiesName, nameof(Person.JobType));
            
            string data = string.Empty;

            while ((data = sr.ReadLine()) != null)
            {
                var properties = data.Split(',');
                
                var person = new Person
                {
                    Name = properties[nameIdx],
                    Age = int.Parse(properties[ageIdx]),
                    Sex = (Sex)Enum.Parse(typeof(Sex), properties[sexIdx]),
                    Hobby = (Hobby)Enum.Parse(typeof(Hobby), properties[hobbyIdx]),
                    JobType = (JobType)Enum.Parse(typeof(JobType), properties[jobTypeIdx]),
                };
                
                people.Add(person);
            }
        }

        return people;
    }
}
