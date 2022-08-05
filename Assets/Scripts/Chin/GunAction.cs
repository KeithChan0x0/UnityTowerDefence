using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAction : MonoBehaviour
{
	//----------------------
	//! バレット設定
	//----------------------
	public GameObject bullet;
	public float shootForce;
	public float upwardForce;

	//----------------------
	//! 銃設定
	//----------------------
	public float timeBetweenShooting;
	public float spread;
	public float reloadTime;
	public float timeBetweenShots;
	public int magazineSize;
	public int bulletsPerTap;
	public bool allowButtonHold = true;

	private int bulletsLeft;
	private int bulletsShot;

	//リコイル
	public Rigidbody playerRb;
	public float recoilForce;

	// status
	private bool shooting, readyToShoot, reloading;

	//---------------------
	// オブジェクト参照
	//---------------------
	public Camera fpsCam;
	public Transform attackPoint;

	// エフェクト
	public GameObject muzzleFlash;

	public bool allowInvoke = true;

	private void Awake()
	{
		bulletsLeft = magazineSize;
		readyToShoot = true;
	}

	// Start is called before the first frame update
	void Start()
	{
#if UNITY_EDITOR
		transform.localPosition = new Vector3(0.0f, -0.5f, 0.0f);
#endif
	}

	// Update is called once per frame
	void Update()
	{
		//HandleInput();
	}

	public void HandleInput()
	{
		// ボタンを押している状態なら、連射する
		if (allowButtonHold)
		{
#if UNITY_EDITOR
			shooting = Input.GetKey(KeyCode.Mouse0);
#else
			shooting = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0;
#endif
		}
		else
		{
#if UNITY_EDITOR
			shooting = Input.GetKeyDown(KeyCode.Mouse0);
#else
			shooting = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0;
#endif
		}
		//　弾をリロード 
		if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
			Reload();

		// 自動的にリロードする、連射の状態で
		if (readyToShoot && shooting && !reloading && bulletsLeft <= 0)
			Reload();

		// 射撃
		if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
		{
			//Set bullets shot to 0
			bulletsShot = 0;

			Shoot();
		}
	}

	public void Shoot()
	{
		readyToShoot = false;

		// 当たりレイを作る
		Ray ray = new Ray(attackPoint.position, -attackPoint.up);
		//Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //　今カメラ画面の中心から
		RaycastHit hit;

		// ヒット座標を取ってくる
		Vector3 targetPoint;
		if (Physics.Raycast(ray, out hit))
			targetPoint = hit.point;
		else
			targetPoint = ray.GetPoint(75); // 適当に前向きの座標

		// 攻撃座標から当たった座標のベクトル
		Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

		// 反動の計算
		float x = Random.Range(-spread, spread);
		float y = Random.Range(-spread, spread);
		Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

		// 弾を生成する
		GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); //store instantiated bullet in currentBullet
																								   //Rotate bullet to shoot direction
		currentBullet.transform.forward = directionWithSpread.normalized;

		// 弾の力をあげる
		currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
		currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

		// 弾を発射のエフェクト
		if (muzzleFlash != null)
			Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

		bulletsLeft--;
		bulletsShot++;

		// Invoke resetShot function(if not already invoked), with your timeBetweenShooting
		if (allowInvoke)
		{
			Invoke("ResetShot", timeBetweenShooting);
			allowInvoke = false;

			// 反動
			if (playerRb)
			{
				playerRb.AddForce(-directionWithSpread.normalized * recoilForce, ForceMode.Impulse);
			}
		}

		//if more than one bulletsPerTap make sure to repeat shoot function
		if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
			Invoke("Shoot", timeBetweenShots);


	}

	private void ResetShot()
	{
		// Allow shooting and invoking again
		readyToShoot = true;
		allowInvoke = true;
	}

	private void Reload()
	{
		reloading = true;
		Invoke("ReloadFinished", reloadTime); // Reload時間
	}

	private void ReloadFinished()
	{
		// 弾リセット
		bulletsLeft = magazineSize;
		reloading = false;
	}
}
