using UnityEngine;

public class Follow : MonoBehaviour
{
    // The target we are following
    [SerializeField]
    private Transform target = null;
    // The distance in the x-z plane to the target
    [SerializeField]
    private float distance = 10.0f;
    // the height we want the camera to be above the target
    [SerializeField]
    private float height = 5.0f;

    void LateUpdate()
    {
        // Early out if we don't have a target
        if (!target)
            return;

        transform.position = target.position + Vector3.up * height;

        transform.position -= Vector3.back * distance;
    }
}
