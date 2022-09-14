using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAction : MonoBehaviour
{
	public NavMeshAgent navMeshAgent;
	public GameObject target1;
	public GameObject target2;

	public LayerMask whatIsGround, whatIsPlayer;

	public float health;

	public bool moveToTarget2 = false;
	// Start is called before the first frame update
	public virtual void Start()
	{
		target2 = GameObject.FindGameObjectWithTag("Player");
		navMeshAgent = GetComponent<NavMeshAgent>();
	}

	// Update is called once per frame
	void Update()
	{
		//if (!moveToTarget2)
		//{
		//	float distanceToTarget = Vector3.Distance(transform.position, target1.transform.position);
		//	if (distanceToTarget < 0.1f)
		//	{
		//		moveToTarget2 = true;
		//	}
		//}

		HandleChase();
	}

	private void HandleChase()
	{
		//if (!moveToTarget2)
		//{
		//	navMeshAgent.SetDestination(target1.transform.position);

		//}
		//else
		{
			navMeshAgent.SetDestination(target2.transform.position);
		}
	}
}
