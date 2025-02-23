
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class A1 : MonoBehaviour//用于设置人物属性
{
    [Header("角色主要属性")]
    public GameObject 我方角色属性预制体;
    public float 移动向敌方的速度 = 7;
    public float 返回的速度 = 12;
    public bool 返回是否转头;
    public float 最大生命值 = 100;
    public float 当前生命值 = 100;
    public int 速度 = 100;
    public float 防御力;
    public float 护盾;
    public float 攻击力 = 10;
    public string 属性 = "火";
    public Image 行动条图像预制体;
    public Sprite 角色头像;
    public Sprite 可选行动条角色头像;
    public float 最大必杀技能量 = 140;
    public float 最大韧性 = 5;
    public float 被近战攻击时距离自身多远停下 = 2f;
    public GameObject 出场特效;
    public bool 攻击后删除自身;
        [HideInInspector]
    public Transform 受到伤害或治疗文本生成位置;
    public GameObject 文本预制体;
    public GameObject 效果文本预制体;
    public Image 弱点图像预制体;// 需要复制的图像预制体
    public string[] 弱点属性; // 图像的名称数组

    [Header("敌方设置")]
    public bool 是否是敌方;
    public bool 是否拥有能量条 = true;
    public GameObject 敌方角色属性预制体;
            [HideInInspector]
    public Transform 非BOSS敌方角色属性生成位置;
    public bool 是否是boss;//决定血条是否在屏幕中间
    public bool 是否有两个行动条;
    public GameObject BOSS角色属性预制体;

    [Header("是否是特色角色")]
    public bool 是否是特色召唤物;//召唤物在攻击后自动销毁，且不处于角色队列中
    public int 特色召唤物存在回合;
    public bool 被召唤时是否立即行动;

    Transform boss属性生成位置;
    Transform 弱点属性父级;
    Animator 动画;
    float 当前能量百分比;
    EasySlider 韧性条;

    EasySlider 血条;
    EasySlider 护盾条;
    EasySlider 必杀技进度;
    UnityEngine.UI.Button 必杀技按钮;
    Transform 行动条父级;
    Image 属性图像;
    Text 生命数字;
    Transform 我方角色属性面板;
    GameObject 生成的角色属性;//生成的角色属性
    float 当前韧性条;

    [Header("属性图像，可以在脚本中自定义")]
    public Sprite 火;
    public Sprite 冰;
    public Sprite 雷;
    public Sprite 风;
    public Sprite 物理;
    public Sprite 虚数;
    public Sprite 水;

    //[Header("下面的不用管")]
    [HideInInspector]
    public Transform buff生成位置;
            [HideInInspector]
    public bool 死亡;
        [HideInInspector]
    public bool 破韧;
            [HideInInspector]
    public float 当前必杀技能量;
            [HideInInspector]
    public Image 自己对应的行动条;
            [HideInInspector]
    public Image 自己对应的行动条1;
            [HideInInspector]
    public GameObject 生成的冻结特效;
            [HideInInspector]
    public int 特色召唤物剩余回合;
    [Header("角色受到效果")]
            [HideInInspector]
    public bool 是否被强控;
            [HideInInspector]
    public bool 是否受到持续灼伤伤害;
            [HideInInspector]
    public bool 是否受到持续触电伤害;
            [HideInInspector]
    public bool 是否提升攻击力;
            [HideInInspector]
    public bool 是否降低防御力;
            [HideInInspector]
    public string 自己受到的持续伤害属性;
            [HideInInspector]
    public int 受到的持续伤害破韧量;
            [HideInInspector]
    public Color 文本颜色;
            [HideInInspector]
    public Color 持续伤害A颜色;
            [HideInInspector]
    public Color 持续伤害B颜色;
    // 初始化
    private void OnEnable()
    {
        if (出场特效 != null)
        {
            var 生成出场特效 = Instantiate(出场特效, transform);
            Destroy(生成出场特效, 1);
        }
    }
    private void Start()
    {
        var a = transform.Find("快捷角色设置");
        受到伤害或治疗文本生成位置 = a.transform.Find("受到伤害或治疗文本生成位置");
        非BOSS敌方角色属性生成位置 = a.transform.Find("敌方角色属性生成位置");

        GameObject 音效播放对象 = GameObject.Find("角色音效播放位置");

        //当前必杀技能量 = 30;
        我方角色属性面板 = B1.Instance.角色属性父级;
        boss属性生成位置 = B1.Instance.boss属性父级;
        行动条父级 = B1.Instance.行动条父级;

        Image newImage = Instantiate(行动条图像预制体, 行动条父级);
        if (可选行动条角色头像 != null)
            newImage.sprite = 可选行动条角色头像;
        else
            newImage.sprite = 角色头像;
        newImage.GetComponent<A2>().行动条对应角色 = this.gameObject;
        自己对应的行动条 = newImage;
        if (是否有两个行动条)
        {
            Image newImage1 = Instantiate(行动条图像预制体, 行动条父级);
            if (可选行动条角色头像 != null)
                newImage1.sprite = 可选行动条角色头像;
            else
                newImage1.sprite = 角色头像;
            newImage1.GetComponent<A2>().行动条对应角色 = this.gameObject;
            自己对应的行动条1 = newImage;
        }
        if (是否是特色召唤物 == false)
        {
            if (!是否是敌方)//如果是我方角色
            {
                生成的角色属性 = Instantiate(我方角色属性预制体);
                生成的角色属性.transform.SetParent(我方角色属性面板, false);

                var sx = 生成的角色属性.transform.Find("属性图像");
                属性图像 = sx.gameObject.GetComponent<Image>();
                var xt = 生成的角色属性.transform.Find("血条");
                血条 = xt.gameObject.GetComponent<EasySlider>();
                var hd = 生成的角色属性.transform.Find("护盾条");
                护盾条 = hd.gameObject.GetComponent<EasySlider>();
                if (是否拥有能量条)
                {
                    var jd = 生成的角色属性.transform.Find("必杀技进度");
                    必杀技进度 = jd.gameObject.GetComponent<EasySlider>();
                    var jd1 = 必杀技进度.transform.Find("fill");
                    jd1.gameObject.GetComponent<Image>().sprite = 角色头像;
                    var jd2 = 必杀技进度.transform.Find("outline");
                    jd2.gameObject.GetComponent<Image>().sprite = 角色头像;
                    var 按钮 = 生成的角色属性.transform.Find("必杀技按钮");
                    必杀技按钮 = 按钮.GetComponent<UnityEngine.UI.Button>();
                    必杀技按钮.gameObject.GetComponent<Image>().sprite = 角色头像;
                }
                var b2 = transform.Find("对战UI");
                必杀技按钮.onClick.AddListener(传递是自己使用必杀技);
                float 当前护盾百分比 = 护盾 / 最大生命值;
                护盾条.SetValue(当前护盾百分比);
                var 韧性 = 生成的角色属性.transform.Find("韧性条");
                韧性条 = 韧性.GetComponent<EasySlider>();
                当前韧性条 = 最大韧性;
                float 当前韧性条百分比 = 当前韧性条 / 最大韧性;
                韧性条.SetValue(当前韧性条百分比);
                var wz = 生成的角色属性.transform.Find("弱点属性位置");
                弱点属性父级 = wz.gameObject.transform;
                if (弱点属性.Length > 0)
                {
                    创建弱点图像();
                }
            }
            else//如果是敌方
            {
                if (是否是boss == false)
                {

                    生成的角色属性 = Instantiate(敌方角色属性预制体);
                    生成的角色属性.transform.SetParent(非BOSS敌方角色属性生成位置, false);
                    var wz = 生成的角色属性.transform.Find("弱点属性位置");
                    弱点属性父级 = wz.gameObject.transform;
                    var xt = 生成的角色属性.transform.Find("血条");
                    血条 = xt.gameObject.GetComponent<EasySlider>();
                    var hd = 生成的角色属性.transform.Find("护盾条");
                    护盾条 = hd.gameObject.GetComponent<EasySlider>();
                    var 韧性 = 生成的角色属性.transform.Find("韧性条");
                    var sx = 生成的角色属性.transform.Find("属性图像");
                    属性图像 = sx.gameObject.GetComponent<Image>();
                    韧性条 = 韧性.GetComponent<EasySlider>();
                    当前韧性条 = 最大韧性;
                    float 当前韧性条百分比 = 当前韧性条 / 最大韧性;
                    韧性条.SetValue(当前韧性条百分比);
                    if (是否拥有能量条)
                    {
                        var jd = 生成的角色属性.transform.Find("必杀技进度");
                        必杀技进度 = jd.gameObject.GetComponent<EasySlider>();
                        var jd1 = 必杀技进度.transform.Find("fill");
                        jd1.gameObject.GetComponent<Image>().sprite = 角色头像;
                        var jd2 = 必杀技进度.transform.Find("outline");
                        jd2.gameObject.GetComponent<Image>().sprite = 角色头像;
                    }
                }
                else//如果是boos
                {
                    生成的角色属性 = Instantiate(BOSS角色属性预制体);
                    // 将实例化的UI对象的父对象设置为目标面板，使其成为目标面板的子对象
                    生成的角色属性.transform.SetParent(boss属性生成位置, false);

                    var sx = 生成的角色属性.transform.Find("属性图像");
                    属性图像 = sx.gameObject.GetComponent<Image>();
                    var xt = 生成的角色属性.transform.Find("血条");
                    血条 = xt.gameObject.GetComponent<EasySlider>();
                    var hd = 生成的角色属性.transform.Find("护盾条");
                    护盾条 = hd.gameObject.GetComponent<EasySlider>();
                    if (是否拥有能量条)
                    {
                        var jd = 生成的角色属性.transform.Find("必杀技进度");
                        必杀技进度 = jd.gameObject.GetComponent<EasySlider>();
                        var jd1 = 必杀技进度.transform.Find("fill");
                        jd1.gameObject.GetComponent<Image>().sprite = 角色头像;
                        var jd2 = 必杀技进度.transform.Find("outline");
                        jd2.gameObject.GetComponent<Image>().sprite = 角色头像;
                        var 按钮 = 生成的角色属性.transform.Find("必杀技按钮");
                    }
                    float 护盾百分比 = 护盾 / 最大生命值;
                    护盾条.SetValue(护盾百分比);
                    var 韧性 = 生成的角色属性.transform.Find("韧性条");
                    韧性条 = 韧性.GetComponent<EasySlider>();
                    当前韧性条 = 最大韧性;
                    float 当前韧性条百分比 = 当前韧性条 / 最大韧性;
                    韧性条.SetValue(当前韧性条百分比);
                    var wz = 生成的角色属性.transform.Find("弱点属性位置");
                    弱点属性父级 = wz.gameObject.transform;
                }

                if (弱点属性.Length > 0)
                {
                    创建弱点图像();
                }
                float 当前护盾百分比 = 护盾 / 最大生命值;
                护盾条.SetValue(当前护盾百分比);
                buff生成位置 = 生成的角色属性.transform.Find("buff生成位置");
            }



            UpdateImageSprite();
            var smz = 生成的角色属性.transform.Find("生命值");
            生命数字 = smz.GetComponent<Text>();
            生命数字.text = 当前生命值.ToString();
            buff生成位置 = 生成的角色属性.transform.Find("buff生成位置");
        }
        if (被召唤时是否立即行动)
        {
            //立即行动();
            B2.Instance.可以使用必杀技(this);
        }
    }
    private void Update()
    {
        if (是否是特色召唤物 == false&&当前生命值>0)
        {
            生命数字.text = 当前生命值.ToString();
            设置必杀技();
            刷新能量条();
            float 当前护盾百分比 = 护盾 / 最大生命值;
            护盾条.SetValue(当前护盾百分比);
            float 当前生命百分比 = 当前生命值 / 最大生命值;
            if (当前生命值 > 最大生命值)
                当前生命值 = 最大生命值;
            血条.SetValue(当前生命百分比);
        }
        if (是否是敌方)
        {
            if (当前必杀技能量 >= 最大必杀技能量)
            {
                立即行动();
            }
        }
        if (当前生命值 <= 0)
        {
            死亡 = true;
            当前生命值 = 0;

            if (B2.Instance.isAttacking == false)
                延迟删除(1f);
        }
    }
    // 受到伤害
    public void NPC受到伤害(float damage, string 攻击属性, int 破韧量)
    {
        动画 = GetComponent<Animator>();
        修改攻击文本颜色(攻击属性);
        if (用于检查敌方弱点(弱点属性, 攻击属性))
        {
            当前韧性条 -= 破韧量;
            float 当前韧性条百分比 = 当前韧性条 / 最大韧性;
            韧性条.SetValue(当前韧性条百分比, true);
        }
        if (当前韧性条 <= 0)
        {
            GetComponent<A4>().蓄力中 = false;
            GetComponent<A4>().生成的蓄力特效 = null;
            破韧 = true;
            动画.SetBool("破韧", 破韧);
            生成伤害文本(damage, 文本颜色);

            动画.SetTrigger("受伤");
            if (护盾 > damage)//护盾抵扣伤害
            {
                护盾 -= damage;
                float 当前护盾百分比 = 护盾 / 最大生命值;
                护盾条.SetValue(当前护盾百分比);
            }
            else if (护盾 <= damage)
            {
                float 剩余伤害 = damage - 护盾;
                护盾 = 0;
                float 当前护盾百分比 = 护盾 / 最大生命值;
                护盾条.SetValue(当前护盾百分比);

                当前生命值 -= 剩余伤害;
                float 当前生命百分比 = 当前生命值 / 最大生命值;
                血条.SetValue(当前生命百分比, true);
            }
            else if (护盾 <= 0)
            {
                当前生命值 -= damage;
                float 当前生命百分比 = 当前生命值 / 最大生命值;
                血条.SetValue(当前生命百分比, true);
            }
        }
        else
        {
            // 考虑防御力，伤害减少
            float 实际受到伤害 = Mathf.Max(damage - 防御力, 0);
            生成伤害文本(实际受到伤害, 文本颜色);
            动画.SetTrigger("受伤");
            if (护盾 > 实际受到伤害)//护盾抵扣伤害
            {
                护盾 -= 实际受到伤害;
                float 当前护盾百分比 = 护盾 / 最大生命值;
                护盾条.SetValue(当前护盾百分比);
            }
            else if (护盾 <= 实际受到伤害)
            {
                float 剩余伤害 = 实际受到伤害 - 护盾;
                护盾 = 0;
                float 当前护盾百分比 = 护盾 / 最大生命值;
                护盾条.SetValue(当前护盾百分比);

                当前生命值 -= 剩余伤害;
                float 当前生命百分比 = 当前生命值 / 最大生命值;
                血条.SetValue(当前生命百分比, true);
            }
            else if (护盾 <= 0)
            {
                当前生命值 -= 实际受到伤害;
                float 当前生命百分比 = 当前生命值 / 最大生命值;
                血条.SetValue(当前生命百分比, true);
            }


        }

        if (当前生命值 <= 0)
        {
            死亡 = true;
            //当指定图层和名称的动画播放完毕
            延迟删除(1f);
        }
        //设置必杀技();
        生命数字.text = 当前生命值.ToString();
    }
    public void 恢复破韧()
    {
        if (破韧)
        {
            破韧 = false;
            动画.SetBool("破韧", 破韧);
            当前韧性条 = 最大韧性;
            float 当前韧性条百分比 = 当前韧性条 / 最大韧性;
            韧性条.SetValue(当前韧性条百分比, true);
        }
    }
    public void 恢复冻结()
    {
        if (是否被强控)
        {
            if (GetComponent<A4>().剩余被强控回合数 == 0)
            {
                是否被强控 = false;
                动画.SetBool("冻结", 是否被强控);
                if (生成的冻结特效 != null)
                {
                    Destroy(生成的冻结特效);
                }
                if (GetComponent<A4>().生成的强控buff图标 != null)
                {
                    Destroy(GetComponent<A4>().生成的强控buff图标.gameObject);
                }
            }
        }
    }
    public void 恢复攻击力提升()
    {
        if (是否提升攻击力)
        {
            if (GetComponent<A4>().剩余攻击力提升回合数 == 0)
            {
                攻击力 -= GetComponent<A4>().提升的攻击力;
                是否提升攻击力 = false;
                if (GetComponent<A4>().生成的攻击力提升buff图标 != null)
                    Destroy(GetComponent<A4>().生成的攻击力提升buff图标.gameObject);

            }
        }
    }
    public void 恢复翻防御力降低()
    {
        if (是否降低防御力)
        {
            if (GetComponent<A4>().剩余防御力降低回合数 == 0)
            {
                防御力 += GetComponent<A4>().降低的防御力;
                是否降低防御力 = false;
                if (GetComponent<A4>().生成的防御力降低buff图标 != null)
                    Destroy(GetComponent<A4>().生成的防御力降低buff图标.gameObject);

            }
        }
    }
    public void 恢复受到持续伤害()
    {
        if (是否受到持续灼伤伤害)
        {
            if (GetComponent<A4>().剩余持续伤害A回合数 == 0)
            {
                是否受到持续灼伤伤害 = false;
                if (GetComponent<A4>().生成的持续灼伤伤害buff图标 != null)
                    Destroy(GetComponent<A4>().生成的持续灼伤伤害buff图标.gameObject);

            }
        }
        if (是否受到持续触电伤害)
        {
            if (GetComponent<A4>().剩余持续伤害B回合数 == 0)
            {
                是否受到持续触电伤害 = false;
                if (GetComponent<A4>().生成的持续触电伤害buff图标 != null)
                    Destroy(GetComponent<A4>().生成的持续触电伤害buff图标.gameObject);

            }
        }
    }

    public void 受到伤害(float damage, string 攻击属性, int 破韧量)
    {
        动画 = GetComponent<Animator>();
        修改攻击文本颜色(攻击属性);
        动画.SetTrigger("受伤");
        float 实际受到伤害 = Mathf.Max(damage - 防御力, 0);
        生成伤害文本(实际受到伤害, 文本颜色);
        // 考虑防御力，伤害减少
        if (护盾 > 实际受到伤害)//护盾抵扣伤害
        {
            护盾 -= 实际受到伤害;
            float 当前护盾百分比 = 护盾 / 最大生命值;
            护盾条.SetValue(当前护盾百分比);
        }
        else if (护盾 <= 实际受到伤害)
        {
            float 剩余伤害 = 实际受到伤害 - 护盾;
            护盾 = 0;
            float 当前护盾百分比 = 护盾 / 最大生命值;
            护盾条.SetValue(当前护盾百分比);

            当前生命值 -= 剩余伤害;
            float 当前生命百分比 = 当前生命值 / 最大生命值;
            血条.SetValue(当前生命百分比, true);
        }
        else if (护盾 <= 0)
        {
            当前生命值 -= 实际受到伤害;
            float 当前生命百分比 = 当前生命值 / 最大生命值;
            血条.SetValue(当前生命百分比, true);

        }
        if (当前生命值 <= 0)
        {
            死亡 = true;
            当前生命值 = 0;
            B2.Instance.有人死亡 = true;
            if (B2.Instance.isAttacking == false)
                延迟删除(1f);
        }

        if (弱点属性.Length > 0)
        {

            if (用于检查敌方弱点(弱点属性, 攻击属性))
            {
                当前韧性条 -= 破韧量;
                float 当前韧性条百分比 = 当前韧性条 / 最大韧性;
                韧性条.SetValue(当前韧性条百分比, true);
            }
            if (当前韧性条 <= 0)
            {
                GetComponent<A4>().蓄力中 = false;
                破韧 = true;
                动画.SetBool("破韧", 破韧);
                GetComponent<A4>().生成的蓄力特效 = null;
            }
        }

        生命数字.text = 当前生命值.ToString();
    }
    public void 受到持续伤害(float damage, string 攻击属性, int 破韧量)
    {
        动画 = GetComponent<Animator>();
        修改攻击文本颜色(攻击属性);
        if (GetComponent<A4>().让敌方持续受到伤害A)
            生成效果文本(B1.Instance.持续伤害A名称, 文本颜色);
        else if (GetComponent<A4>().让敌方持续受到伤害B)
            生成效果文本(B1.Instance.持续伤害B名称, 文本颜色);
        动画.SetTrigger("受伤");
        生成伤害文本(damage, 文本颜色);
        // 不考虑防御力
            当前生命值 -= damage;
            float 当前生命百分比 = 当前生命值 / 最大生命值;
            血条.SetValue(当前生命百分比, true);


        if (当前生命值 <= 0)
        {
            死亡 = true;
            当前生命值 = 0;
            B2.Instance.有人死亡 = true;
            if (B2.Instance.isAttacking == false)
                延迟删除(1f);
        }

        if (弱点属性.Length > 0)
        {

            if (用于检查敌方弱点(弱点属性, 攻击属性))
            {
                当前韧性条 -= 破韧量;
                float 当前韧性条百分比 = 当前韧性条 / 最大韧性;
                韧性条.SetValue(当前韧性条百分比, true);
            }
            if (当前韧性条 <= 0)
            {
                GetComponent<A4>().蓄力中 = false;
                破韧 = true;
                动画.SetBool("破韧", 破韧);
                GetComponent<A4>().生成的蓄力特效 = null;
            }
        }

        生命数字.text = 当前生命值.ToString();

    }
    public void 发动技能攻击(GameObject game, float damage, string 攻击属性)
    {
        //Debug.Log(攻击属性);
        if (game.GetComponent<A1>() != null && game.GetComponent<A1>().是否是敌方)
            game.GetComponent<A1>().受到伤害(damage, 攻击属性, GetComponent<A4>().技能攻击破韧值);
        if (game.GetComponent<A1>() != null && game.GetComponent<A1>().是否是敌方 == false)
            game.GetComponent<A1>().受到伤害(damage, 攻击属性, GetComponent<A4>().技能攻击破韧值);
        设置必杀技();
    }//只需要使用发动攻击，会自动让目标受到伤害
    public void 发动攻击(GameObject game, float damage, string 攻击属性)
    {
        //Debug.Log(攻击属性);
        if (game.GetComponent<A1>() != null && game.GetComponent<A1>().是否是敌方)
            game.GetComponent<A1>().受到伤害(damage, 攻击属性, GetComponent<A4>().普攻破韧值);
        if (game.GetComponent<A1>() != null && game.GetComponent<A1>().是否是敌方 == false)
            game.GetComponent<A1>().受到伤害(damage, 攻击属性, GetComponent<A4>().普攻破韧值);

        设置必杀技();
    }//只需要使用发动攻击，会自动让目标受到伤害
    public void 发动蓄力攻击(GameObject game, float damage, string 攻击属性)
    {
        //当前必杀技能量 += 20;
        if (game.GetComponent<A1>() != null && game.GetComponent<A1>().是否是敌方)
            game.GetComponent<A1>().受到伤害(damage, 攻击属性, GetComponent<A4>().蓄力攻击破韧值);
        if (game.GetComponent<A1>() != null && game.GetComponent<A1>().是否是敌方 == false)
            game.GetComponent<A1>().受到伤害(damage, 攻击属性, GetComponent<A4>().蓄力攻击破韧值);
    }//只需要使用发动攻击，会自动让目标受到伤害
    public void 回复生命(float 回复生命值)
    {

        Vector3 textSpawnPosition = 受到伤害或治疗文本生成位置.position;

        // 在XY轴上随机增加正负0.3
        textSpawnPosition.x += UnityEngine.Random.Range(-0.3f, 0.3f);
        textSpawnPosition.y += UnityEngine.Random.Range(-0.2f, 0.2f);
        // 生成文本
        GameObject damageTextInstance = Instantiate(文本预制体, textSpawnPosition, Quaternion.identity);
        Text damageTextMesh = damageTextInstance.GetComponentInChildren<Text>();
        // 设置文本内容和颜色
        damageTextMesh.text = 回复生命值.ToString();
        damageTextMesh.color = Color.green;
        if (当前生命值 < 最大生命值)
        {
            当前生命值 += 回复生命值;
            float 当前生命百分比 = 当前生命值 / 最大生命值;
            血条.SetValue(当前生命百分比, true);
        }
        else if (当前生命值 >= 最大生命值)
            当前生命值 = 最大生命值;

        生命数字.text = 当前生命值.ToString();
        Destroy(damageTextInstance, 0.7f);
    }

    public void 增加护盾(float 增加护盾量)
    {
        Vector3 textSpawnPosition = 受到伤害或治疗文本生成位置.position;

        // 在XY轴上随机增加正负0.3
        textSpawnPosition.x += UnityEngine.Random.Range(-0.3f, 0.3f);
        textSpawnPosition.y += UnityEngine.Random.Range(-0.2f, 0.2f);
        // 生成文本
        GameObject damageTextInstance = Instantiate(文本预制体, textSpawnPosition, Quaternion.identity);
        Text damageTextMesh = damageTextInstance.GetComponentInChildren<Text>();
        // 设置文本内容和颜色
        damageTextMesh.text = 增加护盾量.ToString();
        damageTextMesh.color = Color.yellow;
        if (护盾 < 最大生命值)
        {
            护盾 += 增加护盾量;
        }
        else
            护盾 = 最大生命值;

        float 当前护盾百分比 = 护盾 / 最大生命值;
        血条.SetValue(当前护盾百分比);
        //Debug.Log(当前护盾百分比);
        Destroy(damageTextInstance, 0.7f);
    }
    // 在属性变量改变时更新图像精灵
    public void 刷新图像()
    {
        UpdateImageSprite();
    }
    // 根据属性名称更新图像精灵
    private void UpdateImageSprite()
    {
        if (属性图像 != null)
            属性图像.sprite = GetAttributeSprite(属性);

    }

    // 根据属性名称获取对应的属性图像精灵
    private Sprite GetAttributeSprite(string attributeName)
    {
        switch (attributeName)
        {
            case "火":
                return 火;
            case "冰":
                return 冰;
            case "雷":
                return 雷;
            case "风":
                return 风;
            case "物理":
                return 物理;
            case "虚数":
                return 虚数;
            case "水":
                return 水;
            default:
                return null;
        }
    }
    // 根据属性名称切换对应的攻击颜色
    private void 修改攻击文本颜色(string 攻击属性)
    {

        switch (攻击属性)
        {
            case "火":
                文本颜色 = Color.red;
                break;
            case "冰":
                文本颜色 = new Color(0.5f, 0.5f, 1f); // 淡蓝色
                break;
            case "水":
                文本颜色 = Color.blue;
                break;
            case "雷":
                文本颜色 = Color.magenta; // 紫色
                break;
            case "风":
                文本颜色 = Color.green;
                break;
            case "物理":
                文本颜色 = Color.gray; // 银色
                break;
            case "虚数":
                文本颜色 = Color.yellow; // 金色
                break;
        }
    }
    // 死亡
    private void 创建弱点图像()
    {
        foreach (string 弱点属性 in 弱点属性)
        {
            if (弱点图像预制体 != null)
            {
                // 创建一个新的图像对象并设置其精灵
                Image newImage = Instantiate(弱点图像预制体, 弱点属性父级);
                newImage.gameObject.SetActive(true);
                newImage.sprite = GetAttributeSprite(弱点属性);
                //Debug.Log("生成弱点");
            }

        }
    }

    public void 攻击结束()
    {
        必杀技按钮.onClick.AddListener(B1.Instance.释放必杀技);
    }
    public void 延迟删除(float 延迟时间)
    {
        GetComponent<Animator>().SetTrigger("死亡");

        if (自己对应的行动条 != null)
            Destroy(自己对应的行动条.gameObject);
        if (生成的角色属性 != null)
            Destroy(生成的角色属性.gameObject);
        Invoke("Die", 延迟时间);

    }
    public void Die()
    {
        Destroy(this.gameObject);
    }

    bool 用于检查敌方弱点(string[] array, string target)//用于检查敌方弱点
    {
        foreach (string str in array)
        {
            if (str == target)
            {
                return true;
            }
        }
        return false;
    }

    public void 设置必杀技()
    {
        if (必杀技按钮 != null)
        {
            if (当前能量百分比 >= 1)
            {
                当前能量百分比 = 1;
                必杀技按钮.gameObject.SetActive(true);
            }
            else
                必杀技按钮.gameObject.SetActive(false);
        }

    }
    public void 传递是自己使用必杀技()
    {
        B2.Instance.可以使用必杀技(this);
    }
    void 刷新能量条()
    {
        if (必杀技进度 != null)
        {
            当前能量百分比 = 当前必杀技能量 / 最大必杀技能量;
            必杀技进度.SetValue(当前能量百分比);
            //Debug.Log("111");
        }
    }

    void 立即行动()
    {
        //Debug.Log("11");
        if (B2.Instance.当前行动角色.GetComponent<A1>().自己对应的行动条 != null)
        {
            //Debug.Log("22");
            Transform targetTransform = B2.Instance.当前行动角色.GetComponent<A1>().自己对应的行动条.transform;
            // 目标的行动条
            Transform objectToMove = 自己对应的行动条.transform;

            if (objectToMove != null && targetTransform != null && B1.Instance.排行第二的对象 != objectToMove && targetTransform != objectToMove)
            {
                //Debug.Log("33");
                if (targetTransform != null)
                {
                    Debug.Log("44");
                    // 获取目标对象的层级排列位置
                    int targetSiblingIndex = targetTransform.GetSiblingIndex();

                    // 将需要移动的对象设置到目标对象的后方（索引 + 1 的位置）
                    objectToMove.SetSiblingIndex(targetSiblingIndex + 1);
                    生成效果文本("行动大幅加快", Color.red);
                }
                当前必杀技能量 = 0;
            }
        }

    }
    public void 生成伤害文本(float damageValue, Color textColor)
    {

        Vector3 textSpawnPosition = 受到伤害或治疗文本生成位置.position;

        // 在XY轴上随机增加正负0.3
        textSpawnPosition.x += UnityEngine.Random.Range(-0.3f, 0.3f);
        textSpawnPosition.y += UnityEngine.Random.Range(-0.2f, 0.2f);

        // 生成文本
        GameObject damageTextInstance = Instantiate(文本预制体, textSpawnPosition, Quaternion.identity);
        Text damageTextMesh = damageTextInstance.GetComponentInChildren<Text>();
        // 设置文本内容和颜色
        damageTextMesh.text = damageValue.ToString();
        damageTextMesh.color = textColor;
        // 启动协程以在一秒后销毁伤害文本
        // StartCoroutine(DestroyDamageText(damageTextInstance));
        Destroy(damageTextInstance.gameObject, 0.7f);
    }
    public void 生成效果文本(string damageValue, Color textColor)
    {
        //Debug.Log("22");
        Vector3 textSpawnPosition = 受到伤害或治疗文本生成位置.position;

        // 在XY轴上随机增加正负0.3
        textSpawnPosition.x += UnityEngine.Random.Range(-0.3f, 0.3f);
        textSpawnPosition.y += UnityEngine.Random.Range(-0.3f, 0.3f);
        GameObject damageTextInstance = Instantiate(效果文本预制体, textSpawnPosition + new Vector3(0, 1, 0), Quaternion.identity);
        Text damageTextMesh = damageTextInstance.GetComponentInChildren<Text>();
        // 设置文本内容和颜色
        damageTextMesh.text = damageValue;
        damageTextMesh.color = textColor;
        // 启动协程以在一秒后销毁伤害文本
        Destroy(damageTextInstance.gameObject, 0.7f);
    }
}
