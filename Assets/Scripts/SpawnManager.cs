using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] InputAction fire;
    [SerializeField] float radius;
    [SerializeField] ObjectPooler enemyPooler;

    private void Awake()
    {
        fire.performed += (_) => { SpawnEnemy(); };
        fire.Enable();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color=Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private Vector3 GetSpawnLocation()
    {
        Vector2 xzPosition = radius * Random.onUnitCircle;
        return transform.position + new Vector3(xzPosition.x, 0, xzPosition.y);
    }

    public void SpawnEnemy()
    {
        GameObject enemy = enemyPooler.GetGameObject();
        if (enemy == null) {
            Debug.Log("Failed to retrieve enemy from pool.");
            return;
        }
        enemy.transform.position = GetSpawnLocation();
        enemy.SetActive(true);
    }
}
