using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
public class B2 : Singleton<B2>
{
    private Vector3 初始位置; // 初始位置
    private Quaternion 初始旋转角度; // 初始旋转角度
    private GameObject 玩家队列; // 用于寻找玩家队列
    private GameObject NPC队列;
    public GameObject 当前行动角色;
    public Transform 当前攻击目标; // 当前目标玩家
    [HideInInspector]
    public Animator 当前行动角色动画; // 动画控制器
    [HideInInspector]
    public bool isMoving = false; // 是否正在移动
    [HideInInspector]
    public bool isAttacking = false; // 是否正在攻击
    [HideInInspector]
    public bool 开始返回;

    public GameObject[] playerCharacters; // 我方角色队列
    public GameObject[] enemyCharacters; // 敌方角色队列

    [Header("摄像机")]
    public CinemachineVirtualCamera 我方摄像机;
    public CinemachineVirtualCamera NPC方摄像机;
    public CinemachineVirtualCamera 看向我方的摄像机;
    public CinemachineVirtualCamera 看向敌方特色召唤物的摄像机;

    [Header("按钮")]
    public GameObject 普攻按钮;
    public GameObject 技能按钮;
    public GameObject 召唤按钮;
    public GameObject 蓄力按钮;
    public Transform 按钮生成位置;
    Button 普通攻击按钮;
    Button 技能攻击按钮;
    Button 召唤技按钮;
    Button 蓄力技按钮;

    [Header("战斗结束图标")]
    public GameObject 战斗胜利图标;
    public GameObject 战斗失败图标;
    [Header("下面的不用管")]
        public Text 显示技能名称文本;
    public GameObject 指示箭头;
    public Transform 行动条父级;
        [HideInInspector]
    public bool 是否可以使用普攻 = false;
        [HideInInspector]
    public bool 是否可以使用技能 = false;
        [HideInInspector]
    private bool 是否可以使用召唤技 = false;
        [HideInInspector]
    public bool 是否可以使用蓄力技 = false;
        [HideInInspector]
    public bool 使用普攻 = false;
        [HideInInspector]
    private bool 使用技能 = false;
        [HideInInspector]
    public bool 技能 = false;
        [HideInInspector]
    public bool 使用召唤 = false;
        [HideInInspector]
    public bool 使用蓄力 = false;
        [HideInInspector]
    public bool 保存初始位置;
        [HideInInspector]
    private Vector3 普攻按钮初始大小;
    private Vector3 技能按钮初始大小;
    private Vector3 召唤按钮初始大小;
    private Vector3 蓄力按钮初始大小;
    public int NPC剩余人数;
        [HideInInspector]
    public bool NPC是否使用普攻;//NPC随机攻击
        [HideInInspector]
    public bool NPC是否使用技能;
        [HideInInspector]
    public bool NPC是否使用蓄力;
        [HideInInspector]
    public bool NPC是否使用召唤;
        [HideInInspector]
    public bool 是否继续使用蓄力;
        [HideInInspector]
    public string 释放蓄力的NPC;
    public int 玩家剩余人数;

        [HideInInspector]
    public GameObject 生成的指示箭头;
    [HideInInspector]
    public bool 可以生成按钮 = true;
    bool 选择当前行动角色 = true;
        [HideInInspector]
    public Transform 行动条排行第二的对象;
        [HideInInspector]
    public bool 有人死亡;
    bool b = true;
    bool 初次显示按钮 = true;
    enum Action { 普攻, 技能, 蓄力技, 召唤技 };
    Action NPC当前行动;
    private void Start()
    {
        按钮生成位置 = transform.Find("按钮");
        玩家队列 = GameObject.Find("我方队列"); // 找到玩家队列
        NPC队列 = GameObject.Find("敌方队列"); // 找到敌方队列
    }



    private void Update()
    {
        NPC剩余人数 = 0;
        
         玩家剩余人数 = 0;
        foreach (Transform child in NPC队列.transform)
        {
            A1 script = child.GetComponent<A1>();
            if (script != null && !script.死亡)
                NPC剩余人数++;
        }
        foreach (Transform child in 玩家队列.transform)
        {
            A1 script = child.GetComponent<A1>();
            if (script != null && !script.死亡)
                玩家剩余人数++;
        }

        //Debug.Log("NPC队列中还活着的NPC数量：" + NPC剩余人数);
        //Debug.Log("玩家队列中还活着的玩家数量：" + 玩家剩余人数);
        if (NPC剩余人数 > 0 && 玩家剩余人数 > 0)
        {
            选择行动角色();
            if (选择当前行动角色 == false&&当前行动角色!=null)
                if (当前行动角色.GetComponent<A1>().是否是敌方)
                    如果行动的是敌方();
                else
                    如果行动的是我方();
        }

        else if (NPC剩余人数 <= 0 || 玩家剩余人数 <= 0)
        {
            // B3.Instance.根据双方剩余人数显示输赢();
            Invoke("显示输赢", 1.5f);

        }
        
    }
    private void 显示输赢()
    {
        if (NPC剩余人数 <= 0)
            战斗胜利图标.SetActive(true);

        if (玩家剩余人数 <= 0)
            战斗失败图标.SetActive(true);
    }
    void 选择行动角色()
    {
        if (选择当前行动角色)
        {
            b = true;
            var a = FindTopObject(行动条父级);
            if (a != null)
                当前行动角色 = a.GetComponent<A2>().行动条对应角色;
            当前行动角色动画 = 当前行动角色.GetComponent<Animator>(); // 获取动画控制器组件
            初始位置 = 当前行动角色.transform.position; // 记录初始位置
            //Debug.Log(初始位置);
            初始旋转角度 = 当前行动角色.transform.rotation; // 记录初始旋转角度

            //Debug.Log(初始位置);
            Cursor.lockState = CursorLockMode.None; // 可见时，解锁鼠标
            if (当前行动角色 != null&&b)
            {
                A1 当前行动角色的A1 = 当前行动角色.GetComponent<A1>();
                if (当前行动角色的A1.是否是敌方)
                {
                    //Debug.Log("当前行动的是NPC");
                    当前攻击目标 = null;
                    if ((当前行动角色的A1.破韧) || 当前行动角色的A1.是否被强控)
                    {
                        移动当前行动角色的行动条到最底部();
                        b = false;
                    }
                    else
                    {
                        当前行动角色 = a.GetComponent<A2>().行动条对应角色;
                        当前行动角色动画 = 当前行动角色.GetComponent<Animator>(); // 获取动画控制器组件

                        if (生成的指示箭头 != null)
                            Destroy(生成的指示箭头);

                        var _A4 = 当前行动角色.GetComponent<A4>();
                        bool 是否可以使用召唤技;
                        if (NPC剩余人数 <= 4 && _A4.是否可以使用召唤技&& _A4.生成的特色召唤物==null)
                        {
                            是否可以使用召唤技 = true;
                        }else
                            是否可以使用召唤技 = false;
                        bool[] actions = new bool[]
                            {
                         _A4.是否可以使用普攻,
                         _A4.是否可以使用技能,
                         _A4.是否可以使用蓄力技,
                         是否可以使用召唤技
                            };

                            // 统计可以行动的总数
                            int trueCount = actions.Count(action => action == true);
                            // 根据NPC当前行动设置对应的bool变量
                            if (trueCount == 1)
                                NPC当前行动 = (Action)Array.IndexOf(actions, true);
                            else if (trueCount > 1)
                            {
                                List<int> trueIndices = new List<int>();
                                for (int i = 0; i < actions.Length; i++)
                                {
                                    if (actions[i]) // 如果这个行动是可用的（即对应的bool为true）
                                    {
                                        trueIndices.Add(i); // 就将其索引添加到列表中
                                    }
                                }
                                int randomIndex = UnityEngine.Random.Range(0, trueIndices.Count); // 从所有可用的索引中随机选择一个
                                NPC当前行动 = (Action)trueIndices[randomIndex];// 根据这个随机索引设置NPC的当前行动
                            }
                        
                        if (当前行动角色.GetComponent<A4>().蓄力中)
                        {
                            NPC是否使用蓄力 = true;
                        }

                        else
                        {
                            NPC是否使用普攻 = NPC当前行动 == Action.普攻;
                            NPC是否使用技能 = NPC当前行动 == Action.技能;
                            NPC是否使用召唤 = NPC当前行动 == Action.召唤技;
                            NPC是否使用蓄力 = NPC当前行动 == Action.蓄力技;
                        }
                        if (当前攻击目标 == null)
                        {
                            if (玩家剩余人数 > 0)
                            {
                                if (当前行动角色.GetComponent<A4>().技能是否对我方使用 == false & NPC是否使用普攻 == false)
                                {
                                    if (当前行动角色.GetComponent<A4>().对目标队列全体生效)
                                    {                // 计算中间位置的索引
                                        int middleIndex = 玩家剩余人数 / 2;

                                        // 获取中间位置的子对象
                                        Transform middleChild = 玩家队列.transform.GetChild(middleIndex);

                                        当前攻击目标 = middleChild;
                                    }
                                    else
                                    {
                                        // 随机选择一个子对象
                                        int randomIndex = UnityEngine.Random.Range(0, 玩家剩余人数);
                                        Transform randomChild = 玩家队列.transform.GetChild(randomIndex);
                                        当前攻击目标 = randomChild;
                                    }
                                }
                                else
                                if (当前行动角色.GetComponent<A4>().技能是否对我方使用 && NPC是否使用普攻 == false)
                                {
                                    A1[] a1Scripts = NPC队列.GetComponentsInChildren<A1>();
                                    float minHealth = float.MaxValue;
                                    GameObject minHealthObject = null;
                                    // 遍历所有的A1脚本
                                    foreach (A1 a1 in a1Scripts)
                                    {
                                        // 获取A1脚本中的当前生命值
                                        float currentHealth = a1.当前生命值;
                                        float 最大生命值 = a1.最大生命值;
                                        float 当前生命百分比 = currentHealth / 最大生命值;
                                        // 检查是否是最小生命值
                                        if (当前生命百分比 < minHealth)
                                        {
                                            minHealth = 当前生命百分比;
                                            minHealthObject = a1.gameObject;
                                        }
                                    }
                                    当前攻击目标 = minHealthObject.transform;
                                }
                                else
                                if (NPC是否使用普攻)
                                {
                                    // 随机选择一个子对象
                                    int randomIndex = UnityEngine.Random.Range(0, 玩家剩余人数);
                                    Transform randomChild = 玩家队列.transform.GetChild(randomIndex);
                                    当前攻击目标 = randomChild;
                                }

                            }
                        }
                        if (当前行动角色.GetComponent<A1>().是否是特色召唤物 == false)
                        {
                            我方摄像机.gameObject.SetActive(false);
                            NPC方摄像机.gameObject.SetActive(true);
                            NPC方摄像机.Follow = 当前攻击目标.transform;
                            NPC方摄像机.LookAt = 当前行动角色.transform;
                            看向敌方特色召唤物的摄像机.gameObject.SetActive(false);
                        }
                        else
                        {
                            看向敌方特色召唤物的摄像机.gameObject.SetActive(true);
                            看向敌方特色召唤物的摄像机.LookAt = 当前行动角色.transform;
                        }
                    }
                }
                else if (当前行动角色的A1.是否是敌方 == false)
                {
                    //Debug.Log("当前行动的是玩家");
                    if ((当前行动角色的A1.破韧 || 当前行动角色的A1.是否被强控))
                    {
                        移动当前行动角色的行动条到最底部();
                    }
                    else
                    {
                        当前行动角色 = a.GetComponent<A2>().行动条对应角色;
                        当前行动角色动画 = 当前行动角色.GetComponent<Animator>(); // 获取动画控制器组件
                        我方摄像机.gameObject.SetActive(true);
                        NPC方摄像机.gameObject.SetActive(false);
                            我方摄像机.Follow = 当前行动角色.transform;
                        保存初始位置 = true;

                        if (当前攻击目标 == null)
                        {
                            int childCount = NPC队列.transform.childCount;

                            if (childCount > 0)
                            {
                                // 随机选择一个子对象
                                int randomIndex = UnityEngine.Random.Range(0, childCount);
                                Transform randomChild = NPC队列.transform.GetChild(randomIndex);
                                当前攻击目标 = randomChild;
                                if (randomChild.transform.Find("快捷角色设置") != null)
                                {
                                    //Debug.Log("11");
                                    我方摄像机.LookAt = randomChild.transform.Find("快捷角色设置").transform.Find("摄像机看向位置");
                                    当前行动角色.transform.LookAt(当前攻击目标.transform);
                                }

                            }
                        }
                        if (!当前攻击目标.GetComponent<A1>().是否是敌方)
                            选择目标队列中间对象作为攻击目标(NPC队列);
                    }
                    if (生成的指示箭头 == null)
                    {
                        生成的指示箭头 = Instantiate(指示箭头);
                    }
                    if (可以生成按钮)
                    {
                        if (初次显示按钮)
                        {
                            StartCoroutine(延迟生成按钮());
                            初次显示按钮 = false;
                        }
                        else
                            生成按钮();
                    }
                }
            }

        }
        选择当前行动角色 = false;
    }
    void 生成按钮()
    {
        if (当前行动角色.GetComponent<A4>().是否可以使用召唤技)
        {
            var zh = Instantiate(召唤按钮, 按钮生成位置);
            召唤技按钮 = zh.GetComponent<Button>();
        }
        if (当前行动角色.GetComponent<A4>().是否可以使用蓄力技)
        {
            var xl = Instantiate(蓄力按钮, 按钮生成位置);
            蓄力技按钮 = xl.GetComponent<Button>();
        }
        if (当前行动角色.GetComponent<A4>().是否可以使用技能)
        {
            var jn = Instantiate(技能按钮, 按钮生成位置);
            技能攻击按钮 = jn.GetComponent<Button>();
        }
        if (当前行动角色.GetComponent<A4>().是否可以使用普攻)
        {
            var pg = Instantiate(普攻按钮, 按钮生成位置);
            普通攻击按钮 = pg.GetComponent<Button>();
        }
        if (当前行动角色.GetComponent<A4>().蓄力中)
        {
            销毁攻击按钮();
            var xl = Instantiate(蓄力按钮, 按钮生成位置);
            蓄力技按钮 = xl.GetComponent<Button>();
        }
        保存按钮();
        可以生成按钮 = false;
    }
    private IEnumerator 延迟生成按钮()
    {
        yield return new WaitForSeconds(0.9f);
        生成按钮();
    }

    void 如果行动的是敌方()
    {

        if (NPC是否使用普攻)
        {
            if (当前行动角色.GetComponent<A4>().普攻是否移动到目标 == true)
            {
                if (!isMoving && !isAttacking)
                {                 // 面向目标玩家
                    当前行动角色.transform.LookAt(当前攻击目标);
                    当前行动角色动画.SetBool("准备释放普攻", true);
                    当前行动角色动画.SetBool("IsMoving", true);
                    Invoke("延迟移动向目标", 当前行动角色.GetComponent<A4>().如果普攻移动到目标播放动画多久开始移动);
                    //isMoving = true;
                }
                if (isMoving && !isAttacking && !开始返回)
                {

                    当前行动角色.transform.position = Vector3.MoveTowards(当前行动角色.transform.position, 当前攻击目标.position, 当前行动角色.GetComponent<A1>().移动向敌方的速度 * Time.deltaTime);
                    if (Vector3.Distance(当前行动角色.transform.position, 当前攻击目标.position) <= 当前攻击目标.GetComponent<A1>().被近战攻击时距离自身多远停下)
                    {
                        isMoving = false;
                        isAttacking = true;

                        当前行动角色动画.SetBool("IsMoving", false);
                        当前行动角色动画.SetBool("IsAttacking", true);

                        Invoke("造成普攻伤害", 当前行动角色.GetComponent<A4>().开始播放普攻攻击动画多久后造成伤害);
                        Invoke("播放普攻特效", 当前行动角色.GetComponent<A4>().开始播放普攻动画后多久后生成特效);
                        Invoke("AttackAnimationComplete", 当前行动角色.GetComponent<A4>().普攻动画时长);
                        显示技能或普攻名称文本(当前行动角色.GetComponent<A4>().普攻名称);
                    }
                }
            }
            else if (当前行动角色.GetComponent<A4>().普攻是否移动到目标 == false)
            {
                if (!isMoving && !isAttacking)
                {                 // 面向目标玩家
                    当前行动角色.transform.LookAt(当前攻击目标);
                    isMoving = true;
                }
                if (isMoving && !isAttacking && !开始返回)
                {
                    isMoving = false;
                    isAttacking = true;
                    当前行动角色动画.SetBool("IsAttacking", true);
                    Invoke("造成普攻伤害", 当前行动角色.GetComponent<A4>().开始播放普攻攻击动画多久后造成伤害);
                    Invoke("播放普攻特效", 当前行动角色.GetComponent<A4>().开始播放普攻动画后多久后生成特效);
                    Invoke("AttackAnimationComplete", 当前行动角色.GetComponent<A4>().普攻动画时长);
                    显示技能或普攻名称文本(当前行动角色.GetComponent<A4>().普攻名称);
                }
            }
        }
        // 使用技能
        else if (NPC是否使用技能)
        {
            //当前行动角色动画.SetBool("准备释放技能", true);
            if (当前行动角色.GetComponent<A4>().技能攻击是否是移动到目标 == true)
            {
                if (!isMoving && !isAttacking)
                {                 // 面向目标玩家
                    当前行动角色.transform.LookAt(当前攻击目标);

                    //isMoving = true;
                    当前行动角色动画.SetBool("IsMoving", true);
                    Invoke("延迟移动向目标", 当前行动角色.GetComponent<A4>().如果技能移动到目标播放动画多久开始移动);
                }
                if (isMoving && !isAttacking && !开始返回)
                {
                    当前行动角色.transform.position = Vector3.MoveTowards(当前行动角色.transform.position, 当前攻击目标.position, 当前行动角色.GetComponent<A1>().移动向敌方的速度 * Time.deltaTime);
                    if (Vector3.Distance(当前行动角色.transform.position, 当前攻击目标.position) <= 当前攻击目标.GetComponent<A1>().被近战攻击时距离自身多远停下)
                    {
                        isMoving = false;
                        isAttacking = true;
                        当前行动角色动画.SetBool("准备释放技能", true);
                        当前行动角色动画.SetBool("IsMoving", false);
                        当前行动角色动画.SetBool("释放技能", true);
                        Invoke("播放技能特效", 当前行动角色.GetComponent<A4>().开始播放技能动画后多久后生成特效);
                        Invoke("造成技能伤害", 当前行动角色.GetComponent<A4>().开始播放技能攻击动画多久后造成伤害);
                        Invoke("AttackAnimationComplete", 当前行动角色.GetComponent<A4>().技能动画时长);
                        显示技能或普攻名称文本(当前行动角色.GetComponent<A4>().技能名称);
                    }
                }
            }
            else if (当前行动角色.GetComponent<A4>().技能攻击是否是移动到目标 == false)
            {
                if (!isMoving && !isAttacking)
                {                 // 面向目标玩家
                    当前行动角色.transform.LookAt(当前攻击目标);
                    isMoving = true;
                }
                if (isMoving && !isAttacking && !开始返回 && 当前行动角色.GetComponent<A4>().技能是否对我方使用 == false)
                {
                    isAttacking = true;
                    当前行动角色动画.SetBool("准备释放技能", true);
                    当前行动角色动画.SetBool("释放技能", true);
                    Invoke("造成技能伤害", 当前行动角色.GetComponent<A4>().开始播放技能攻击动画多久后造成伤害);
                    Invoke("播放技能特效", 当前行动角色.GetComponent<A4>().开始播放技能动画后多久后生成特效);
                    Invoke("AttackAnimationComplete", 当前行动角色.GetComponent<A4>().技能动画时长);
                    显示技能或普攻名称文本(当前行动角色.GetComponent<A4>().技能名称);
                }
                if (isMoving && !isAttacking && !开始返回 && 当前行动角色.GetComponent<A4>().技能是否对我方使用 == true)
                {
                    isAttacking = true;
                    当前行动角色动画.SetBool("释放技能", true);
                    Invoke("造成技能伤害", 当前行动角色.GetComponent<A4>().开始播放技能攻击动画多久后造成伤害);
                    Invoke("播放技能特效", 当前行动角色.GetComponent<A4>().开始播放技能动画后多久后生成特效);
                    Invoke("AttackAnimationComplete", 当前行动角色.GetComponent<A4>().技能动画时长);
                    显示技能或普攻名称文本(当前行动角色.GetComponent<A4>().技能名称);
                }
            }
        }
        else if (NPC是否使用召唤)
        {
            if (!isMoving && !isAttacking)
            {                 // 面向目标玩家
                当前行动角色.transform.LookAt(当前攻击目标);
                isMoving = true;
            }
            if (isMoving && !isAttacking && b )
            {
                isMoving = false;
                使用召唤=true;
                当前行动角色动画.SetBool("召唤", true);
                Invoke("使用召唤技", 当前行动角色.GetComponent<A4>().开始播放召唤动画后多久后开始召唤);
                Invoke("AttackAnimationComplete", 当前行动角色.GetComponent<A4>().召唤动画时长);
                显示技能或普攻名称文本(当前行动角色.GetComponent<A4>().召唤技名称);
                b = false;
            }
        }
        else if (NPC是否使用蓄力)
        {
            if (!isMoving  && b)
            {                 // 面向目标玩家
                初始位置 = 当前行动角色.transform.position; // 记录初始位置
                当前行动角色.transform.LookAt(当前攻击目标);
                Invoke("延迟移动向目标", 当前行动角色.GetComponent<A4>().如果蓄力移动到目标播放动画多久开始移动);
                if (当前行动角色.GetComponent<A4>().蓄力中)
                    显示技能或普攻名称文本(当前行动角色.GetComponent<A4>().蓄力技完成名称);
                else
                    显示技能或普攻名称文本(当前行动角色.GetComponent<A4>().蓄力技名称);
            }
            if (!当前行动角色.GetComponent<A4>().蓄力技是否移动到目标释放 && isAttacking == false&&isMoving)
            {
                if (当前行动角色.GetComponent<A4>().蓄力中 == false && b)
                {
                    当前行动角色动画.SetBool("蓄力", true);
                    Invoke("使用蓄力技", 当前行动角色.GetComponent<A4>().开始播放蓄力动画后多久后造成伤害);
                    Invoke("AttackAnimationComplete", 当前行动角色.GetComponent<A4>().蓄力动画时长);
                    isAttacking = true;
                    b = false;
                }
                else if (当前行动角色.GetComponent<A4>().蓄力中 && b)
                {
                    当前行动角色.GetComponent<A4>().释放蓄力技(当前攻击目标.gameObject, 当前行动角色.GetComponent<A1>().属性);
                    Invoke("造成蓄力伤害", 当前行动角色.GetComponent<A4>().开始播放蓄力动画后多久后造成伤害);
                    Invoke("AttackAnimationComplete", 当前行动角色.GetComponent<A4>().蓄力动画时长);
                    isAttacking = true;
                    b = false;
                    NPC是否使用蓄力 = false;
                }
            }
            else if (当前行动角色.GetComponent<A4>().蓄力技是否移动到目标释放 && isAttacking == false)
            {
                if (当前行动角色.GetComponent<A4>().蓄力中 == false && b)
                {
                    显示技能或普攻名称文本(当前行动角色.GetComponent<A4>().蓄力技名称);
                    isAttacking = true;
                    当前行动角色动画.SetBool("蓄力", true);
                    Invoke("使用蓄力技", 当前行动角色.GetComponent<A4>().开始播放蓄力动画后多久后造成伤害);
                    Invoke("AttackAnimationComplete", 当前行动角色.GetComponent<A4>().蓄力动画时长);
                    b = false;
                }
                else if (当前行动角色.GetComponent<A4>().蓄力中 && b)
                {
                    显示技能或普攻名称文本(当前行动角色.GetComponent<A4>().蓄力技完成名称);
                    当前行动角色.transform.position = Vector3.MoveTowards(当前行动角色.transform.position, 当前攻击目标.position, 当前行动角色.GetComponent<A1>().移动向敌方的速度 * Time.deltaTime);
                    if (Vector3.Distance(当前行动角色.transform.position, 当前攻击目标.position) <= 当前攻击目标.GetComponent<A1>().被近战攻击时距离自身多远停下)
                    {
                        isAttacking = true;
                        当前行动角色.GetComponent<A4>().释放蓄力技(当前攻击目标.gameObject, 当前行动角色.GetComponent<A1>().属性);
                        Invoke("造成蓄力伤害", 当前行动角色.GetComponent<A4>().开始播放蓄力动画后多久后造成伤害);
                        Invoke("AttackAnimationComplete", 当前行动角色.GetComponent<A4>().蓄力动画时长);
                        b = false;
                        NPC是否使用蓄力 = false;
                    }
                }

            }
        }
    }


    void 如果行动的是我方()
    {

        // 玩家选择敌方目标
        if (Input.GetMouseButtonDown(0)&&!isAttacking&&!isMoving) // 检测鼠标左键点击
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.gameObject.CompareTag("NPC"))
                {
                    GameObject targetObject = hitInfo.collider.gameObject;
                    if (targetObject.transform.Find("快捷角色设置") != null)
                    {
                        我方摄像机.LookAt = targetObject.transform.Find("快捷角色设置").transform.Find("摄像机看向位置");
                    }
                    else
                        我方摄像机.LookAt = targetObject.transform;
                    当前攻击目标 = targetObject.transform;
                    当前行动角色.transform.LookAt(targetObject.transform);
                    if (生成的指示箭头 != null)
                        生成的指示箭头.transform.position = 当前攻击目标.position + new Vector3(0, 3.5f, 0);
                }
            }
        }
        if (生成的指示箭头 != null && 当前攻击目标 != null)
            生成的指示箭头.transform.position = 当前攻击目标.position + new Vector3(0, 3.5f, 0);
        if (使用普攻)
        {
            if (当前行动角色.GetComponent<A4>().普攻是否移动到目标 == true)
            {
                近战普攻();
            }
        }
        if (是否可以使用技能)
        {
            当前行动角色动画.SetBool("准备释放技能", true);
            当前行动角色动画.SetBool("准备释放普攻", false);
            看向我方的摄像机.gameObject.SetActive(false);
            我方摄像机.gameObject.SetActive(true);
            使用技能 = true;

            if (技能)
                近战技能();
            if (当前行动角色.GetComponent<A4>().技能是否对我方使用 == true && 当前行动角色.GetComponent<A4>().技能攻击是否是移动到目标 == false)//如果对己方释放技能
            {
                看向我方的摄像机.gameObject.SetActive(true);
                我方摄像机.gameObject.SetActive(false);
                if (Input.GetMouseButtonDown(0)) // 检测鼠标左键点击
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hitInfo;
                    if (Physics.Raycast(ray, out hitInfo))
                    {
                        if (hitInfo.collider.gameObject.CompareTag("Player"))
                        {
                            GameObject targetObject = hitInfo.collider.gameObject;
                            看向我方的摄像机.LookAt = targetObject.transform;
                            当前攻击目标 = targetObject.transform;
                        }
                    }
                }

                if (看向我方的摄像机.LookAt == null)
                {
                    看向我方的摄像机.LookAt = 当前行动角色.transform;
                }

            }
        }
        if (是否可以使用召唤技)
        {
            使用召唤 = true;
            看向我方的摄像机.gameObject.SetActive(true);
            我方摄像机.gameObject.SetActive(false);
            看向我方的摄像机.LookAt = 当前行动角色.transform;
        }
        if (是否可以使用蓄力技)
        {
            看向我方的摄像机.gameObject.SetActive(false);
            我方摄像机.gameObject.SetActive(true);
            使用蓄力 = true;
        }
    }

    public void AttackAnimationComplete()
    {
        //if(当前行动角色.GetComponent<A1>().是否是敌方==false){
        //    我方摄像机.Follow=null;
        //    //我方摄像机.LookAt=null;
        //}

        当前行动角色动画.SetBool("IsAttacking", false);
        当前行动角色动画.SetBool("IsMoving", false);
        当前行动角色动画.SetBool("释放技能", false);
        当前行动角色动画.SetBool("准备释放技能", false);
        当前行动角色动画.SetBool("准备释放普攻", false);
        是否可以使用普攻 = false;
        是否可以使用技能 = false;
        是否可以使用召唤技 = false;
        技能 = false;
        使用普攻 = false;
        使用技能 = false;

        当前行动角色动画.SetBool("召唤", false);
        当前行动角色动画.SetBool("释放蓄力", false);
        是否可以使用蓄力技 = false;
        使用蓄力 = false;
        NPC是否使用普攻 = false;
        NPC是否使用技能 = false;
        isAttacking = false;
        NPC是否使用召唤=false;
        StartCoroutine(MoveBackToInitialPosition());
        if (当前行动角色.GetComponent<A4>().技能是否对我方使用)
            选择目标队列中间对象作为攻击目标(NPC队列);
    }        // 攻击动画播放完毕后的回调函数

    // 移动回初始位置的协程
    private IEnumerator MoveBackToInitialPosition()
    {
        var characterA1 = 当前行动角色.GetComponent<A1>();
        var characterA4 = 当前行动角色.GetComponent<A4>();
        var characterAnimator = 当前行动角色动画;
        if (characterA1.攻击后删除自身 && !characterA4.蓄力中)
        {
            characterA1.延迟删除(0.01f);
            StartCoroutine(DelayedExecutionCoroutine());

        }
        else
        {
            if (!使用召唤)
            {
                while (Vector3.Distance(当前行动角色.transform.position, 初始位置) > 0.2f)
                {
                    //Debug.Log(初始位置);
                    characterAnimator.SetBool("IsMoving", true);
                    MoveCharacterTowards(当前行动角色, 初始位置, 当前行动角色.GetComponent<A1>().返回的速度);

                    if (characterA1.返回是否转头)
                    {
                        FaceCharacterTowards(当前行动角色, 初始位置);
                    }
                    yield return null;
                }

                isMoving = false;
                characterAnimator.SetBool("IsMoving", false);
                ResetCharacterState(characterAnimator, 当前行动角色, 初始旋转角度);
            }
            else
            {
                使用召唤 = false;
                isAttacking = false;
                isMoving = false;
            }
            StartCoroutine(DelayedExecutionCoroutine3());
        }

    }

    private void MoveCharacterTowards(GameObject character, Vector3 targetPosition, float speed)
    {
        Vector3 direction = (targetPosition - character.transform.position).normalized;
        character.transform.position += direction * speed * Time.deltaTime;
    }

    private void FaceCharacterTowards(GameObject character, Vector3 targetPosition)
    {
        character.transform.LookAt(targetPosition);
    }

    private void ResetCharacterState(Animator animator, GameObject character, Quaternion initialRotation)
    {
        animator.SetBool("IsMoving", false);
        character.transform.position = 初始位置;
        character.transform.rotation = initialRotation;
        看向我方的摄像机.gameObject.SetActive(false);
    }

    private void 销毁攻击按钮()
    {
        // 销毁攻击按钮的逻辑
        if (普通攻击按钮 != null)
            Destroy(普通攻击按钮.gameObject);
        if (技能攻击按钮 != null)
            Destroy(技能攻击按钮.gameObject);
        if (召唤技按钮 != null)
            Destroy(召唤技按钮.gameObject);
        if (蓄力技按钮 != null)
            Destroy(蓄力技按钮.gameObject);
    }
    void 延迟移动向目标()
    {
        isMoving = true;
        //Debug.Log("66");
    }

    void 保存按钮()
    {
        // 保存初始按钮大小
        if (普通攻击按钮 != null)
        {
            普攻按钮初始大小 = 普通攻击按钮.transform.localScale;
            普通攻击按钮.onClick.AddListener(可以使用普攻);
        }
        if (技能攻击按钮 != null)
        {
            技能按钮初始大小 = 技能攻击按钮.transform.localScale;
            技能攻击按钮.onClick.AddListener(可以使用技能);
        }
        if (召唤技按钮 != null)
        {
            召唤按钮初始大小 = 召唤技按钮.transform.localScale;
            召唤技按钮.onClick.AddListener(可以使用召唤技);
        }
        if (蓄力技按钮 != null)
        {
            蓄力按钮初始大小 = 蓄力技按钮.transform.localScale;
            蓄力技按钮.onClick.AddListener(可以使用蓄力技);
        }
    }
    public void 近战普攻()
    {

        if (!isMoving && !isAttacking)
        {                 // 面向目标玩家
            初始位置 = 当前行动角色.transform.position; // 记录初始位置
            //Debug.Log(初始位置);
            当前行动角色.transform.LookAt(当前攻击目标);
            当前行动角色动画.SetBool("IsMoving", true);
           
            if (当前行动角色.GetComponent<A4>().普攻是否回复战技点)
                B1.Instance.增加战技点();
            显示技能或普攻名称文本(当前行动角色.GetComponent<A4>().普攻名称);
            Invoke("延迟移动向目标", 当前行动角色.GetComponent<A4>().如果普攻移动到目标播放动画多久开始移动);
        }
        else
        if (isMoving && !isAttacking && !开始返回)
        {
            当前行动角色.transform.position = Vector3.MoveTowards(当前行动角色.transform.position, 当前攻击目标.position, 当前行动角色.GetComponent<A1>().移动向敌方的速度 * Time.deltaTime);
            if (Vector3.Distance(当前行动角色.transform.position, 当前攻击目标.position) <= 当前攻击目标.GetComponent<A1>().被近战攻击时距离自身多远停下)
            {
                isMoving = false;
                isAttacking = true;
                当前行动角色动画.SetBool("IsMoving", false);
                当前行动角色动画.SetBool("IsAttacking", true);
                Invoke("造成普攻伤害", 当前行动角色.GetComponent<A4>().开始播放普攻攻击动画多久后造成伤害);
                Invoke("播放普攻特效", 当前行动角色.GetComponent<A4>().开始播放普攻动画后多久后生成特效);
                Invoke("AttackAnimationComplete", 当前行动角色.GetComponent<A4>().普攻动画时长);
            }

        }
    }
    public void 远程普攻()
    {
        if (!isMoving && !isAttacking)
        {                 // 面向目标玩家
            初始位置 = 当前行动角色.transform.position; // 记录初始位置
            当前行动角色.transform.LookAt(当前攻击目标);
            isMoving = true;
            显示技能或普攻名称文本(当前行动角色.GetComponent<A4>().普攻名称);
            if (当前行动角色.GetComponent<A4>().普攻是否回复战技点)
                B1.Instance.增加战技点();
        }
        if (isMoving && !isAttacking && !开始返回)
        {
            isMoving = false;
            当前行动角色动画.SetBool("IsAttacking", true);
            isAttacking = true;
            Invoke("造成普攻伤害", 当前行动角色.GetComponent<A4>().开始播放普攻攻击动画多久后造成伤害);
            Invoke("播放普攻特效", 当前行动角色.GetComponent<A4>().开始播放普攻动画后多久后生成特效);
            Invoke("AttackAnimationComplete", 当前行动角色.GetComponent<A4>().普攻动画时长);
        }
    }
    public void 近战技能()
    {
        if (!isMoving && !isAttacking)
        {                 // 面向目标玩家
            初始位置 = 当前行动角色.transform.position; // 记录初始位置
            当前行动角色.transform.LookAt(当前攻击目标);
            //isMoving = true;
            Invoke("延迟移动向目标", 当前行动角色.GetComponent<A4>().如果技能移动到目标播放动画多久开始移动);
            当前行动角色动画.SetBool("IsMoving", true);
            显示技能或普攻名称文本(当前行动角色.GetComponent<A4>().普攻名称);
            if (当前行动角色.GetComponent<A6>() != null)
                当前行动角色.GetComponent<A6>().释放技能增加行动次数 = true;
        }
        if (isMoving && !isAttacking && !开始返回)
        {
            // 移动向目标玩家
            当前行动角色.transform.position = Vector3.MoveTowards(当前行动角色.transform.position, 当前攻击目标.position, 当前行动角色.GetComponent<A1>().移动向敌方的速度 * Time.deltaTime);

            //当距离目标一定距离时，停下并播放攻击动画
            if (Vector3.Distance(当前行动角色.transform.position, 当前攻击目标.position) <= 当前攻击目标.GetComponent<A1>().被近战攻击时距离自身多远停下)
            {
                isMoving = false;
                isAttacking = true;
                当前行动角色动画.SetBool("IsMoving", false);
                当前行动角色动画.SetBool("释放技能", true);
                Invoke("造成技能伤害", 当前行动角色.GetComponent<A4>().开始播放技能攻击动画多久后造成伤害);
                Invoke("播放技能特效", 当前行动角色.GetComponent<A4>().开始播放技能动画后多久后生成特效);
                Invoke("AttackAnimationComplete", 当前行动角色.GetComponent<A4>().技能动画时长);
            }

        }
    }
    public void 远程技能()
    {
        if (!isMoving && !isAttacking)
        {                 // 面向目标玩家
            初始位置 = 当前行动角色.transform.position; // 记录初始位置
            当前行动角色.transform.LookAt(当前攻击目标);
            isMoving = true;
            显示技能或普攻名称文本(当前行动角色.GetComponent<A4>().技能名称);
            if (当前行动角色.GetComponent<A6>() != null)
                当前行动角色.GetComponent<A6>().释放技能增加行动次数 = true;
        }
        if (isMoving && !isAttacking && !开始返回)
        {
            isMoving = false;
            isAttacking = true;
            当前行动角色动画.SetBool("释放技能", true);

            if (当前行动角色.GetComponent<A4>().技能是否对我方使用)
            {
                Invoke("播放技能特效", 当前行动角色.GetComponent<A4>().开始播放技能动画后多久后生成特效);
                Invoke("造成技能伤害", 当前行动角色.GetComponent<A4>().开始播放技能攻击动画多久后造成伤害);
            }

            else
            {
                Invoke("造成技能伤害", 当前行动角色.GetComponent<A4>().开始播放技能攻击动画多久后造成伤害);
                Invoke("播放技能特效", 当前行动角色.GetComponent<A4>().开始播放技能动画后多久后生成特效);
            }
            Invoke("AttackAnimationComplete", 当前行动角色.GetComponent<A4>().技能动画时长);
        }
    }
    public void 召唤技能()
    {
        if (!isMoving && !isAttacking)
        {
            isMoving = true;
            显示技能或普攻名称文本(当前行动角色.GetComponent<A4>().召唤技名称);
        }
        if (isMoving && !isAttacking && !开始返回)
        {
            isMoving = false;
            当前行动角色动画.SetBool("召唤", true);
            Invoke("使用召唤技", 当前行动角色.GetComponent<A4>().开始播放召唤动画后多久后开始召唤);
            Invoke("记录初始位置", 0.5f);
            Invoke("AttackAnimationComplete", 当前行动角色.GetComponent<A4>().召唤动画时长);
        }
    }
    public void 蓄力技能()
    {
        if (!isMoving && !isAttacking)
        {                 // 面向目标玩家
            初始位置 = 当前行动角色.transform.position; // 记录初始位置
            当前行动角色.transform.LookAt(当前攻击目标);
            isMoving = true;
            if (当前行动角色.GetComponent<A4>().蓄力中 == false)
            {
                显示技能或普攻名称文本(当前行动角色.GetComponent<A4>().蓄力技名称);
                B1.Instance.减少战技点();
            }
            else
                显示技能或普攻名称文本(当前行动角色.GetComponent<A4>().蓄力技完成名称);
        }
        if (isMoving && !isAttacking && !开始返回)
        {
            isMoving = false;
            isAttacking = true;
            if (当前行动角色.GetComponent<A4>().蓄力中 == false)
            {
                当前行动角色动画.SetBool("蓄力", true);
                Invoke("使用蓄力技", 当前行动角色.GetComponent<A4>().开始播放蓄力动画后多久后造成伤害);
            }
            else
            {
                当前行动角色.GetComponent<A4>().释放蓄力技(当前攻击目标.gameObject, 当前行动角色.GetComponent<A1>().属性);
                Invoke("造成蓄力伤害", 当前行动角色.GetComponent<A4>().开始播放蓄力动画后多久后造成伤害);
            }
            Invoke("AttackAnimationComplete", 当前行动角色.GetComponent<A4>().蓄力动画时长);
        }
        是否可以使用蓄力技 = false;
    }
    public void 近战蓄力技能()
    {
        if (!isMoving && !isAttacking)
        {                 // 面向目标玩家
            初始位置 = 当前行动角色.transform.position; // 记录初始位置
            当前行动角色.transform.LookAt(当前攻击目标);
            //isMoving = true;
            Invoke("延迟移动向目标", 当前行动角色.GetComponent<A4>().如果蓄力移动到目标播放动画多久开始移动);
            if (当前行动角色.GetComponent<A4>().蓄力中 == false)
            {
                显示技能或普攻名称文本(当前行动角色.GetComponent<A4>().蓄力技名称);
                B1.Instance.减少战技点();
            }
            else
                显示技能或普攻名称文本(当前行动角色.GetComponent<A4>().蓄力技完成名称);
            是否可以使用蓄力技 = false;
        }
        if (isMoving && !isAttacking && !开始返回)
        {
            if (当前行动角色.GetComponent<A4>().蓄力中 == false)
            {
                Debug.Log("11");
                当前行动角色动画.SetBool("蓄力", true);
                Invoke("使用蓄力技", 当前行动角色.GetComponent<A4>().开始播放蓄力动画后多久后造成伤害);
            }
            else
            if (当前行动角色.GetComponent<A4>().蓄力中)
            {
                Debug.Log("22");
                当前行动角色.transform.position = Vector3.MoveTowards(当前行动角色.transform.position, 当前攻击目标.position, 当前行动角色.GetComponent<A1>().移动向敌方的速度 * Time.deltaTime);
                if (Vector3.Distance(当前行动角色.transform.position, 当前攻击目标.position) <= 当前攻击目标.GetComponent<A1>().被近战攻击时距离自身多远停下)
                {
                    isMoving = false;
                    isAttacking = true;
                    当前行动角色.GetComponent<A4>().释放蓄力技(当前攻击目标.gameObject, 当前行动角色.GetComponent<A1>().属性);
                    Invoke("造成蓄力伤害", 当前行动角色.GetComponent<A4>().开始播放蓄力动画后多久后造成伤害);
                }
            }
            Invoke("AttackAnimationComplete", 当前行动角色.GetComponent<A4>().蓄力动画时长);
            是否可以使用蓄力技 = false;
        }
    }
    bool 射线判断玩家列表(GameObject character)// 如果射线击中的是玩家列表中的
    {
        foreach (GameObject playerChar in playerCharacters)
        {
            if (playerChar == character)
            {
                return true;
            }
        }
        return false;
    }
    bool 射线判断敌方列表(GameObject character)// 如果射线击中的是NPC列表中的
    {
        foreach (GameObject playerChar in enemyCharacters)
        {
            if (playerChar == character)
            {
                return true;
            }
        }
        return false;
    }
    private void 可以使用普攻()
    {
        if(是否可以使用技能&&当前行动角色.GetComponent<A4>().技能是否对我方使用)
            选择目标队列中间对象作为攻击目标(NPC队列);
        是否可以使用技能 = false;
        使用技能 = false;
        是否可以使用召唤技 = false;
        看向我方的摄像机.gameObject.SetActive(false);
        我方摄像机.gameObject.SetActive(true);
        当前行动角色动画.SetBool("准备释放技能", false);
        当前行动角色动画.SetBool("准备释放普攻", true);

        if (是否可以使用普攻)
        {
            使用普攻 = true;
            if (当前行动角色.GetComponent<A4>().普攻是否移动到目标 == true)
            {
//
            }
            else if (使用普攻)
            {
                远程普攻();
            }
        }
        是否可以使用普攻 = true;
        if (普通攻击按钮 != null)
            普通攻击按钮.transform.localScale = 普攻按钮初始大小 * 1.2f;
        if (技能攻击按钮 != null)
            技能攻击按钮.transform.localScale = 技能按钮初始大小;
        if (召唤技按钮 != null)
            召唤技按钮.transform.localScale = 召唤按钮初始大小;
        if (蓄力技按钮 != null)
            蓄力技按钮.transform.localScale = 蓄力按钮初始大小;
    }

    private void 可以使用技能()
    {
        是否可以使用普攻 = false;
        是否可以使用技能 = true;
        使用普攻 = false;
        是否可以使用召唤技 = false;
        if (使用技能&& B1.Instance.当前战技点 > 0)
        {
            if (当前行动角色.GetComponent<A4>().技能攻击是否是移动到目标 == true)
            {
                技能 = true;
                if (当前行动角色.GetComponent<A4>().技能是否消耗战技点)
                    B1.Instance.减少战技点();
            }
            else
            {
                远程技能();
                if (当前行动角色.GetComponent<A4>().技能是否消耗战技点)
                    B1.Instance.减少战技点();
            }
        }
        if (当前行动角色.GetComponent<A4>().技能是否对我方使用 == true && 当前攻击目标.GetComponent<A1>().是否是敌方)
        {
            当前攻击目标 = 当前行动角色.transform;
        }
        // 改变按钮大小
        if (技能攻击按钮 != null)
            技能攻击按钮.transform.localScale = 技能按钮初始大小 * 1.2f;
        if (普通攻击按钮 != null)
            普通攻击按钮.transform.localScale = 普攻按钮初始大小;
        if (召唤技按钮 != null)
            召唤技按钮.transform.localScale = 召唤按钮初始大小;
        if (蓄力技按钮 != null)
            蓄力技按钮.transform.localScale = 蓄力按钮初始大小;
    }
    private void 可以使用召唤技()
    {
        是否可以使用召唤技 = true;
        使用普攻 = false;
        是否可以使用普攻 = false;
        是否可以使用技能 = false;
        使用技能 = false;
        if (使用召唤&&B1.Instance.当前战技点>0)
        {
            召唤技能();
            B1.Instance.减少战技点();
        }
        // 改变按钮大小
        if (技能攻击按钮 != null)
            技能攻击按钮.transform.localScale = 技能按钮初始大小;
        if (普通攻击按钮 != null)
            普通攻击按钮.transform.localScale = 普攻按钮初始大小;
        if (召唤技按钮 != null)
            召唤技按钮.transform.localScale = 召唤按钮初始大小 * 1.2f;
        if (蓄力技按钮 != null)
            蓄力技按钮.transform.localScale = 蓄力按钮初始大小;
    }
    public void 可以使用必杀技(A1 谁使用必杀技)//立即行动
    {
        谁使用必杀技.GetComponent<A1>().当前必杀技能量 = 0;
        // 最上层的行动条
        Transform targetTransform = B1.Instance.最上层的对象.transform;
        // 目标的行动条
        Transform objectToMove = 谁使用必杀技.GetComponent<A1>().自己对应的行动条.transform;

        if (objectToMove != null && targetTransform != null)
        {
            if (targetTransform != null)
            {
                // 获取目标对象的层级排列位置
                int targetSiblingIndex = targetTransform.GetSiblingIndex();

                // 将需要移动的对象设置到目标对象的后方（索引 + 1 的位置）
                objectToMove.SetSiblingIndex(targetSiblingIndex + 1);
                谁使用必杀技.GetComponent<A1>().生成效果文本("立即行动", Color.red);
            }
        }
    }
    public void 可以使用蓄力技()
    {
        是否可以使用蓄力技 = true;
        if (技能攻击按钮 != null)
            技能攻击按钮.transform.localScale = 技能按钮初始大小;
        if (普通攻击按钮 != null)
            普通攻击按钮.transform.localScale = 普攻按钮初始大小;
        if (召唤技按钮 != null)
            召唤技按钮.transform.localScale = 召唤按钮初始大小;
        if (蓄力技按钮 != null)
            蓄力技按钮.transform.localScale = 蓄力按钮初始大小 * 1.2f;
        if (使用蓄力 && 当前行动角色.GetComponent<A4>().蓄力技是否移动到目标释放 == false)
        {
            蓄力技能();
        }
        if (使用蓄力 && 当前行动角色.GetComponent<A4>().蓄力技是否移动到目标释放)
        {
            近战蓄力技能();
        }
    }
    public void 显示技能或普攻名称文本(string textToDisplay)
    {
        if (显示技能名称文本 != null)
        {
            // 设置文本内容
            显示技能名称文本.text = textToDisplay;

            // 启用文本对象
            显示技能名称文本.gameObject.SetActive(true);

            // 延迟一定时间后禁用文本对象
            StartCoroutine(DisableTextAfterDelay());
        }
    }

    // 协程用于禁用文本对象
    private IEnumerator DisableTextAfterDelay()
    {
        yield return new WaitForSeconds(1);//文本显示时间

        if (显示技能名称文本 != null)
        {
            // 禁用文本对象
            显示技能名称文本.gameObject.SetActive(false);
        }
    }
    void 记录初始位置()
    {
        初始位置 = 当前行动角色.transform.position; // 记录初始位置
    }
    void 造成技能伤害()
    {
        当前行动角色.GetComponent<A4>().使用技能(当前攻击目标.gameObject, 当前行动角色.GetComponent<A1>().属性);
    }
    void 造成普攻伤害()
    {
        当前行动角色.GetComponent<A4>().使用普攻(当前攻击目标.gameObject, 当前行动角色.GetComponent<A1>().属性);
    }
    void 使用召唤技()
    {
        当前行动角色.GetComponent<A4>().使用召唤技();
    }
    void 使用蓄力技()
    {
        if(当前行动角色!=null&&当前攻击目标!=null)
        当前行动角色.GetComponent<A4>().使用蓄力技(当前攻击目标.gameObject, 当前行动角色.GetComponent<A1>().属性);
    }
    void 造成蓄力伤害()
    {
        当前行动角色.GetComponent<A4>().蓄力造成伤害(当前攻击目标.gameObject, 当前行动角色.GetComponent<A1>().属性);
    }
    void 播放技能特效()
    {
        当前行动角色.GetComponent<A4>().播放指定技能特效(当前攻击目标);
    }
    void 播放普攻特效()
    {
        当前行动角色.GetComponent<A4>().播放指定普攻特效(当前攻击目标);
    }
    void 移动当前行动角色的行动条到最底部()
    {
        isAttacking = false;
        isMoving = false;
        GameObject 对象 = B1.Instance.最上层的对象.GetComponent<A2>().行动条对应角色;
        Transform parentTransform = B1.Instance.拖入行动条;
        int childCount = parentTransform.childCount;

        if (childCount > 1)
        {
            Transform topChild = parentTransform.GetChild(0);
            Transform bottomChild = parentTransform.GetChild(childCount - 1);
            对象.GetComponent<A4>().减少所有Buff剩余回合();
            topChild.SetAsLastSibling();
            对象.GetComponent<A1>().恢复破韧();
        }
        StartCoroutine(DelayedExecutionCoroutine());
    }

    private IEnumerator DelayedExecutionCoroutine()
    {
        yield return new WaitForSeconds(0.35f);
        if (有人死亡)
        {
            yield return new WaitForSeconds(0.8f);
            有人死亡 = false;
        }
        销毁攻击按钮();
        选择当前行动角色 = true;
        可以生成按钮 = true;
    }
    private IEnumerator DelayedExecutionCoroutine2()
    {
        yield return new WaitForSeconds(0.8f);
        选择当前行动角色 = false;
    }
    private IEnumerator DelayedExecutionCoroutine3()
    {
        if (当前攻击目标 == null&&当前行动角色.GetComponent<A1>().是否是敌方)
        {
                选择目标队列中间对象作为攻击目标(玩家队列);
                yield return new WaitForSeconds(0.5f);
                移动当前行动角色的行动条到最底部();

        }
        else
        {
            yield return new WaitForSeconds(0.4f);
            移动当前行动角色的行动条到最底部(); ;
            isMoving = false;
            销毁攻击按钮();
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

    public Transform FindSecondSubset(Transform parent)
    {
        Transform secondSubset = null;

        if (parent.childCount >= 2)
        {
            // 如果有至少两个子对象，获取第二个子对象作为目标子集
            secondSubset = parent.GetChild(1);
        }
        else
        {
            // 如果子对象数量不足两个，递归查找子对象的子对象
            for (int i = 0; i < parent.childCount; i++)
            {
                Transform child = parent.GetChild(i);
                Transform childSubset = FindSecondSubset(child);

                if (childSubset != null)
                {
                    secondSubset = childSubset;
                    break;
                }
            }
        }

        return secondSubset;
    }
    void 选择目标队列中间对象作为攻击目标(GameObject 玩家或敌方队列)
    {
        int childCount = 玩家或敌方队列.transform.childCount;
        if (childCount > 0)
        {
            // 计算中间位置的索引
            int middleIndex = childCount / 2;
            // 获取中间位置的子对象
            Transform middleChild = 玩家或敌方队列.transform.GetChild(middleIndex);

            当前攻击目标 = middleChild;
        }
        当前行动角色.transform.LookAt(当前攻击目标.transform);
        if(玩家或敌方队列==NPC队列&&当前攻击目标!=null)
        我方摄像机.LookAt = 当前攻击目标.transform.Find("快捷角色设置").transform.Find("摄像机看向位置");
    }

}



