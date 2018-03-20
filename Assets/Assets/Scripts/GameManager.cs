using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    [SerializeField] GameObject spawnPoint = null;
    [SerializeField] GameObject[] enemies = null;
    [SerializeField] int maxEnemiesOnScreen = 1;
    [SerializeField] int totalEnemies = 1;
    [SerializeField] int enemiesPerSpawn = 1;
    [SerializeField] float spawnDelay = 0.5f;

    private int enemiesOnScreen = 0;

	private void Awake()
	{
        if (instance == null)
            instance = this;
        else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
        StartCoroutine(spawn());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator spawn() {
        if (enemiesPerSpawn > 0 && enemiesOnScreen < totalEnemies)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                if (enemiesOnScreen < maxEnemiesOnScreen)
                {
                    GameObject newEnemy = Instantiate(enemies[0]) as GameObject;
                    newEnemy.transform.position = spawnPoint.transform.position;
                    enemiesOnScreen += 1;
                }
            }
        }
        yield return new WaitForSeconds(spawnDelay);
        StartCoroutine(spawn());
    }

    public void removeEnemy() {
        if (enemiesOnScreen > 0) {
            enemiesOnScreen -= 1;
        }
    }
}
