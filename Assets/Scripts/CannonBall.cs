using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CannonBall : MonoBehaviour 
{
    [SerializeField]
    GameObject deathEffect;
    private object targetGameObject;
    public GameObject FxPrefabLOW;
    double Target;


    void start(){
         Scene scene = SceneManager.GetActiveScene();
         if(scene.name=="level1"){
            Target=2.6;
         }
         if(scene.name=="level2"){
            Target=3.6;
         }
         if(scene.name=="level3"){
            Target=2.0;
         }
         if(scene.name=="level4"){
            Target=1.6;
         }
         if(scene.name=="level5"){
            Target=0.6;
         }
    }
    

    void OnCollisionEnter(Collision collision)
    {
        Instantiate(deathEffect, transform.position, Quaternion.LookRotation(collision.contacts[0].normal));
        Destroy(gameObject);
        float time = CannonController.TimeOfFlight;

        if (time==Target)
        {
            Time.timeScale = 0.5f;
           // Instantiate(FxPrefabLOW, new Vector3(0, 20, 0), Quaternion.identity);
            Instantiate(FxPrefabLOW, transform.position, Quaternion.LookRotation(collision.contacts[0].normal));

            //SceneManager.LoadScene(1);
        }

    }
}
