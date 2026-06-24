using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    private struct InventoryEntry
    {
        public GameObject prefab;
        public int quantity;
    }

    [SerializeField] InventoryEntry[] inventory;

    List<GameObject> pool;

    private void Awake()
    {
        pool = new List<GameObject>();
        foreach (InventoryEntry entry in inventory)
        {
            for (int i = 0; i < entry.quantity; i++)
            {
                GameObject obj = Instantiate(entry.prefab);
                obj.SetActive(false);
                pool.Add(obj);
                obj.transform.SetParent(transform);
            }
        }
    }

    public GameObject GetGameObject()
    {
        if(!pool.Any()) {
            Debug.Log("Pool is empty.");
            return null;
        }

        int index = Random.Range(0, pool.Count());
        GameObject obj = pool.ElementAt(index);
        pool.RemoveAt(index);
        return obj;
    }
}