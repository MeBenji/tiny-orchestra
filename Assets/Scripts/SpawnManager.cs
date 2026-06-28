using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    [System.Serializable]
    public struct Phase
    {
        public GameObject[] prefabs;
        public int quantity;
        public bool minOneEach;
    }

    [SerializeField] float spawnRadius;
    public float spawnDelay;
    private float initialDelay = 3;
    [SerializeField] List<Phase> phases;
    [SerializeField] List<GameObject> enemySpawnList;
    public UnityEvent onEnemyDirected;
    int enemiesLeft;

    private void OnEnable()
    {
        onEnemyDirected.AddListener(OnEnemyDirected);
    }

    private void OnDisable()
    {
        onEnemyDirected.RemoveListener(OnEnemyDirected);
    }

    private void OnEnemyDirected()
    {
        enemiesLeft--;
        if (enemiesLeft <= 0)
        {
            GameManager.Instance.onWin?.Invoke();
        }
    }

    private void Awake()
    {
        BuildEnemySpawnList();
        enemiesLeft = enemySpawnList.Count;
    }

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(initialDelay);
        foreach(GameObject enemy in enemySpawnList)
        {
            SpawnEnemy(enemy);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private GameObject GetRandomFromArray(GameObject[] prefabs)
    {
        int index = Random.Range(0, prefabs.Length);
        return prefabs[index];
    }

    private List<T> Shuffle<T>(List<T> list)
    {
        int length = list.Count;
        List<T> tmpList = new List<T>();
        for (int i = 0; i < length; i++)
        {
            int index = Random.Range(0, list.Count);
            tmpList.Add(list[index]);
            list.RemoveAt(index);
        }
        return tmpList;
    }

    private void BuildEnemySpawnList()
    {
        foreach (Phase phase in phases)
        {
            int remaining = phase.quantity;
            List<GameObject> phaseSpawnList = new List<GameObject>();
            if (phase.minOneEach)
            {
                for (int i = 0; i < phase.prefabs.Length; i++)
                {
                    phaseSpawnList.Add(phase.prefabs[i]);
                }
                remaining -= phase.prefabs.Length;
            }
            for (int i = 0; i < remaining; i++)
            {
                phaseSpawnList.Add(GetRandomFromArray(phase.prefabs));
            }

            phaseSpawnList = Shuffle(phaseSpawnList);

            foreach(GameObject prefab in phaseSpawnList)
            {
                enemySpawnList.Add(prefab);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }

    private Vector3 GetSpawnLocation()
    {
        Vector2 xzPosition = spawnRadius * Random.onUnitCircle;
        return transform.position + new Vector3(xzPosition.x, 0, xzPosition.y);
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        Vector3 position = GetSpawnLocation();
        Vector3 lookDirection = transform.position - position;
        lookDirection = new Vector3(lookDirection.x, 0f, lookDirection.z).normalized;
        GameObject enemy = Instantiate(enemyPrefab);
        enemy.transform.position = position;
        enemy.transform.forward = lookDirection;
        enemy.GetComponent<EnemyController>().Init(this);
        enemy.SetActive(true);
    }
}
