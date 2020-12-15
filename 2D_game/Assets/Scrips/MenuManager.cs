//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    #region 方法
    /// <summary>
    /// 開始遊戲
    /// </summary>
    public void statgame()
    {
        SceneManager.LoadScene("遊戲畫面");
    }
    /// <summary>
    /// 離開遊戲
    /// </summary>
    public void exitgame()
    {
        Application.Quit();
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        
    }
}
