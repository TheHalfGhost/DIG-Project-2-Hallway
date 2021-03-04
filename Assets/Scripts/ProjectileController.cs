// Made by Fisher Hensly for use in DIG-4715 Group 10's Project 2.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    Rigidbody pb3D;

    void Awake()
    {
        pb3D = gameObject.GetComponent<Rigidbody>();
    }
    void Update()
    {
        if(transform.position.magnitude > 500.0f)
        {
            Destroy(gameObject);
        }
    }
    public void Launch(float force, GameObject player)
    {
        pb3D.velocity = player.transform.forward * force;
    }
}