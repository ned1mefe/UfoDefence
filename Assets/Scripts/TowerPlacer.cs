using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerPlacer : MonoBehaviour
{
    [SerializeField] private Tilemap placementTilemap; 
    [SerializeField] private GameObject[] towerPrefabs;
    [SerializeField] private UI ui;
    [HideInInspector] public int availableTower1 = 0;
    [HideInInspector] public int availableTower2 = 0;
    [HideInInspector] public int availableTower3 = 0;

    private List<Tower> _placedTowers;
    private int _selectedTowerIndex;
    
    
    private void Awake()
    {
        _placedTowers = new List<Tower>();
        enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_selectedTowerIndex != -1)
                TryPlaceTower();
        }
        if (Input.GetMouseButtonDown(1))
        {
            TryDeleteTower();
        }
    }
    public void SetTowerPlacer(LevelTowerData towerData)
    {
        availableTower1 = towerData.Tower1Count;
        availableTower2 = towerData.Tower2Count;
        availableTower3 = towerData.Tower3Count;
        ui.UpdateTowerButtons();
    }
    
    public void StartPlacing(int towerIndex)
    {
        _selectedTowerIndex = towerIndex;
        enabled = true;
    }

    public void StopPlacing()
    {
        _selectedTowerIndex = -1;
        enabled = false;
    }
    
    public void ClearTowers()
    {
        _placedTowers.ForEach(x => Destroy(x.gameObject));
        _placedTowers.Clear();
    }

    public void ActivateTowers() => _placedTowers.ForEach(x => x.Activate());
    public void DeactivateTowers() => _placedTowers.ForEach(x => x.Deactivate());
    
    
    private void TryPlaceTower()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        mouseWorldPos.y -= 0.25f;// aligning mistake with tiles
        Vector3Int cellPos = placementTilemap.WorldToCell(mouseWorldPos);

        TileBase clickedTile = placementTilemap.GetTile(cellPos);

        if (clickedTile is null || clickedTile.name != "buildingPlaceGrass") return;
        
        Vector3 placePos = placementTilemap.GetCellCenterWorld(cellPos);
        var selectedTower = towerPrefabs[_selectedTowerIndex];
        
        placePos.y += selectedTower.transform.position.y;
        placePos.y += 0.5f;  // aligning mistake with tiles

        if (!IsTowerOnTile(placePos))
        {
            var newTower = Instantiate(selectedTower, placePos, Quaternion.identity);
            _placedTowers.Add(newTower.GetComponent<Tower>());

            if (!DecreaseAndCheck(_selectedTowerIndex))
                _selectedTowerIndex = -1;
            
            ui.UpdateTowerButtons();
        }
    }

    private bool IsTowerOnTile(Vector3 worldPos)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(worldPos, 0.1f);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Tower"))
                return true;
        }
        return false;
    }
    private bool DecreaseAndCheck(int index) // returns selected one is still available
    {
        switch (index)
        {
            case 0:
                availableTower1--;
                return availableTower1 > 0;
            case 1:
                availableTower2--;
                return availableTower2 > 0;
            case 2:
                availableTower3--;
                return availableTower3 > 0;
        }

        return false;
    }

    private void TryDeleteTower()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        Collider2D[] hits = Physics2D.OverlapCircleAll(mouseWorldPos, 0.1f);
        foreach (var hit in hits)
        {
            if (hit is not null && hit.CompareTag("Tower"))
            {
                GameObject towerToDelete = hit.gameObject;
                int towerIndex = -1;
                for (int i = 0; i < towerPrefabs.Length; i++)
                {
                    if (towerToDelete.name.Contains(towerPrefabs[i].name))
                    {
                        towerIndex = i;
                        break;
                    }
                }

                if (towerIndex != -1)
                {
                    switch (towerIndex)
                    {
                        case 0:
                            availableTower1++;
                            break;
                        case 1:
                            availableTower2++;
                            break;
                        case 2:
                            availableTower3++;
                            break;
                    }

                    ui.UpdateTowerButtons();
                }

                _placedTowers.Remove(towerToDelete.GetComponent<Tower>());
                Destroy(towerToDelete);
            }
        }
    }
}