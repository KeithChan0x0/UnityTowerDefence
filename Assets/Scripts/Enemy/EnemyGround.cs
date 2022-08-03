using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGround : EnemyBase
{
	// ターゲットとの距離の基準
	const float TARGET_LENGTH_STANDARD = 1.0f;
	// ターゲットとの距離で前後する範囲
	const float TARGET_LENGTH_AROUND = 5.0f;

	// Start is called before the first frame update
	void Start()
    {
		TargetSet();
		targetNearLength = TARGET_LENGTH_STANDARD;
	}

	
	// Update is called once per frame
	void Update()
    {
		Move();
		// 攻撃処理
	}
}
