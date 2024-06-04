using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleButtonScript : MonoBehaviour
{
    public GameManager gm;
    public int index = 0;
    public TMPro.TextMeshProUGUI buttontext;
    RockBattleScript RBS;
    public bool isEnemy = false;
    public string name;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (isEnemy) { RBS = gm.Enemies[index].GetComponent<RockBattleScript>(); }
        else { RBS = gm.BRocks[index].GetComponent<RockBattleScript>(); }
        buttontext.text = RBS.name;
        name = RBS.name;
    }



    public void OnClick()
    {
        
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
