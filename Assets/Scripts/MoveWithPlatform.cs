using UnityEngine;

public class MoveWithPlatform : MonoBehaviour
{
    Transform platform;
    Vector3 localPos;
    Quaternion rotation;

    private void LateUpdate() {
        SetUpdatedPosition();

        GetLocalPosition();
    }
    void SetUpdatedPosition() {
        if(platform == null) return;

        Quaternion aQuaternion = Quaternion.identity * Quaternion.Inverse(platform.rotation);
        Quaternion bQuaternion = Quaternion.identity * Quaternion.Inverse(rotation);
        Quaternion deltaQuaternion = bQuaternion * Quaternion.Inverse(aQuaternion);
        Debug.Log(deltaQuaternion.eulerAngles.magnitude);

        if(deltaQuaternion.eulerAngles.magnitude == 0f) return;

        transform.position = platform.position + (platform.rotation * localPos);
    } 

    void GetLocalPosition() {
        if(Physics.Raycast(transform.position + (transform.up * 0.5f), -transform.up, out RaycastHit hit, 5)) {
            localPos = Quaternion.Inverse(hit.transform.rotation) * (hit.point - hit.transform.position);
            platform = hit.transform;
            rotation = platform.rotation;
        }
    }
}
