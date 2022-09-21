using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obstacle;
using Photon.Pun;

public class Bullet : MonoBehaviour
{
    private int _force;
    [SerializeField] private LayerMask layer;
    [SerializeField] private Transform[] impactEffect;
    private int _forceLocal = 1;
    private int IDPlayer;
    private PhotonView pv;
    private void Start()
    {
        Destroy(gameObject,2);
        pv = GetComponent<PhotonView>();
       
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * 100 * Time.deltaTime * _forceLocal);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10, layer))
        {
            Vector3 direction = (-transform.position + hit.transform.position).normalized;
            if (hit.collider.GetComponent<ObstacleType>() != null)
            {
                Transform impactGameObject;
                gameObject.GetComponent<MeshRenderer>().enabled = false;    
                if (hit.collider.GetComponent<ObstacleType>().GetObstacleType() == ObstacleTypes.Human)
                {
                    impactGameObject = Instantiate(impactEffect[0], hit.point, Quaternion.LookRotation(direction));
                    hit.transform.GetComponent<CharacterController>().Move(direction * _force );
                    hit.transform.GetComponent<PlayerManager>().SetIDPlayerIsShooted(IDPlayer);
                    hit.transform.GetComponent<Animator>().SetTrigger("TakeDamage");
                }
                else if (hit.collider.GetComponent<ObstacleType>().GetObstacleType() == ObstacleTypes.Wall)
                {
                    impactGameObject = Instantiate(impactEffect[1], hit.point, Quaternion.LookRotation(direction));
                }
                else if (hit.collider.GetComponent<ObstacleType>().GetObstacleType() == ObstacleTypes.Wood)
                {
                    impactGameObject = Instantiate(impactEffect[2], hit.point, Quaternion.LookRotation(direction));
                }
                else
                {
                    impactGameObject = Instantiate(impactEffect[3], hit.point, Quaternion.LookRotation(direction));
                }
                impactGameObject.SetParent(gameObject.transform);
            }
            if (hit.collider.tag == "Obstacle")
            {
                //direction.y = 0;
                hit.transform.GetComponent<Rigidbody>().AddForce(direction * _force * Time.deltaTime, ForceMode.Impulse);
            }
            _forceLocal = 0;
            Destroy(gameObject, 0.2f);
        }
    }
    
    public void SetForceValue(int force)
    {
        _force = force;
    }
    public void SetIDPlayer(int id)
    {
        IDPlayer = id;
    }
}
