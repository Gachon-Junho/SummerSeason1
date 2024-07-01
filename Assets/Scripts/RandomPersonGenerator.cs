using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomPersonGenerator
{
    char[] LastName = { '김', '이', '박', '최', '정', '강', '조', '윤', '장' };

    string[] MaleFirstName = { "민준", "서준", "도윤", "시우", "예준", "하준", "지호", "주원", "지우", "준우",
                               "준서", "도현", "현우", "건우", "우진", "지훈", "선우", "유준", "은우", "서진",
                               "민재", "이준", "현준", "시윤", "연우" };

    string[] FemaleFirstName = { "서윤", "서연", "지우", "하윤", "서현", "하은", "민서", "지유", "윤서", "채원",
                                 "수아", "지민", "지아", "지윤", "다은", "은서", "지안", "예은", "서아", "소율",
                                 "예린", "수빈", "하린", "소윤", "예원" };

    System.Random random = new System.Random();

    public event Action<Person> OnGet;

    public Person Get()
    {
        Sex sex = (Sex)random.Next(3);
        string firstName = sex == Sex.NotSelected ? MaleFirstName.Concat(FemaleFirstName).ToArray()[random.Next(MaleFirstName.Length + FemaleFirstName.Length)] :
                           (sex == Sex.Male ? MaleFirstName[random.Next(MaleFirstName.Length)] : FemaleFirstName[random.Next(FemaleFirstName.Length)]);
        string name = $"{LastName[random.Next(LastName.Length)]}{firstName}";
        int age = random.Next(20, 61);
        Hobby hobby = (Hobby)random.Next(10);
        JobType jobType = (JobType)random.Next(3);

        Person person = new Person
        {
            Name = name,
            Age = age,
            Sex = sex,
            Hobby = hobby,
            JobType = jobType
        };

        OnGet?.Invoke(person);

        return person;
    }
}

public class Person : IEquatable<Person>
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Sex Sex { get; set; }
    public Hobby Hobby { get; set; }
    public JobType JobType { get; set; }

    public bool Equals(Person person)
    {
        if (person == null)
            return false;
        
        return Name == person.Name &&
               Age == person.Age &&
               Sex == person.Sex &&
               Hobby == person.Hobby &&
               JobType == person.JobType;
    }

    /// <summary>
    /// 모든 요소의 값을 공백단위로 나열합니다. 
    /// </summary>
    /// <returns>나열된 값.</returns>
    public override string ToString() => $"{Name} {Age} {Sex} {Hobby} {JobType}";
}

public enum Sex
{
    Male,
    Female,
    NotSelected
}

public enum Hobby
{
    ReadBooks,
    PlayGame,
    Fishing,
    Drawing,
    Calligraphy,
    Coding,
    Driving,
    SingSong,
    Cooking,
    Sleeping
}

public enum JobType
{
    Programming,
    Planning,
    Art
}
