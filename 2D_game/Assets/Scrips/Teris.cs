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
    //檢測是否碰撞右邊牆
    public bool wall_right;
    public bool wall_left;
    public bool wall_down;
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
        }
    }

    /// <summary>
    /// 偵測是否碰撞牆壁
    /// </summary>
    private void checkwall()
    {
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
