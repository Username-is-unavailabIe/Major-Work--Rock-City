using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.SearchService;
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

    //public SceneManager ExpeditionScene = SceneManager.GetSceneAt(1);
    // Start is called before the first frame update
    void Start()
    {
        unitCanvas.enabled = false;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void LoadExpedition()
    {
        if (Time.time > GameManager.StartTime + 20)
        {

            GameManager.StartTime = Time.time;
            SceneManager.LoadScene(1); 
        }

    }

    public void LoadBattle()
    {
        SceneManager.LoadScene(2);

        
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

    public void SeeUnits()
    {
        mainCanvas.enabled = false;
        unitCanvas.enabled = true;
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
        mainCanvas.enabled = true;
        unitCanvas.enabled = false;
    }
}
