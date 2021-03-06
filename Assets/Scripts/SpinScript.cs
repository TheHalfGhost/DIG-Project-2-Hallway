using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinScript : MonoBehaviour
{
    public GameObject Rock;
    public float speed;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, 100f, 0f) * Time.deltaTime);

        float y = Mathf.PingPong(Time.time * speed, 1) * 1 + 1;
        Rock.transform.position = new Vector3(transform.position.x, y, transform.position.z);

    }
}
