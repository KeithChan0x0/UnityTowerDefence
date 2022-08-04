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
		// 狙うもののタグか座標渡して
		// 複数あるなら編集しといて
		GameObject Wall = GameObject.FindWithTag("Wall");
		if (Wall)
		{
			targetPos = GameObject.FindWithTag("Wall").transform.position;
			// 距離はそれぞれ基準とどれだけ前後するかをもらったものを使って計算
			targetNearLength = Random.Range(lemgthStandard_ - lengthAround_,
											lemgthStandard_ + lengthAround_);
		}
	}

	// ダメージ処理
	public virtual void Damage(int point_)
	{
		hp -= point_;
	}

	// 移動量の計算
	protected Vector3 MoveValueCalc()
	{
		// Y座標抜きの距離を確かめる
		Vector3 target = targetPos;
		target.y = 0.0f;
		Vector3 pos = transform.position;
		pos.y = 0.0f;
		// 移動に使う方向もY座標抜き
		Vector3 dir = target - pos;
		return dir;
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
		Vector3 dir = MoveValueCalc();
		dir = dir.normalized;
		transform.position += (dir * moveSpeed * Time.deltaTime);
	}

}
