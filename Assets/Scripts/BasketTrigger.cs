using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasketTrigger : MonoBehaviour
{
    Vector3 prevPos;
    public GameObject basketEffect;
    IgnoreBasket ignoreBasket;

    // Start is called before the first frame update
    void Start()
    {
        ignoreBasket = transform.GetChild(0).GetComponent<IgnoreBasket>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ball"))
        {
            prevPos = other.transform.position;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ball"))
        {
            if (prevPos.y - other.transform.position.y > 0)
            {
                GameObject go = Instantiate(basketEffect, transform.position, Quaternion.identity);
                ParticleSystem ps = go.GetComponent<ParticleSystem>();
                Destroy(go, ps.main.duration);
                Destroy(other, 3f);

                ScoreKeeper.Instance.IncrementScore(1);
                StartCoroutine(NewPos(1f));
            }
        }

//        ignoreBasket.ignore = false;
    }


    IEnumerator NewPos(float wait)
    {
        yield return new WaitForSeconds(wait);

        List<Renderer> renderers = transform.root.GetComponentsInChildren<Renderer>().ToList();

        foreach (var renderer in renderers)
            renderer.enabled = false;

        Vector3 pos = new Vector3(Random.Range(-35f, 35f), -3.5f, Random.Range(-90f, 90f));
        transform.root.position = pos;
        transform.root.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

        PredictionManager.Instance.killAllObstacles();
        PredictionManager.Instance.copyAllObstacles();

        foreach (var renderer in renderers)
            renderer.enabled = true;
    }
}