using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A2 : MonoBehaviour//�����ж�����
{
    public  GameObject �ж�����Ӧ��ɫ;

    private void Update()
    {
        if (�ж�����Ӧ��ɫ!=null)
        {
            if (�ж�����Ӧ��ɫ.GetComponent<A1>().��ǰ����ֵ <= 0)
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
