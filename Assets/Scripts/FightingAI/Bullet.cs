using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 10;
    public float attack = 100;
    void Start()
    {
        Destroy(this.gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == Tags.soulBoss || col.tag == Tags.soulMonster)
        {
            col.GetComponent<ATKAndDamage>().TakeDamage(attack);
        }
    }
}
