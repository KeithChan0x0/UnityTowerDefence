using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStoneMonster : EnemySky
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

	// ダメージ処理
	public override void Damage(int point_)
	{
		base.Damage(point_);
		// アニメーションの設定（いったん放置）
	}

	// Update is called once per frame
	public override void Update()
    {
		base.Update();
	}
}
