using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    { ;
        Destroy(gameObject,2);
       
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * 100 * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            Vector3 direction = (-transform.position + other.transform.position).normalized;
            direction.y = 0;
            other.GetComponent<Rigidbody>().AddForce(direction * 200 * Time.deltaTime,ForceMode.Force);
        }
        Destroy(gameObject);
    }
}
