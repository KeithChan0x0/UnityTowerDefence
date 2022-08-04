using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : EnemyGround
{
	// ターゲットとの距離の基準
	const float TARGET_LENGTH_STANDARD = 1.0f;
	// ターゲットとの距離で前後する範囲
	const float TARGET_LENGTH_AROUND = 0.0f;

	// Start is called before the first frame update
	void Start()
    {
		TargetSet(TARGET_LENGTH_STANDARD, TARGET_LENGTH_AROUND);

	}

	// Update is called once per frame
	public override void Update()
    {
        
    }
}
