using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
	// 攻撃力
	protected int power;
	// 体力
	protected int hp;
	// 移動速度
	protected float moveSpeed;
	// ターゲットの座標
	protected Vector3 targetPos;

}
