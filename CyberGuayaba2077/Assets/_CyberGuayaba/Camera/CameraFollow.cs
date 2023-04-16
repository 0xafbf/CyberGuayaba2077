using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f;
    private Vector3 offset;

    private Vector3 velocity = Vector3.zero;


	private void Awake()
	{
        SetOffset();
	}

	void SetOffset()
	{
        offset = transform.position - target.transform.position;
	}

    void FixedUpdate()
    {
        Vector3 targetPos = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}