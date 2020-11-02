using UnityEngine;
using System.Collections;

public class SecondaryTrigger : MonoBehaviour
{
    public static SecondaryTrigger Instance;

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

    void OnTriggerEnter(Collider collider)
    {
        ThirdTrigger.Instance.ExpectCollider(collider);
    }
}