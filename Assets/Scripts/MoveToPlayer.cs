using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float bumpDistance = 0.5f;
    private Transform target;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Vector3 newPos = transform.position + (speed * Time.deltaTime * direction);
        newPos.y = transform.position.y;

        transform.position = newPos;

        if ((target.position - transform.position).magnitude < bumpDistance)
        {
            GetComponent<EnemyController>().StopSteps();
            PlayerController player = target.gameObject.GetComponent<PlayerController>();
            if (player.hasLost) { return; }
            player.onLose?.Invoke();
            gameObject.GetComponent<EnemyController>().Trip();
        }
    }
}
