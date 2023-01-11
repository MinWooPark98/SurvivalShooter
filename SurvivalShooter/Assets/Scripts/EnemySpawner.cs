using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Serializable]
    public struct Spawner
    {
        public GameObject enemyPrefab;
        public Transform spawnPoint;
        public float spawnDelay;
    }
    public Spawner[] spawners;

    public int poolCount = 10;
    private LinkedList<GameObject>[] usingEnemies;
    private LinkedList<GameObject>[] unusingEnemies;

    // Start is called before the first frame update
    void Start()
    {
        usingEnemies = new LinkedList<GameObject>[spawners.Length];
        unusingEnemies = new LinkedList<GameObject>[spawners.Length];
        for (int i = 0; i < spawners.Length; ++i)
        {
            usingEnemies[i] = new LinkedList<GameObject>();
            unusingEnemies[i] = new LinkedList<GameObject>();
            CreateEnemy(i);
            StartCoroutine(Spawn(i));
        }
    }

    private void Update()
    {
        for (int i = 0; i < spawners.Length; ++i)
        {
            var enumerator = usingEnemies[i].GetEnumerator();
            if (enumerator.Current != null)
            {
                do
                {
                    if (!enumerator.Current.activeSelf)
                    {
                        var curr = enumerator.Current;
                        unusingEnemies[i].AddLast(curr);
                        usingEnemies[i].Remove(curr);
                    }
                } while (!enumerator.MoveNext());
            }
        }
    }

    private IEnumerator Spawn(int idx)
    {
        while(true)
        {
            yield return new WaitForSeconds(spawners[idx].spawnDelay);
            if (unusingEnemies[idx].Count == 0)
                CreateEnemy(idx);
            var enemy = unusingEnemies[idx].First();
            usingEnemies[idx].AddLast(enemy);
            unusingEnemies[idx].RemoveFirst();
            enemy.GetComponent<Enemy>().ResetAll();
        }
    }

    private void CreateEnemy(int idx)
    {
        var point = spawners[idx].spawnPoint;
        for (int i = 0; i < poolCount; ++i)
        {
            var newGO = Instantiate(spawners[idx].enemyPrefab, point.position, point.rotation);
            unusingEnemies[idx].AddLast(newGO);
        }
    }
}
