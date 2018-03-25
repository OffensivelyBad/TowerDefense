using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    [SerializeField] private float timeBetweenAttacks = 1f;
    [SerializeField] private float attackRadius = 1f;
    private Projectile projectile;
    private Enemy targetedEnemy = null;
    private float attackCounter;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private List<Enemy> GetEnemiesInRange() {
        List<Enemy> enemiesInRange = new List<Enemy>();
        foreach(Enemy enemy in GameManager.Instance.enemyList) {
            if(Vector2.Distance(transform.position, enemy.transform.position) <= attackRadius) {
                enemiesInRange.Add(enemy);
            }
        }
        return enemiesInRange;
    }

    private Enemy GetNearestEnemy() {
        Enemy nearestEnemy = null;
        float smallestDistance = float.PositiveInfinity;
        foreach(Enemy enemy in GetEnemiesInRange()) {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if(distance < smallestDistance) {
                smallestDistance = distance;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }

}
