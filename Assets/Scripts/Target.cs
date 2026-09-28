using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float distanceFromCamera = 10f; // how far in front of camera
    [SerializeField] private float scrollSensitivity = 5f;
    [SerializeField] private float minDistance = 1f;
    [SerializeField] private float maxDistance = 50f;

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distanceFromCamera += scroll * scrollSensitivity;
        distanceFromCamera = Mathf.Clamp(distanceFromCamera, minDistance, maxDistance);

        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = distanceFromCamera;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        if (!Input.GetMouseButton(0))
            transform.position = worldPos;
    }
}
