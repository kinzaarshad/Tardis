using UnityEngine;

public class CannonTargetManager : MonoBehaviour
{
    private Renderer targetGraphic;
    public Transform arrow;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        targetGraphic = target.GetComponent<Renderer>();
        AssignRandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AssignRandomPosition()
    {
        float angle = Random.Range(0f, 360f);
        float distance = Random.Range(10f, 60f);

        target.Rotate(new Vector3(0, angle, 0));
        target.Translate(Vector3.zero);
        target.Translate(new Vector3(distance, 0f, distance));
        Ray ray = new Ray();

        ray.origin = target.position;
        ray.direction = target.up;
        
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue /*, LayerMask.GetMask("Ground")*/))
        {
            var temp = target.position;
            temp.y = hit.point.y;
            target.position = temp;
        }


        Debug.DrawRay(target.position + target.up, target.up * 100000f, Color.blue);
    }

    public void ChangeTargetColor(bool cursorOnTarget)
    {
        if (cursorOnTarget)
            targetGraphic.material.color = Color.green;
        else
            targetGraphic.material.color = Color.white;
    }
}