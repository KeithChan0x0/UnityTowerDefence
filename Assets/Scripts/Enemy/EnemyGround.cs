using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGround : EnemyBase
{
	public NavMeshAgent navMeshAgent;
	public LayerMask whatIsGround, whatIsPlayer;
	public GameObject player;

	public float AttackMargin = 2.1f; //攻撃が始まるプレイヤーとの距離
	float Margin; //プレイヤーとの距離

	// Start is called before the first frame update
	public virtual void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		navMeshAgent = GetComponent<NavMeshAgent>();
	}

	// Update is called once per frame
	public virtual void Update()
	{
		m_attackCool -= Time.deltaTime;
		//ナビメッシュが有効な時のみ実施
		if (navMeshAgent.enabled)
		{
			//プレイヤーとの距離を求める
			Margin = Vector3.Distance(transform.position, player.transform.position);
			Debug.Log(Margin);
			if (Margin > AttackMargin)
			{
				navMeshAgent.SetDestination(player.transform.position);
				//ナビメッシュの速度を歩く真偽値に転用する
				//myAnim.SetBool("Walk", navMeshAgent.velocity.magnitude > 0.1f);
			}
			else
			{
				Attack();
			}
		}
	}

	protected virtual void Attack()
	{

	}
}
