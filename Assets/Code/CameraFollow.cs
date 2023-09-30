using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject followGameObject;
    [SerializeField] private float followSpeed;
    [SerializeField] private Vector3 offset;
    private Vector2 target;
    private Vector3 velocity;

    void Update()
    {
        target = new Vector3(followGameObject.transform.position.x, followGameObject.transform.position.y, transform.position.z) + offset;
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, followSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
