using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CityScript : MonoBehaviour
{

    
    public TextMeshProUGUI Timeleft;
    
    //public SceneManager ExpeditionScene = SceneManager.GetSceneAt(1);
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void LoadExpedition()
    {
        if (Time.time > GameManager.StartTime + 20)
        {

            GameManager.StartTime = Time.time;
            SceneManager.LoadScene(1); 
        }

    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
}
