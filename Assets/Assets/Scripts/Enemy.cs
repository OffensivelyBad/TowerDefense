using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] int target = 0;
    [SerializeField] Transform finish = null;
    [SerializeField] Transform[] checkPoints = null;
    [SerializeField] float navigationUpdate = 0f;

    private Transform enemyPosition;
    private float navigationTime = 0f;

	// Use this for initialization
	void Start () {
        enemyPosition = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        if (checkPoints != null) {
            navigationTime += Time.deltaTime;
            if (navigationTime > navigationUpdate) {
                if (target < checkPoints.Length) {
                    enemyPosition.position = Vector2.MoveTowards(enemyPosition.position, checkPoints[target].position, navigationTime);
                }
                else {
                    enemyPosition.position = Vector2.MoveTowards(enemyPosition.position, checkPoints[target].position, navigationTime);
                }
                navigationTime = 0;
            }
        }
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.tag == "Checkpoint")
            target += 1;
        else if (collision.tag == "Finish") {
            GameManager.instance.removeEnemy();
            Destroy(gameObject);
        }
	}
}
