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
        foreach (GameObject Rock in GameManager.BRocks)
        {
            
            GameObject NewUnit = Instantiate(UnitPrefab);
            //NewUnit.
        }
        
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

    public void SeeUnits()
    {
        mainCanvas.enabled = false;
        unitCanvas.enabled = true;
        
    }

    public void ExitUnits()
    {
        mainCanvas.enabled = true;
        unitCanvas.enabled = false;
    }
}
