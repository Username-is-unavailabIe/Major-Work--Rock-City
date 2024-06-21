using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CityScript : MonoBehaviour
{
    public UnityEngine.Canvas mainCanvas;
    public UnityEngine.Canvas unitCanvas;
    public GameObject UnitPrefab;
    public TextMeshProUGUI Timeleft;
    public Transform Grid;
    public GameManager gm;
    public TextMeshProUGUI gameleveltext;

    //public SceneManager ExpeditionScene = SceneManager.GetSceneAt(1);
    // Start is called before the first frame update
    void Start()
    {
        //The canvas that displays units is turned off by default
        unitCanvas.enabled = false;
        //Finds game manager and displays game level
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameleveltext.text = "Game Level: " + gm.GameLevel.ToString();
    }
    public void LoadExpedition()
    {
        //Loads the expedition scene so recruitment can be implemented as long as it hasn't happened so far before battling
        if (gm.HasRecruited == false)
        {
            //Tracks time: this was later made more efficient making this code obsolete
            GameManager.StartTime = Time.time;

            SceneManager.LoadScene(2); 
            gm.HasRecruited = true;
        }

    }

    public void LoadBattle()
    {
        //Loads Battle Scene
        SceneManager.LoadScene(3);

        
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

    public void SeeUnits()
    {
        //Displays unit Canvas
        mainCanvas.enabled = false;
        unitCanvas.enabled = true;

        //Displays the recruited rocks and their relevant information
        foreach (GameObject Rock in gm.BRocks)
        {
            RockBattleScript NewRBS = Rock.GetComponent<RockBattleScript>();
            GameObject NewUnit = Instantiate(UnitPrefab, Grid);
            RockScript NewRockScript = NewUnit.GetComponent<RockScript>();
            NewRockScript.level = NewRBS.level;
            NewRockScript.name = NewRBS.name;
            NewRockScript.Archetype = NewRBS.Archetype;
            NewRockScript.attack = NewRBS.attack;
            NewRockScript.defence = NewRBS.defence;
            NewRockScript.magecraft = NewRBS.magecraft;
            NewRockScript.health = NewRBS.health;
            NewRockScript.speed = NewRBS.speed;


        }

    }

    public void ExitUnits()
    {
        //Disables the unit canvas
        mainCanvas.enabled = true;
        unitCanvas.enabled = false;
    }
}
