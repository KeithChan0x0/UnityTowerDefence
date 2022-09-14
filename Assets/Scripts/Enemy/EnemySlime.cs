using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : EnemyGround
{
	// ターゲットとの距離の基準
	const float TARGET_LENGTH_STANDARD = 1.0f;
	// ターゲットとの距離で前後する範囲
	const float TARGET_LENGTH_AROUND = 0.0f;
	// アニメーター
	Animator animator;
	// 攻撃のクールタイム（最大）
	float ATTACK_COOL_MAX = 2.0f;

	// Start is called before the first frame update
	public override void Start()
	{
		base.Start();
		TargetSet(TARGET_LENGTH_STANDARD, TARGET_LENGTH_AROUND);
		animator = GetComponent<Animator>();
	}

	// ダメージ処理
	public override void Damage(int point_)
	{
		base.Damage(point_);
		if (hp > 0)
		{
			animator.SetTrigger("Damage");
		}
		else
		{
			navMeshAgent.enabled = false;
			animator.SetBool("Death", true);
		}

		Debug.Log("Called");
	}

	// Update is called once per frame
	public override void Update()
	{
		base.Update();
		//Attack();
	}

	// 攻撃処理
	protected override void Attack()
	{
		// クールタイムが終わっていなければ何もしない
		if (m_attackCool >= 0.0f) return;
		// 攻撃のクールタイムが終わったら攻撃のアニメーション
		animator.SetTrigger("Attack");
		m_attackCool = ATTACK_COOL_MAX;
	}
}
