using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PeopleManager : MonoBehaviour
{
    [SerializeField]
    GameObject personCard;

    [SerializeField]
    GameObject scrollContent;

    [SerializeField] 
    Button createButton;

    RandomPersonGenerator randomPersonGenerator = new RandomPersonGenerator();

    public GameObject PersonProfile;

    List<Person> people = new List<Person>();
    List<Person> filteredPeople = new List<Person>();

    CSVWriter csvWriter = new CSVWriter();
    CSVReader csvReader = new CSVReader();

    bool isDescending = true;
    int orderPropertyIndex = 0;
    string query = string.Empty;

    private void Start()
    {
        var data = csvReader.Get(Path.Combine("Assets", CSVWriter.FILE_NAME));

        if (data == null)
            return;

        foreach (var person in data)
            people.Add(person);

        createButton.interactable = false;
        
        updateItems();
    }

    public void Add(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var person = randomPersonGenerator.Get();
            people.Add(person);
        }

        if (createButton != null)
            createButton.interactable = false;
        
        updateItems();
        
        csvWriter.Write(people);
    }

    public void ScrollChanged(ScrollRect scroll)
    {
        if (scroll.verticalNormalizedPosition <= 0.01f)
        {
            var pending = filteredPeople.Except(scrollContent.GetComponentsInChildren<PersonCard>().Select(p => p.Person));

            if (pending.Count() == 0)
                return;
            
            var newPerson = Instantiate(personCard);
        
            newPerson.transform.SetParent(scrollContent.transform);
            newPerson.GetComponent<PersonCard>().Person = pending.First();
        }
    }

    public void QueryChanged(TMP_InputField inputField)
    {
        query = inputField.text.Trim();
        
        updateItems();
    }

    public void OrderDirectionChanged(bool isDescending)
    {
        this.isDescending = isDescending;
        
        updateItems();
    }

    public void OrderPropertyChanged(TMP_Dropdown dropdown)
    {
        orderPropertyIndex = dropdown.value;
        
        updateItems();
    }

    async void updateItems()
    {
        var filterTask = Task.Run(() => filter(query).ToList());
        
        filteredPeople = await filterTask;

        foreach (var card in scrollContent.GetComponentsInChildren<PersonCard>())
            Destroy(card.gameObject);

        // ScrollView.Height / (PersonCard.Height + VerticalSpacing) + 1 = 5
        for (int i = 0; i < 5; i++)
        {
            if (i + 1 >= filteredPeople.Count)
                return;
            
            var newPerson = Instantiate(personCard);
        
            newPerson.transform.SetParent(scrollContent.transform);
            newPerson.GetComponent<PersonCard>().Person = filteredPeople[i];
        }

        /*foreach (var person in filteredPeople)
        {
            var newPerson = Instantiate(personCard);
        
            newPerson.transform.SetParent(scrollContent.transform);
            newPerson.GetComponent<PersonCard>().Person = person;
        }*/
    }

    IEnumerable<Person> filter(string query)
    {
        if (string.IsNullOrEmpty(query))
            return order(isDescending, orderPropertyIndex);
        
        List<Person> filtered = new List<Person>();

        foreach (var person in order(isDescending, orderPropertyIndex))
        {
            var keywords = query.ToLower().Split(' ');
            var availiable = person.ToString().ToLower().Split().Intersect(keywords, new KeywordEqualityComparer());
            
            if (availiable.Count() == keywords.Length)
                filtered.Add(person);
        }

        return filtered;
    }

    IEnumerable<Person> order(bool isDescending, int orderPropertyIndex)
    {
        switch (getIndexToName(orderPropertyIndex))
        {
            case nameof(Person.Name):
                return isDescending ? people.OrderByDescending(p => p.Name) : people.OrderBy(p => p.Name);
            
            case nameof(Person.Age):
                return isDescending ? people.OrderByDescending(p => p.Age) : people.OrderBy(p => p.Age);
            
            case nameof(Person.Sex):
                return isDescending ? people.OrderByDescending(p => p.Sex) : people.OrderBy(p => p.Sex);
            
            case nameof(Person.Hobby):
                return isDescending ? people.OrderByDescending(p => p.Hobby) : people.OrderBy(p => p.Hobby);
            
            case nameof(Person.JobType):
                return isDescending ? people.OrderByDescending(p => p.JobType) : people.OrderBy(p => p.JobType);
            
            default:
                throw new IndexOutOfRangeException();
        }
    }

    string getIndexToName(int index)
    {
        switch (index)
        {
            case 0:
                return nameof(Person.Name);
            
            case 1:
                return nameof(Person.Age);
            
            case 2:
                return nameof(Person.Sex);
            
            case 3:
                return nameof(Person.Hobby);
            
            case 4:
                return nameof(Person.JobType);
            
            default:
                throw new IndexOutOfRangeException("Unknown index.");
        }
    }
}
