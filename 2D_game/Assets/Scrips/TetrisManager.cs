using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class TetrisManager : MonoBehaviour
{
    [Header("掉落時間")]
    [Range(0.1f, 3f)]
    public float Drop_Speed = 0.8F;
    [Header("目前分數")]
    public float Your_score = 0f;// 目前分數
    [Header("最高分數")]
    public float highest_score;// 最高分數      
    [Header("等級")]
    public float Level = 1;// 等級        
    [Header("結束畫面")]
    public GameObject result_scene;// 結束畫面      
    [Header("音效:掉落、移動與旋轉、消除、結束")]
    public AudioClip drop_sound;//方塊掉落音效    
    public AudioClip rotate_sound;//方塊移動與旋轉音效 
    public AudioClip delete_sound;//方塊消除音效    
    public AudioClip End_sound;//遊戲結束音效    
    [Header("下一個方塊")]
    public Transform nextteris;
    [Header("產生方塊的目標區域")]
    public Transform to_canvas;
    public int nexttarget;
    private RectTransform current_falling;
    private float timer;
    //12/29新增 為了方塊
    private int rec_now;
    private bool if_fast_down;
    private Vector2[] pos_start =
    {
    new Vector2(0,330),
    new Vector2(0,330),
    new Vector2(15,315),
    new Vector2(15,315),
    new Vector2(0,315),
    new Vector2(-15,315)
    };
    private void Start()
    {

        generate();
        scoretxt.text = "你的分數:0";
        leveltxt.text = "等級:1";
    }
    private void Update()
    {

        Fast_down();
        if (current_falling)
        {
            Teris teris = current_falling.GetComponent<Teris>();
            int z = (int)teris.transform.eulerAngles.z;
            counting();
            if (timer >= Drop_Speed)
            {
                timer = 0;
                current_falling.anchoredPosition -= new Vector2(0f, 30f);
            }
            #region 按鍵左右選轉加速落下
            //按鍵往左往右
            if (current_falling.anchoredPosition.x < 195 && rec_now != 5)
            {
                if (!teris.wall_right && !teris.small_right)
                {
                    if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        current_falling.anchoredPosition += new Vector2(30f, 0f);
                    }
                }
            }
            if (current_falling.anchoredPosition.x > -195 && rec_now != 5)
            {
                if (!teris.wall_left && !teris.small_left)
                {
                    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        current_falling.anchoredPosition -= new Vector2(30f, 0f);
                    }
                }
            }

            #region 1字形特赦
            if (current_falling.anchoredPosition.x < 200 && rec_now == 5 && (z == 0 || z == 180))
            {

                if (!teris.wall_right && !teris.small_right)
                {
                    if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        current_falling.anchoredPosition += new Vector2(30f, 0f);
                    }
                }
            }
            if (current_falling.anchoredPosition.x > -200 && rec_now == 5 && (z == 0 || z == 180))
            {
                if (!teris.wall_left && !teris.small_left)
                {
                    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        current_falling.anchoredPosition -= new Vector2(30f, 0f);
                    }
                }
            }
            if (current_falling.anchoredPosition.x < 170 && rec_now == 5 && (z == 90 || z == 270))
            {

                if (!teris.wall_right && !teris.small_right)
                {
                    if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        current_falling.anchoredPosition += new Vector2(30f, 0f);
                    }
                }
            }
            if (current_falling.anchoredPosition.x > -155 && rec_now == 5 && (z == 90 || z == 270))
            {
                if (!teris.wall_left && !teris.small_left)
                {
                    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        current_falling.anchoredPosition -= new Vector2(30f, 0f);
                    }
                }
            }
            #endregion
            //按鍵旋轉
            if (teris.can_rotate)
            {
                if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.UpArrow))
                {

                    current_falling.Rotate(0, 0, -90);
                    if (rec_now != 4)
                    {
                        if (z == 0 || z == 180)
                        {
                            current_falling.anchoredPosition -= new Vector2(teris.offsetX, teris.offsetY);
                        }
                        if (z == 90 || z == 270)
                        {
                            current_falling.anchoredPosition += new Vector2(teris.offsetX, teris.offsetY);
                        }
                    }
                }
            }
            //按鍵加速掉落否則停止
            if (!if_fast_down)
            {


                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    Drop_Speed = 0.2f;
                }
                else
                {
                    Drop_Speed = 0.8f;
                }
            }
            #endregion
            #region 停止判定
            //print(teris.name+teris.wall_down);
            if (teris.wall_down || teris.small_bottom)
            {
                Setground();
                StartCoroutine(Check_tetris());
                shaker();
                StartGAME();
            }
            #endregion
        }


    }
    private void Setground()
    {
        int count = current_falling.childCount;
        for (int i = 0; i < count; i++)
        {
            current_falling.GetChild(i).name = "方塊";
            current_falling.GetChild(i).gameObject.layer = 10;

        }

    }
    public void StartGAME()
    {
        if_fast_down = false;//阻止繼續快速落下
        //生成generate()裡所指定之方塊樣式
        //Instantiate(產生的子物件,子物件的目標區域);
        GameObject teris = nextteris.GetChild(nexttarget).gameObject;
        rec_now = nexttarget;
        GameObject current = Instantiate(teris, to_canvas);
        current.GetComponent<RectTransform>().anchoredPosition = pos_start[(nexttarget)];
        teris.SetActive(false);
        generate();
        current_falling = current.GetComponent<RectTransform>();
    }
    public void Fast_down()
    {
        if (current_falling && !if_fast_down)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if_fast_down = true;
                Drop_Speed = 0.02f;
                StartCoroutine(shaker());
            }
        }

    }

    [Header("分數判定區域")]
    public Transform to_score_area;
    public RectTransform[] rectsmall;
    public bool[]destroyrow = new bool [19];
    public float[] downheight;
    public IEnumerator Check_tetris()
    {
        int count = current_falling.childCount;
        for (int i = 0; i < count; i++)
        {
            current_falling.GetChild(0).SetParent(to_score_area);
        }
        Destroy(current_falling.gameObject);
        rectsmall = new RectTransform[to_score_area.childCount];
        for (int i = 0; i < to_score_area.childCount; i++)
        {
            rectsmall[i] = to_score_area.GetChild(i).GetComponent<RectTransform>();
        }

        int row = 19;
        for (int i = 0; i < row; i++)
        {
            float bottom = -270f;
            float step = 30f;
            var small = rectsmall.Where(x => x.anchoredPosition.y >= bottom+step*i - 5 && x.anchoredPosition.y <= bottom + step * i + 5);
            if (small.ToArray().Length==16)
            {
                yield return StartCoroutine(shine(small.ToArray()));
                destroyrow[i] = true;
            }
        }
        downheight = new float[to_score_area.childCount];
        for (int i = 0; i < downheight.Length; i++)
        {
            downheight[i] = 0;
        }
        //計算掉落高度
        for (int i = 0; i < destroyrow.Length; i++)
        {
            if (!destroyrow[i]) continue;
            for (int j = 0; j < rectsmall.Length; j++)
            {
                if (rectsmall[j].anchoredPosition.y > -270 +30*i-5)
                {
                    downheight[j] -=30;
                }
            }
            destroyrow[i] = false;
        }
        for (int i = 0; i < rectsmall.Length; i++)
        {
            rectsmall[i].anchoredPosition += Vector2.up*downheight[i];
        }
    }
    public IEnumerator shine(RectTransform[] smalls)
    {
        for (int i = 0; i < 16; i++)
        {
            smalls[i].GetComponent<Image>().enabled = false;       
        }
        yield return new WaitForSeconds(0.05f);
        for (int i = 0; i < 16; i++)
        {
            smalls[i].GetComponent<Image>().enabled = true;
        }
        yield return new WaitForSeconds(0.05f);
        for (int i = 0; i < 16; i++)
        {
            smalls[i].GetComponent<Image>().enabled = false;
        }
        yield return new WaitForSeconds(0.05f);
        for (int i = 0; i < 16; i++)
        {
            smalls[i].GetComponent<Image>().enabled = true;
        }
        yield return new WaitForSeconds(0.05f);

        yield return new WaitForSeconds(0.05f);
        for (int i = 0; i < 16; i++)
        {
            Destroy(smalls[i].gameObject);
            
        }
        add_score(100f);
        //避免消除完上層方塊未掉落產生錯誤
        yield return new WaitForSeconds(0.05f);
        rectsmall = new RectTransform[to_score_area.childCount];
        for (int i = 0; i < to_score_area.childCount; i++)
        {
            rectsmall[i] = to_score_area.GetChild(i).GetComponent<RectTransform>();
        }
    }
    #region 方法
    /// <summary>
    /// 生成俄羅斯方塊
    /// </summary>
    private void generate()
    {
       nexttarget = Random.Range(0, 6);
       nexttarget = 5;//測試用
       //nexttarget = 5;//測試用
        nextteris.GetChild(nexttarget).gameObject.SetActive(true); 
    }
    /// <summary>
    /// 計時器
    /// </summary>
    private void counting()
    {
        timer += Time.deltaTime;
    }
    [Header("現在分數")]
    public Text scoretxt;
    [Header("你的等級")]
    public Text leveltxt;
    /// <summary>
    /// 添加分數
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public void add_score(float x)
    {
        Your_score += x;
        scoretxt.text = "你的分數" + Your_score;
        Level = Your_score / 100f + 1;
        leveltxt.text = "等級:" + Level;
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

    private IEnumerator shaker()
    {
        RectTransform rect = to_canvas.GetComponent<RectTransform>();
        rect.anchoredPosition += Vector2.up * 30;
        yield return new WaitForSeconds(0.05f);
        rect.anchoredPosition = Vector2.zero;
        yield return new WaitForSeconds(0.05f);
        rect.anchoredPosition += Vector2.up * 20;
        yield return new WaitForSeconds(0.5f);
        rect.anchoredPosition = Vector2.zero;
    }

    #endregion
}
