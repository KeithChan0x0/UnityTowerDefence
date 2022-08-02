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
	protected float moveSpeed;
	// ターゲットの座標
	protected Vector3 targetPos;
	// 実際のターゲットとの距離（これ以上は近づかないって範囲）
	protected float targetNearLength = 0.0f;


	// ターゲットのセット
	protected void TargetSet()
	{
		targetPos = GameObject.FindWithTag("Wall").transform.position;
	}

	// 移動処理
	protected void CharacterMove()
	{
		float length = Vector3.Distance(targetPos, transform.position);
		// 一定距離より遠かったら移動してくる
		if (length < targetNearLength) return;
		Vector3 dir = targetPos - transform.position;
		dir = dir.normalized;
		transform.position += (dir * moveSpeed * Time.deltaTime);
	}

}
