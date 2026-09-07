using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0f, 4f, -6f);
    [SerializeField] private float followSpeed = 8f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Vector3 lookOffset = new Vector3(0f, 1.5f, 0f);

    private void LateUpdate()
    {
       // Gewünschte Kameraposition
        Vector3 desiredPosition = target.position + target.rotation * offset;

        // Sanft folgen
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            followSpeed * Time.deltaTime);

        // Zur Spielfigur schauen
        Vector3 lookTarget = target.position + lookOffset;

        Quaternion targetRotation = Quaternion.LookRotation(lookTarget - transform.position);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime);
    }
}
