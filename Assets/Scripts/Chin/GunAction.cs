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


	bool allowInvoke = true;
	private void Awake()
	{
		bulletsLeft = magazineSize;
		readyToShoot = true;
	}

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		HandleInput();
	}

	void HandleInput()
	{
		// ボタンを押している状態なら、連射する
		if (allowButtonHold)
		{
			shooting = Input.GetKey(KeyCode.Mouse0);
		}
		else
		{
			shooting = Input.GetKeyDown(KeyCode.Mouse0);
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

	void Shoot()
	{
		// 当たりレイを作る
		Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //　今カメラ画面の中心から
		RaycastHit hit;

		// ヒット座標を取ってくる
		Vector3 targetPoint;
		if (Physics.Raycast(ray, out hit))
			targetPoint = hit.point;
		else
			targetPoint = ray.GetPoint(75); //Just a point far away from the player

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

		//Instantiate muzzle flash, if you have one
		if (muzzleFlash != null)
			Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

		bulletsLeft--;
		bulletsShot++;

		// Invoke resetShot function(if not already invoked), with your timeBetweenShooting
		if (allowInvoke)
		{
			Invoke("ResetShot", timeBetweenShooting);
			allowInvoke = false;

			//Add recoil to player (should only be called once)
			//playerRb.AddForce(-directionWithSpread.normalized * recoilForce, ForceMode.Impulse);
		}

		//if more than one bulletsPerTap make sure to repeat shoot function
		if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
			Invoke("Shoot", timeBetweenShots);


	}

	private void ResetShot()
	{
		//Allow shooting and invoking again
		readyToShoot = true;
		allowInvoke = true;
	}

	private void Reload()
	{
		reloading = true;
		Invoke("ReloadFinished", reloadTime); //Invoke ReloadFinished function with your reloadTime as delay
	}
	private void ReloadFinished()
	{
		//Fill magazine
		bulletsLeft = magazineSize;
		reloading = false;
	}
}
