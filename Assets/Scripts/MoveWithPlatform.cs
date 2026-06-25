using UnityEngine;

public class MoveWithPlatform : MonoBehaviour
{
    Transform platform;
    [SerializeField] LayerMask mask = 64;

    private void LateUpdate() {
        SetUpdatedPosition();
    }
    void SetUpdatedPosition() {

        Debug.DrawRay(transform.position + (transform.up * 0.5f), -transform.up);
        if(Physics.Raycast(transform.position + (transform.up * 0.5f), -transform.up, out RaycastHit hit, 5, mask)) {
            platform = hit.transform;
        }

        transform.parent = platform;
    } 
}
