using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] private int target = 0;
    [SerializeField] private Transform finish = null;
    [SerializeField] private Transform[] checkPoints = null;
    [SerializeField] private float navigationUpdate = 0f;
    [SerializeField] private int healthPoints = 10;
    [SerializeField] private int rewardAmount;

    private Transform enemyPosition;
    private float navigationTime = 0f;
    private bool isDead = false;
    private Animator anim;
    private Collider2D colliderComponent;
    public bool IsDead {
        get {
            return isDead;
        }
    }
    public int RewardAmount {
        get {
            return rewardAmount;
        }
    }

	// Use this for initialization
	void Start () {
        enemyPosition = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        colliderComponent = GetComponent<Collider2D>();
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
                GameManager.Instance.EnemyEscaped(this);
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
            GameManager.Instance.PlaySound(SoundManager.Instance.Hit);
            anim.Play("Hurt1");
        }
    }

    private void Die() {
        GameManager.Instance.PlaySound(SoundManager.Instance.Death);
        GameManager.Instance.EnemyKilled(this);
        anim.SetTrigger("didDie");
        colliderComponent.enabled = false;
        isDead = true;
    }
}
