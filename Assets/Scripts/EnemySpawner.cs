using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> EnemyPrefabs;
    [SerializeField] private float delayBetweenSpawns;
    private Transform[] _spawners;
    
    private void Start()
    {
        _spawners = GetComponentsInChildren<Transform>()
            .Where(t => t != transform)
            .ToArray();

        foreach (var spawner in _spawners)
        {
            Debug.Log(spawner.position);
        }
        
        var x = new LevelData
        {
            Enemy1Count = 3,
            Enemy2Count = 1,
            Enemy3Count = 2
        };

        StartCoroutine(SpawnLevel(x));
    }

    public IEnumerator SpawnLevel(LevelData levelData)
    {
        var spawnIndices = new List<int>();

        for (var i = 0; i < levelData.Enemy1Count; i++) spawnIndices.Add(0);
        for (var i = 0; i < levelData.Enemy2Count; i++) spawnIndices.Add(1);
        for (var i = 0; i < levelData.Enemy3Count; i++) spawnIndices.Add(2);

        var shuffledIndices = spawnIndices.OrderBy(x => Random.value);

        foreach (var index in shuffledIndices)
        {
            yield return new WaitForSeconds(delayBetweenSpawns);
            
            var spawner = _spawners[Random.Range(0, _spawners.Length)];
            var enemy = EnemyPrefabs[index];

            Instantiate(enemy, spawner.position, Quaternion.identity);
        }
        
    }
    
}
