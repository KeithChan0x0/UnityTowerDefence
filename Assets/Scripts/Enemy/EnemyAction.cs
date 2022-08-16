using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAction : MonoBehaviour
{
	public NavMeshAgent navMeshAgent;
	public GameObject target;
	public LayerMask whatIsGround, whatIsPlayer;

	public float health;

	// Start is called before the first frame update
	void Start()
    {
		target = GameObject.FindGameObjectWithTag("Player");
		navMeshAgent = GetComponent<NavMeshAgent>();
	}

    // Update is called once per frame
    void Update()
    {
		HandleChase();
	}

	private void HandleChase()
	{
		navMeshAgent.SetDestination(target.transform.position);
	}
}
