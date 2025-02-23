using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class A4 : MonoBehaviour//放在角色身上用于控制角色技能
{
    [Header("                                              普攻效果")]
    public string 普攻名称;
    public string 普攻描述;
    public bool 是否可以使用普攻 = true;
    public bool 普攻是否回复战技点 = true;
    public int 普攻造成伤害次数 = 1;
    public float 普攻造成多次伤害间隔 = 0.1f;
    public bool 普攻是否移动到目标;
    [HideInInspector]
    public float 如果普攻移动到目标播放动画多久开始移动 = 0.1f;
    public float 普攻伤害倍率 = 1f;
    public int 普攻破韧值 = 1;
    public float 普攻动画时长 = 1.5f;
    public float 开始播放普攻攻击动画多久后造成伤害 = 0.2f;
    public float 开始播放普攻动画后多久后生成特效 = 0.2f;
    public GameObject 普攻粒子特效;
    public float 普攻特效存在时间 = 0.6f;
    public AudioClip[] 普攻击中音效;
    AudioSource 音效播放器;
    [Header("                                              技能效果")]
    public string 技能名称;
    public string 技能描述;
    public bool 是否可以使用技能;
    public bool 技能攻击是否是移动到目标;
    [HideInInspector]
    public float 如果技能移动到目标播放动画多久开始移动 = 0.1f;
    public float 技能倍率 = 2;
    public bool 技能是否对我方使用; // 是否对我方使用
    public bool 技能是否消耗战技点;
    public int 技能造成伤害次数 = 1;
    public float 技能造成多次伤害间隔数 = 0.2f;
    public int 技能攻击破韧值 = 2;
    public float 技能动画时长 = 1.5f;
    public float 开始播放技能攻击动画多久后造成伤害;
    public float 开始播放技能动画后多久后生成特效 = 0;
    public GameObject 技能粒子特效;
    public float 技能特效存在时间 = 1;
    public AudioClip[] 技能击中音效;

    [Header("技能生效人数选择一个")]
    public bool 对单个生效 = true;
    public bool 对目标队列全体生效;
    public bool 对目标和左右对象生效;
    public float 左右目标受到伤害比例 = 0.5f;
    public bool 对目标和两个随机对象生效;

    [Header("技能对敌方的效果")]
    public bool 对敌方造成伤害 = true;

    public bool 让敌方持续受到伤害A;
    //public string 持续伤害A名称="灼伤";
    public bool 让敌方持续受到伤害B;
    //public string 持续伤害B名称 = "触电";
    public int 持续受到伤害回合 = 3;
    public float 持续伤害倍率 = 0.2f;
    public int 持续伤害破韧量 = 0;

    public bool 让敌方防御力降低;
    public int 防御力降低多少 = 30;
    public int 防御力降低持续回合 = 1;

    public bool 让敌方行动降低;
    public int 行动降低多少 = 1;

    public bool 概率强控敌方;
    public float 概率 = 0.6f;
    //public GameObject 停留在目标身上的强控特效;
    public int 强控持续回合 = 1;

    [Header("技对己方的效果")]
    public bool 给友方回复生命;
    public bool 给友方回增加护盾;
    public bool 让友方立即行动;
    public bool 让友方解除控制;
    public bool 给友方增加攻击力;
    public int 攻击力提升多少 = 30;
    public int 增加攻击力持续回合 = 1;
    public bool 让友方行动上升;
    public int 行动上升多少 = 1;

    [Header("                                              蓄力技效果")]
    public string 蓄力技名称;
    public string 蓄力技完成名称;
    public string 蓄力技描述;
    public bool 是否可以使用蓄力技;
    public float 蓄力技效果倍率 = 3;
    public bool 蓄力中 = false;
    public GameObject 蓄力特效;
    public GameObject 释放蓄力特效;
    public int 蓄力攻击破韧值 = 1;
    public bool 蓄力技对单个生效 = true;
    public bool 蓄力技是否对目标队列全体生效;
    public bool 蓄力技是否对目标和左右对象生效;
    public float 蓄力技左右目标受到伤害比例 = 0.5f;
    public int 蓄力造成伤害次数 = 1;
    public float 蓄力造成多次伤害间隔数 = 0.2f;
    public bool 蓄力技是否移动到目标释放;
    [HideInInspector]
    public float 如果蓄力移动到目标播放动画多久开始移动 = 0;
    public float 开始播放蓄力动画后多久后造成伤害 = 0;
    public float 蓄力特效存在时间 = 0.6f;
    public float 蓄力动画时长 = 1.5f;

    [Header("                                              召唤技效果")]
    public string 召唤技名称;
    public string 召唤技描述;
    public bool 是否可以使用召唤技;//决定是否显示召唤按钮
    public GameObject[] 召唤列表; // 存放你想要生成的游戏对象的数组
    public int 最大一次召唤多少目标;   // 想要生成的游戏对象数量
    public float 开始播放召唤动画后多久后开始召唤 = 0;
    public float 召唤动画时长 = 1.5f;
    public bool 召唤物是否为特色召唤物;
    public GameObject 特色召唤物;

    [Header("特效")]
    public GameObject 目标受伤特效;
    public float 受伤特效存在时长=0.2f;
    public bool 普攻特效是否直接在目标释放 = false;
    public bool 技能特效是否直接在目标释放 = false;
    public bool 蓄力特效是否直接在目标释放 = false;
            [HideInInspector]
    public Transform 特效发射位置;

    [Header("buff相关")]
    public GameObject buff图像预制体;
    Sprite 特色召唤物图标;


    //[Header("下面的不用管")]
        [HideInInspector]
    public GameObject 生成的蓄力特效;
        [HideInInspector]
    public GameObject 生成的特色召唤物buff图标;
        [HideInInspector]
    public GameObject 生成的强控buff图标;
        [HideInInspector]
    public int 剩余被强控回合数;
        [HideInInspector]
    public GameObject 生成的攻击力提升buff图标;
        [HideInInspector]
    public int 剩余攻击力提升回合数;
        [HideInInspector]
    public float 提升的攻击力;
        [HideInInspector]
    public GameObject 生成的防御力降低buff图标;
        [HideInInspector]
    public int 剩余防御力降低回合数;
        [HideInInspector]
    public float 降低的防御力;
        [HideInInspector]
    public GameObject 生成的持续灼伤伤害buff图标;
        [HideInInspector]
    public GameObject 生成的持续触电伤害buff图标;
        [HideInInspector]
    public int 剩余持续伤害A回合数;
        [HideInInspector]
    public int 剩余持续伤害B回合数;
        [HideInInspector]
    public float 持续受到伤害量;
        [HideInInspector]
    public GameObject 生成的特色召唤物;
    int callCount = 0;//蓄力计数

     Camera 初始摄像机;
    Image buff背景;
    Transform 目标队列;
    int 执行次数 = 0;
    int 最大执行次数 = 2; // 想要执行的最大次数
    GameObject 攻击目标;
    string 技能攻击属性;

    void Start()
    {
        var a = transform.Find("快捷角色设置");
        特效发射位置 = a.transform.Find("特效发射位置");
        音效播放器 = GetComponent<AudioSource>();
        初始摄像机 = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        让所有buff剩余回合刷新();

        if (GetComponent<A1>().是否被强控 && 剩余被强控回合数 == 0)

            GetComponent<A1>().恢复冻结();

        if (GetComponent<A1>().是否受到持续灼伤伤害 && 剩余持续伤害A回合数 == 0)
        {
            GetComponent<A1>().恢复受到持续伤害();
        }
        if (GetComponent<A1>().是否受到持续触电伤害 && 剩余持续伤害B回合数 == 0)
        {
            GetComponent<A1>().恢复受到持续伤害();
        }
        if (GetComponent<A1>().是否提升攻击力 && 剩余攻击力提升回合数 == 0)
        {
            GetComponent<A1>().恢复攻击力提升();
        }
        if (GetComponent<A1>().是否降低防御力 && 剩余防御力降低回合数 == 0)
        {
            GetComponent<A1>().恢复翻防御力降低();
        }
        if (GetComponent<A1>().当前生命值 <= 0)
        {
            if (生成的特色召唤物 != null)
            {
                生成的特色召唤物.GetComponent<A1>().延迟删除(0.8f);
            }
        }
    }
    public void 使用技能(GameObject 目标, string 攻击属性)
    {
        播放技能音效();
        攻击目标 = 目标;
        技能攻击属性 = 攻击属性;
        StartCoroutine(触发技能伤害多次());
    }
    public void 使用普攻(GameObject 目标, string 攻击属性)
    {
        攻击目标 = 目标;
        播放普攻音效();
        StartCoroutine(触发普攻伤害多次(目标, 攻击属性));
    }
    public void 使用召唤技()
    {
        召唤();
    }

    public void 使用蓄力技(GameObject 目标, string 攻击属性)
    {
        攻击目标 = 目标;
        callCount++;
        if (生成的蓄力特效 == null && 蓄力特效 != null)
            生成的蓄力特效 = Instantiate(蓄力特效, transform);

        蓄力中 = true;
        if (callCount >= 2)
        {
            // 重置计数器
            callCount = 0;
        }
    }

    public void 释放蓄力技(GameObject 目标, string 攻击属性)
    {
        Animator animator = GetComponent<Animator>();
        蓄力中 = false;
        animator.SetBool("蓄力", false);
        animator.SetBool("释放蓄力", true);
        Destroy(生成的蓄力特效);
    }
    public void 蓄力造成伤害(GameObject 目标, string 攻击属性)
    {
        //Debug.Log(目标.name);
        if (蓄力特效是否直接在目标释放 && 释放蓄力特效 != null)
        {
            GameObject sf = Instantiate(释放蓄力特效, 目标.transform);
            Destroy(sf, 蓄力特效存在时间);
        }
        else if (!蓄力特效是否直接在目标释放 && 释放蓄力特效 != null)
        {
            GameObject sf = Instantiate(释放蓄力特效, 特效发射位置);
            Destroy(sf, 蓄力特效存在时间);
        }
        float 造成伤害 = GetComponent<A1>().攻击力 * 蓄力技效果倍率;
        Transform parentTransform = 目标.transform.parent;

        // 使用协程触发蓄力伤害
        StartCoroutine(触发蓄力技伤害多次(目标, 攻击属性, 蓄力造成伤害次数, 蓄力造成多次伤害间隔数, 造成伤害));
    }
    private void 召唤()
    {
        if (召唤物是否为特色召唤物 == false)
        {
            bool 放在最上方 = true;
            int 实际召唤数量 = 最大一次召唤多少目标;
            if (gameObject.GetComponent<A1>().是否是敌方 == false)
            {
                Debug.Log("222");
                if (B2.Instance.玩家剩余人数 + 最大一次召唤多少目标 >= 4)
                {
                    实际召唤数量 = 4 - B2.Instance.玩家剩余人数;
                }
            }
            for (int i = 0; i < 实际召唤数量; i++)
            {
                GameObject randomObject = GetRandomObject();

                if (randomObject != null && gameObject.GetComponent<A1>().是否是敌方 == false)
                    {

                        GameObject spawnedObject = Instantiate(randomObject, B1.Instance.我方队列);
                        Debug.Log("222");
                        // 根据放在最上方的布尔变量来选择放置位置
                        if (放在最上方)
                        {
                            spawnedObject.transform.SetAsFirstSibling(); // 放在最上方
                        }
                        else
                        {
                            spawnedObject.transform.SetAsLastSibling(); // 放在最下方
                        }

                        // 切换布尔变量的值
                        放在最上方 = !放在最上方;
                }
                else if (randomObject != null && gameObject.GetComponent<A1>().是否是敌方)
                {
                    GameObject spawnedObject = Instantiate(randomObject, B1.Instance.敌方队列);

                    // 根据放在最上方的布尔变量来选择放置位置
                    if (放在最上方)
                    {
                        spawnedObject.transform.SetAsFirstSibling(); // 放在最上方
                    }
                    else
                    {
                        spawnedObject.transform.SetAsLastSibling(); // 放在最下方
                    }

                    // 切换布尔变量的值
                    放在最上方 = !放在最上方;
                }

            }
        }
        else
        {
            if (B2.Instance.当前行动角色.GetComponent<A1>().是否是敌方 == false)
            {
                GameObject 生成位置;
                生成位置 = GameObject.Find("2生成特殊角色的位置");
                if (生成的特色召唤物 == null)
                {
                    生成的特色召唤物 = Instantiate(特色召唤物, 生成位置.transform);
                    特色召唤物图标 = 特色召唤物.GetComponent<A1>().角色头像;
                    生成的特色召唤物buff图标 = Instantiate(buff图像预制体, GetComponent<A1>().buff生成位置);
                }
            }
            if (B2.Instance.当前行动角色.GetComponent<A1>().是否是敌方 == true)
            {
                GameObject 生成位置;
                生成位置 = GameObject.Find("2生成特殊角色的位置(敌方)");
                if (生成的特色召唤物 == null)
                {
                    生成的特色召唤物 = Instantiate(特色召唤物, 生成位置.transform);
                    特色召唤物图标 = 特色召唤物.GetComponent<A1>().角色头像;
                    生成的特色召唤物buff图标 = Instantiate(buff图像预制体, GetComponent<A1>().buff生成位置);
                }
            }

            buff背景 = 生成的特色召唤物buff图标.GetComponent<Image>();
            buff背景.sprite = 特色召唤物图标;
            生成的特色召唤物.GetComponent<A1>().特色召唤物剩余回合 = 生成的特色召唤物.GetComponent<A1>().特色召唤物存在回合;
        }
    }

    private GameObject GetRandomObject()//随机选择召唤列表的一个
    {
        int randomIndex = Random.Range(0, 召唤列表.Length);
        return 召唤列表[randomIndex];
    }



    public void 播放指定普攻特效(Transform transform)
    {
        //Debug.Log("普攻特效");
        if (普攻是否移动到目标)
        {
            // 近战攻击
            普攻特效(transform.transform);
        }
        else
        {
            // 远程攻击
            if (普攻特效是否直接在目标释放)
            {
                普攻特效(transform.transform);
            }
            else
            {
                移动普攻特效(transform.transform);
            }
        }

    }
    public void 播放指定技能特效(Transform transform)
    {
        //Debug.Log("技能特效");
        if (技能攻击是否是移动到目标)
        {
            // 近战攻击
            非移动技能特效(transform.transform);
        }
        else
        {

            // 远程攻击
            if (技能特效是否直接在目标释放)
            {
                非移动技能特效(transform.transform);
            }
            else
            {
                移动技能特效(transform.transform);
            }
        }
    }
    private void 普攻特效(Transform 目标位置)
    {
        // 创建近战粒子特效
        if (普攻是否移动到目标)
        {
            if (普攻粒子特效 != null && 特效发射位置 != null)
            {
                GameObject meleeEffect = Instantiate(普攻粒子特效, 目标位置.position, Quaternion.identity);
                Destroy(meleeEffect.gameObject, 普攻特效存在时间);
            }
        }
    }
    private void 移动普攻特效(Transform 目标位置)
    {
        // 创建近战粒子特效
        if (普攻粒子特效 != null && 特效发射位置 != null)
        {
            GameObject meleeEffect = Instantiate(普攻粒子特效, 特效发射位置.position, Quaternion.identity);
            if (目标位置.Find("快捷角色设置").transform.Find("摄像机看向位置") != null)
                meleeEffect.transform.LookAt(目标位置.Find("快捷角色设置").transform.Find("摄像机看向位置").position);
            else
                meleeEffect.transform.LookAt(目标位置.position);
            meleeEffect.transform.position = Vector3.MoveTowards(meleeEffect.transform.position, 目标位置.Find("快捷角色设置").transform.Find("摄像机看向位置").position, 10 * Time.deltaTime);
            Destroy(meleeEffect.gameObject, 普攻特效存在时间);
        }
    }

    private void 非移动技能特效(Transform 目标位置)
    {
        // 创建远程粒子特效
        if (技能粒子特效 != null)
        {
            GameObject meleeEffect = Instantiate(技能粒子特效, 目标位置.position, Quaternion.identity);
            Destroy(meleeEffect.gameObject, 技能特效存在时间);
        }
    }

    private void 移动技能特效(Transform 目标位置)
    {
        if (技能粒子特效 != null)
        {
            GameObject meleeEffect = Instantiate(技能粒子特效, 特效发射位置.position, Quaternion.identity);
            if (目标位置.Find("快捷角色设置").transform.Find("摄像机看向位置") != null)
                meleeEffect.transform.LookAt(目标位置.Find("快捷角色设置").transform.Find("摄像机看向位置").position);
            else
                meleeEffect.transform.LookAt(目标位置.position);
            meleeEffect.transform.position = Vector3.MoveTowards(meleeEffect.transform.position, 目标位置.Find("快捷角色设置").transform.Find("摄像机看向位置").position, 10 * Time.deltaTime);
            Destroy(meleeEffect.gameObject, 技能特效存在时间);

        }
    }

    void 让目标行动后退多少(GameObject 目标, int 提前或延后)
    {
        Transform objectToMove = 目标.GetComponent<A1>().自己对应的行动条.transform;
        if (objectToMove != null)
        {
            // 获取目标对象的层级排列位置
            int targetSiblingIndex = objectToMove.GetSiblingIndex();
            // 移动的对象设置到目标对象的后方就是（索引 + 1 ）
            objectToMove.SetSiblingIndex(targetSiblingIndex + 提前或延后);
        }
    }
    void 给目标增加攻击buff(GameObject 目标)
    {
        if (目标.GetComponent<A4>().生成的攻击力提升buff图标 == null)
        {
            目标.GetComponent<A4>().提升的攻击力 = 攻击力提升多少;
            目标.GetComponent<A1>().攻击力 += 攻击力提升多少;
            目标.GetComponent<A4>().剩余攻击力提升回合数 = 增加攻击力持续回合;
            目标.GetComponent<A4>().生成的攻击力提升buff图标 = Instantiate(buff图像预制体, 目标.GetComponent<A1>().buff生成位置);
            buff背景 = 攻击目标.GetComponent<A4>().生成的攻击力提升buff图标.GetComponent<Image>();
            buff背景.sprite = B1.Instance. 攻击力提升图标;
        }
        else
        {
            目标.GetComponent<A4>().剩余防御力降低回合数 = 防御力降低持续回合;
        }
    }
    void 给目标降低防御力(GameObject 目标)
    {

        if (目标.GetComponent<A4>().生成的防御力降低buff图标 == null)
        {
            目标.GetComponent<A4>().降低的防御力 = 防御力降低多少;
            目标.GetComponent<A1>().防御力 -= 防御力降低多少;
            目标.GetComponent<A4>().剩余防御力降低回合数 = 防御力降低持续回合;
            目标.GetComponent<A4>().生成的防御力降低buff图标 = Instantiate(buff图像预制体, 目标.GetComponent<A1>().buff生成位置);
            buff背景 = 攻击目标.GetComponent<A4>().生成的防御力降低buff图标.GetComponent<Image>();
            buff背景.sprite = B1.Instance.防御力下降图标;
        }
        else
        {
            目标.GetComponent<A4>().剩余防御力降低回合数 = 防御力降低持续回合;
        }
    }
    void 给目标添加强控buff(GameObject 目标)
    {
        目标.GetComponent<A1>().是否被强控 = true;
        目标.GetComponent<Animator>().SetBool("冻结", true);
        if (目标.GetComponent<A4>().生成的强控buff图标 == null)
        {
            目标.GetComponent<A1>().生成的冻结特效 = Instantiate(B1.Instance. 停留在目标身上的强控特效, 目标.transform);
            目标.GetComponent<A4>().剩余被强控回合数 = 强控持续回合;
            目标.GetComponent<A4>().生成的强控buff图标 = Instantiate(buff图像预制体, 目标.GetComponent<A1>().buff生成位置);
            buff背景 = 攻击目标.GetComponent<A4>().生成的强控buff图标.GetComponent<Image>();
            buff背景.sprite = B1.Instance.强控图标;

        }
        else
        {
            目标.GetComponent<A4>().剩余被强控回合数 = 强控持续回合;
        }
    }
    void 给目标添加持续伤害A(GameObject 目标, int 持续伤害破韧量)
    {
        目标.GetComponent<A1>().是否受到持续灼伤伤害 = true;
        if (目标.GetComponent<A4>().生成的持续灼伤伤害buff图标 == null)
        {
            目标.GetComponent<A4>().持续受到伤害量 = GetComponent<A1>().攻击力 * 持续伤害倍率;
            //目标.GetComponent<A1>().受到持续伤害(目标.GetComponent<A4>().持续受到伤害量, GetComponent<A1>().属性, 持续伤害破韧量);
            目标.GetComponent<A1>().生成效果文本(B1.Instance.持续伤害A名称, 目标.GetComponent<A1>().持续伤害A颜色);
            目标.GetComponent<A4>().剩余持续伤害A回合数 = 持续受到伤害回合;
            目标.GetComponent<A4>().生成的持续灼伤伤害buff图标 = Instantiate(buff图像预制体, 目标.GetComponent<A1>().buff生成位置);
            目标.GetComponent<A1>().受到的持续伤害破韧量 = 持续伤害破韧量;
            buff背景 = 攻击目标.GetComponent<A4>().生成的持续灼伤伤害buff图标.GetComponent<Image>();
            buff背景.sprite =B1.Instance. 持续伤害图标A;
            
        }
        else
        {
            目标.GetComponent<A4>().剩余持续伤害A回合数 = 持续受到伤害回合;
        }
    }
    void 给目标添加持续伤害B(GameObject 目标, int 持续伤害破韧量)
    {
        目标.GetComponent<A1>().是否受到持续触电伤害 = true;
        if (目标.GetComponent<A4>().生成的持续触电伤害buff图标 == null)
        {
            目标.GetComponent<A4>().持续受到伤害量 = GetComponent<A1>().攻击力 * 持续伤害倍率;
            //目标.GetComponent<A1>().受到持续伤害(目标.GetComponent<A4>().持续受到伤害量, GetComponent<A1>().属性, 持续伤害破韧量);
            目标.GetComponent<A1>().生成效果文本(B1.Instance.持续伤害B名称, 目标.GetComponent<A1>().持续伤害B颜色);
            目标.GetComponent<A4>().剩余持续伤害B回合数 = 持续受到伤害回合;
            目标.GetComponent<A4>().生成的持续触电伤害buff图标 = Instantiate(buff图像预制体, 目标.GetComponent<A1>().buff生成位置);
            目标.GetComponent<A1>().受到的持续伤害破韧量 = 持续伤害破韧量;
            buff背景 = 攻击目标.GetComponent<A4>().生成的持续触电伤害buff图标.GetComponent<Image>();
            buff背景.sprite = B1.Instance. 持续伤害图标B;
        }
        else
        {
            目标.GetComponent<A4>().剩余持续伤害B回合数 = 持续受到伤害回合;
        }
    }


    private IEnumerator 延迟回复(float 回复量)            // 对随机两个目标回复生命
    {
        执行次数 = 0;
        while (执行次数 < 最大执行次数)
        {
            yield return new WaitForSeconds(0.2f);

            // 获取敌方队列的所有子集
            A1[] 子集 = 目标队列.GetComponentsInChildren<A1>();
            if (子集.Length > 0)
            {
                int 随机索引 = Random.Range(0, 子集.Length);
                A1 随机目标 = 子集[随机索引];

                // 对随机目标回复生命
                随机目标.回复生命(回复量);
                随机目标.生成效果文本("回复生命", Color.green);
                回复量 = 回复量 * 0.7f;

                执行次数++;
            }
        }
    }
    private IEnumerator 延迟伤害(GameObject 目标, float 伤害, string 属性)            // 对随机两个造成伤害
    {
        执行次数 = 0;
        while (执行次数 < 最大执行次数)
        {
            yield return new WaitForSeconds(0.2f);

            // 获取敌方队列的所有子集
            A1[] 子集 = 目标队列.GetComponentsInChildren<A1>();
            if (子集.Length > 0)
            {
                int 随机索引 = Random.Range(0, 子集.Length);
                A1 随机目标 = 子集[随机索引];

                // 对随机目标回复生命
                随机目标.发动攻击(随机目标.gameObject, 伤害, 属性);
                // Debug.Log("11");
                if (目标受伤特效 != null)
                {
                    GameObject meleeEffect = Instantiate(目标受伤特效, 随机目标.transform.position, Quaternion.identity);
                    Destroy(meleeEffect.gameObject, 受伤特效存在时长);
                }
                执行次数++;
            }
        }
    }
    public void 触发技能伤害()
    {
        if (技能是否对我方使用)
        {
            if (给友方回复生命)
            {
                if (对单个生效)
                {
                    float 造成伤害 = GetComponent<A1>().攻击力 * 技能倍率;
                    this.攻击目标.GetComponent<A1>().回复生命(造成伤害);
                    this.攻击目标.GetComponent<A1>().生成效果文本("回复生命", Color.green);
                }
                else
                if (对目标和两个随机对象生效)
                {
                    float 造成伤害 = GetComponent<A1>().攻击力 * 技能倍率;
                    this.攻击目标.GetComponent<A1>().回复生命(造成伤害);
                    this.攻击目标.GetComponent<A1>().生成效果文本("回复生命", Color.green);

                    if (this.攻击目标.GetComponent<A1>().是否是敌方)
                    {
                        目标队列 = B1.Instance.敌方队列;
                        StartCoroutine(延迟回复(造成伤害 * 0.7f));
                    }
                    if (this.攻击目标.GetComponent<A1>().是否是敌方 == false)
                    {
                        目标队列 = B1.Instance.我方队列;
                        StartCoroutine(延迟回复(造成伤害 * 0.7f));
                    }
                }
                else               
                if (对目标队列全体生效)
                {
                    float 造成伤害 = GetComponent<A1>().攻击力 * 技能倍率;
                    Transform parentTransform = this.攻击目标.transform.parent;
                    // 遍历所有子级游戏对象
                    foreach (Transform childTransform in parentTransform)
                    {
                        GameObject childObject = childTransform.gameObject;
                        childObject.GetComponent<A1>().回复生命(造成伤害);
                        childObject.GetComponent<A1>().生成效果文本("回复生命", Color.green);
                        if (目标受伤特效 != null)
                        {
                            GameObject meleeEffect = Instantiate(目标受伤特效, childObject.transform.position, Quaternion.identity);
                            Destroy(meleeEffect.gameObject, 受伤特效存在时长);
                        }
                    }
                }
            }
            if (让友方立即行动)
            {
                if (对单个生效 && 攻击目标 != this.gameObject)
                {
                    // 自己的行动条
                    Transform targetTransform = gameObject.GetComponent<A1>().自己对应的行动条.transform;
                    // 目标的行动条
                    Transform objectToMove = 攻击目标.GetComponent<A1>().自己对应的行动条.transform;

                    if (objectToMove != null && targetTransform != null)
                    {
                        if (targetTransform != null)
                        {
                            // 获取目标对象的层级排列位置
                            int targetSiblingIndex = targetTransform.GetSiblingIndex();

                            // 将需要移动的对象设置到目标对象的后方（索引 + 1 的位置）
                            objectToMove.SetSiblingIndex(targetSiblingIndex + 1);
                            this.攻击目标.GetComponent<A1>().生成效果文本("立即行动", Color.red);
                        }
                    }
                }
            }
            if (给友方回增加护盾)
            {
                if (对单个生效)
                {
                    float 造成伤害 = GetComponent<A1>().攻击力 * 技能倍率;
                    this.攻击目标.GetComponent<A1>().增加护盾(造成伤害);
                    this.攻击目标.GetComponent<A1>().生成效果文本("增加护盾", Color.yellow);
                }
            }
            if (让友方解除控制)
            {
                if (对单个生效)
                {
                    this.攻击目标.GetComponent<A1>().恢复破韧();
                    this.攻击目标.GetComponent<A1>().生成效果文本("解除控制", Color.red);
                    this.攻击目标.GetComponent<A4>().剩余被强控回合数 = 0;
                }
            }
            if (给友方增加攻击力)
            {
                if (对单个生效)
                {
                    this.攻击目标.GetComponent<A1>().生成效果文本("攻击力提升", Color.red);
                    给目标增加攻击buff(this.攻击目标);
                }
            }
            if (让友方行动上升)
            {
                Transform objectToMove = this.攻击目标.GetComponent<A1>().自己对应的行动条.transform;
                if (objectToMove != null)
                {
                    // 获取目标对象的层级排列位置
                    int targetSiblingIndex = objectToMove.GetSiblingIndex();
                    // 将目标对象向上移动指定的层级位置
                    if(targetSiblingIndex-行动上升多少>1)
                    objectToMove.SetSiblingIndex(targetSiblingIndex - 行动上升多少);
                    else
                    objectToMove.SetSiblingIndex(1);
                    this.攻击目标.GetComponent<A1>().生成效果文本("行动加快", Color.red);
                }
            }
        }
        else
        {
            if (对敌方造成伤害)
            {
                if (对单个生效)
                {
                    float 造成伤害 = GetComponent<A1>().攻击力 * 技能倍率;
                    this.攻击目标.GetComponent<A1>().发动技能攻击(this.攻击目标, 造成伤害, 技能攻击属性);
                    if (目标受伤特效 != null)
                    {
                        GameObject meleeEffect = Instantiate(目标受伤特效, this.攻击目标.transform.position, Quaternion.identity);
                        Destroy(meleeEffect.gameObject, 受伤特效存在时长);
                    }
                }
                else
                if (对目标队列全体生效)
                {
                    float 造成伤害 = GetComponent<A1>().攻击力 * 技能倍率;
                    Transform parentTransform = this.攻击目标.transform.parent;
                    // 遍历所有子级游戏对象
                    foreach (Transform childTransform in parentTransform)
                    {
                        GameObject childObject = childTransform.gameObject;
                        childObject.GetComponent<A1>().发动技能攻击(childObject, 造成伤害, 技能攻击属性);
                        if (目标受伤特效 != null)
                        {
                            GameObject meleeEffect = Instantiate(目标受伤特效, childObject.transform.position, Quaternion.identity);
                            Destroy(meleeEffect.gameObject, 受伤特效存在时长);
                        }
                    }
                }
                else
                if (对目标和左右对象生效)
                {
                    float 造成伤害 = GetComponent<A1>().攻击力 * 技能倍率;
                    Transform parentTransform = this.攻击目标.transform.parent;
                    this.攻击目标.GetComponent<A1>().发动技能攻击(this.攻击目标, 造成伤害, 技能攻击属性);
                    if (目标受伤特效 != null)
                    {
                        GameObject meleeEffect = Instantiate(目标受伤特效, this.攻击目标.transform.position, Quaternion.identity);
                        Destroy(meleeEffect.gameObject, 受伤特效存在时长);
                    }
                    if (parentTransform != null)
                    {
                        int siblingIndex = this.攻击目标.transform.GetSiblingIndex();

                        int aboveIndex = siblingIndex - 1;
                        int belowIndex = siblingIndex + 1;

                        Transform aboveObject = aboveIndex >= 0 ? parentTransform.GetChild(aboveIndex) : null;
                        Transform belowObject = belowIndex < parentTransform.childCount ? parentTransform.GetChild(belowIndex) : null;
                        if (aboveObject != null)
                        {
                            aboveObject.GetComponent<A1>().发动攻击(aboveObject.gameObject, 造成伤害 * 左右目标受到伤害比例, 技能攻击属性);
                            if (目标受伤特效 != null)
                            {
                                GameObject meleeEffect1 = Instantiate(目标受伤特效, aboveObject.transform.position, Quaternion.identity);
                                Destroy(meleeEffect1.gameObject, 受伤特效存在时长);
                            }
                        }
                        if (belowObject != null)
                        {
                            belowObject.GetComponent<A1>().发动攻击(belowObject.gameObject, 造成伤害 * 左右目标受到伤害比例, 技能攻击属性);
                            if (目标受伤特效 != null)
                            {
                                GameObject meleeEffect2 = Instantiate(目标受伤特效, belowObject.transform.position, Quaternion.identity);
                                Destroy(meleeEffect2.gameObject, 受伤特效存在时长);
                            }
                        }

                    }
                }
                else
                if (对目标和两个随机对象生效)
                {
                    float 造成伤害 = GetComponent<A1>().攻击力 * 技能倍率;
                    this.攻击目标.GetComponent<A1>().发动技能攻击(攻击目标, 造成伤害, 技能攻击属性);
                    延迟伤害(this.攻击目标, 造成伤害 * 0.5f, 技能攻击属性);
                    if (目标受伤特效 != null)
                    {
                        GameObject meleeEffect = Instantiate(目标受伤特效, this.攻击目标.transform.position, Quaternion.identity);
                        Destroy(meleeEffect.gameObject, 受伤特效存在时长);
                    }
                    if (this.攻击目标.GetComponent<A1>().是否是敌方)
                    {
                        目标队列 = B1.Instance.敌方队列;
                        StartCoroutine(延迟伤害(this.攻击目标, 造成伤害 * 0.5f, 技能攻击属性));
                    }
                    if (this.攻击目标.GetComponent<A1>().是否是敌方 == false)
                    {
                        目标队列 = B1.Instance.我方队列;
                        StartCoroutine(延迟伤害(this.攻击目标, 造成伤害 * 0.5f, 技能攻击属性));
                    }
                }
            }

        }
    }
    void 给目标添加效果()
    {
       if(技能是否对我方使用==false){
            if (让敌方防御力降低)
            {
                this.攻击目标.GetComponent<A1>().生成效果文本("防御降低", Color.red);
                给目标降低防御力(this.攻击目标);
            }
            if (让敌方行动降低)
            {
                让目标行动后退多少(this.攻击目标, 行动降低多少);
                this.攻击目标.GetComponent<A1>().生成效果文本("行动延迟", Color.red);
            }
            if (概率强控敌方)
            {
                if (Random.Range(0f, 1f) < 概率)
                {
                    this.攻击目标.GetComponent<A1>().生成效果文本(B1.Instance.强控名称, new Color(0.5f, 0.5f, 1f));
                    //让目标行动后退多少(this.攻击目标, 4);
                    给目标添加强控buff(攻击目标);
                }
            }
            if (让敌方持续受到伤害A)
            {
                //Debug.Log("1");
                给目标添加持续伤害A(攻击目标, 持续伤害破韧量);
                this.攻击目标.GetComponent<A1>().持续伤害A颜色 = this.攻击目标.GetComponent<A1>().文本颜色;
                this.攻击目标.GetComponent<A1>().自己受到的持续伤害属性 = GetComponent<A1>().属性;
                this.攻击目标.GetComponent<A1>().生成效果文本(B1.Instance.持续伤害A名称, this.攻击目标.GetComponent<A1>().持续伤害A颜色);
            }
            if (让敌方持续受到伤害B)
            {
                //Debug.Log("2");
                给目标添加持续伤害B(攻击目标, 持续伤害破韧量);
                this.攻击目标.GetComponent<A1>().持续伤害B颜色 = this.攻击目标.GetComponent<A1>().文本颜色;
                this.攻击目标.GetComponent<A1>().自己受到的持续伤害属性 = GetComponent<A1>().属性;
                this.攻击目标.GetComponent<A1>().生成效果文本(B1.Instance.持续伤害B名称, this.攻击目标.GetComponent<A1>().持续伤害B颜色);
            }
            if (GetComponent<A6>() != null)
            {
                if (GetComponent<A6>().处于强化状态中)
                    GetComponent<A6>().减少我方全体生命值();
            }
       }
    }
    IEnumerator 触发技能伤害多次()
    {
        B2.Instance.当前攻击目标.GetComponent<A1>().当前必杀技能量 += 10;
        GetComponent<A1>().当前必杀技能量 += 20;
        //Debug.Log("1");
        for (int i = 0; i < 技能造成伤害次数; i++)
        {
            yield return new WaitForSeconds(技能造成多次伤害间隔数);
            触发技能伤害();
        }
        给目标添加效果();
    }
    IEnumerator 触发普攻伤害多次(GameObject 目标, string 攻击属性)
    {
        目标.GetComponent<A1>().当前必杀技能量 += 5;
        GetComponent<A1>().当前必杀技能量 += 10;
        for (int i = 0; i < 普攻造成伤害次数; i++)
        {
            yield return new WaitForSeconds(i * 普攻造成多次伤害间隔);
            float 造成伤害 = GetComponent<A1>().攻击力 * 普攻伤害倍率;
            目标.GetComponent<A1>().发动攻击(目标, 造成伤害, 攻击属性);
            if (目标受伤特效 != null)
            {
                GameObject meleeEffect = Instantiate(目标受伤特效, 目标.transform.position, Quaternion.identity);
                Destroy(meleeEffect.gameObject, 受伤特效存在时长);
            }
        }
    }
    private IEnumerator 触发蓄力技伤害多次(GameObject 目标, string 攻击属性, int 次数, float 间隔时间, float 造成伤害)
    {
        目标.GetComponent<A1>().当前必杀技能量 += 10;
        GetComponent<A1>().当前必杀技能量 += 20;
        for (int i = 0; i < 次数; i++)
        {
            yield return new WaitForSeconds(间隔时间);
            if (蓄力技对单个生效)
            {
                目标.GetComponent<A1>().发动蓄力攻击(目标, 造成伤害, 攻击属性);
            }
            if (蓄力技是否对目标队列全体生效)
            {
                //Debug.Log("1");
                Transform parentTransform = 目标.transform.parent;

                foreach (Transform childTransform in parentTransform)
                {
                    GameObject childObject = childTransform.gameObject;
                    childObject.GetComponent<A1>().发动蓄力攻击(childObject, 造成伤害, 攻击属性);
                }
            }
            if (蓄力技是否对目标和左右对象生效)
            {
                Transform parentTransform = 目标.transform.parent;
                目标.GetComponent<A1>().发动蓄力攻击(目标, 造成伤害, 攻击属性);

                if (parentTransform != null)
                {
                    int siblingIndex = this.攻击目标.transform.GetSiblingIndex();

                    int aboveIndex = siblingIndex - 1;
                    int belowIndex = siblingIndex + 1;

                    Transform aboveObject = aboveIndex >= 0 ? parentTransform.GetChild(aboveIndex) : null;
                    Transform belowObject = belowIndex < parentTransform.childCount ? parentTransform.GetChild(belowIndex) : null;
                    if (aboveObject != null)
                        aboveObject.GetComponent<A1>().发动蓄力攻击(aboveObject.gameObject, 造成伤害 * 蓄力技左右目标受到伤害比例, 攻击属性);
                    if (belowObject != null)
                        belowObject.GetComponent<A1>().发动蓄力攻击(belowObject.gameObject, 造成伤害 * 蓄力技左右目标受到伤害比例, 攻击属性);
                }
            }
            if (目标受伤特效 != null)
            {
                GameObject meleeEffect = Instantiate(目标受伤特效, this.攻击目标.transform.position, Quaternion.identity);
                Destroy(meleeEffect.gameObject, 受伤特效存在时长);
            }
        }
        // 延迟0.2秒结束函数
        //yield return new WaitForSeconds(0.2f);

        蓄力中 = false;
        // 其他结束函数的操作...
    }
    public void 播放技能音效()
    {
        if (技能击中音效.Length > 0)
        {
            int 随机索引 = UnityEngine.Random.Range(0, 技能击中音效.Length);
            AudioClip 随机音效 = 技能击中音效[随机索引];
            音效播放器.clip = 随机音效;
            音效播放器.Play();
        }
    }

    // 播放随机的普攻音效
    public void 播放普攻音效()
    {
        if (普攻击中音效.Length > 0)
        {
            int 随机索引 = UnityEngine.Random.Range(0, 普攻击中音效.Length);
            AudioClip 随机音效 = 普攻击中音效[随机索引];
            音效播放器.clip = 随机音效;
            音效播放器.Play();
        }
    }
    public void 减少所有Buff剩余回合()
    {
        //Debug.Log("11");
        if (剩余被强控回合数 > 0)
            剩余被强控回合数 -= 1;
        if (剩余持续伤害A回合数 > 0 && !GetComponent<A1>().死亡)
        {
            剩余持续伤害A回合数 -= 1;
            GetComponent<A1>().受到持续伤害(持续受到伤害量, GetComponent<A1>().自己受到的持续伤害属性, GetComponent<A1>().受到的持续伤害破韧量);
            GetComponent<A1>().生成效果文本(B1.Instance.持续伤害A名称, GetComponent<A1>().持续伤害A颜色);
            if (B1.Instance.造成持续伤害A时的特效 != null)
            {
                GameObject meleeEffect = Instantiate(B1.Instance.造成持续伤害A时的特效, transform.position, Quaternion.identity);
                Destroy(meleeEffect.gameObject, 0.2f);
            }
        }
        if (剩余持续伤害B回合数 > 0 && !GetComponent<A1>().死亡)
        {
            剩余持续伤害B回合数 -= 1;
            GetComponent<A1>().受到持续伤害(持续受到伤害量, GetComponent<A1>().自己受到的持续伤害属性, GetComponent<A1>().受到的持续伤害破韧量);
            GetComponent<A1>().生成效果文本(B1.Instance.持续伤害B名称, GetComponent<A1>().持续伤害B颜色);
            if (B1.Instance.造成持续伤害B时的特效 != null)
            {
                GameObject meleeEffect = Instantiate(B1.Instance.造成持续伤害B时的特效, transform.position, Quaternion.identity);
                Destroy(meleeEffect.gameObject, 0.2f);
            }
        }
        if (剩余攻击力提升回合数 > 0)
            剩余攻击力提升回合数 -= 1;
        if (剩余防御力降低回合数 > 0)
            剩余防御力降低回合数 -= 1;
        if (GetComponent<A1>().是否是特色召唤物 && GetComponent<A1>().特色召唤物剩余回合 > 0)
            GetComponent<A1>().特色召唤物剩余回合 -= 1;

    }
    void 让所有buff剩余回合刷新()
    {
        if (生成的强控buff图标 != null)
        {
            Image 强控buff背景 = 生成的强控buff图标.GetComponent<Image>();
            var js = 强控buff背景.transform.Find("Text");
            Text 强控剩余 = js.GetComponent<Text>();
            强控剩余.text = 剩余被强控回合数.ToString();
        }
        if (生成的攻击力提升buff图标 != null)
        {
            Image 攻击力提升buff背景 = 生成的攻击力提升buff图标.GetComponent<Image>();
            var js = 攻击力提升buff背景.transform.Find("Text");
            Text 攻击力提升剩余 = js.GetComponent<Text>();
            攻击力提升剩余.text = 剩余攻击力提升回合数.ToString();

        }
        if (生成的防御力降低buff图标 != null)
        {
            Image 防御力降低buff背景 = 生成的防御力降低buff图标.GetComponent<Image>();
            var js = 防御力降低buff背景.transform.Find("Text");
            Text 防御力降低剩余 = js.GetComponent<Text>();
            防御力降低剩余.text = 剩余防御力降低回合数.ToString();

        }
        if (生成的持续灼伤伤害buff图标 != null)
        {
            Image 持续灼伤buff背景 = 生成的持续灼伤伤害buff图标.GetComponent<Image>();
            var js = 持续灼伤buff背景.transform.Find("Text");
            Text 持续灼伤剩余 = js.GetComponent<Text>();
            持续灼伤剩余.text = 剩余持续伤害A回合数.ToString();
        }
        if (生成的持续触电伤害buff图标 != null)
        {
            Image 持续触电buff背景 = 生成的持续触电伤害buff图标.GetComponent<Image>();
            var js = 持续触电buff背景.transform.Find("Text");
            Text 持续触电剩余 = js.GetComponent<Text>();
            持续触电剩余.text = 剩余持续伤害B回合数.ToString();
        }
        if (生成的特色召唤物buff图标 != null)
        {
            Image 特色召唤物buff背景 = 生成的特色召唤物buff图标.GetComponent<Image>();
            var js = 特色召唤物buff背景.transform.Find("Text");
            Text 特色召唤物剩余 = js.GetComponent<Text>();
            特色召唤物剩余.text = 生成的特色召唤物.GetComponent<A1>().特色召唤物剩余回合.ToString();
            if (生成的特色召唤物.GetComponent<A1>().特色召唤物剩余回合 == 0)
            {
                Destroy(生成的特色召唤物buff图标);
                生成的特色召唤物.GetComponent<A1>().延迟删除(0.8f);
            }
        }
    }
}
