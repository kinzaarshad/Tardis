using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour
{
    public int score = 0;
    public GameObject camera;
    public GameObject ring;
    public GameObject player;
    public Text Distance;
    public Text Angle;
    public Text Time;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (ring != null && player != null)
        {
            var dist = Vector3.Distance(ring.transform.position, player.transform.position);
            //dist = Vector3.distance(ring.transform.position, player.transform.position);
            //Debug.Log(string.Format("Distance between {0} and {1} is: {2}", ring, player, dist));
            //var rot = camera.transform.rotation.y;
            float rot = Quaternion.Angle(Quaternion.Euler(new Vector3(0, 0, 0)), camera.transform.rotation);
            Distance.text = "Distance:" + dist;
            Angle.text = "Angle:" + rot;
            float speed = 20;
            float ySpeed = speed * Mathf.Sin(rot);

            float time = (ySpeed + Mathf.Sqrt((ySpeed * ySpeed) + 2 * 10 * 5)) / 10;
            Time.text = "Time=" + time;
        }
    }

    public void IncrementScore(int amount)
    {
        score += amount;
        AudioSource source = GetComponent<AudioSource>();
        source.Play();
        source.Play();
        source.Play();
        //SceneManager.LoadScene(1);
    }
}