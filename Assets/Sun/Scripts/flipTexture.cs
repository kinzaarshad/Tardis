using UnityEngine;

public class flipTexture : MonoBehaviour
{
    void Start()
    {
        GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(-1, -1));
    }
}