//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class CAR : MonoBehaviour
{
    // [Header("字串")]  標題黨
    // [Tooltip("字串")] 註解(游標接觸觸發)
    // [Range( n , m )] 數值範圍 n m 為最小值及最大值(注意類別: float double)
    [Header("車輛參數設定")]
    public int size = 3;
    [Tooltip("單位:頓")]
    [Range( 0F , 1.5F )]
    public float weight = 0.5F;
    public string colar = "white";
    public string logo = "BMW";
    public bool has_window = false;
    //===============================顏色設定
    public Color colorA = Color.blue;
    //自訂義(新增)要加 NEW
    public Color colorB = new Color(); //新增自定義顏色
    public Color colorC = new Color( 0.1f , 0.3f , 0.5f ,0.7f ); //新增自定義顏色( R, G, B, A) ,RGB值及A值(透明度)皆界在0~1
    //===============================向量設定 VECTOR 有2-4維
    public Vector2 V2A = Vector2.one; //vector2 是2維
    public Vector2 V2B = Vector2.zero;
    public Vector2 V2C = new Vector2(0.5f,0.5f);
    //====音效片段
    public AudioClip AudioA;
    //====自訂義圖片(需從檔案輸入)可改變2D圖片及介面圖片
    public Sprite SpriteA;
    //====新增遊戲元件 物件名稱左側有立方
    public GameObject GO_A;
    public GameObject GO_B;
    //====遊戲跟目錄屬性(可折疊 粗體字)有包含即可套入
    public Transform tra_1;
    public Camera cm_1;
    //====開始事件 資料的取得與設定
    private void Start()
    {
        print("Hello Kid");
        print("品牌為"+logo);
    }

}
