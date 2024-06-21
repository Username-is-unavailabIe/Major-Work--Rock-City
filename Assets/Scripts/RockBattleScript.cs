
using UnityEngine;

public class RockBattleScript : MonoBehaviour
{

    //Each of these variables are copied over from RockScript in order to be stored outside of the recruitment scene
    public int xp;
    public int TempHealth;
    public int points;
    public int level;
    public string name;
    public int rarity;
    public string Archetype;
    public string subtype;
    public int attack;
    public int defence;
    public int health;
    public int magecraft;
    public int speed;


    //Not implemented
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
