using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySky : EnemyBase
{
	// ターゲットとの距離の基準
	const float TARGET_LENGTH_STANDARD = 10.0f;
	// ターゲットとの距離で前後する範囲
	const float TARGET_LENGTH_AROUND = 5.0f;

	// Start is called before the first frame update
	void Start()
    {
		TargetSet();
		// ある程度基準距離からずらす
		targetNearLength = TARGET_LENGTH_STANDARD;
		moveSpeed = 7.0f;
	}

	void Attack()
	{

	}

    // Update is called once per frame
    void Update()
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
		// 攻撃処理
		Attack();
    }
}
