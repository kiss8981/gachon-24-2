using UnityEngine;
using UnityEngine.EventSystems; // EventSystems 네임스페이스 추가

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
