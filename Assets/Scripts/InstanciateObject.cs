using System.Collections.Generic;
using UnityEngine;

public class InstanciateObject : MonoBehaviour
{
    public GameObject prefabGameObject = null;
    public Transform followedObject = null;
    private List<GameObject> arkList = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i <= 300; i += 10)
        {
            Vector3 newPosition = new Vector3(
                transform.position.x,
                transform.position.y,
                transform.position.z+i
            );
            var ark = Instantiate(prefabGameObject, newPosition, Quaternion.identity, gameObject.transform);
            arkList.Add(ark);
        }
    }    
    void LateUpdate()
    {
        // Do not show the objects behind the followed object
        arkList.FindAll(x => x.transform.position.z > followedObject.position.z).ForEach(z => z.SetActive(false));
        // Show the objects in front of the followed object
        arkList.FindAll(x => x.transform.position.z < followedObject.position.z).ForEach(z => z.SetActive(true));
    }
}
