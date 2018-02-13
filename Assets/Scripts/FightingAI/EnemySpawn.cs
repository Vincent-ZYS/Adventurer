using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
    public int manNum = 5;
    private int currentNum = 0;
    public float time = 3;
    private float timer = 0;
    public GameObject prefab;
    void Update()
    {
        if (currentNum < manNum)
        {
            timer += Time.deltaTime;
            if (timer > time)
            {
                Vector3 pos = transform.position;
                //pos.x = Random.Range(-0.5f, 0.5f);
                //pos.z = Random.Range(-0.5f, 0.5f);
                GameObject spawn = GameObject.Instantiate(prefab, pos+new Vector3(Random.Range(-5,5),0,Random.Range(-5,5)), Quaternion.identity);
                spawn.GetComponent<Enemy>().spawn = this;
                SpawnManager.instance.enemyList.Add(spawn);
                timer = 0;
                currentNum++;
            }
        }
    }
    public void MinusNumber()
    {
        this.currentNum--;
    }
}
