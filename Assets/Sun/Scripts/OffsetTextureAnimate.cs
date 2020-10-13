using UnityEngine;
// Scroll main texture based on time
//v1.01
//-IOS compatible
public class OffsetTextureAnimate : MonoBehaviour
{
float scrollSpeedX = 0.015f;
float scrollSpeedY = 0.015f;
float scrollSpeedXMaterial2= 0.015f;
float scrollSpeedYMaterial2 = 0.015f;
void Update () {
    var offsetX = Time.time * scrollSpeedX % 1f;
    var offsetY = Time.time * scrollSpeedY % 1f;
    var offset2X = Time.time * scrollSpeedXMaterial2 % 1f;
    var offset2Y = Time.time * scrollSpeedYMaterial2 % 1f;
    GetComponent<Renderer>().material.SetTextureOffset ("_BumpMap", new Vector2(offsetX,offsetY));
    GetComponent<Renderer>().material.SetTextureOffset ("_MainTex", new Vector2(offsetX,offsetY));
    if(GetComponent<Renderer>().materials.Length>1){
   		 GetComponent<Renderer>().materials[1].SetTextureOffset ("_MainTex", new Vector2(offset2X,offset2Y));
  		 GetComponent<Renderer>().materials[1].SetTextureOffset ("_BumpMap", new Vector2(offset2X,offset2Y));
    }
}
}