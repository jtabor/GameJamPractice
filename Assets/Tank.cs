using UnityEngine;
using Unity.Cinemachine;
using DG.Tweening;
using System;
using System.Numerics;
using UnityEditor.Experimental;

public class Tank : MonoBehaviour
{

    private static int numVehicles = 0;
    public int vehicleId;
    public float moveSpeed = 5f;
    public float rotationSpeed = 90f;
    public float turretRotationSpeed = 60f;

    private CinemachineCamera cam;
    private Transform turret;

    void Awake()
    {

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GetComponentInChildren<CinemachineCamera>(true);
        vehicleId = numVehicles;
        numVehicles++;
        this.tag = "PlayerTank";
        
        // Find the turret (Cylinder object based on hierarchy)
        turret = transform.Find("Tank/Bone/Bone.001");
        if (turret == null)
        {
            Debug.LogWarning("Turret (Cylinder) not found in hierarchy for " + gameObject.name);
        }
    }

    public void HandleInput(UnityEngine.Vector3 direction)
    {
        transform.Translate(direction * Time.deltaTime * moveSpeed);
    }

    public void HandleRotation(float rotation)
    {
        transform.Rotate(0, rotation * Time.deltaTime * rotationSpeed, 0, Space.Self);
    }

    public void HandleTurretRotation(float rotation)
    {
        if (turret != null)
        {
            turret.Rotate(0, rotation * Time.deltaTime * turretRotationSpeed, 0, Space.Self);
        }
    }
    public void aimAtTarget(Transform target, float duration, Action<float> onProgress = null)
    {
        UnityEngine.Vector3 dir = target.position - transform.position;
        UnityEngine.Quaternion targetRotation = UnityEngine.Quaternion.LookRotation(dir);

        var tween = transform.DORotateQuaternion(targetRotation, duration)
         .SetEase(Ease.InOutSine);

        tween.OnUpdate(() =>
        {
            float progress = tween.ElapsedPercentage();
            onProgress?.Invoke(progress);
        });

    }
    // Update is called once per frame
        void Update()
    {

    }
    public void deactivateTank()
    {
        cam.gameObject.SetActive(false);
    }
    public void activateTank()
    {
        cam.gameObject.SetActive(true);
    }
}
