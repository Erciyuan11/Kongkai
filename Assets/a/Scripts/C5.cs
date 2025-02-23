using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C5 : MonoBehaviour
{
    private Camera mainCamera;   //用于让生成的敌方属性和伤害文本正对摄像机
    void Start()
    {
        // 通过标签获取主摄像头
        GameObject cameraObject = GameObject.FindWithTag("MainCamera");
        if (cameraObject != null)
        {
            mainCamera = cameraObject.GetComponent<Camera>();
        }
        if (mainCamera == null)
        {
            Debug.Log("无法找到标签为'MainCamera'的对象");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 获取摄影机与物体之间的方向 
        Vector3 dir = mainCamera.transform.position - transform.position;
        // 生成新的四元数
        Quaternion rot = Quaternion.LookRotation(dir);
        // 去除x与z轴的旋转
        rot.x = 0;
        rot.z = 0;
        // 使物体保持面向摄像头
        transform.rotation = rot;
    }
}
