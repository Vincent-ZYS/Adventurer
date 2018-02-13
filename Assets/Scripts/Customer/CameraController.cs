using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HedgehogTeam.EasyTouch;
public class CameraController : MonoBehaviour {

    public float angle = 30f;
    public float distance = 1f;//相机与角色的距离
    private Transform player;
    public float nearDis = 3;
    public float farDis = 8;
    [Header("旋转限制速度")]
    public float limitSpeed = 0.5f;//旋转限制速度
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = player.position
            -Vector3.forward*Mathf.Cos(angle/180*3.14159f)+ Vector3.up * Mathf.Sin(angle / 180 * 3.14159f);

        transform.forward = player.position - transform.position;
        EasyTouch.On_PinchOut += OnPincOut;
        EasyTouch.On_PinchIn += OnPinchIn;
        EasyTouch.On_TouchDown += OnSwipe;
    }
    void OnDestroy()
    {
        EasyTouch.On_PinchOut -= OnPincOut;
        EasyTouch.On_PinchIn -= OnPinchIn;
        EasyTouch.On_TouchDown -= OnSwipe;
    }
    void LateUpdate()
    {
        transform.position = player.position - transform.forward * distance;
    }
    void OnPincOut(Gesture gesture)
    {
        distance -= gesture.deltaPinch * 0.02f;
        distance = Mathf.Clamp(distance, nearDis, farDis);
    }
    void OnPinchIn(Gesture gesture)
    {
        distance += gesture.deltaPinch * 0.02f;
        distance = Mathf.Clamp(distance, nearDis, farDis);
    }
    void OnSwipe(Gesture gesture)
    {
        Vector3 originalPos = transform.position;
        Quaternion originalRotation = transform.rotation;
        //transform.RotateAround(player.position, player.up, gesture.deltaPosition.x * limitSpeed);
        transform.RotateAround(player.position, transform.right, -gesture.deltaPosition.y * limitSpeed);
       
        float x = transform.eulerAngles.x;
        float y = transform.localEulerAngles.y;
        if (x < 0 || x > 80)
        {
            transform.position = originalPos;
            transform.rotation = originalRotation;
        }
    }
    }
