using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class A6 : MonoBehaviour//被动技能，召唤技，景元，镜流
{
    [Header("挂在角色身上，角色行动指定次数后触发效果")]
    public int 行动指定次数后;
    public int 当前行动次数=0;
    public GameObject 当前行动角色;
    public GameObject 召唤对象; // 存放你想要生成的游戏对象的数组
    bool 是否可以增加行动次数;
    public bool 释放技能增加行动次数=false;
    bool 增加行动次数=true;
    public GameObject buff图像;
    GameObject 生成的对象;
    GameObject 生成位置;
    public GameObject 生成的buff图标;
    Image buff背景;
    Text buff计数;
    public bool 是否可以使用镜流强化技;
    public GameObject 强化粒子特效;
    GameObject 生成的强化特效;
    public bool 处于强化状态中;
    public bool 是否可以立即行动;
    //减少全队生命值的总和
    public float totalDamage = 0.0f;
    [Header("用于设置是谁的被动")]
    public bool 景元;
    public bool 镜流;
    void Start()
    {
        生成位置 = GameObject.Find("生成特殊角色的位置");
    }

    // Update is called once per frame
    void Update()
    {
        当前行动角色 = B1.Instance.最上层的对象.GetComponent<A2>().行动条对应角色;
        if (当前行动角色 == transform.gameObject&& 增加行动次数)
        {
            是否可以增加行动次数 = true;
            if (当前行动角色 == transform.gameObject && 是否可以增加行动次数&&生成的对象==null&& 释放技能增加行动次数&& !处于强化状态中 )
            {
                当前行动次数++;
                是否可以增加行动次数 = false;
                增加行动次数 = false;
                释放技能增加行动次数 = false;
                生成buff图标();
                buff计数.text= 当前行动次数.ToString();
                //Debug.Log("11");
            }
            if (当前行动角色 == transform.gameObject && 是否可以增加行动次数 && 镜流 && 是否可以使用镜流强化技&& 处于强化状态中)
            {
                当前行动次数--;
                是否可以增加行动次数 = false;
                增加行动次数 = false;
                buff计数.text = null;
                减少我方全体生命值();
                if(当前行动次数 == 行动指定次数后)
                GetComponent<A1>().攻击力 += totalDamage;
                if (当前行动次数 == 0)
                {
                    是否可以使用镜流强化技 = false;
                    处于强化状态中 = false;
                    增加行动次数 = true;
                    GetComponent<A1>().攻击力 -= totalDamage;
                    Destroy(生成的buff图标);
                    //totalDamage= 0;
                }
            }
        }

        if (当前行动角色 != transform.gameObject)
        {
            增加行动次数 = true;
            是否可以增加行动次数 = false;
            释放技能增加行动次数 = false;
        }

        if(当前行动次数== 行动指定次数后)
        {
            if (景元)
            {
                召唤();
                当前行动次数 = 0;
                buff计数.text = 当前行动次数.ToString();
                Destroy(生成的buff图标);
            }
            if (镜流&& !处于强化状态中)
            {
                减少我方全体生命值();
                GetComponent<A1>().攻击力 += totalDamage;
                是否可以使用镜流强化技 = true;
                处于强化状态中 = true;
                Invoke("生成强化特效", 1.5f);
                //totalDamage = 0;
            }
            buff计数.text = null;
        }
        if (当前行动次数 == 0&&镜流)//用来删除镜流的强化特效
        {
            if (生成的强化特效 != null)
            {
                totalDamage = 0;
                GetComponent<A1>().攻击力 -= totalDamage;
                Destroy(生成的强化特效.gameObject);
            }
        }

            if (GetComponent<A1>().是否被强控)
        {
            if (景元)
            {
                if (生成的对象 != null)
                    生成的对象.GetComponent<A1>().延迟删除(1.3f);
                当前行动次数 = 0;
                buff计数.text = 当前行动次数.ToString();
            }
        }
    }
    private void OnDestroy()
    {
       // if(生成的对象!=null)
       // 生成的对象.GetComponent<A1>().延迟删除(0.5f);
    }
    private void 召唤()
    {

        // 循环生成游戏对象，最多生成5个子集
        for (int i = 0; i <1; i++)
        {
            GameObject randomObject = 召唤对象;
            if (randomObject != null && gameObject.GetComponent<A1>().是否是敌方 == false)
            {
                 生成的对象 = Instantiate(randomObject, 生成位置.transform);

            }
            else if (randomObject != null && gameObject.GetComponent<A1>().是否是敌方)
            {
                 生成的对象 = Instantiate(randomObject, 生成位置.transform);

            }
            生成的对象.GetComponent<A1>().攻击力 = GetComponent<A1>().攻击力;
        }
    }

    private void 镜流的强化技能()//在释放技能的时候使用
    {

    }
    void 生成强化特效()
    {
        if (强化粒子特效 != null && 生成的强化特效 == null)
        {
            生成的强化特效 = Instantiate(强化粒子特效, transform);
            是否可以立即行动 = true;
        }

    }
    public void 减少我方全体生命值()
    {
        totalDamage = 0;
        Transform parentTransform = B1.Instance.我方队列;
        foreach (Transform childTransform in parentTransform)
        {
            GameObject childObject = childTransform.gameObject;

            // 获取子对象上的"A1"脚本
            A1 script = childObject.GetComponent<A1>();

            // 如果脚本存在
            if (script != null)
            {
                // 计算需要减少的生命值
                float maxHealth = script.最大生命值;
                float damage = maxHealth * 0.1f;

                // 如果减少的生命值大于最大生命值的10%，则将其限制为10%
                //damage = Mathf.Min(damage, maxHealth * 0.1f);

                // 减少当前生命值
                if (script.当前生命值 > damage)
                    script.当前生命值 -= damage;
                else
                    script.当前生命值 = 1;

                // 确保当前生命值不小于1
                //script.当前生命值 = Mathf.Max(script.当前生命值, 1.0f);

                // 累加总伤害
                totalDamage += damage;
            }
        }
        // 打印总伤害
        //Debug.Log("总伤害：" + totalDamage);
    }
    void 生成buff图标()
    {
        if(生成的buff图标==null)
        生成的buff图标 = Instantiate(buff图像, GetComponent<A1>().buff生成位置);
        
        buff背景 = 生成的buff图标.GetComponent<Image>();
        if (景元)
            buff背景.sprite = 召唤对象.GetComponent<A1>().角色头像;
        if (镜流)
            buff背景.sprite = GetComponent<A1>().角色头像;
        var js = buff背景.transform.Find("Text");
        buff计数 = js.GetComponent<Text>();
        buff计数.text = 当前行动次数.ToString();

    }
}
