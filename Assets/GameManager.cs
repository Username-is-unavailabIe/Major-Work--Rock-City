

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script manages the game state
public class GameManager : MonoBehaviour
{
    // Singleton instance of GameManager
    private static GameManager instance;

    // Variables related to game state
    public GameObject RockParent;
    public int GameLevel = 1;
    public TextMeshProUGUI Timeleft;
    public int ExpeditionTime = 0;
    public static float StartTime = -20;
    public List<GameObject> BRocks = new List<GameObject>();
    public List<GameObject> Enemies = new List<GameObject>();
    public GameObject EnemyPrefab;
    public GameObject BattleRockPrefab;
    public GameObject EnemyParent;


    //spawn a new rock
    public void SpawnRock(RockScript rock)
    {
        // Instantiate a new BattleRock and get RockBattleScript off of that
        GameObject Newrock = Instantiate(BattleRockPrefab, RockParent.transform);
        RockBattleScript NewBRS = Newrock.GetComponent<RockBattleScript>();

        // Copy everything from the original rock to the new BattleRock
        NewBRS.name = rock.name;
        NewBRS.points = rock.points;
        NewBRS.speed = rock.speed;
        NewBRS.health = rock.health;
        NewBRS.xp = rock.xp;
        NewBRS.Archetype = rock.Archetype;
        NewBRS.defence = rock.defence;
        NewBRS.attack = rock.attack;
        NewBRS.magecraft = rock.magecraft;
        NewBRS.rarity = rock.rarity;
        NewBRS.level = rock.level;


        // Add the new BattleRock to the list
        BRocks.Add(Newrock);
    }

    //load the battle scene
    public void LoadBattle()
    {
        SceneManager.LoadScene(3);
    }


    void Start()
    {
        EnemyParent = GameObject.Find("EnemiesParent");
        RockParent = GameObject.Find("RocksParent");
        // Call the ShowTime method every second
        InvokeRepeating("ShowTime", 0, 1);
    }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        //  if an instance already exists, destroy this one
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //show the current time
    void ShowTime()
    {
        //print(Time.time);
    }

    public RockBattleScript FindTurn()
    {

        List<RockBattleScript> CombinedList = new List<RockBattleScript>();


        for (int i = 0; i < BRocks.Count; i++)
        {
            for (int j = 0; j < BRocks[i].GetComponent<RockBattleScript>().speed; j++)
            {
                CombinedList.Add(BRocks[i].GetComponent<RockBattleScript>());
            }
        }
        for (int i = 0; i < Enemies.Count; i++)
        {
            for (int l = 0; l < Enemies[i].GetComponent<RockBattleScript>().speed; l++)
            {
                CombinedList.Add(Enemies[i].GetComponent<RockBattleScript>());
            }
        }
        int ranNum = Random.Range(0, CombinedList.Count);
        //CombinedList[ranNum].health = 200;
        print(CombinedList[ranNum].name);
        return CombinedList[ranNum];

        //int Speedtotal = 0;
        //for (int i = 0; i < BRocks.Count; i++)
        //{
        //    RockBattleScript RBSbattle = BRocks[i].GetComponent <RockBattleScript>();
        //    Speedtotal = Speedtotal + RBSbattle.speed;
        //}
        //for (int i = 0; i < Enemies.Count; i++)
        //{
        //    RockBattleScript EnemyBS = Enemies[i].GetComponent <RockBattleScript>();
        //    Speedtotal += EnemyBS.speed;
        //}
        //print(Speedtotal);
        //int TurnRandom = Random.Range(1, Speedtotal);
        //if (TurnRandom <= BRocks[0].GetComponent<RockBattleScript>().speed)
        //{
        //    return 0;
        //}
        //else if (TurnRandom > BRocks[0].GetComponent<RockBattleScript>().speed && TurnRandom >= BRocks[1].GetComponent<RockBattleScript>().speed)
        //{

        //}
    }
    public RockBattleScript FindTarget(string Turnclassification)
    {
        if (Turnclassification == "Enemy")
        {
            return BRocks[Random.Range(0, BRocks.Count)].GetComponent<RockBattleScript>();
           
        }
        else
        {
            return Enemies[Random.Range(0, Enemies.Count)].GetComponent<RockBattleScript>();
        }
    }
 
    void Update()
    {

    }
}
