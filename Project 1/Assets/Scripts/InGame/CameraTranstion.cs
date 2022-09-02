using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using Photon.Pun;

public class CameraTranstion : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cameraAim;
    [SerializeField] private CinemachineVirtualCamera _cameraFollow;
    [SerializeField] private int SensitivityCamera;
    [SerializeField] private LayerMask aimColliderLayerMask;
    [SerializeField] private Transform debugTransform;

    private ThirdPersonController thirdPersonController;
    private Vector3 mouseWorldPosition = Vector3.zero;
    private Shoot _shoot;
    private PhotonView pv;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        _shoot = GetComponent<Shoot>();
        if (!pv.IsMine)
        {
            Destroy(_cameraAim);
            Destroy(_cameraFollow);
        }
    }
    private void Update()
    {
        if(!pv.IsMine) return;
        if (_cameraAim == null || _cameraFollow == null) return;
        Vector2 screenPointCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenPointCenter);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }
        if (Input.GetMouseButton(1)) // aim
        {
            _cameraFollow.gameObject.SetActive(false);
            _cameraAim.gameObject.SetActive(true);
            thirdPersonController.SetSensitivityCamera(SensitivityCamera);
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward,aimDirection,Time.deltaTime * 20f);
        }else if (Input.GetMouseButton(0))
        {
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            _cameraAim.gameObject.SetActive(false);
            _cameraFollow.gameObject.SetActive(true);
            thirdPersonController.SetSensitivityCamera(1);
        }
        _shoot.SetMousePos(mouseWorldPosition);
    }
}
