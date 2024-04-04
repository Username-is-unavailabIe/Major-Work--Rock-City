using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public TextMeshProUGUI Timeleft;
    public int ExpeditionTime = 0;
    public static float StartTime = -20;
    public static List<GameObject> BRocks = new List<GameObject>();
    public GameObject BattleRockPrefab;
    // Start is called before the first frame update
    
    public void SpawnRock(RockScript rock)
    {
        //GameManager becomes unattached to button when you go on an expedition
        //LoadBattle();
        GameObject Newrock = Instantiate(BattleRockPrefab, transform);
        //Right now it makes a new BR and adds the script to it which is an issue
        RockBattleScript NewBRS = Newrock.GetComponent<RockBattleScript>();
        NewBRS.name = rock.name;
        NewBRS.health = rock.health;
        NewBRS.xp = rock.xp;
        NewBRS.Archetype = rock.Archetype;
        NewBRS.ability1 = rock.ability1;
        NewBRS.ability2 = rock.ability2;
        NewBRS.ability3 = rock.ability3;
        NewBRS.subtype = rock.subtype;
        NewBRS.attack = rock.attack;
        NewBRS.defence = rock.defence;
        NewBRS.level = rock.level;
        NewBRS.magecraft = rock.magecraft;
        NewBRS.speed = rock.speed;
        NewBRS.points = rock.points;
        BRocks.Add(Newrock);

    }
    
    /**public void GenerateEnemies()
    {

    }**/


    public void LoadBattle()
    {
        SceneManager.LoadScene(2);
    }
    
    
    void Start()
    {
        //if () {;}
        //DontDestroyOnLoad(gameObject);

        InvokeRepeating("ShowTime", 0, 1);
    }
    private void Awake()
    {
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


    
    /**void TimeLeft()
    {
        if (ExpeditionTime > 0)
        {
            ExpeditionTime--;
            Timeleft.text = ExpeditionTime.ToString();

        }

    }**/
    void ShowTime()
    {
        print(Time.time);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
