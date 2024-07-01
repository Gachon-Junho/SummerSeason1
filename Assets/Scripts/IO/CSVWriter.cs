using System.Collections.Generic;
using System.IO;

public class CSVWriter
{
    public const string FILE_NAME = "People.csv";

    public void Write(IEnumerable<Person> data)
    {
        using (StreamWriter sw = new StreamWriter(File.Open(Path.Combine("Assets", FILE_NAME), FileMode.OpenOrCreate)))
        {
            sw.WriteLine($"{nameof(Person.Name)},{nameof(Person.Age)},{nameof(Person.Sex)},{nameof(Person.Hobby)},{nameof(Person.JobType)}");

            foreach (var people in data)
                sw.WriteLine($"{people.Name},{people.Age},{people.Sex},{people.Hobby},{people.JobType}");
        }
    }
}
