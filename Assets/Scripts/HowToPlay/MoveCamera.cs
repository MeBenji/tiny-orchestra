using System;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    Transform cam;
    [SerializeField] Transform[] points;
    [SerializeField] Transform[] ui;
    int currentPoint;

    [SerializeField] float smoothness = 1;
    [SerializeField] float smoothnessRot = 80;

    public static Action nextPosition;
    public static Action prevPosition;

    private void Awake() {
        nextPosition = onNextPosition;
        prevPosition = onPreviousPosition;
        cam = Camera.main.transform;

        for(int i = 0; i < ui.Length; i++) {
            ui[i].gameObject.SetActive(false);
        }
    }

    private void OnDestroy() {
        nextPosition = null;
        prevPosition = null;
    }

    private void Update() {
        cam.position = Vector3.MoveTowards(cam.position, points[currentPoint].position, Time.deltaTime * smoothness);
        cam.rotation = Quaternion.RotateTowards(cam.rotation, points[currentPoint].rotation, Time.deltaTime * smoothnessRot);
    }

    void onNextPosition() {
        ui[currentPoint].gameObject.SetActive(false);   
        currentPoint = Mathf.Clamp(currentPoint + 1, 0, points.Length - 1);

        ui[currentPoint].gameObject.SetActive(true);
    }

    void onPreviousPosition() {
        ui[currentPoint].gameObject.SetActive(false);
        currentPoint = Mathf.Clamp(currentPoint - 1, 0, points.Length - 1);

        ui[currentPoint].gameObject.SetActive(true);
    }
}
