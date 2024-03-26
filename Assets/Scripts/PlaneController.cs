using UnityEngine;
using DG.Tweening;

public class PlaneController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private float rotationSpeed = 2.0f;
    [SerializeField] private float maxRotationX = 15.0f;
    [SerializeField] private float maxRotationZ = 80.0f;

    private Vector3 velocity = Vector3.zero;
    private float zRot = 0f;
    private float xRot = 0f;

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z));
            Vector3 posToMove = new Vector3(mousePos.x, mousePos.y, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, posToMove, ref velocity, moveSpeed * Time.deltaTime);

            zRot = Mathf.Clamp((transform.position.x - mousePos.x) * maxRotationZ, -maxRotationZ, maxRotationZ);
            xRot = Mathf.Clamp((transform.position.y - mousePos.y) * maxRotationX, -maxRotationX, maxRotationX);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(xRot, 0, -zRot), Time.deltaTime * rotationSpeed);
        }
        else
        {
            if (velocity != Vector3.zero)
                transform.position = Vector3.SmoothDamp(transform.position, transform.position, ref velocity, 0.1f);
            
            if (transform.localRotation != Quaternion.Euler(0, 0, 0))
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, 0), 0.1f);
        }
    }
}
