using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAction : MonoBehaviour
{
	public string enemyTag = "Enemy";
	public string environmentTag = "Environment";
	public int damage;
	public GameObject sparkVFX;

	private void Start()
	{
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag(enemyTag))
		{
			HandleCollisionWithEnemy(collision.gameObject);
		}
		else if (collision.gameObject.CompareTag(environmentTag))
		{
			HandleCollisionWithEnvironment(collision.gameObject);
		}
		InstantiateSparkVFX();
		Destroy(this);
	}

	void HandleCollisionWithEnemy(GameObject enemy)
	{
		EnemyBase enemyClass = enemy.GetComponent<EnemyBase>();
		if (enemyClass)
		{
			enemyClass.Damage(damage);
			// TODO: エフェクト
			// TODO: SE
			Debug.Log("OK");
		}
		else
		{
			Debug.LogWarning("EnemyBaseクラス見つからない");
		}
		gameObject.SetActive(false);
	}

	void HandleCollisionWithEnvironment(GameObject environemnt)
	{
		gameObject.SetActive(false);
	}

	void InstantiateSparkVFX()
	{
		if (!sparkVFX) return;

		GameObject newSparkVFX = Instantiate(sparkVFX, transform.position, Quaternion.identity);
		Destroy(newSparkVFX, 5.0f);
	}
}
