using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cursor : MonoBehaviour
{
//    public TextMeshProUGUI debug;
//    Vector3 clickPos;

    public FixedJoystick joystick;
    public CannonTargetManager cannonTargetManager;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, float.MaxValue,
            LayerMask.GetMask("Ground", "CannonTarget")))
        {
            var temp = transform.position;
            temp.y = hit.point.y;
            transform.position = temp;
//            transform.up = hit.normal;
        }
        else
        {
            var temp = transform.position;
            temp.y += 0.1f;
            transform.position = temp;
        }


        var dir = joystick.Direction;
        Vector3 direction = new Vector3(dir.x, 0, dir.y);
        transform.position += direction * (Time.deltaTime * 40f);

        var pos = transform.position;

        transform.position = new Vector3(Mathf.Clamp(pos.x, -60f, 60f), pos.y, Mathf.Clamp(pos.z, -60f, 60f));
        if (Physics.Raycast(transform.position + (Vector3.up * 5f), -transform.up, out hit, float.MaxValue, LayerMask.GetMask("CannonTarget")))
            cannonTargetManager.ChangeTargetColor(hit.collider.CompareTag("CannonTarget"));
        else
            cannonTargetManager.ChangeTargetColor(false);
    }

    public bool IsPointerOverUIObject()
    {
        if (EventSystem.current == null) return false;
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
#if UNITY_EDITOR
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
#elif !UNITY_EDITOR
        eventDataCurrentPosition.position = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
#endif
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0 || EventSystem.current.currentSelectedGameObject != null;
    }
}