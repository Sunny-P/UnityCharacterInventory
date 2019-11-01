using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBackPanelControl : MonoBehaviour
{
    public CharacterPanelCameraController characterCameraControl;

    [Range(0.0f, 25.0f)]
    public float distanceFromCamera = 3;
    float targetDistance;

    // Start is called before the first frame update
    void Start()
    {
        targetDistance = distanceFromCamera + characterCameraControl.targetDistance;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distPos = new Vector3(0.0f, 0.0f, targetDistance);
        transform.localPosition = distPos;
    }
}
