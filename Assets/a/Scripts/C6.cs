using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C6 : MonoBehaviour
{
  public float speed = 50f;  // ����ָʾ��ͷ��ת

    void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }
}
