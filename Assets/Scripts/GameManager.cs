using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<LevelTowerData> levelTowerData;
    [SerializeField] private List<LevelEnemyData> levelEnemyData;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private TowerPlacer towerPlacer;
    [SerializeField] private UI ui;
    
    private enum GamePhase
    {
        Preparation,
        Defence
    }

    private GamePhase _gamePhase;
    private int _currentLevelIndex;

    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        _currentLevelIndex = 0;
        _gamePhase = GamePhase.Preparation;
        StartPreparationPhase();
    }

    public void CheckWinLevel()
    {
        if (enemySpawner.AllEnemiesDefeated())
            WinLevel();
    }

    public void LoseLevel()
    {
        ToggleGamePhase();
        ui.OnLevelLose();
    }

    public void ToggleGamePhase()
    {
        _gamePhase = 1 - _gamePhase;

        switch (_gamePhase)
        {
            case GamePhase.Preparation:
                StartPreparationPhase();
                break;
            case GamePhase.Defence:
                StartDefencePhase();
                break;
        }
        StartCoroutine(ui.DisableAndToggleActionButton());
    }

    private void WinLevel()
    {
        if (_currentLevelIndex == levelEnemyData.Count() - 1)
        {
            WinGame();
            ToggleGamePhase();
            return;
        }
        
        _currentLevelIndex++;
        
        ui.OnLevelEnd(_currentLevelIndex + 1);
        
        ToggleGamePhase();
    }

    private void WinGame() // could make something else
    {
        ui.OnLevelEnd(_currentLevelIndex + 2);
    }

    private void StartDefencePhase()
    {
        towerPlacer.StopPlacing();
        towerPlacer.ActivateTowers();
        towerPlacer.gameObject.SetActive(false);
        enemySpawner.Activate();
    }

    private void StartPreparationPhase()
    {
        enemySpawner.Deactivate();
        enemySpawner.SetEnemySpawner(levelEnemyData[_currentLevelIndex]);
        
        towerPlacer.gameObject.SetActive(true);
        towerPlacer.ClearTowers();
        towerPlacer.SetTowerPlacer(levelTowerData[_currentLevelIndex]);
    }


}
