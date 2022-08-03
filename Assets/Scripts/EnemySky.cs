using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySky : EnemyBase
{
	// ターゲットとの距離の基準
	const float TARGET_LENGTH_STANDARD = 20.0f;
	// ターゲットとの距離で前後する範囲
	const float TARGET_LENGTH_AROUND = 5.0f;

	// Start is called before the first frame update
	void Start()
    {
		TargetSet();
		// ある程度基準距離からずらす
        targetNearLength = Random.Range(TARGET_LENGTH_STANDARD - TARGET_LENGTH_AROUND,
										TARGET_LENGTH_STANDARD + TARGET_LENGTH_AROUND);
    }

	void Attack()
	{

	}

    // Update is called once per frame
    void Update()
    {
        CharacterMove();
		// 攻撃処理

    }
}
