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
		targetPos.x = GameObject.FindWithTag("Wall").transform.position.x;
	}

	// 移動処理
	protected void CharacterMove()
	{
		float length = 0.0f;
#if false
		length = Vector3.Distance(targetPos, transform.position);
#else
		Vector3 target = targetPos;
		target.y = 0.0f;
		target.z = 0.0f;
		Vector3 pos = transform.position;
		pos.y = 0.0f;
		pos.z = 0.0f;
		length = Vector3.Distance(target, pos);
#endif
		// 一定距離より遠かったら移動してくる
		if (length < targetNearLength) return;
#if false
		Vector3 dir = targetPos - transform.position;
		dir = dir.normalized;
		transform.position += (dir * moveSpeed * Time.deltaTime);
#else
		Vector3 dir = targetPos - transform.position;
		dir.y = 0.0f;
		dir.z = 0.0f;
		dir.x = dir.normalized.x;
		transform.position += (dir * moveSpeed * Time.deltaTime);
#endif
	}

}
