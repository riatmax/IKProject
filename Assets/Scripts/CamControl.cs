using UnityEngine;

public class CamControl : MonoBehaviour
{
    [SerializeField] private float maxRadius;
    [SerializeField] private Transform camPivot;
    [SerializeField] private float sensitivity = 3f;

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");

        if (Input.GetMouseButton(0)) {
            camPivot.Rotate(Vector3.up, mouseX * sensitivity * Time.deltaTime);
        }
        
    }
}
