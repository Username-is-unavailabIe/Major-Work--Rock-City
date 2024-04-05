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
    
    //public SceneManager ExpeditionScene = SceneManager.GetSceneAt(1);
    // Start is called before the first frame update
    void Start()
    {
        unitCanvas.enabled = false;
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
        foreach (GameObject Rock in GameManager.BRocks)
        {
            RockBattleScript NewRBS = Rock.GetComponent<RockBattleScript>();
            GameObject NewUnit = Instantiate(UnitPrefab, Grid);
            RockScript NewRockScript = NewUnit.GetComponent<RockScript>();
            NewRockScript.leveltext.text = NewRBS.level.ToString();
            NewRockScript.nametext.text = NewRBS.name;
            NewRockScript.ArchText.text = NewRBS.Archetype.ToString();
            NewRockScript.attacktext.text = $"Attack: {NewRBS.attack}";
            NewRockScript.defencetext.text = $"Defence: {NewRBS.defence}";
            NewRockScript.magecrafttext.text = $"Magecraft: {NewRBS.magecraft}";
            NewRockScript.healthtext.text = $"health: {NewRBS.health}";
            NewRockScript.speedtext.text = $"speed: {NewRBS.speed}";


        }

    }

    public void ExitUnits()
    {
        mainCanvas.enabled = true;
        unitCanvas.enabled = false;
    }
}
