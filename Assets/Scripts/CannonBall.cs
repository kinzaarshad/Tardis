using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CannonBall : MonoBehaviour 
{
    [SerializeField]
    GameObject deathEffect;
    private object targetGameObject;
    public GameObject FxPrefabLOW;
    

    void OnCollisionEnter(Collision collision)
    {
        Instantiate(deathEffect, transform.position, Quaternion.LookRotation(collision.contacts[0].normal));
        Destroy(gameObject);
        float time = CannonController.TimeOfFlight;
        if (time>=1)
        {
            Time.timeScale = 0.5f;
           // Instantiate(FxPrefabLOW, new Vector3(0, 20, 0), Quaternion.identity);
            Instantiate(FxPrefabLOW, transform.position, Quaternion.LookRotation(collision.contacts[0].normal));

            //SceneManager.LoadScene(1);
        }

    }
}
