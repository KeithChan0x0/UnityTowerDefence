using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunAction : MonoBehaviour
{
	//----------------------
	//! バレット設定
	//----------------------
	[Header("Bullet Settings")]
	public GameObject bullet;   //!< 弾
	public float shootForce;    //!< 速さ
	public float upwardForce;   //!< 投射角度

	//----------------------
	//! 銃設定
	//----------------------
	[Header("Gun Settings")]
	public float timeBetweenShooting;   //!< CD(連射)
	public float spread;                //!< 後座力
	public float reloadTime;            //!< 弾をかえる時間
	public float timeBetweenShots;      //!< CD
	public int magazineSize;            //!< MAX弾数
	public int bulletsPerTap;           //!< １発何弾を使う
	public bool allowButtonHold = true; //!< 連射できるか
	public Transform attackPoint;
	[Range(0.0f, 1.0f)]
	public float vibrationFrequency = 0.0f; // 振動の強さ
	[Range(0.0f, 1.0f)]
	public float vibrationAmplitude = 0.0f; // 振動の大きさ


	private int bulletsLeft;            //!< 弾数
	private int bulletsShot;            //!< 時間

	//! リコイル
	[Header("Recoil Settings")]
	public Rigidbody playerRb;
	public float recoilForce;

	// status
	private bool shooting, readyToShoot, reloading;

	//---------------------
	// オブジェクト参照
	//---------------------
	[Header("Object Ref")]
	public Camera fpsCam;

	// エフェクト
	[Header("Effect Settings")]
	public GameObject muzzleFlash;
	public Transform muzzleFlashPosition;

	// エフェクト
	[Header("Settings")]
	public bool isRightHandControl = true;
	public bool allowInvoke = true;

	// UI
	[Header("UI")]
	public Image imgBulletleft;

	private void Awake()
	{
		bulletsLeft = magazineSize;
		readyToShoot = true;
	}

	// Start is called before the first frame update
	void Start()
	{
		//imgBulletleft = GameObject.Find("");
	}

	// Update is called once per frame
	void Update()
	{
	}

	public void HandleInput()
	{
		// ボタンを押している状態なら、連射する
		if (allowButtonHold)
		{
#if UNITY_EDITOR
			shooting = Input.GetKey(KeyCode.Mouse0);
#else
            if (isRightHandControl)
			{
				shooting = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0;
				OVRInput.SetControllerVibration(vibrationFrequency, vibrationAmplitude, OVRInput.Controller.RTouch);
			}
			else
			{
				shooting = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0;
				OVRInput.SetControllerVibration(vibrationFrequency, vibrationAmplitude, OVRInput.Controller.LTouch);
			}
#endif
		}
		else
		{
#if UNITY_EDITOR
			shooting = Input.GetKeyDown(KeyCode.Mouse0);
#else
			if (isRightHandControl)
			{
				shooting = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0;
				OVRInput.SetControllerVibration(vibrationFrequency, vibrationAmplitude, OVRInput.Controller.RTouch);
			}
			else
			{
				shooting = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0;
				OVRInput.SetControllerVibration(vibrationFrequency, vibrationAmplitude, OVRInput.Controller.LTouch);
			}
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
		else
		{
			// 振動を停止
			if (isRightHandControl)
			{
				OVRInput.SetControllerVibration(0.0f, 0.0f, OVRInput.Controller.RTouch);
			}
			else
			{
				OVRInput.SetControllerVibration(0.0f, 0.0f, OVRInput.Controller.LTouch);
			}
		}
	}

	public void Shoot()
	{
		readyToShoot = false;

		// 当たりレイを作る
		Ray ray = new Ray(attackPoint.position, attackPoint.forward);
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
		{
			GameObject muzzleEffectInstant = Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
			Destroy(muzzleEffectInstant, 2f);
		}

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
