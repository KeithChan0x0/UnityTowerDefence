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
	// 攻撃のクールタイム（最大）
	float ATTACK_COOL_MAX = 3.0f;
	AudioSource myAudio; //自身の音源
	public GameObject ballPrefab; //球プレハブ
	//public AudioClip SeBall; //球射出音
	public float BallSpeed = 3.0f; //球の速度
	public float BallLife = 2.0f; //球の寿命

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
			animator.SetTrigger( "Damage");
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

	// 攻撃処理
	private void Attack()
	{
		// クールタイムが終わっていなければ何もしない
		if (m_attackCool <= 0.0f) return;
		// 攻撃のクールタイムが終わったら攻撃のアニメーション
		animator.SetTrigger("Attack");
		m_attackCool = ATTACK_COOL_MAX;
		GameObject ball = Instantiate(ballPrefab); //レーザー生成
		ball.transform.position = transform.position; //位置を補正
		ball.GetComponent<Rigidbody>().velocity = Vector3.forward * BallSpeed; //速度を与える
		Destroy(ball, BallLife); //寿命を与える
		//myAudio.PlayOneShot(SeBall); //レーザー射出音鳴動
	}
}
