using UnityEngine;

public class Cursor : MonoBehaviour
{
    public CannonTargetManager cannonTargetManager;

    void Update()
    {
#if UNITY_EDITOR
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#elif !UNITY_EDITOR
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
#endif

        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, LayerMask.GetMask("Ground", "CannonTarget")))
        {
            transform.position = hit.point;
            cannonTargetManager.ChangeTargetColor(hit.collider.CompareTag("CannonTarget"));
        }
    }
}