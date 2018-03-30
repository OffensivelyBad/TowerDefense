using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameStatus {
    next, play, gameover, win
}

public class GameManager : Singleton<GameManager> {
    [SerializeField] int totalWaves = 10;
    [SerializeField] Text totalMoneyLabel;
    [SerializeField] Text currentWaveLabel;
    [SerializeField] Text totalEscapedLabel;
    [SerializeField] Text playButtonLabel;
    [SerializeField] Button playButton;
    [SerializeField] GameObject spawnPoint = null;
    [SerializeField] GameObject[] enemies = null;
    [SerializeField] int maxEnemiesOnScreen = 1;
    [SerializeField] int totalEnemies = 1;
    [SerializeField] int enemiesPerSpawn = 1;
    [SerializeField] float spawnDelay = 0.5f;

    private int waveNumber = 0;
    private int totalMoney = 10;
    private int totalEscaped = 0;
    private int roundEscaped = 0;
    private int totalKilled = 0;
    private int enemyIndexToSpawn = 0;
    private GameStatus currentState = GameStatus.play;

    public int TotalMoney {
        get {
            return totalMoney;
        }
        set {
            totalMoney = value;
            totalMoneyLabel.text = value.ToString();
        }
    }
    public List<Enemy> enemyList = new List<Enemy>();

	// Use this for initialization
	void Start () {
        playButton.gameObject.SetActive(false);
        showMenu();
	}
	
	// Update is called once per frame
	void Update () {
        handleEscape();
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

    public void addMoney(int amount) {
        TotalMoney += amount;
    }

    public void subractMoney(int amount) {
        TotalMoney -= amount;
    }

    public void showMenu() {
        switch (currentState)
        {
            case GameStatus.gameover:
                playButtonLabel.text = "Play Again!";
                break;
            case GameStatus.next:
                playButtonLabel.text = "Next Wave";
                break;
            case GameStatus.play:
                playButtonLabel.text = "Play";
                break;
            case GameStatus.win:
                playButtonLabel.text = "Play";
                break;
        }
        playButton.gameObject.SetActive(true);
    }

    public void handleEscape() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TowerManager.Instance.DisableDragSprite();
            TowerManager.Instance.towerBtnPressed = null;
        }
    }

}
