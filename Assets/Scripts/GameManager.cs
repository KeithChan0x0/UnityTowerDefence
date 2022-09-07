using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //uGUIの利用に必要

public class GameManager : MonoBehaviour
{
	// シーン関係とUIの作成
	public enum MODE
	{
		TITLE,		// タイトル
		PLAYING,	// プレイ中
		RESULT,		// リザルト
		RANKING,	// ランキング
	}

	//-------------------------------------------
	// システム関係
	//-------------------------------------------
	public Text txtMessage;     // システムメッセージ
	MODE GameMode;				//ゲームの状態

	//-------------------------------------------
	// 音関係
	//-------------------------------------------
	public AudioClip Music_Play;		// プレイ中のBGM
	public AudioClip Music_Title;		// タイトル画面のBGM
	public AudioClip SE_sceneChange;	// シーンを移行した際のSE
	AudioSource myAudio;				// 自身の音源

	//-------------------------------------------
	// スコア関係
	//-------------------------------------------
	public Image imgRank;					// ランキング画面で使用する画像（いらなかったら撤去）
	public Text[] txtRank = new Text[5];	// ランキング
	public Text txtScore;                   // 現在のスコア

	//-------------------------------------------
	// 時間関係
	//-------------------------------------------
	public Text txtTime;		// テキスト
	public float maxTime;		// 最大時間
	private float nowTime;      // 現在の時間
	
	//-------------------------------------------


	// Start is called before the first frame update
	void Start()
    {
		// ゲームモードの設定（タイトル）
        GameMode = MODE.TITLE;
		nowTime = maxTime;
		// 音関係の初期化
	}

	//　ボタンを押した際の処理
	void Selected()
	{
		switch (GameMode)
		{
			case MODE.TITLE:
				GameMode = MODE.PLAYING;
				nowTime = maxTime;
				break;
			case MODE.RESULT:
				GameMode = MODE.RANKING;
				break;
			case MODE.RANKING:
				GameMode = MODE.TITLE;
				break;
		}
	}

	// Update is called once per frame
	void Update()
    {
		switch (GameMode)
		{
			// タイトル
			case MODE.TITLE:
				
				break;

			// ゲームプレイ中
			case MODE.PLAYING:
				nowTime -= Time.deltaTime;

				// 時間が無くなったらリザルト画面へ
                if (nowTime < 0.0)
                {
					GameMode = MODE.RESULT;
                }
				// 体力がなくなったらリザルトへ移行

				break;

			// リザルト
			case MODE.RESULT:
				
				break;

			// ランキング
			case MODE.RANKING:
				
				break;
		}
	}
}
