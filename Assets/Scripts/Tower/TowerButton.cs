using UnityEngine;
using UnityEngine.EventSystems;

public class TowerButton : MonoBehaviour, IPointerDownHandler
{
    public GameObject towerObject;
    public Sprite dragSprite;
    public int towerPrice;

    public void OnPointerDown(PointerEventData eventData)
    {
        onPress();
    }

    public void onPress()
    {
        TowerManager.Instance.selectedTower(this);
    }
}
