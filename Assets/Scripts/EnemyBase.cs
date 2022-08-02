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
	

}
