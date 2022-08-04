using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //uGUIの利用に必要

public class GameManager : MonoBehaviour
{
	// シーン関係とUIの作成
	public enum MODE
	{
		TITLE,
		PLAYING,
		TIMEUP,
		RESULT
	}
	MODE GameMode; //ゲームの状態
	public Text txtScore;
	public Text txtTime;
	public Image imgTime;
	public Image imgRank;
	public Text txtTitle;
	public Text[] txtRank = new Text[5];
	public Image imgNaviBack;
	public Text txtNavi;
	public AudioClip Music_Play;
	public AudioClip Music_Title;
	public AudioClip SE_Hit;
	AudioSource myAudio; //自身の音源


	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
