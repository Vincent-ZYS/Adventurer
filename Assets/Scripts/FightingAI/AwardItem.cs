using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AwardType
{
    Gun,
    DualSword
}
public class AwardItem : MonoBehaviour
{
    public AwardType type;
    private Rigidbody rigidbody;
    private bool startMove = false;
    private Transform player;
    public float speed = 8;
    public AudioClip pickup;
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        this.rigidbody.velocity = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
    }
    void Update()
    {
        if (startMove)
        {
            transform.position = Vector3.Lerp(transform.position, player.position + Vector3.up, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, player.position + Vector3.up) < 0.5f)
            {
                player.GetComponent<PlayerAward>().GetAward(type);
                Destroy(this.gameObject);
                AudioSource.PlayClipAtPoint(pickup, transform.position, 0.8f);
            }
        }
    }
    void OnCollisionEnter(Collision collison)
    {
        if (collison.collider.tag == Tags.ground)
        {
            this.rigidbody.useGravity = false;
            this.rigidbody.isKinematic = true;
            SphereCollider col = this.GetComponent<SphereCollider>();//掉落到地面上即便成触发器
            col.isTrigger = true;
            col.radius = 2;
        }

    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == Tags.player)
        {
            startMove = true;
            player = col.transform;
        }
    }
}
