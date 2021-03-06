using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    void OnCollisionEnter(Collision PickedUp)
    {
        if (PickedUp.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
