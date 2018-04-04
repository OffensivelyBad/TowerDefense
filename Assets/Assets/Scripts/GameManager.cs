using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameStatus {
    menu, next, play, gameover, win
}

public class GameManager : Singleton<GameManager> {
    [SerializeField] int totalWaves = 10;
    [SerializeField] Text totalMoneyLabel;
    [SerializeField] Text currentWaveLabel;
    [SerializeField] Text totalEscapedLabel;
    [SerializeField] Text playButtonLabel;
    [SerializeField] Text bannerText;
    [SerializeField] Image banner;
    [SerializeField] Button playButton;
    [SerializeField] GameObject spawnPoint = null;
    [SerializeField] GameObject[] enemies = null;
    [SerializeField] int maxEnemiesOnScreen = 1;
    [SerializeField] int totalEnemies = 3;
    [SerializeField] float spawnDelay = 0.5f;
    [SerializeField] int startMoney = 10;

    private int waveNumber = 0;
    private int totalMoney = 0;
    private int totalEscaped = 0;
    private int roundEscaped = 0;
    private int totalKilled = 0;
    private int enemyIndexToSpawn = 0;
    private int escapedEnemyLimit = 10;
    private int waveTotalEnemies = 0;
    private GameStatus currentState = GameStatus.menu;

    public GameStatus CurrentState {
        get {
            return currentState;
        }
    }

    public int TotalEscaped {
        get {
            return totalEscaped;
        }
    }

    public int RoundEscaped {
        get {
            return roundEscaped;
        }
    }

    public int TotalKilled {
        get {
            return totalKilled;
        }
    }

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
        waveTotalEnemies = totalEnemies;
        TotalMoney = startMoney;
        ShowMenu();
	}
	
	// Update is called once per frame
	void Update () {
        HandleEscape();
	}

    IEnumerator Spawn() {
        if (enemyList.Count < waveTotalEnemies)
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

    private void UnregisterEnemy(Enemy enemy) {
        enemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    private void DestroyAllEnemies() {
        foreach(Enemy enemy in enemyList) {
            Destroy(enemy.gameObject);
        }
        enemyList.Clear();
    }

    private void AddMoney(int amount) {
        TotalMoney += amount;
    }

    private void SubtractMoney(int amount) {
        TotalMoney -= amount;
    }

    private void CheckWaveOver() {
        totalEscapedLabel.text = "Escaped " + totalEscaped + "/" + escapedEnemyLimit;
        SetGameState();
    }

    private void SetGameState() {
        if ((roundEscaped + totalKilled) >= waveTotalEnemies && waveNumber >= totalWaves) {
            currentState = GameStatus.win;
        }
        else if (totalEscaped >= escapedEnemyLimit) {
            currentState = GameStatus.gameover;
        }
        else if ((roundEscaped + totalKilled) >= waveTotalEnemies) {
            currentState = GameStatus.next;
        }
        else {
            currentState = GameStatus.play;
        }
        ShowMenu();
    }

    private void ShowMenu() {
        switch (currentState)
        {
            case GameStatus.gameover:
                banner.gameObject.SetActive(true);
                bannerText.text = "You're a loser!";
                playButton.gameObject.SetActive(true);
                playButtonLabel.text = "Play Again!";
                break;
            case GameStatus.next:
                banner.gameObject.SetActive(false);
                playButton.gameObject.SetActive(true);
                playButtonLabel.text = "Next Wave";
                break;
            case GameStatus.play:
                banner.gameObject.SetActive(false);
                playButton.gameObject.SetActive(false);
                break;
            case GameStatus.win:
                banner.gameObject.SetActive(true);
                bannerText.text = "You win! Yay!";
                playButton.gameObject.SetActive(true);
                playButtonLabel.text = "Play";
                break;
            case GameStatus.menu:
                banner.gameObject.SetActive(true);
                bannerText.text = "Can you win?";
                playButton.gameObject.SetActive(true);
                playButtonLabel.text = "Play";
                break;
        }
    }

    public void HandleEscape() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TowerManager.Instance.DisableDragSprite();
            TowerManager.Instance.towerBtnPressed = null;
        }
    }

    public void EnemyEscaped(Enemy enemy) {
        totalEscaped += 1;
        roundEscaped += 1;
        UnregisterEnemy(enemy);
        CheckWaveOver();
    }

    public void EnemyKilled(Enemy enemy) {
        totalKilled += 1;
        AddMoney(enemy.RewardAmount);
        CheckWaveOver();
    }

    public void PlayButtonPressed() {
        switch (currentState) {
            case GameStatus.next:
                waveNumber += 1;
                waveTotalEnemies += waveNumber;
                break;
            default:
                TowerManager.Instance.ResetTowers();
                waveTotalEnemies = totalEnemies;
                totalEscaped = 0;
                TotalMoney = startMoney;
                totalEscapedLabel.text = "Escaped " + 0 + "/" + escapedEnemyLimit;
                break;
        }
        DestroyAllEnemies();
        totalKilled = 0;
        roundEscaped = 0;
        currentWaveLabel.text = "Wave " + (waveNumber + 1);
        currentState = GameStatus.play;
        ShowMenu();
        StartCoroutine(Spawn());
    }

    public bool CanBuyItem(int price) {
        if (totalMoney >= price) {
            return true;
        }
        return false;
    }

    public void BuyItem(int price) {
        TotalMoney -= price;
    }

}
