using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A2 : MonoBehaviour//放在行动条上
{
    public  GameObject 行动条对应角色;

    private void Update()
    {
        if (行动条对应角色!=null)
        {
            if (行动条对应角色.GetComponent<A1>().当前生命值 <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
