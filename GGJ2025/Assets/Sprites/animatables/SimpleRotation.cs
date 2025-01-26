using UnityEngine;

public class SimpleRotation : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 30f; // Speed of rotation, positive or negative
    public Vector3 rotationAxis = Vector3.forward; // Axis of rotation (default is Y-axis)

    void Update()
    {
        // Apply rotation based on the speed and axis
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}
