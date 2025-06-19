using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private float delayBetweenSpawns;
    private Transform[] _spawners;
    private Coroutine _spawnRoutine;
    private LevelEnemyData _levelEnemyData;
    private List<GameObject> _activeEnemies;
    
    private void Awake()
    {
        _activeEnemies = new List<GameObject>();
        
        _spawners = GetComponentsInChildren<Transform>()
            .Where(t => t != transform)
            .ToArray();
    }

    public void SetEnemySpawner(LevelEnemyData led) => _levelEnemyData = led;
    public void Activate()
    {
        if (_spawnRoutine is not null) return;
        
        _spawnRoutine = StartCoroutine(SpawnLevel());
    }
    
    public void Deactivate()
    {
        if (_spawnRoutine is null) return;
        
        ClearEnemies();
        StopCoroutine(_spawnRoutine);
        _spawnRoutine = null;
    }

    public bool AllEnemiesDefeated()
    {
        var defeatedCount = _activeEnemies.Count(x => !x.activeSelf);

        return defeatedCount ==
               (_levelEnemyData.Enemy1Count + _levelEnemyData.Enemy2Count + _levelEnemyData.Enemy3Count);
    }

    private void ClearEnemies()
    {
        _activeEnemies.ForEach(Destroy);
        _activeEnemies.Clear();
    }

    private IEnumerator SpawnLevel()
    {
        var spawnIndices = new List<int>();

        for (var i = 0; i < _levelEnemyData.Enemy1Count; i++) spawnIndices.Add(0);
        for (var i = 0; i < _levelEnemyData.Enemy2Count; i++) spawnIndices.Add(1);
        for (var i = 0; i < _levelEnemyData.Enemy3Count; i++) spawnIndices.Add(2);

        var shuffledIndices = spawnIndices.OrderBy(x => Random.value);

        foreach (var index in shuffledIndices)
        {
            yield return new WaitForSeconds(delayBetweenSpawns);
            
            var spawner = _spawners[Random.Range(0, _spawners.Length)];
            var enemy = enemyPrefabs[index];

            _activeEnemies.Add(Instantiate(enemy, spawner.position, Quaternion.identity));
        }
        
    }
    
}
