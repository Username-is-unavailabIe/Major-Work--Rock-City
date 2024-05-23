//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.SceneManagement;


//public class GameManager : MonoBehaviour
//{
//    private static GameManager instance;
//    public int GameLevel = 1;
//    public TextMeshProUGUI Timeleft;
//    public int ExpeditionTime = 0;
//    public static float StartTime = -20;
//    public List<GameObject> BRocks = new List<GameObject>();
//    public GameObject BattleRockPrefab;
//    // Start is called before the first frame update

//    public void SpawnRock(RockScript rock)
//    {
//        //GameManager becomes unattached to button when you go on an expedition
//        //LoadBattle();
//        GameObject Newrock = Instantiate(BattleRockPrefab, transform);
//        //Right now it makes a new BR and adds the script to it which is an issue
//        RockBattleScript NewBRS = Newrock.GetComponent<RockBattleScript>();
//        NewBRS.name = rock.name;
//        NewBRS.health = rock.health;
//        NewBRS.xp = rock.xp;
//        NewBRS.Archetype = rock.Archetype;
//        NewBRS.ability1 = rock.ability1;
//        NewBRS.ability2 = rock.ability2;
//        NewBRS.ability3 = rock.ability3;
//        NewBRS.subtype = rock.subtype;
//        NewBRS.attack = rock.attack;
//        NewBRS.defence = rock.defence;
//        NewBRS.level = rock.level;
//        NewBRS.magecraft = rock.magecraft;
//        NewBRS.speed = rock.speed;
//        NewBRS.points = rock.points;
//        BRocks.Add(Newrock);

//    }

//    /**public void GenerateEnemies()
//    {

//    }**/


//    public void LoadBattle()
//    {
//        SceneManager.LoadScene(2);
//    }


//    void Start()
//    {
//        //if () {;}
//        //DontDestroyOnLoad(gameObject);

//        InvokeRepeating("ShowTime", 0, 1);
//    }
//    private void Awake()
//    {
//        if (instance == null)
//        {
//            instance = this;
//            DontDestroyOnLoad(gameObject);
//        }
//        else
//        {
//            Destroy(gameObject);
//        }

//    }



//    /**void TimeLeft()
//    {
//        if (ExpeditionTime > 0)
//        {
//            ExpeditionTime--;
//            Timeleft.text = ExpeditionTime.ToString();

//        }

//    }**/
//    void ShowTime()
//    {
//        print(Time.time);
//    }
//    // Update is called once per frame
//    void Update()
//    {

//    }
//}

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
        SceneManager.LoadScene(2);
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

 
    void Update()
    {

    }
}
