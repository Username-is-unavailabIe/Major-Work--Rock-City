using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class RockBattleScript : MonoBehaviour
{
    public int xp;
    public int points;
    //xp needed for next level = 100r^(n-1)
    public int level;
   // public TextMeshProUGUI leveltext;
    public string name;
   // public TextMeshProUGUI nametext;
    public int rarity;
  //  public TextMeshProUGUI raritytext;
    public string Archetype;
   // public TextMeshProUGUI ArchText;
    public string subtype;
    public int attack;
  //  public TextMeshProUGUI attacktext;
    public int defence;
  //  public TextMeshProUGUI defencetext;
    public int health;
   // public TextMeshProUGUI healthtext;
    public int magecraft;
   // public TextMeshProUGUI magecrafttext;
    public int speed;
   // public TextMeshProUGUI speedtext;
  //  public bool Chosen = false;
  //  public UnityEngine.UI.Toggle ChosenToggle;


    public int ability1;
    public int ability2;
    public int ability3;

    // Start is called before the first frame update
    void Start()
    {



    }

    public void SetRarity()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Getxp(int xpGained)
    {
        xp = xp + xpGained;
        if (xp >= (100*(Mathf.Pow(1.2f, level+1))))
        {
            levelup();
        }
    }
    public void levelup()
    {
        level++;
        points = points + (2+Mathf.RoundToInt(rarity/2));
    }
}
