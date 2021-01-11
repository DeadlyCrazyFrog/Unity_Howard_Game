using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Teris : MonoBehaviour
{
    public GameObject T;
    [Header("Scale_X")]
    public float Scale_X = 0.3F;
    [Header("0度的長度")]
    public float length_0 = 75F;
    [Header("90度的長度")]
    public float length_90 = 75F;
    private float length_down;
    public float rec_length;
    [Header("檢測能否旋轉")]
    public float Limit_R_length0r;
    public float Limit_R_length90r;
    public float Limit_R_length0l;
    public float Limit_R_length90l;
    private float Limit_R_length0;
    private float Limit_R_length90;
    
    [Header("X的位移")]
    public float offsetX = 15F;
    [Header("Y的位移")]
    public float offsetY = 15F;
    //檢測是否碰撞右邊牆
    public bool wall_right;
    public bool wall_left;
    public bool wall_down = false;
    public bool can_rotate;
    public bool small_bottom;
    private float small_length=40;
    public bool small_right;
    public bool small_left;
    //判斷所有方塊右邊是否有其他方塊
    public bool [] small_right_all;
    public bool [] small_left_all;
    private void OnDrawGizmos()
    {
        #region 旋轉和移動判定線
        //ector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        int z = (int)transform.eulerAngles.z;
        if (z == 0 || z == 180)
        {
            //紀錄該角度的長度
            rec_length = length_0;
            length_down = length_90;
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, Vector3.right * length_0);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, Vector3.left * length_0);
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, Vector3.down * length_down);
            //旋轉判定
            Limit_R_length0 = Limit_R_length0l;
            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(transform.position, Vector3.left * Limit_R_length0l);
            Gizmos.DrawRay(transform.position, Vector3.right * Limit_R_length0r);
        }
        else if (z == 90 || z == 270)
        {
            //紀錄該角度的長度
            rec_length = length_90;
            length_down = length_0;
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, Vector3.right * length_90);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, Vector3.left * length_90);
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, Vector3.down * length_down);
            //旋轉判定
            Limit_R_length90 = Limit_R_length90l;
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, Vector3.left * Limit_R_length90l);
            Gizmos.DrawRay(transform.position, Vector3.right * Limit_R_length90r);
        }
        // length_down += 60;//offset of down
        #endregion
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawRay(transform.GetChild(i).position, Vector2.down * small_length);
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawRay(transform.GetChild(i).position, Vector2.right * small_length);
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawRay(transform.GetChild(i).position, Vector2.left * small_length);
        }
    }
    private void check_bottom()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.GetChild(i).position, Vector3.down, small_length, 1 << 10);
            if (hit && hit.collider.name == "方塊")
            {
                small_bottom = true;
            }

        }
        for (int i = 0; i < transform.childCount; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.GetChild(i).position, Vector3.right, small_length, 1 << 10);
            //如果右邊有方塊就統計多少和那些方塊被打勾
            if (hit && hit.collider.name == "方塊")
            {
                small_right_all[i] = true;
            }
            else
            {
                small_right_all[i] = false;
            }
         }
        var all_right = small_right_all.Where(x => x == true);
        small_right = all_right.ToArray().Length > 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.GetChild(i).position, Vector3.left, small_length, 1 << 10);
            if (hit && hit.collider.name == "方塊")
            {
                small_left_all[i] = true;
            }
            else
            {
                small_left_all[i] = false;
            }
        }
        var all_left = small_left_all.Where(x => x == true);
        small_left = all_left.ToArray().Length > 0;
    }

    /// <summary>
    /// 偵測是否碰撞牆壁
    /// </summary>
    private void checkwall()
    {
        //設限能否旋轉
        RaycastHit2D hit_R_L0 = Physics2D.Raycast(transform.position, Vector3.left, Limit_R_length0, 1 << 8);
        RaycastHit2D hit_R_L90 = Physics2D.Raycast(transform.position, Vector3.left, Limit_R_length90, 1 << 8);
        RaycastHit2D hit_R_R0 = Physics2D.Raycast(transform.position, Vector3.right, Limit_R_length0r, 1 << 8);
        RaycastHit2D hit_R_R90 = Physics2D.Raycast(transform.position, Vector3.right, Limit_R_length90r, 1 << 8);
        //RaycastHit2D hit_R_R = Physics2D.Raycast(transform.position, Vector3.left, Limit_R_length90, 1 << 8);
        if ((hit_R_L0 && hit_R_L0.transform.name == "左牆壁") || (hit_R_L90 && hit_R_L90.transform.name == "左牆壁") || (hit_R_R90 && hit_R_R90.transform.name == "右牆壁") || (hit_R_R0 && hit_R_R0.transform.name == "右牆壁"))
        {
            //print("hit");
            can_rotate = false;
        }
        else
        {
            can_rotate = true;
        }

        //射線偵測是否碰撞牆壁(位置 方向 長度 塗層)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.right, rec_length, 1 << 8);
        if (hit && hit.transform.name == "右牆壁")
        {
            wall_right = true;
        }
        else
        {
            wall_right = false;
        }
        RaycastHit2D hitL = Physics2D.Raycast(transform.position, Vector3.left, rec_length, 1 << 8);
        if (hitL && hitL.transform.name == "左牆壁")
        {
            wall_left = true;
        }
        else
        {
            wall_left = false;
        }
        RaycastHit2D hitD = Physics2D.Raycast(transform.position, Vector3.down, length_down, 1 << 9);
        if (hitD && hitD.transform.name == "地板" && T.layer!=9)
        {
            wall_down = true;
        }
        else
        {
            wall_down = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        T.transform.localScale = new Vector3(Scale_X, 0.3f, 1f);
        small_right_all = new bool[transform.childCount];
        small_left_all = new bool[transform.childCount];
    }

    // Update is called once per frame
    void Update()
    {
        //紀錄該角度的長度
        rec_length = length_0;
        checkwall();
        check_bottom();
    }

}
