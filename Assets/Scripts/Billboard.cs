using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform cameraTransform;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(cameraTransform);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 180f, 0);
    }
}
