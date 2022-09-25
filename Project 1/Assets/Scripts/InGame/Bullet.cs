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
        pv = GetComponent<PhotonView>();
       
    }
    private void OnEnable()
    {
        Invoke("Hide",2);
    }
    private IEnumerator DestroyGameObject(float time,GameObject obj)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10, layer))
        {
            print(hit.collider.name);
            Vector3 direction = (-transform.position + hit.transform.position).normalized;
            if (hit.collider.GetComponent<ObstacleType>() != null)
            {
                print(hit.collider.name);
                if (hit.collider.GetComponent<ObstacleType>().GetObstacleType() == ObstacleTypes.Human)
                {
                    ObjectPooler.Instance.SpawnObjectPool(ObjectPooler.TypeObjectPool.humanImpact, hit.point, Quaternion.LookRotation(direction));
                    hit.transform.GetComponent<CharacterController>().Move(direction * _force);
                    hit.transform.GetComponent<PlayerManager>().SetIDPlayerIsShooted(IDPlayer);
                    hit.transform.GetComponent<Animator>().SetTrigger("TakeDamage");
                }
                else if (hit.collider.GetComponent<ObstacleType>().GetObstacleType() == ObstacleTypes.Wall)
                {
                    ObjectPooler.Instance.SpawnObjectPool(ObjectPooler.TypeObjectPool.rockImpact,hit.collider.transform.position, Quaternion.LookRotation(direction));
                }
                else if (hit.collider.GetComponent<ObstacleType>().GetObstacleType() == ObstacleTypes.Wood)
                {
                    ObjectPooler.Instance.SpawnObjectPool(ObjectPooler.TypeObjectPool.woodImpact, hit.point, Quaternion.LookRotation(direction));
                }
                else
                {
                    ObjectPooler.Instance.SpawnObjectPool(ObjectPooler.TypeObjectPool.rockImpact, hit.point, Quaternion.LookRotation(direction));
                }

            }
            if (hit.collider.tag == "Obstacle")
            {
                //direction.y = 0;
                hit.transform.GetComponent<Rigidbody>().AddForce(direction * _force * Time.deltaTime, ForceMode.Impulse);
            }
            _forceLocal = 0;
            gameObject.SetActive(false);
        }
        else
        {
            transform.Translate(Vector3.forward * 100 * Time.deltaTime * _force);
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
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
