using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using StarterAssets;

public class RigTranstion : MonoBehaviour
{
    [SerializeField] private RigBuilder _rigBuilder;

    private StarterAssetsInputs _input;

    public enum StateCharacter 
    { 
        nothing,
        hasGunRun,
        hasGunShoot,
    }
    [SerializeField] public StateCharacter _stateCharacter; // Set Null
    protected bool IsUpdateStateCharacter = false;
    private void Awake()
    {
      
    }
    private void Start()
    {
        _rigBuilder = GetComponent<RigBuilder>();
        _input = GetComponent<StarterAssetsInputs>();  
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1) || Input.GetMouseButton(0))
        {
            IsUpdateStateCharacter = true;
            _stateCharacter = StateCharacter.hasGunShoot;
        }
        else
        {  
            IsUpdateStateCharacter = false;
            _stateCharacter = StateCharacter.hasGunRun;
        }
    }
}
