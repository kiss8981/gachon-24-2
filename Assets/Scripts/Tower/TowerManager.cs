using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TowerManager : Singleton<TowerManager>
{
    public TowerButton towerBtnPressed { get; set; }
    public List<GameObject> TowerList = new List<GameObject>();
    public List<Collider2D> BuildList = new List<Collider2D>();

    private SpriteRenderer spriteRenderer;
    private Collider2D buildTile;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        buildTile = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            Debug.DrawRay(worldPoint, Vector2.zero, Color.green, 1.0f);

            if (hit.collider != null && hit.collider.CompareTag("BuildSite"))
            {
                buildTile = hit.collider;
                buildTile.tag = "BuildSiteFull";
                RegisterBuildSite(buildTile);
                placeTower(hit);
            }
        }

        if (spriteRenderer.enabled)
        {
            followMouse();
        }
    }

    public void RegisterBuildSite(Collider2D buildTag)
    {
        BuildList.Add(buildTag);
    }

    public void RenameTagsBuildSites()
    {
        foreach (Collider2D buildTag in BuildList)
        {
            buildTag.tag = "BuildSite";
        }
        BuildList.Clear();
    }

    public void RegisterTower(GameObject tower)
    {
        TowerList.Add(tower);
    }

    public void DestroyAllTowers()
    {
        foreach (GameObject tower in TowerList)
        {
            if (tower != null)
                Destroy(tower.gameObject);
        }
        TowerList.Clear();
    }

    public void placeTower(RaycastHit2D hit)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && towerBtnPressed != null)
        {
            GameObject newTower = Instantiate(towerBtnPressed.towerObject);
            newTower.transform.position = hit.collider.bounds.center;

            RegisterTower(newTower);

            SpriteRenderer towerSpriteRenderer = newTower.GetComponent<SpriteRenderer>();
            if (towerSpriteRenderer != null)
            {
                towerSpriteRenderer.sortingOrder = 2;
            }

            buyTower(towerBtnPressed.towerPrice);
            disableDragSprite();
        }
    }

    public void selectedTower(TowerButton towerBtn)
    {
        if (towerBtn.towerPrice <= GameManager.Instance.playerMoney)
        {
            towerBtnPressed = towerBtn;
            enableDragSprite(towerBtn.dragSprite);
        }
    }

    public void buyTower(int price)
    {
        GameManager.Instance.subtractMoney(price);
    }

    private void followMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePosition.x, mousePosition.y);
    }

    public void enableDragSprite(Sprite sprite)
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = sprite;
    }

    public void disableDragSprite()
    {
        spriteRenderer.enabled = false;
        towerBtnPressed = null;
    }
}
