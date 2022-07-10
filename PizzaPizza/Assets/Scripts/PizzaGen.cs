using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaGen : MonoBehaviour
{
    [Tooltip("all of the types of pizza having a big old party")]
    public List<string> pizzaParty;
    [Tooltip("all of the pizza types that are in paraside, happily waiting to be used for generation")]
    private List<string> pizzaParadise;
    [Tooltip("all of the pizza types that are in purgatory, waiting to be freed so that they can be ordered again")]
    private List<string> pizzaPurgatory;
    [Tooltip("the number of poor pizza souls forced to wait in purgatory at once")]
    public int pizzaPurgatoryPopulation = 2;

    [Tooltip("all of the first names")]
    public List<string> firstNames;
    [Tooltip("the first names which can be used")]
    private List<string> validFirstNames;
    [Tooltip("the first names which are on hold")]
    private List<string> holdFirstNames;
    [Tooltip("the number of first names on hold at once")]
    public int holdFirstCount = 3;

    [Tooltip("all of the last names")]
    public List<string> lastNames;
    [Tooltip("the last names which can be used")]
    private List<string> validLastNames;
    [Tooltip("the last names which are on hold")]
    private List<string> holdLastNames;
    [Tooltip("the number of last names on hold at once")]
    public int holdLastCount = 4;

    // Start is called before the first frame update
    void Start()
    {
        pizzaParadise = new List<string>();
        pizzaPurgatory = new List<string>();
        validFirstNames = new List<string>();
        holdFirstNames = new List<string>();
        validLastNames = new List<string>();
        holdLastNames = new List<string>();

        foreach (string pizzaPizza in pizzaParty)
        {
            pizzaParadise.Add(pizzaPizza);
        }
        foreach (string s in firstNames)
        {
            validFirstNames.Add(s);
        }
        foreach (string s in lastNames)
        {
            validLastNames.Add(s);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetPizzaPopularVote()
    {
        string PizzaPopulistCandidate = pizzaParadise[Random.Range(0, pizzaParadise.Count)];
        pizzaParadise.Remove(PizzaPopulistCandidate);
        pizzaPurgatory.Add(PizzaPopulistCandidate);
        if(pizzaPurgatory.Count > pizzaPurgatoryPopulation)
        {
            pizzaParadise.Add(pizzaPurgatory[0]);
            pizzaPurgatory.RemoveAt(0);
        }
        return PizzaPopulistCandidate;
    }

    public string GetOrderName()
    {
        string firstName = validFirstNames[Random.Range(0, validFirstNames.Count)];
        validFirstNames.Remove(firstName);
        holdFirstNames.Add(firstName);
        if (holdFirstNames.Count > holdFirstCount)
        {
            validFirstNames.Add(holdFirstNames[0]);
            holdFirstNames.RemoveAt(0);
        }

        string lastName = validLastNames[Random.Range(0, validLastNames.Count)];
        validLastNames.Remove(firstName);
        holdLastNames.Add(firstName);
        if (holdLastNames.Count > holdLastCount)
        {
            validLastNames.Add(holdLastNames[0]);
            holdLastNames.RemoveAt(0);
        }

        return firstName + " " + lastName;
    }
}
