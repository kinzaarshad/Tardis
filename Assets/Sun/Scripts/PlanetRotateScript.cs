using UnityEngine;

public class PlanetRotateScript : MonoBehaviour
{
int speed =1 ;
void Update() {
  
    transform.Rotate(Vector3.up * Time.deltaTime*speed);
}
}