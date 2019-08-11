using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float distance = 3f;
    public float speed = 0.18f;

    void FixedUpdate()
    {
        Vector3 currentPos = gameObject.transform.position;
        Vector3 targetPos = new Vector3(10, gameObject.transform.position.y, gameObject.transform.position.z + distance);
        transform.position = Vector3.MoveTowards(currentPos, targetPos, speed);

        if ((currentPos.z > 200) | (currentPos.y < 0))
        {
            gameObject.transform.position = new Vector3(10, 1, 20);
        }
    }
}
