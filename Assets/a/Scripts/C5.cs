using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C5 : MonoBehaviour
{
    private Camera mainCamera;   //���������ɵĵз����Ժ��˺��ı����������
    void Start()
    {
        // ͨ����ǩ��ȡ������ͷ
        GameObject cameraObject = GameObject.FindWithTag("MainCamera");
        if (cameraObject != null)
        {
            mainCamera = cameraObject.GetComponent<Camera>();
        }
        if (mainCamera == null)
        {
            Debug.Log("�޷��ҵ���ǩΪ'MainCamera'�Ķ���");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ��ȡ��Ӱ��������֮��ķ��� 
        Vector3 dir = mainCamera.transform.position - transform.position;
        // �����µ���Ԫ��
        Quaternion rot = Quaternion.LookRotation(dir);
        // ȥ��x��z�����ת
        rot.x = 0;
        rot.z = 0;
        // ʹ���屣����������ͷ
        transform.rotation = rot;
    }
}
