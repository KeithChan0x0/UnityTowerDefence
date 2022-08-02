using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGround : EnemyBase
{
	// ターゲットに近づく距離の基準
	const float TARGET_LENGTH_STANDARD = 10.0f;
	// 距離の前後する範囲
	const float TARGET_LENGTH_AROUND = 5.0f;
	// 実際のターゲットに近づく距離（計算後）
	float targetLength = 0.0f;

	// Start is called before the first frame update
	void Start()
    {
		targetLength = Random.Range(TARGET_LENGTH_STANDARD - TARGET_LENGTH_AROUND,
									TARGET_LENGTH_STANDARD + TARGET_LENGTH_AROUND);
	}

    // Update is called once per frame
    void Update()
    {
		float length = Vector3.Distance(transform.position, targetPos);
		if (length > targetLength)
		{
			Vector3 dirVec = transform.position - targetPos;
			dirVec = Vector3.Normalize(dirVec);
			transform.position += (dirVec * moveSpeed);
		}
	}
}
