using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //uGUIの利用に必要

public class ButtonAction : MonoBehaviour
{

	public Image imgButton;         //ボタン画像
	GameObject Manager;             //マネージャー
	float Elapsed = 0.0f;           //経過時間
	bool Selected = false;          //選択完了の真偽値


	// Start is called before the first frame update
	void Start()
	{
		Manager = GameObject.FindGameObjectWithTag("GameController");
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Pointer")
		{
			if (Input.GetMouseButtonDown(0) || OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
			{
				Selected = true;
				//マネージャーに選択完了を送信
				Manager.SendMessage("Selected",
				SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}
