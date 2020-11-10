using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cursor : MonoBehaviour
{
    public CannonTargetManager cannonTargetManager;

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() || IsPointerOverUIObject()) return;
//#if UNITY_EDITOR
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//#elif !UNITY_EDITOR
//        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
//#endif
//        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
//        Ray ray = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue,
            1 << LayerMask.NameToLayer("Ground") | LayerMask.NameToLayer("CannonTarget")))
        {
            transform.position = hit.point;
            cannonTargetManager.ChangeTargetColor(hit.collider.CompareTag("CannonTarget"));
        }
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