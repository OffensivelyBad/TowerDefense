using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] int target = 0;
    [SerializeField] Transform finish = null;
    [SerializeField] Transform[] checkPoints = null;
    [SerializeField] float navigationUpdate = 0f;
    [SerializeField] int healthPoints = 10;
    [SerializeField] int rewardAmount;

    private Transform enemyPosition;
    private float navigationTime = 0f;
    private bool isDead = false;
    private Animator anim;
    private Collider2D collider;
    public bool IsDead {
        get {
            return isDead;
        }
    }

	// Use this for initialization
	void Start () {
        enemyPosition = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        GameManager.Instance.RegisterEnemy(this);
	}
	
	// Update is called once per frame
	void Update () {
        if (checkPoints != null && !isDead) {
            navigationTime += Time.deltaTime;
            if (navigationTime > navigationUpdate) {
                if (target < checkPoints.Length) {
                    enemyPosition.position = Vector2.MoveTowards(enemyPosition.position, checkPoints[target].position, navigationTime);
                }
                else {
                    enemyPosition.position = Vector2.MoveTowards(enemyPosition.position, finish.position, navigationTime);
                }
                navigationTime = 0;
            }
        }
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        switch (collision.tag) {
            case "Checkpoint":
                target += 1;
                break;
            case "Finish":
                GameManager.Instance.UnregisterEnemy(this);
                Destroy(gameObject);
                break;
            case "projectile":
                Projectile newProjectile = collision.gameObject.GetComponent<Projectile>();
                if (newProjectile != null)
                {
                    TakeDamage(newProjectile.AttackStrength);
                    Destroy(collision.gameObject);
                }
                break;
        }
	}

    private void TakeDamage(int hitPoints) {
        healthPoints -= hitPoints;
        if (healthPoints < 0) {
            Die();
        } else {
            anim.Play("Hurt1");
        }
    }

    private void Die() {
        anim.SetTrigger("didDie");
        collider.enabled = false;
        isDead = true;
    }
}
