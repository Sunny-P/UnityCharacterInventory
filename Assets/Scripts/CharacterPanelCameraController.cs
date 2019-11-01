using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanelCameraController : MonoBehaviour
{
    [Header("Target to Render")]
    public GameObject targetObject;

    [Header("Distance")]
    [Range(1.0f, 10.0f)]
    public float targetDistance = 2.0f;

    [Header("Camera Angle")]
    [SerializeField]
    [Range(0.0f, 50.0f)]
    float yAngle = 25.0f;

    [Header("Height Offset")]
    [Range(0.0f, 10.0f)]
    public float heightOffset = 0.0f;

    [SerializeField]
    [Range(0.0f, 360.0f)]
    public float xAngleOffset = 0.0f;
    float xAngle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (targetObject != null)
        {
            xAngle = targetObject.transform.rotation.eulerAngles.y + xAngleOffset;
        }
        else
        {
            xAngle = 0.0f;
        }

        yAngle = ClampAngle(yAngle, 0.0f, 50.0f);
        xAngle = ClampAngle(xAngle, -360.0f, 360.0f);

        Quaternion nextRotation = Quaternion.Euler(yAngle, xAngle, 0);

        Vector3 negDist = new Vector3(0.0f, 0.0f, -targetDistance);
        Vector3 offsetTargetPosition = targetObject.transform.position;
        offsetTargetPosition.y += heightOffset;
        Vector3 nextPosition = nextRotation * negDist + offsetTargetPosition;

        transform.rotation = nextRotation;
        transform.position = nextPosition;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
