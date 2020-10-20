using UnityEngine;

public class Cursor : MonoBehaviour 
{    
	void Update () 
	{
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue, LayerMask.GetMask("Ground")))
        {
            transform.position = hit.point;
        }
	}
}
