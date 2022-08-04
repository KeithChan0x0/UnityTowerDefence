using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEyeBall : EnemySky
{
	// ターゲットとの距離の基準
	const float TARGET_LENGTH_STANDARD = 10.0f;
	// ターゲットとの距離で前後する範囲
	const float TARGET_LENGTH_AROUND = 3.0f;
	// アニメーター
	Animator animator;

	// Start is called before the first frame update
	void Start()
    {
		TargetSet(TARGET_LENGTH_STANDARD, TARGET_LENGTH_AROUND);
		animator = GetComponentInChildren<Animator>();
	}


	protected override void Move()
	{
		Vector3 dir = MoveValueCalc();
		animator.SetFloat("Move", Vector3.Distance(dir, Vector3.zero));
		base.Move();
	}

	// ダメージ処理
	public override void Damage(int point_)
	{
		base.Damage(point_);
		if (hp > 0)
		{
			animator.SetBool( "Damage", true);
		}
		else
		{
			animator.SetBool( "Death", true);
		}
	}

	// Update is called once per frame
	public override void Update()
    {
		base.Update();
	}
}
