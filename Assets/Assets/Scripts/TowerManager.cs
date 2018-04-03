using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections.Generic;

public class TowerManager : Singleton<TowerManager> {

    public TowerBtn towerBtnPressed { get; set; }
    private SpriteRenderer spriteRenderer;
    private List<Tower> towerList = new List<Tower>();
    private List<Collider2D> buildSiteList = new List<Collider2D>();

	// Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider.tag == "buildSite" && hit.collider.tag != "tower")
            {
                hit.collider.tag = "buildSiteFull";
                RegisterBuildSite(hit.collider);
                PlaceTower(hit);
            }
        }

        if (spriteRenderer.enabled)
        {
            FollowMouse();
        }
	}

    private void PlaceTower(RaycastHit2D hit) {
        if (!EventSystem.current.IsPointerOverGameObject() && towerBtnPressed != null)
        {
            BuyTower(towerBtnPressed);
            Tower newTower = Instantiate(towerBtnPressed.TowerObject);
            RegisterTower(newTower);
            newTower.transform.position = hit.transform.position;
            spriteRenderer.enabled = false;
        }
    }

    private void BuyTower(TowerBtn tower) {
        GameManager.Instance.BuyItem(tower.TowerPrice);
    }

    private void SelectedTower(TowerBtn selectedTowerBtn) {
        if (GameManager.Instance.CanBuyItem(selectedTowerBtn.TowerPrice))
        {
            towerBtnPressed = selectedTowerBtn;
            EnableDragSprite(towerBtnPressed.DragSprite);
        }
    }

    private void FollowMouse() {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(transform.position.x, transform.position.y);
    }

    private void EnableDragSprite(Sprite sprite) {
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = sprite;
    }

    private void RegisterTower(Tower tower) {
        towerList.Add(tower);
    }

    private void RegisterBuildSite(Collider2D buildSite) {
        buildSiteList.Add(buildSite);
    }

    public void ResetTowers() {
        foreach(Tower tower in towerList) {
            Destroy(tower);
        }
        towerList.Clear();
        foreach(Collider2D site in buildSiteList) {
            site.tag = "buildSite";
        }
        buildSiteList.Clear();
    }

    public void DisableDragSprite()
    {
        spriteRenderer.enabled = false;
    }

}
