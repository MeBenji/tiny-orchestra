using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class Direct : MonoBehaviour
{
    [SerializeField] LayerMask enemyMask;
    [SerializeField] PlayerInput input;
    Camera cam;

    EnemyController previousEnemy;

    [SerializeField] Material defaultMaterial;
    [SerializeField] Material mouseOnMaterial;

    [SerializeField] bool mouseAim;
    float maxDist = 25f;
    [SerializeField] float radiusCast = 1;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (mouseAim)
        {
            RayCastMouse(Input.mousePosition);
        }
        else
        {
            RayCastPlayer();
        }

        if (Input.GetMouseButtonDown(0) && previousEnemy != null)
        {
            previousEnemy.onPlayInstrument?.Invoke();
            previousEnemy.Select(false);
            previousEnemy = null;
        } else if (input.actions["Fire"].WasPressedThisFrame() && previousEnemy != null)
        {
            previousEnemy.onPlayInstrument?.Invoke();
            previousEnemy.Select(false);
            previousEnemy = null;
        }
    }

    void RayCast(Ray ray)
    {
        Debug.DrawRay(ray.origin, ray.direction * maxDist);

        RaycastHit hit;
        bool cast = mouseAim ? Physics.Raycast(ray, out hit, maxDist, enemyMask) : Physics.SphereCast(ray, radiusCast, out hit, maxDist, enemyMask);

        if (cast)
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red);

            EnemyController currentEnemy = hit.transform.GetComponent<EnemyController>();
            if (previousEnemy && currentEnemy != previousEnemy)
            {
                previousEnemy.Select(false);
            }
            currentEnemy.Select(true);
            previousEnemy = currentEnemy;
        }
        else if (previousEnemy != null)
        {
            previousEnemy.Select(false);
        }
    }

    void RayCastMouse(Vector3 mousePos)
    {
        Ray ray = cam.ScreenPointToRay(mousePos);
        RayCast(ray);
    }

    void RayCastPlayer()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RayCast(ray);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.aquamarine;
        Gizmos.DrawWireSphere(transform.position, !mouseAim ? radiusCast : maxDist);
    }
}
