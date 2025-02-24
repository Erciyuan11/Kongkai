using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    //走单例模式
    public static UImanager instance;
    public TextMeshProUGUI nameText;
    public Slider HpSlider;
    public Slider ShieldSlider;
    public Slider UltSkillSlider;

    public void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //实例化UI函数
    public void InitHUD(CharactorBasic charactor)
    {
        nameText.text = charactor.charactername;
        HpSlider.maxValue = charactor.maxhp;
        ShieldSlider.maxValue = charactor.maxshield;
        UltSkillSlider.maxValue = charactor.maxultskill;
        HpSlider.value = charactor.currenthp;
        ShieldSlider.value = charactor.currentshield;
        HpSlider.value = charactor.currentskill;

    }

    public void UpdateHp(float hp)
    {
        HpSlider.value = hp;
    }

    public void UpdateShield(float shield)
    {
        HpSlider.value = shield;
    }

    public void UpdateSkill(float skill)
    {
        HpSlider.value = skill;
    }



}
