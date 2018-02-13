using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
    private Transform player;
    [Header("Offset")]
    public float x=0.5f;
    public float y=3.1f;
    public float z=-2.42f;
    private float speed = 2f;
  void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
    }
    void Update()
    {
        Vector3 targetPos = player.position + new Vector3(x, y, z);
        transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
        Quaternion targetRot = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, speed * Time.deltaTime);
    }
}
