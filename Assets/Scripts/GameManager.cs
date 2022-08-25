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

	MODE GameMode; //ゲームの状態

	//-------------------------------------------
	// 音関係
	//-------------------------------------------
	public AudioClip Music_Play;		// プレイ中のBGM
	public AudioClip Music_Title;		// タイトル画面のBGM
	public AudioClip SE_sceneChange;	// シーンを移行した際のSE
	AudioSource myAudio;				// 自身の音源

	//-------------------------------------------
	// システム関係
	//-------------------------------------------
	public Text txtMessage;         // システムメッセージ
	Image imgFill;					//自身のフィル画像
	Image imgButton;				//自身の配下のボタン画像
	GameObject Manager;				//マネージャー
	float Elapsed = 0.0f;			//経過時間
	bool Selected = false;			//選択完了の真偽値
	public Sprite imgUnlock;		//アンロック画像

	//-------------------------------------------
	// スコア関係
	//-------------------------------------------
	public Image imgRank;					// ランキング画面で使用する画像（いらなかったら撤去）
	public Text[] txtRank = new Text[5];	// ランキング
	public Text txtScore;                   // 現在のスコア

	//-------------------------------------------
	// 時間関係
	//-------------------------------------------
	public Text txtTime;	// テキスト
	public int maxTime;		// 最大時間
	private int time;       // 現在の時間
	
	//-------------------------------------------


	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
