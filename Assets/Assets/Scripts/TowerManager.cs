using UnityEngine.EventSystems;
using UnityEngine;

public class TowerManager : Singleton<TowerManager> {

    private TowerBtn towerBtnPressed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider.tag == "buildSite" && hit.collider.tag != "tower")
            {
                PlaceTower(hit);
            }
        }
	}

    private void PlaceTower(RaycastHit2D hit) {
        if (!EventSystem.current.IsPointerOverGameObject() && towerBtnPressed != null)
        {
            GameObject newTower = Instantiate(towerBtnPressed.TowerObject);
            newTower.transform.position = hit.transform.position;
        }
    }

    public void SelectedTower(TowerBtn selectedTowerBtn) {
        towerBtnPressed = selectedTowerBtn;
        Debug.Log("selected tower: " + towerBtnPressed.gameObject);
    }

}
