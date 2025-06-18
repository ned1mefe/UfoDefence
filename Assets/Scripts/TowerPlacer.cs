using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerPlacer : MonoBehaviour
{
    [SerializeField] private Tilemap placementTilemap; 
    [SerializeField] private GameObject[] towerPrefabs;
    private GameObject _selectedTower;

    private int index = 0;
    private void Start()
    {
        //enabled = false;
        _selectedTower = towerPrefabs[index];
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryPlaceTower();
        }
    }
    
    public void StartPlacing(GameObject tower)
    {
        _selectedTower = tower;
        enabled = true;
    }

    public void StopPlacing()
    {
        _selectedTower = null;
        enabled = false;
    }

    private void TryPlaceTower()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        mouseWorldPos.y -= 0.25f;// aligning mistake with tiles
        Vector3Int cellPos = placementTilemap.WorldToCell(mouseWorldPos);

        TileBase clickedTile = placementTilemap.GetTile(cellPos);

        if (clickedTile is null || clickedTile.name != "buildingPlaceGrass") return;
            
            
        Vector3 placePos = placementTilemap.GetCellCenterWorld(cellPos);

        placePos.y += _selectedTower.transform.position.y;
            
        placePos.y += 0.5f;  // aligning mistake with tiles

        if (!IsTowerOnTile(placePos))
        {
            Instantiate(_selectedTower, placePos, Quaternion.identity);
            index = (index + 1) % 3;
            _selectedTower = towerPrefabs[index];
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
}