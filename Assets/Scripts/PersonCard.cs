using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PersonCard : MonoBehaviour
{
    [SerializeField]
    public GameObject Name;

    [SerializeField]
    public GameObject Age;

    [SerializeField]
    public GameObject Sex;

    GameObject PersonProfile;
    PersonProfile popup => PersonProfile.GetComponent<PersonProfile>();
    public Person Person;

    // Start is called before the first frame update
    void Start()
    {
        PersonProfile = GameObject.Find("PeopleManager").GetComponent<PeopleManager>().PersonProfile;
        Name.GetComponent<TextMeshProUGUI>().text = $"Name: {Person.Name}";
        Age.GetComponent<TextMeshProUGUI>().text = $"Age: {Person.Age}";
        Sex.GetComponent<TextMeshProUGUI>().text = $"Sex: {Person.Sex}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClicked()
    {
        popup.Show(Person);
    }
}
