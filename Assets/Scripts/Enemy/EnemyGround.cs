using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGround : EnemyBase
{
	// Start is called before the first frame update
	void Start()
    {
	}


	// Update is called once per frame
	public virtual void Update()
    {
		m_attackCool -= Time.deltaTime;
		Move();
	}
}
