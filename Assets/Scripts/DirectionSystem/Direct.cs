using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class Direct : MonoBehaviour
{
    [SerializeField] LayerMask enemyMask;
    PlayerInput input;
    Camera cam;

    Enemy currentEnemy;

    [SerializeField] Material defaultMaterial;
    [SerializeField] Material mouseOnMaterial;

    private void Awake() {
        input = GetComponent<PlayerInput>();
        cam = Camera.main;
    }

    private void Update() {
        RayCast(Input.mousePosition);

        if(Input.GetMouseButtonDown(0) && currentEnemy != null) {
            currentEnemy.OnPlayerClicksOn();
        }
    }

    void RayCast(Vector3 mousePos) {
        Ray ray = cam.ScreenPointToRay(mousePos);

        Debug.DrawRay(ray.origin, ray.direction * 100);
        if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, enemyMask)) {
            Debug.DrawLine(ray.origin, hit.point, Color.red);

            currentEnemy = hit.transform.GetComponent<Enemy>();
            currentEnemy.getRenderer().sharedMaterial = mouseOnMaterial;

        } else if(currentEnemy != null) {
            currentEnemy.getRenderer().sharedMaterial = defaultMaterial;
        }
    }
}
