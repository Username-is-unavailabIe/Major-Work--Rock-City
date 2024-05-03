using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneScript : MonoBehaviour
{
    public GameManager gm;
    public RockScript rockScript;
    public int GameLevel;
    public GameObject EnemyPrefab;
    public List<string> archetypes = new List<string> { "Wizard", "Sorcerer", "Assassin", "Nimble", "Tank", "Fighter" };
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameLevel = gm.GameLevel;
        CreateEnemies(Random.Range(1,3));
    }

    public void MakeEnemy(int GameLevel)
    {
        GameObject NewEnemy = Instantiate(EnemyPrefab);
        RockScript EnemyScript = EnemyPrefab.GetComponent<RockScript>();
        int leveldeterminer = Random.Range(-(1+GameLevel/3), 1+GameLevel/3);
        EnemyScript.Archetype = archetypes[Random.Range(0, archetypes.Count)];
        if (Random.Range(1,10) == 10) 
        {
            EnemyScript.level = (GameLevel + leveldeterminer) * Random.Range(1, 3);
            EnemyScript.name = $"BOSS: {EnemyScript.Archetype} {EnemyScript.level}";
        }
    }

    public void CreateEnemies(int num)
    {
        for (int i = 0; i < num; i++) 
        {
            MakeEnemy(GameLevel);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
