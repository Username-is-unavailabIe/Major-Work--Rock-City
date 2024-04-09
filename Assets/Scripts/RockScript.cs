using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class RockScript : MonoBehaviour
{
    
    public int points;
    //xp needed for next level = 100r^(n-1)
    public int level = 1;
    public TextMeshProUGUI leveltext;
    public string name;
    public TextMeshProUGUI nametext;
    public int rarity;
    public TextMeshProUGUI raritytext;
    public string Archetype;
    public TextMeshProUGUI ArchText;
    public string subtype;
    public int attack;
    public TextMeshProUGUI attacktext;
    public int defence;
    public TextMeshProUGUI defencetext;
    public int health;
    public TextMeshProUGUI healthtext;
    public int magecraft;
    public TextMeshProUGUI magecrafttext;
    public int speed;
    public TextMeshProUGUI speedtext;
    public bool Chosen = false;
    public UnityEngine.UI.Toggle ChosenToggle;
    public int xp;
    public int xpNeeded;

    public int ability1;
    public int ability2;
    public int ability3;

    // Start is called before the first frame update
    void Start()
    {
        xp = Mathf.RoundToInt(100 * (1-(Mathf.Pow(1.2f, level-1)))/-0.2f);
        leveltext.text = level.ToString();
        nametext.text = name;
        ArchText.text = Archetype;
        attacktext.text = $"Attack: {attack}";
        if (defence < 1) { defence = 1; }
        defencetext.text = $"defence: {defence}";
        healthtext.text = $"Health: {health}";
        magecrafttext.text = $"Magecraft: {magecraft}";
        if (speed < 1) { speed = 1; }
        speedtext.text = $"Speed: {speed}";
        SetRarity();


    }

    public void SetRarity()
    {
        if ( rarity == 1 ) { raritytext.text = "Common"; }
        else if (rarity == 2 ) { raritytext.text = "Uncommon"; }
        else if (rarity == 3 ) { raritytext.text = "Rare"; }
        else if (rarity == 4 ) {raritytext.text = "Epic"; }
        else if (rarity == 5 ) { raritytext.text = "Legendary"; }
    }
    // Update is called once per frame
    void Update()
    {
        if (ChosenToggle != null && ChosenToggle.isOn) { Chosen = true; }
        else { Chosen = false; }
        xpNeeded = Mathf.RoundToInt(100 * (1 - Mathf.Pow(1.2f, level)) / -0.2f)-xp;


    }
    public void Getxp(int xpGained)
    {
        xp = xp + xpGained;
        if (xpNeeded <= 0)
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
