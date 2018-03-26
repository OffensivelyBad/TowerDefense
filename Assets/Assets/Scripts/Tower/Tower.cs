using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    [SerializeField] private float timeBetweenAttacks = 1f;
    [SerializeField] private float attackRadius = 1f;
    [SerializeField] private Projectile projectile;
    private float attackCounter = 0f;
    private Enemy NearestEnemy {
        get {
            return GetNearestEnemy();
        }
    }
    private bool isAttacking = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        attackCounter += Time.deltaTime;
        if (attackCounter >= timeBetweenAttacks) {
            attackCounter = 0;
            isAttacking = true;
        }
        else {
            isAttacking = false;
        }

	}

	private void FixedUpdate()
	{
        if (isAttacking) 
            Attack(NearestEnemy);
	}

	private void Attack(Enemy enemy) {
        isAttacking = false;
        if (enemy != null)
        {
            Projectile newProjectile = Instantiate(projectile) as Projectile;
            newProjectile.transform.localPosition = transform.localPosition;
            StartCoroutine(MoveProjectile(newProjectile, enemy));
        }
    }

    IEnumerator MoveProjectile(Projectile newProjectile, Enemy enemy) {
        float enemyDistance = Vector2.Distance(enemy.transform.localPosition, transform.localPosition);
        while(enemyDistance > 0.2f && newProjectile != null) {
            var direction = enemy.transform.localPosition - transform.localPosition;
            var angleDirection = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            newProjectile.transform.rotation = Quaternion.AngleAxis(angleDirection, Vector3.forward);
            newProjectile.transform.localPosition = Vector2.MoveTowards(newProjectile.transform.localPosition, enemy.transform.localPosition, 5f * Time.deltaTime);
            yield return null;
        }
        if (newProjectile != null || NearestEnemy == null) {
            Destroy(newProjectile);
        }
    }

    private List<Enemy> GetEnemiesInRange() {
        List<Enemy> enemiesInRange = new List<Enemy>();
        foreach(Enemy enemy in GameManager.Instance.enemyList) {
            if(Vector2.Distance(transform.localPosition, enemy.transform.localPosition) <= attackRadius) {
                enemiesInRange.Add(enemy);
            }
        }
        return enemiesInRange;
    }

    private Enemy GetNearestEnemy() {
        Enemy nearestEnemy = null;
        float smallestDistance = float.PositiveInfinity;
        foreach(Enemy enemy in GetEnemiesInRange()) {
            float distance = Vector2.Distance(transform.localPosition, enemy.transform.localPosition);
            if(distance < smallestDistance) {
                smallestDistance = distance;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }

}
