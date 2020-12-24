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
    public float rec_length;
    //檢測是否碰撞右邊牆
    public bool wall_right;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //ector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        int z = (int)transform.eulerAngles.z;
        if (z == 0 || z == 180)
        {
            //紀錄該角度的長度
            rec_length = length_0;
            Gizmos.DrawRay(transform.position, Vector3.right*length_0);
        }
        else if (z == 90 || z == 270)
        {
            //紀錄該角度的長度
            rec_length = length_90;
            Gizmos.DrawRay(transform.position, Vector3.right * length_90);
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
