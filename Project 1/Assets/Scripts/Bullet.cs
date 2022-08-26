using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obstacle;

public class Bullet : MonoBehaviour
{
    private int _force;
    [SerializeField] private LayerMask layer;
    [SerializeField] private Transform[] impactEffect;
    Transform impactGameObject;
    private int _forceLocal = 1;
    private void Start()
    {
        Destroy(gameObject,2);
       
    }
    private void Update()
    {
        RaycastHit hit;
        transform.Translate(Vector3.forward * 100 * Time.deltaTime * _forceLocal);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward),out hit,10,layer))
        {
            if (hit.collider.tag == "Obstacle")
            {
                Vector3 direction = (-transform.position + hit.transform.position).normalized;
                //direction.y = 0;
                hit.transform.GetComponent<Rigidbody>().AddForce(direction * _force * Time.deltaTime, ForceMode.Impulse);
            }
            if (hit.collider.GetComponent<ObstacleType>() != null) 
            {
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                if (hit.collider.GetComponent<ObstacleType>().GetObstacleType() == ObstacleTypes.Human)
                {
                    impactGameObject = Instantiate(impactEffect[0], hit.point, Quaternion.identity);
                }else if (hit.collider.GetComponent<ObstacleType>().GetObstacleType() == ObstacleTypes.Wall)
                {
                    impactGameObject = Instantiate(impactEffect[1], hit.point, Quaternion.identity);
                }else if (hit.collider.GetComponent<ObstacleType>().GetObstacleType() == ObstacleTypes.Wood)
                {
                    impactGameObject = Instantiate(impactEffect[2], hit.point, Quaternion.identity);
                }else
                {
                    impactGameObject = Instantiate(impactEffect[3], hit.point, Quaternion.identity);
                }
                _forceLocal = 0;
                Destroy(impactGameObject.gameObject, 0.3f);
                Destroy(gameObject, 0.4f);
            }
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Obstacle")
    //    {
    //        Vector3 direction = (-transform.position + other.transform.position).normalized;
    //        direction.y = 0;
    //        other.GetComponent<Rigidbody>().AddForce(direction * _force * Time.deltaTime,ForceMode.Force);
    //    }
    //    Destroy(gameObject);
    //}
    public void SetForceValue(int force)
    {
        _force = force;
    }
}
