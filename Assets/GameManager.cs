

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

    public void FindTurn()
    {
        int Speedtotal = 0;
        for (int i = 0; i < BRocks.Count; i++)
        {
            RockBattleScript RBSbattle = BRocks[i].GetComponent <RockBattleScript>();
            Speedtotal = Speedtotal + RBSbattle.speed;
        }
    }
 
    void Update()
    {

    }
}
