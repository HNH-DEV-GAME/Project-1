using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    { ;
        Destroy(gameObject,3);
       
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * 100 * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
