//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class TetrisManager : MonoBehaviour
{
   [Header("掉落時間")] 
   [Range(0.1f,3f)]
   public float Drop_Speed=1.5F;
   [Header("目前分數")]
   public float Your_score;// 目前分數
   [Header("最高分數")]
   public float highest_score;// 最高分數      
   [Header("等級")]
   public int Level =1;// 等級        
   [Header("結束畫面")]
   public GameObject result_scene;// 結束畫面      
   [Header("音效:掉落、移動與旋轉、消除、結束")]
   public AudioClip drop_sound;//方塊掉落音效    
   public AudioClip rotate_sound;//方塊移動與旋轉音效 
   public AudioClip delete_sound;//方塊消除音效    
   public AudioClip End_sound;//遊戲結束音效    
    #region 方法
    /// <summary>
    /// 生成俄羅斯方塊
    /// </summary>
    private void generate()
    {
    }
    /// <summary>
    /// 添加分數
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public int add_score(int x)
    {
        return x;
    }
    /// <summary>
    /// 遊戲時間
    /// </summary>
    private void time_pass()
    {
    }
    /// <summary>
    /// 遊戲結束
    /// </summary>
    private void end_of_game()
    {
    }
    /// <summary>
    /// 重新遊戲
    /// </summary>
    public void restarted()
    {
    }
    /// <summary>
    /// 離開遊戲
    /// </summary>
    public void exit_game()
    {
    }


    #endregion
}
