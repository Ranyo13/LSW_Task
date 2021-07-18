using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        Vector2 targetPos = new Vector2(target.transform.position.x, target.transform.position.y);
        transform.position = Vector2.Lerp(transform.position, targetPos, 2f * Time.deltaTime);
        transform.Translate(0, 0, target.transform.position.z - 0.5f);
    }
}
