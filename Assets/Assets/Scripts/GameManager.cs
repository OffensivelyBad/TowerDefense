using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    [SerializeField] GameObject spawnPoint = null;
    [SerializeField] GameObject[] enemies = null;
    [SerializeField] int maxEnemiesOnScreen = 1;
    [SerializeField] int totalEnemies = 1;
    [SerializeField] int enemiesPerSpawn = 1;
    [SerializeField] float spawnDelay = 0.5f;

    public List<Enemy> enemyList = new List<Enemy>();

	// Use this for initialization
	void Start () {
        StartCoroutine(Spawn());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Spawn() {
        if (enemiesPerSpawn > 0 && enemyList.Count < totalEnemies)
        {
            if (enemyList.Count < maxEnemiesOnScreen)
            {
                GameObject newEnemy = Instantiate(enemies[0]) as GameObject;
                newEnemy.transform.position = spawnPoint.transform.position;
            }
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(Spawn());
        }
    }

    public void RegisterEnemy(Enemy enemy) {
        enemyList.Add(enemy);
    }

    public void UnregisterEnemy(Enemy enemy) {
        enemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    public void DestroyAllEnemies() {
        foreach(Enemy enemy in enemyList) {
            Destroy(enemy.gameObject);
        }
        enemyList.Clear();
    }

}
