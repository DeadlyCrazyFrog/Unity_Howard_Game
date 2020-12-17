using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APINOSTATIC : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform traA;
    public Transform traB;
    public Transform traC;
    public GameObject GOB_1;
    void Start()
    {
        print("此物件的位置" + traA.position);
        traB.position = new Vector3(1, 2, 65);
        print("此物件的塗層為" + GOB_1.layer);
        GOB_1.layer = 4;
        print(GOB_1.name);
    }

    // Update is called once per frame
    void Update()
    {
        traC.Rotate(0, 0, -1);
        traC.Translate(0, 10, 0);
    }
}
