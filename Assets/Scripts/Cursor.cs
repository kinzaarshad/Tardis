using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/*public class Cursor : MonoBehaviour
{
    public CannonTargetManager cannonTargetManager;
    public TextMeshProUGUI debug;

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() || IsPointerOverUIObject()) return;
#if UNITY_EDITOR
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#elif !UNITY_EDITOR
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
#endif
//        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
//        Ray ray = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, LayerMask.GetMask("Ground", "CannonTarget")/*,
            1 << LayerMask.NameToLayer("Ground") | LayerMask.NameToLayer("CannonTarget")#1#))
        {
            debug.text = hit.collider.name;
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
}*/
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


//        if (pos.x < 15 || pos.x > -15f || pos.x < 60 || pos.x > -60 ||
//            pos.z < 15 || pos.z > -15f || pos.z < 60 || pos.z > -60) return;

        var dir = joystick.Direction;
        Vector3 direction = new Vector3(dir.x, 0, dir.y);
        transform.position += direction * (Time.deltaTime * 40f);

        var pos = transform.position;

        transform.position = new Vector3(Mathf.Clamp(pos.x, -60f, 60f), pos.y, Mathf.Clamp(pos.z, -60f, 60f));
        if (Physics.Raycast(transform.position + (Vector3.up * 5f), -transform.up, out hit, float.MaxValue, LayerMask.GetMask("CannonTarget")))
            cannonTargetManager.ChangeTargetColor(hit.collider.CompareTag("CannonTarget"));
        else
            cannonTargetManager.ChangeTargetColor(false);
//        if (EventSystem.current.IsPointerOverGameObject() || IsPointerOverUIObject()) return;
/*#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            clickPos = Input.GetTouch(0).position;
        }
#endif

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            clickPos = Input.mousePosition;
        }
        //If Click  }
#endif*/

/*#if !UNITY_EDITOR
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
#endif
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
#endif

        {
#if !UNITY_EDITOR
                Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
#endif

#if UNITY_EDITOR
            Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
#endif
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                Debug.Log("Something Hit");
//                debug.text = "Something Hit";

                //OR with Tag

                if (raycastHit.collider.CompareTag("Ground") || raycastHit.collider.CompareTag("CannonTarget"))
                {
                    Debug.Log("Soccer Ball clicked");
//                    debug.text += "Ground" + raycastHit.point;
                    transform.position = raycastHit.point;
                }
            }
        }*/


//        Ray ray = Camera.main.ScreenPointToRay(clickPos);
//        Vector3 pos = Camera.main.ScreenToWorldPoint(clickPos);
//        transform.position = pos;
//        debug.text = pos.ToString();
//        RaycastHit hit;
//        if (Physics.Raycast(ray, out hit, float.MaxValue, LayerMask.GetMask("Ground")))
//        {
//            debug.text = clickPos + " " + hit.point;
//            transform.position = hit.point;
//        }
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