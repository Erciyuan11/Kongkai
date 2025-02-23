using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class B1 : Singleton<B1>//控制角色技能和普攻,生成角色和敌人
{

    [Header("战技点")]
    public int 当前战技点 = 3;
    public int 最大战技点 = 5; // 最大战技点数
    public Image 战技点图像预制体; // 这是您的图像预制体
    Transform 战技点父级;

    [Header("查找行动条最上面的对象")]
    public Transform 拖入行动条; // 指定的父对象
    public Transform 最上层的对象;
    [HideInInspector]
    public Transform 排行第二的对象;

    public GameObject[] 我方角色;
    public Transform 我方队列;

    public GameObject[] 敌方角色;
    public Transform 敌方队列;

    public Transform 角色属性父级;
    public Transform boss属性父级;
    public Transform 行动条父级;

    public GameObject 显示当前行动角色的文本; // 要检查的游戏对象

    [Header("buff相关")]
    public string 持续伤害A名称 = "灼伤";
    public Sprite 持续伤害图标A;
    public string 持续伤害B名称 = "触电";
    public Sprite 持续伤害图标B;
    public Sprite 攻击力提升图标;
    public Sprite 防御力下降图标;
    [Header("强控相关，可以让目标无法动弹，持续指定回合后结束")]
    public string 强控名称 = "冻结";
    public Sprite 强控图标;
    [Header("触发Buff时在目标身上产生的特效")]
    public GameObject 停留在目标身上的强控特效;
    public GameObject 造成持续伤害A时的特效;
    public GameObject 造成持续伤害B时的特效;

    private void OnEnable()
    {
        生成角色();
        最上层的对象 = FindTopObject(行动条父级);
    }

    private void Start()
    {

        //最上层的对象 = FindTopObject(行动条父级);
        显示当前行动角色的文本.gameObject.SetActive(false);

        //生成角色();

        战技点父级 = transform.Find("战技点UI");
        CreateImages(当前战技点);
    }

    private void Update()
    {
        最上层的对象 = FindTopObject(行动条父级);
        //Debug.Log("最上层的对象是" + 最上层的对象.GetComponent<A2>().行动条对应角色.name);
        显示当前行动角色文本();

        排行第二的对象 = GetSecondChildFromTop(拖入行动条);

    }


    private void CreateImages(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Image newImageObj = Instantiate(战技点图像预制体, 战技点父级);
            Image newImage = newImageObj.GetComponent<Image>();
            newImage.transform.SetParent(战技点父级, false);
        }
    }

    // 调用此函数以增加图像数量
    public void 增加战技点()
    {
        if (当前战技点 < 最大战技点)
        {
            当前战技点++;
            CreateImages(1);
        }
    }

    // 调用此函数以减少图像数量
    public void 减少战技点()
    {
        if (当前战技点 > 0)
        {
            当前战技点--;
            // 获取父对象下的最上面子对象并删除
            Transform topChild = 战技点父级.GetChild(0);
            Destroy(topChild.gameObject);
        }
    }





    public Transform FindTopObject(Transform parent)
    {
        Transform topObject = null;
        float highestY = float.MinValue;

        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);

            if (child.position.y > highestY)
            {
                highestY = child.position.y;
                topObject = child;
            }

            // 递归检查子对象的子对象
            Transform childTop = FindTopObject(child);
            if (childTop != null && childTop.position.y > highestY)
            {
                highestY = childTop.position.y;
                topObject = childTop;
            }
        }

        return topObject;
    }//查找行动条最上层的对象

    public void 释放必杀技()
    {

    }
    public void 生成角色()
    {
        foreach (GameObject objPrefab in 我方角色)
        {
            GameObject spawnedObject = Instantiate(objPrefab, 我方队列);
        }
        foreach (GameObject objPrefab in 敌方角色)
        {
            GameObject spawnedObject = Instantiate(objPrefab, 敌方队列);
        }
    }


    public void 显示当前行动角色文本()
    {
        // 检查子集是否为空
        if (检查游戏对象的子集是否不为空(拖入行动条.gameObject))
        {
            显示当前行动角色的文本.SetActive(true); // 启用游戏对象
        }
        else
        {
            显示当前行动角色的文本.SetActive(false); // 关闭游戏对象
        }
    }
    // 检查游戏对象的子集是否不为空
    private bool 检查游戏对象的子集是否不为空(GameObject obj)
    {
        int childCount = obj.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = obj.transform.GetChild(i);
            if (child.gameObject.activeSelf) // 只考虑处于激活状态的子对象
            {
                return true; // 存在非空子集
            }
        }
        return false; // 子集为空
    }
    //查找行动条排行第二的对象
    Transform GetSecondChildFromTop(Transform parent)
    {
        if (parent == null || parent.childCount < 2)
        {
            return null; // 如果父对象为空或子对象不足两个，则返回null
        }

        Transform secondChild = parent.GetChild(1); // 获取第二个子对象（从上往下数）

        return secondChild;
    }
    public void QuitGame()
    {
        // 打印一条消息到控制台（这一步在实际游戏中是可选的）
        Debug.Log("Exiting Game");

        // 退出游戏
        Application.Quit();
    }
}
