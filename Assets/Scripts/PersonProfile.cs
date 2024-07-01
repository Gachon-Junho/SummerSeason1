using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PersonProfile : MonoBehaviour
{
    [SerializeField]
    GameObject gameObjectPersonProfile;

    [SerializeField]
    TextMeshProUGUI text;

    public void Show(Person person)
    {
        text.text = $"Name: {person.Name}\nAge: {person.Age}\nSex: {person.Sex}\nHobby: {person.Hobby}\n Job: {person.JobType}";
        gameObjectPersonProfile.SetActive(true);
    }

    public void Hide()
    {
        gameObjectPersonProfile.SetActive(false);
    }
}
