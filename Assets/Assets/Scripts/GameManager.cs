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

    private int enemiesOnScreen = 0;

	// Use this for initialization
	void Start () {
        StartCoroutine(Spawn());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Spawn() {
        if (enemiesPerSpawn > 0 && enemiesOnScreen < totalEnemies)
        {
            if (enemiesOnScreen < maxEnemiesOnScreen)
            {
                GameObject newEnemy = Instantiate(enemies[0]) as GameObject;
                newEnemy.transform.position = spawnPoint.transform.position;
                enemiesOnScreen += 1;
            }
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(Spawn());
        }
    }

    public void RemoveEnemy() {
        if (enemiesOnScreen > 0) {
            enemiesOnScreen -= 1;
        }
    }
}
