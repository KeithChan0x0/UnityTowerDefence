using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
	// 攻撃力
	public int power;
	// 体力
	public int hp;
	// 移動速度
	public float moveSpeed;
	// ターゲットの座標
	protected Vector3 targetPos;
	// 実際のターゲットとの距離（これ以上は近づかないって範囲）
	protected float targetNearLength = 0.0f;


	// ターゲットのセット
	protected void TargetSet(float lemgthStandard_, float lengthAround_)
	{
		// ターゲットの座標とどれだけターゲットに近づくかをもらう
		// 座標の誤差は狙うものの大きさできまるので引数以外から
		targetPos = GameObject.FindWithTag("Wall").transform.position;
		// 距離はそれぞれ基準とどれだけ前後するかをもらったものを使って計算
		targetNearLength = Random.Range(lemgthStandard_ - lengthAround_,
										lemgthStandard_ + lengthAround_);
	}

	// 接触処理
	void OnTriggerEnter(Collider other)
	{
		// ダメージ値の取得（何が入ってきたかなどを確かめなならんけどタグすらないのでいったん仮）
		// タグ、ゲッターができたら少し調整して終了
		if (other.gameObject.tag != "Arrow") return;
		int damagePoint = 50;
		Damage(damagePoint);
	}

	// ダメージ処理
	void Damage(int point_)
    {
		hp -= point_;
		// あとはダメージのアニメーションのセットをアニメーターにダメージとかを持たせるのでそれ依存で
    }

	// 移動処理
	protected virtual void Move()
	{
		// Y座標抜きの距離を確かめる
		Vector3 target = targetPos;
		target.y = 0.0f;
		Vector3 pos = transform.position;
		pos.y = 0.0f;
		float length = Vector3.Distance(target, pos);
		// 距離が一定距離より遠かったら移動してくる
		if (length < targetNearLength) return;
		// 移動に使う方向もY座標抜き
		Vector3 dir = target - pos;
		dir = dir.normalized;
		transform.position += (dir * moveSpeed * Time.deltaTime);
		// 近接敵の場合は攻撃距離までの移動をしてやらなきゃならんけどその辺は近接タイプの移動を作ってやる
		// これはもう面倒なので派生先に任せる
	}

}
