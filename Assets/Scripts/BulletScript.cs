using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    
    private void OnTriggerEnter(Collider other)
    {
        //the barrel does not have 'block' tag
        if (other.tag == "Block")
        {

            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
    

}
