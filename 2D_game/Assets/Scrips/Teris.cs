using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teris : MonoBehaviour
{
    public GameObject T;
    [Header("Scale_X")]
    public float Scale_X =0.3F;
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
    public bool wall_down;
    public bool can_rotate;
    private void OnDrawGizmos()
    {
        
        //ector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        int z = (int)transform.eulerAngles.z;
        if (z == 0 || z == 180)
        {
            //紀錄該角度的長度
            rec_length = length_0;
            length_down = length_90;
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, Vector3.right*length_0);
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
    }

    /// <summary>
    /// 偵測是否碰撞牆壁
    /// </summary>
    private void checkwall()
    {
        //設限能否旋轉
        RaycastHit2D hit_R_L0 = Physics2D.Raycast(transform.position, Vector3.left, Limit_R_length0, 1 << 8);
        RaycastHit2D hit_R_L90 = Physics2D.Raycast(transform.position, Vector3.left, Limit_R_length90, 1 << 8);
        //RaycastHit2D hit_R_R = Physics2D.Raycast(transform.position, Vector3.left, Limit_R_length90, 1 << 8);
        if ((hit_R_L0 && hit_R_L0.transform.name == "左牆壁") || (hit_R_L90 && hit_R_L90.transform.name == "左牆壁"))
        {
            print("hit");
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
        RaycastHit2D hitD = Physics2D.Raycast(transform.position, Vector3.down, rec_length, 1 << 8);
        if (hitD && hitD.transform.name == "地板")
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
    }

    // Update is called once per frame
    void Update()
    {
        //紀錄該角度的長度
        rec_length = length_0;
        checkwall();
    }

}
