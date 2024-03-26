using UnityEngine;
using DG.Tweening;

public class PlaneController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float distanceToCamera = 5;
    [SerializeField] private float distanceToLook = 5;

    private Vector3 moveTarget;
    private Tween moveTween, lookAtTween;

    void FixedUpdate()
    {
        Vector3 mouseScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToCamera);
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = transform.position.z;

        if (Input.GetMouseButton(0))
        {       
            moveTarget = mouseWorldPosition;
            
            if ((moveTarget - transform.position).sqrMagnitude > 0.001f)
            {
                moveTween?.Kill();
                moveTween = transform.DOMove(moveTarget, speed * Time.deltaTime).SetSpeedBased();
                
                lookAtTween?.Kill();
                Vector3 lookAtTarget = mouseWorldPosition + Vector3.forward * distanceToLook;
                lookAtTween = transform.DOLookAt(lookAtTarget, speed * Time.deltaTime);
            }
        }
        else
        {
            moveTween?.Kill();
            lookAtTween?.Kill();

            Vector3 lookAtTarget = transform.position + new Vector3(0, 0, distanceToLook);
            if (transform.rotation.eulerAngles != Quaternion.LookRotation(lookAtTarget - transform.position).eulerAngles)
            {
                lookAtTween = transform.DOLookAt(lookAtTarget, speed * Time.deltaTime);
            }
        }
    }
}
