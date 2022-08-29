using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    [SerializeField] private float timeDelay;
    void Start()
    {
        Destroy(gameObject,timeDelay);
    }
}
