using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class Direct : MonoBehaviour
{
    [SerializeField] LayerMask enemyMask;
    PlayerInput input;
    Camera cam;

    EnemyController previousEnemy;

    [SerializeField] Material defaultMaterial;
    [SerializeField] Material mouseOnMaterial;

    [SerializeField] bool mouseAim;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
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
        }
    }

    void RayCast(Ray ray)
    {
        float maxDist = 25f;
        Debug.DrawRay(ray.origin, ray.direction * maxDist);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDist, enemyMask))
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
}
