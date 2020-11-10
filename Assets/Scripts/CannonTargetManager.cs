using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CannonTargetManager : MonoBehaviour
{
    private Renderer targetGraphic;
    public Transform arrow;
    public Transform target;
    public int score = -1;

    public static CannonTargetManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        targetGraphic = target.GetComponent<Renderer>();
        AssignRandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (score > 4)
            CannonSceneManager.Instance.GoBack();
    }

    public void AssignRandomPosition()
    {
        float angle = Random.Range(0f, 360f);
        float distance = Random.Range(10f, 50f);

        target.Rotate(new Vector3(0, angle, 0));
        target.Translate(Vector3.zero, Space.Self);
        target.Translate(new Vector3(distance, 0, distance), Space.Self);

        arrow.position = target.position;

        score++;

        StartCoroutine(VerticallyAlignTarget());
    }

    IEnumerator VerticallyAlignTarget()
    {
        yield return new WaitForSeconds(0.1f);

        RaycastHit hit;
        if (Physics.Raycast(target.position, -target.up, out hit, float.MaxValue, LayerMask.GetMask("Ground")))
        {
            var temp = target.position;
            temp.y = hit.point.y;
            target.position = temp;
            target.up = hit.normal;
            arrow.position = temp + Vector3.up * 3.5f;
        }
    }

    public void ChangeTargetColor(bool cursorOnTarget)
    {
        if (cursorOnTarget)
            targetGraphic.material.color = Color.green;
        else
            targetGraphic.material.color = Color.white;
    }
}