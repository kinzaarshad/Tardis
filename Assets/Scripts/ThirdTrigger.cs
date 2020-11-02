using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ThirdTrigger : MonoBehaviour
{
    public static ThirdTrigger Instance;
    public GameObject basketEffect;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    Collider expectedCollider;

    public void ExpectCollider(Collider collider)
    {
        expectedCollider = collider;
    }

    void OnTriggerEnter(Collider other)
    {
        //print("1");
        if (other == expectedCollider)
        {
            //print("Trigger Entered");
            GameObject go = Instantiate(basketEffect, transform.position, Quaternion.identity);
            ParticleSystem ps = go.GetComponent<ParticleSystem>();
            Destroy(go, ps.main.duration);
            Destroy(other, 3f);

            ScoreKeeper.Instance.IncrementScore(1);
            StartCoroutine(NewPos(1f));
        }
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

        foreach (var renderer in renderers)
            renderer.enabled = true;
    }
}