
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script controls the battle scene in the game
public class BattleSceneScript : MonoBehaviour
{
    // References to other scripts and game objects
    public GameManager gm;
    public RockScript rockScript;
    public int GameLevel;
    
    public Transform PlayerGrid;
    public GameObject PlayerPrefab;

    // List of possible enemy archetypes
    public List<string> archetypes = new List<string> { "Wizard", "Sorcerer", "Assassin", "Nimble", "Tank", "Fighter" };

    // Start is called before the first frame update
    void Start()
    {
        // Find the GameManager and get the Game Level from the script that is on the GameManager
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameLevel = gm.GameLevel;

        // Find all Battle Rocks and creates a random amount of enemies (will be changed to
        FindBROCK();
        CreateEnemies(Random.Range(1, 3));
    }

    // Create an enemy with a random archetype and level
    public void MakeEnemy(int GameLevel)
    {
        print("make");
        // Instantiate enemy and get its script
        GameObject NewEnemy = Instantiate(gm.EnemyPrefab);
        RockBattleScript EnemyScript = gm.EnemyPrefab.GetComponent<RockBattleScript>();

        // Determine enemy level based on game level
        int leveldeterminer = Random.Range(-(1 + GameLevel / 3), 1 + GameLevel / 3);
        EnemyScript.Archetype = archetypes[Random.Range(0, archetypes.Count)];

        // 10% chance to make the enemy a boss
        if (Random.Range(1, 10) == 10)
        {
            EnemyScript.level = (GameLevel + leveldeterminer) * Random.Range(1, 3);
            EnemyScript.name = $"BOSS: {EnemyScript.Archetype} {EnemyScript.level}";
        }
        gm.Enemies.Add(NewEnemy);
    }

    // Find all Battle Rocks and instantiate a prefab for each
    public void FindBROCK()
    {
        for (int i = 0; i < gm.BRocks.Count; i++)
        {
            GameObject Prefabi = Instantiate(PlayerPrefab, PlayerGrid);
            BattleButtonScript BBS = Prefabi.GetComponent<BattleButtonScript>();
            BBS.index = i;
        }
    }

    // Create a specified number of enemies
    public void CreateEnemies(int num)
    {
        for (int i = 0; i < num; i++)
        {
            MakeEnemy(GameLevel);
            print("create");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
