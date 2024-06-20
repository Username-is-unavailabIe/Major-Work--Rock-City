using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Mainscript : MonoBehaviour
{
    //Variables Needed for each Rock- names, archetypes, subtypes (that was scrapped), stats, etc.
     List<string> firstnames = new List<string> {"Dave", "Alex", "Serena", "Dwayne", "Spencer", "Elizabeth", "Gabriel", "Phoebe", "Malcolm", "Abel", "Cain", "Fitz", "Hawk", "Faker", "Vera", "Lynette", "Kai", "Sunny", "Charlie", "Lilith"};
     List<string> lastnames = new List<string> {"Skullcrusher", "Slasher", "Frazer", "Blackguard", "Nightingale", "The Arcane", "The Wise", "The Unholy", "The Holy", "Farseer", "Windsor", "Bar-Giora", "Visoli", "Steel" };
    public List<string> archetypes = new List<string> { "Wizard", "Sorcerer", "Assassin", "Nimble", "Tank", "Fighter"};
    public List<string> subtypes = new List<string> { "Sky", "Forest", "Blood", "Sea", "Fire", "Earth"};
   //Pretty much every script needs acccess to game manager and game level off it
    public GameManager gm;
    public int GameLevel;
    public List<int> stats = new List<int>();
    public GameObject RockPrefab;
    public Transform RockGrid;
    public List<RockScript> Rocks = new List<RockScript>();
    // Start is called before the first frame update
    void Start()
    {
        //finds the game manager and gamelevel
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        GameLevel = gm.GameLevel;
        //Creates Rocks to pick from
        MakeRocks(4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeRocks(int number)
    {
        //Generates a specified number of rocks
        for (int i = 0; i < number; i++)
        {
            GenerateRock(Generatelevel());
        }
    }

    public void GenStats(int Max)
    {

        //Generates 20 numbers inbetween 1 and the level times the rarity + 1 

        for (int i = 0; i < 21; i++)
        {
            int temp = UnityEngine.Random.Range(1, Max);
            stats.Add(temp);

            //prints for debugging purposes
            print($"stat number {i} is {stats[i]}");
        }

        InsertionSort();
        
    }

    public int GetRarity()
    {
        //Generates a number corresponding to a rarity, with 1 being most common and 5 being least common
        int temp = UnityEngine.Random.Range(0, 100);
        if (temp > 94) { return 5; }
        else if (temp > 79 && temp < 95) { return 4; }
        else if (temp < 80 && temp > 59) { return 3; }
        else if (temp > 34 && temp < 60) { return 2; }
        else { return 1; }
    }

    public void SaveRock()
    {
        foreach (RockScript Rock in Rocks)
        {
            //Saves the Rock and copies the stats over to RockBattleScript
            if (Rock.Chosen)
            {
                print(Rock.name);
                gm.SpawnRock(Rock);
            }
        }

        CloseScene();
    }


    public void GenerateRock(int level)
    {
        // Rock NewRock = new Rock();
        GameObject NewRockPrefab = Instantiate(RockPrefab, RockGrid);
        RockScript NewRockScript = NewRockPrefab.GetComponent<RockScript>();
        Rocks.Add(NewRockScript);
        //Creates a name
        int random1st = UnityEngine.Random.Range(0, firstnames.Count);
        int random2nd = UnityEngine.Random.Range(0, lastnames.Count);
        string name = firstnames[random1st] + " " + lastnames[random2nd];
        print(name);
        NewRockScript.name = name;
       
        
        //Generates a level
        NewRockScript.level = level;
        

        //Generates archetype and subtype (Subtypes not used)
        int randomarch = UnityEngine.Random.Range(0, archetypes.Count);
        int randomsub = UnityEngine.Random.Range(0, subtypes.Count);
        NewRockScript.Archetype = archetypes[randomarch];
        NewRockScript.subtype = subtypes[randomsub];


        NewRockScript.rarity = GetRarity();

        //generates states 
        GenStats((NewRockScript.level*(NewRockScript.rarity+1)));

        //Sets the stats for the rock depending on
        if (NewRockScript.Archetype == "Wizard")
        {
            NewRockScript.magecraft = stats[20];
            NewRockScript.speed = Mathf.RoundToInt(stats[6]);
            NewRockScript.health = 3 * stats[5];
            NewRockScript.defence = Mathf.RoundToInt(stats[4]/4);
            NewRockScript.attack = stats[3];
        }
        else if (NewRockScript.Archetype == "Sorcerer")
        {
            NewRockScript.magecraft = stats[14];
            NewRockScript.speed = Mathf.RoundToInt(stats[11]); ;
            NewRockScript.health = 3*stats[8];
            NewRockScript.defence = Mathf.RoundToInt(stats[9] / 4);
            NewRockScript.attack = stats[10];
        }
        else if (NewRockScript.Archetype == "Healer")
        {
            NewRockScript.magecraft = stats[8];
            NewRockScript.speed = Mathf.RoundToInt(stats[6]);
            NewRockScript.health = 3 * stats[7];
            NewRockScript.defence = Mathf.RoundToInt(stats[3] / 4);
            NewRockScript.attack = stats[0];
        }
        else if (NewRockScript.Archetype == "Assassin")
        {
            NewRockScript.magecraft = stats[2];
            NewRockScript.speed = NewRockScript.speed = Mathf.RoundToInt(stats[12]);

            NewRockScript.health = 3*stats[0];
            NewRockScript.defence = Mathf.RoundToInt(stats[1] / 4);
            NewRockScript.attack = stats[14];
        }
        else if (NewRockScript.Archetype == "Nimble")
        {
            NewRockScript.magecraft = stats[2];
            NewRockScript.speed = Mathf.RoundToInt(stats[20]);

            NewRockScript.health = 3 * stats[8];
            NewRockScript.defence = Mathf.RoundToInt(stats[7] / 4);
            NewRockScript.attack = stats[10];
        }
        else if (NewRockScript.Archetype == "Tank")
        {
            NewRockScript.magecraft = stats[0];
            NewRockScript.speed = Mathf.RoundToInt(stats[1]);

            NewRockScript.health = 4 * stats[18];
            NewRockScript.defence = Mathf.RoundToInt(stats[16] / 4);
            NewRockScript.attack = stats[10];
        }
        else
        {
            NewRockScript.magecraft = stats[0];
            NewRockScript.speed = Mathf.RoundToInt(stats[11]);
            NewRockScript.health = 3 * stats[10];
            NewRockScript.defence = Mathf.RoundToInt(stats[12] / 4);
            NewRockScript.attack = stats[14];
        }

        stats.Clear();

        NewRockScript.Chosen = IsChosen(NewRockScript.ChosenToggle);

    }

    public void ChooseRocks()
    {

    }

    
    public bool IsChosen(UnityEngine.UI.Toggle Checkbox) 
    {
        if (Checkbox.isOn)
        {
            return true;
        }
        else { return false; }
    }
    public void CloseScene()
    {
        SceneManager.LoadScene(1);
    }

    public int Generatelevel()
    {
        int level = 1;
        for (int i = 0; i < gm.GameLevel; i++)
        {
            level = level + UnityEngine.Random.Range(0, 2);
  
        }
        return level;



    }



    public void InsertionSort()
    {
        for (int i = 1; i < stats.Count; i++)
        {
            //For each number it looks at the number before and sees if it is lower and if it is they swap, and then it compares to the next number before and so on for each number

            for (int a = i; a > 0; a--)
            {
                if (stats[a] < stats[a - 1])
                {
                    SwapNumbers(a, a - 1);
                }
            }

        }
    }

    public void SwapNumbers(int numA, int numB)
    {
        //Function to swap two numbers in the numbers array
        int Placeholder = stats[numA];
        stats[numA] = stats[numB];
        stats[numB] = Placeholder;

    }


}
public class Rock
{
    public int level;
    public string name;
    public int rarity;
    public string Archetype;
    public string subtype;
    public int attack;
    public int defence;
    public int health;
    public int magecraft;
    public int speed;


    public int ability1;
    public int ability2;
    public int ability3;
}