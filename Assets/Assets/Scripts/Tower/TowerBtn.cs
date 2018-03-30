using UnityEngine;

public class TowerBtn : MonoBehaviour {

    [SerializeField] private int towerPrice;
    public int TowerPrice {
        get {
            return towerPrice;
        }
    }
    [SerializeField] private GameObject towerObject;
    public GameObject TowerObject {
        get {
            return towerObject;
        }
    }

    [SerializeField] private Sprite dragSprite;
    public Sprite DragSprite {
        get {
            return dragSprite;
        }
    }

}
