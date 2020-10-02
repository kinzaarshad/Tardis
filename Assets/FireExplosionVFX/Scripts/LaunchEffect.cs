using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class LaunchEffect : MonoBehaviour {

	public GameObject FxPrefabLOW;

	public void LaunchSlow(int _Quality)
	{
		Time.timeScale = 0.5f;
		GameObject fx = Instantiate(FxPrefabLOW, new Vector3(0, 0, -2), Quaternion.identity) as GameObject;
		
	}
}
