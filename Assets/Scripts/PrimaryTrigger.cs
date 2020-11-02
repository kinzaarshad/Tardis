using UnityEngine;
using System.Collections;

public class PrimaryTrigger : MonoBehaviour
{
    public static PrimaryTrigger Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void OnTriggerEnter(Collider collider)
    {
        SecondaryTrigger.Instance.ExpectCollider(collider);
    }
}