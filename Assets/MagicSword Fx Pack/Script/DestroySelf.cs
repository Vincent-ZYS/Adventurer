using UnityEngine;
using System.Collections;

/// <summary>
/// Destroy自己 不是真的Destroy掉
/// 只是把自己藏起來
/// </summary>
public class DestroySelf : MonoBehaviour {



	
	public float timeout = 0.5f;
	private float time;
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if(time > timeout){
			time = 0f;
            Destroy(gameObject);
            try {
                Scavenger.instance.enemyList.Clear();
            }
            catch { }
		}
	}
	public void SetTimeOut(float t){
		timeout = t;
	}
	public void ResetTime(){
		time = 0f;
	}

}
