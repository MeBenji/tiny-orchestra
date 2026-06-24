using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    private Transform target;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position = transform.position + speed * Time.deltaTime * direction;
    }
}
