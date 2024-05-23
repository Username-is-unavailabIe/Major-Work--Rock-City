
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

// This script controls the battle scene in the game
public class BattleSceneScript : MonoBehaviour
{
    // References to other scripts and game objects
    public GameManager gm;
    public RockScript rockScript;
    public int GameLevel;
    
   // public GameObject EnemyParent;
    public Transform PlayerGrid;
    public GameObject PlayerPrefab;
    public Transform EnemyGrid;
    public List<int> EStats = new List<int>();

    // List of possible enemy archetypes
    public List<string> archetypes = new List<string> { "Wizard", "Sorcerer", "Assassin", "Nimble", "Tank", "Fighter" };

    // Start is called before the first frame update
    void Start()
    {
        // Find the GameManager and get the Game Level from the script that is on the GameManager
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.EnemyParent = GameObject.Find("EnemiesParent");
        
        GameLevel = gm.GameLevel;

        // Find all Battle Rocks and creates a random amount of enemies (will be changed to
        FindBROCK();
        CreateEnemies(Random.Range((gm.BRocks.Count-GameLevel+1), gm.BRocks.Count+GameLevel-1));
        FindEnemy();
    }

    // Create an enemy with a random archetype and level
    public void MakeEnemy(int GameLevel)
    {
        print("make");
        // Instantiate enemy and get its script
        GameObject NewEnemy = Instantiate(gm.EnemyPrefab, gm.EnemyParent.transform);
        RockBattleScript EnemyScript = gm.EnemyPrefab.GetComponent<RockBattleScript>();
        // Determine enemy level based on game level, with it being slightly more random at higher levels
        int leveldeterminer = Random.Range(-(1 + GameLevel / 3), 1 + GameLevel / 3);
        EnemyScript.Archetype = archetypes[Random.Range(0, archetypes.Count)];

        // 10% chance to make the enemy a boss
        if (Random.Range(1, 10) == 10)
        {
            EnemyScript.level = Mathf.Abs(GameLevel + leveldeterminer+1) * Random.Range(2, 5);
            EnemyScript.name = $"BOSS: {EnemyScript.Archetype} {EnemyScript.level}";
        }
        else
        {

            EnemyScript.level = Mathf.Clamp(GameLevel + leveldeterminer, 1, 999);
            EnemyScript.name = $"{EnemyScript.Archetype} {EnemyScript.level}";
        }
        
        GenerateEstats(EnemyScript.level*5);

        if (EnemyScript.Archetype == "Wizard" || EnemyScript.Archetype == "Sorcerer")
        {
            EnemyScript.magecraft = EStats[EStats.Count-1];
            EnemyScript.attack = EStats[2];
            EnemyScript.health = EStats[Mathf.RoundToInt(EStats.Count/2)];
            EnemyScript.defence = EStats[Mathf.RoundToInt(EStats.Count / 2)-1];
            EnemyScript.speed = Mathf.Clamp((EnemyScript.defence)/ 3,1, 9999);
        }
        else if (EnemyScript.Archetype == "Fighter" || EnemyScript.Archetype == "Assassin" || EnemyScript.Archetype == "Tank")
        {
            EnemyScript.magecraft = EStats[0];
            EnemyScript.attack = EStats[EStats.Count-1];
            EnemyScript.health = EStats[EStats.Count - 2];
            EnemyScript.defence = EStats[EStats.Count - 3];
            EnemyScript.speed = Mathf.Clamp(EStats[2],1 , 9999);
        }
        else if (EnemyScript.Archetype == "Nimble")
        {
            EnemyScript.magecraft = EStats[0];
            EnemyScript.attack = EStats[Mathf.RoundToInt(EStats.Count/2)];
            EnemyScript.health = EStats[4];
            EnemyScript.defence = EStats[3];
            EnemyScript.speed = EStats[Mathf.RoundToInt(Mathf.Clamp(EStats.Count/3, 1, 9999))];
        }

        gm.Enemies.Add(NewEnemy);
    }

    public void GenerateEstats(int number)
    {
        for (int i = 0; i < number; i++)
        {
            EStats.Add(Random.Range(GameLevel, GameLevel*5));
        }
        InsertionSort();

    }

    // Find all Battle Rocks and instantiate a prefab for each
    public void FindBROCK()
    {
        for (int i = 0; i < gm.BRocks.Count; i++)
        {
            GameObject Prefabi = Instantiate(PlayerPrefab, PlayerGrid);
            BattleButtonScript BBS = Prefabi.GetComponent<BattleButtonScript>();
            BBS.index = i;
        }
    }

    public void FindEnemy()
    {
        for (int i = 0; i < gm.Enemies.Count; i++)
        {
            GameObject Prefabi = Instantiate(PlayerPrefab, EnemyGrid);
            BattleButtonScript BBS = Prefabi.GetComponent<BattleButtonScript>();
            BBS.isEnemy = true;
            BBS.index = i;
        }
    }

    // Create a specified number of enemies
    public void CreateEnemies(int num)
    {
        for (int i = 0; i < num; i++)
        {
            MakeEnemy(GameLevel);
            print("create");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void InsertionSort()
    {
        for (int i = 1; i < EStats.Count; i++)
        {
            //For each number it looks at the number before and sees if it is lower and if it is they swap, and then it compares to the next number before and so on for each number

            for (int a = i; a > 0; a--)
            {
                if (EStats[a] < EStats[a - 1])
                {
                    SwapNumbers(a, a - 1);
                }
            }

        }
    }

    public void SwapNumbers(int numA, int numB)
    {
        //Function to swap two numbers in the numbers array
        int Placeholder = EStats[numA];
        EStats[numA] = EStats[numB];
        EStats[numB] = Placeholder;

    }
}
