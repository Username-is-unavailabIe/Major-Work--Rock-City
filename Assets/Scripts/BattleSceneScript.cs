
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// This script controls the battle scene in the game
public class BattleSceneScript : MonoBehaviour
{
    // References to other scripts and game objects
    public GameManager gm;
    public RockScript rockScript;
    public int GameLevel;
    public TextMeshProUGUI TurnText;
    public TextMeshProUGUI dmgText;
    
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
        CreateEnemies(Mathf.Clamp(Random.Range((gm.BRocks.Count-GameLevel+1), gm.BRocks.Count+GameLevel-1), 1, 1000));
        FindEnemy();
    }

    // Create an enemy with a random archetype and level
    public void MakeEnemy(int EnemyGameLevel)
    {
        print("make");
        // Instantiate enemy and get its script
        GameObject NewEnemy = Instantiate(gm.EnemyPrefab, gm.EnemyParent.transform);
        RockBattleScript EnemyScript = gm.EnemyPrefab.GetComponent<RockBattleScript>();
        // Determine enemy level based on game level, with it being slightly more random at higher levels
        int leveldeterminer = Random.Range(-EnemyGameLevel, 2 * EnemyGameLevel);
        EnemyScript.Archetype = archetypes[Random.Range(0, archetypes.Count)];

        // 10% chance to make the enemy a boss
        if (Random.Range(1, 11) == 10)
        {
            EnemyScript.level = Mathf.Abs(EnemyGameLevel + leveldeterminer+1) * Random.Range(2, 5);
            EnemyScript.name = $"BOSS: {EnemyScript.Archetype} {EnemyScript.level}";
        }
        else
        {

            EnemyScript.level = Mathf.Clamp(GameLevel + leveldeterminer, 1, EnemyGameLevel * 2);
            EnemyScript.name = $"{EnemyScript.Archetype} {EnemyScript.level}";
        }
        
        //generates stats depending on the archetype (grouped together for enemies as they don't need to be as complex)
        GenerateEstats(EnemyScript.level*5);

        if (EnemyScript.Archetype == "Wizard" || EnemyScript.Archetype == "Sorcerer")
        {
            EnemyScript.magecraft = EStats[EStats.Count-1];
            EnemyScript.attack = EStats[2];
            EnemyScript.health = EStats[Mathf.RoundToInt(EStats.Count/2)];
            EnemyScript.defence = EStats[Mathf.RoundToInt(EStats.Count / 2)-1];
            EnemyScript.speed = Mathf.Clamp((EnemyScript.defence),1, 9999);
        }
        else if (EnemyScript.Archetype == "Fighter" || EnemyScript.Archetype == "Assassin" || EnemyScript.Archetype == "Tank")
        {
            EnemyScript.magecraft = EStats[0];
            EnemyScript.attack = Mathf.Clamp(EStats[EStats.Count-1],1, GameLevel*5);
            EnemyScript.health = EStats[EStats.Count - 2];
            EnemyScript.defence = Mathf.Clamp(EStats[EStats.Count - 3], 1, GameLevel*2);
            EnemyScript.speed = Mathf.Clamp(EStats[3],1 , 9999);
        }
        else if (EnemyScript.Archetype == "Nimble")
        {
            EnemyScript.magecraft = EStats[0];
            EnemyScript.attack = EStats[Mathf.RoundToInt(EStats.Count/2)];
            EnemyScript.health = EStats[4];
            EnemyScript.defence = EStats[3];
            EnemyScript.speed = EStats[EStats.Count-1];
        }
        //adds the enemy to the list
        gm.Enemies.Add(NewEnemy);
    }

    public void GenerateEstats(int number)
    {
        //generates a list of possible stats
        for (int i = 0; i < number; i++)
        {
            EStats.Add(Random.Range(GameLevel, GameLevel*3));
        }
        //sorts the stats from lowest to highest
        InsertionSort();
        //prints out the highest stat for debugging purposes
        print("Highest Stat is " + EStats[EStats.Count - 1]);

    }

    // Find all player's Rocks and instantiate a prefab for each
    public void FindBROCK()
    {
        for (int i = 0; i < gm.BRocks.Count; i++)
        {
            GameObject Prefabi = Instantiate(PlayerPrefab, PlayerGrid);
            BattleButtonScript BBS = Prefabi.GetComponent<BattleButtonScript>();
            BBS.index = i;
            gm.BRocks[i].GetComponent<RockBattleScript>().TempHealth = gm.BRocks[i].GetComponent<RockBattleScript>().health;
        }
    }
    //Find all Enemies and instantiate a prefab for each
    public void FindEnemy()
    {
        for (int i = 0; i < gm.Enemies.Count; i++)
        {
            GameObject Prefabi = Instantiate(PlayerPrefab, EnemyGrid);
            BattleButtonScript BBS = Prefabi.GetComponent<BattleButtonScript>();
            BBS.isEnemy = true;
            BBS.index = i;
            gm.Enemies[i].GetComponent<RockBattleScript>().TempHealth = gm.Enemies[i].GetComponent<RockBattleScript>().health;
        }
    }

    // Create a specified number of enemies
    public void CreateEnemies(int num)
    {
        for (int i = 0; i < num; i++)
        {
            MakeEnemy(GameLevel+1);
            //print("create");
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

    public void RunBattle()
    {
        //Runs the batttle

        //debugging purposes
        if (gm.Enemies.Count == 0)
        { print("No Enemies"); }

        //figures out what rock will have its turn + displays it
        RockBattleScript Turn = gm.FindTurn();
        TurnText.text = ($"Turn: {Turn.name}");

        //Finds a target by running a function off the game manager script
        RockBattleScript Target = gm.FindTarget(Turn.gameObject.tag.ToString());
        //Calculates and deals damage to the target and displays it
        int damage = Mathf.Clamp((Turn.attack + (Turn.magecraft)/2 - Target.defence - (Target.magecraft)/3),1, 1999);
        Target.TempHealth -= damage;
        dmgText.text = "damage:" + damage.ToString();

        //debugging
        print("Enemies: " + gm.Enemies.Count);

        //Deactivates the button and removes the rock from the script if it has 0 health (regardless of it being a player or enemy)
        if (Target.TempHealth <= 0)
        {
            bool IsEnemy = true;
            //GameObject ToRemove = new GameObject();
            foreach (Transform Prefab in PlayerGrid)
            {
                if (Prefab.gameObject.GetComponent<BattleButtonScript>().name == Target.name)
                {
                    Prefab.GetComponent<Button>().interactable = false;
                }
            }
            foreach (GameObject PlayerPrefab in gm.BRocks)
            {
                    
                if (PlayerPrefab.gameObject.GetComponent<RockBattleScript>().name == Target.name)
                {
                    print(PlayerPrefab.GetComponent<RockBattleScript>().name);
                    //ToRemove = PlayerPrefab;
                    IsEnemy = false;
                }
            }

            foreach (Transform EnemyPrefab in EnemyGrid)
            {

                if (EnemyPrefab.gameObject.GetComponent<BattleButtonScript>().name == Target.name)
                {
                    print(EnemyPrefab.GetComponent<BattleButtonScript>().name);
                    EnemyPrefab.GetComponent<Button>().interactable = false;
                }
            }

            foreach (GameObject Efab in gm.Enemies)
            {
                if (Efab.gameObject.GetComponent<RockBattleScript>().name == Target.name)
                {
                    print(Efab.GetComponent<RockBattleScript>().name);
                    //ToRemove = Efab;

                }

            }

            if (IsEnemy)
            {
                gm.Enemies.Remove(Target.gameObject);
                Destroy(Target.gameObject);
            }
            else
            {
                gm.BRocks.Remove(Target.gameObject);
                Destroy(Target.gameObject);
            }

                


            

        }
        //debugging
        print("AAAA");

        //Tests if there is 0 enemies or players left
        GameWon();


    }

    
    public void GameWon()
    {
        //When the game has been won, the level goes up by 2 and each rock levels up, and the city scene is loaded
        if (gm.Enemies.Count == 0)
        {
            print("You Win!");
            foreach (GameObject rock in gm.BRocks)
            {
                RockBattleScript script = rock.GetComponent<RockBattleScript>();
                script.level++;
                int statMod = Random.Range(2, 2 + script.rarity);
                script.attack += Random.Range(1, statMod);
                script.defence += Random.Range(0, statMod / 2);
                script.health += Random.Range(3, statMod * 3);
                script.magecraft += Random.Range(0, statMod / 2);
                script.speed += Random.Range(0, statMod / 3);

            }
            gm.GameLevel += 2;
            ExitBattle();
        }

        //Game levels up only once if player loses
        else if (gm.BRocks.Count == 0)
        {
            print("You Lost");
            gm.GameLevel++;
            ExitBattle();
        }

    }

    //Exits the Scene and clears the enemies
    public void ExitBattle()
    {
        SceneManager.LoadScene(1);
        for (int i = 0; i < gm.EnemyParent.transform.childCount; i++)
        {
            Destroy(gm.EnemyParent.transform.GetChild(i).gameObject);
        }
        

        gm.Enemies.Clear();

        //means you can recruit again
        gm.HasRecruited = false;
        
    }


}
