using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySky : EnemyBase
{
	// Start is called before the first frame update
	void Start()
    {
	}

	// Update is called once per frame
	public virtual void Update()
    {
		Move();
		float length = 0.0f;
		Vector3 target = targetPos;
		target.y = 0.0f;
		target.z = 0.0f;
		Vector3 pos = transform.position;
		pos.y = 0.0f;
		pos.z = 0.0f;
		length = Vector3.Distance(target, pos);
		//　空中は一定量近づいたら壁に迫ってくる
		if (length < targetNearLength && length > 1.0f)
		{
			target.y = 3.0f;
			Vector3 dir = target - transform.position;
			dir = dir.normalized;
			dir.z = 0.0f;
			transform.position += (dir * moveSpeed * Time.deltaTime);
		}
    }
}
