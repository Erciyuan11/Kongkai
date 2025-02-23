using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class B4 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler//用于让普攻和技能按钮显示描述
{
    [Header("放在按钮上")]
    public Text 技能文本; //文本
    public Text 普攻文本; //文本
    public Text 召唤文本; //文本;
    public Text 蓄力文本; //文本;

    public bool 是技能按钮;
    public bool 是普攻按钮;
    public bool 是召唤技按钮;
    public bool 是蓄力技按钮;
    private void Start()
    {
        if(技能文本!=null)
            技能文本.gameObject.SetActive(false); // 初始时隐藏文本对象
        if (普攻文本 != null)
            普攻文本.gameObject.SetActive(false); // 初始时隐藏文本对象
        if (召唤文本 != null)
            召唤文本.gameObject.SetActive(false); // 初始时隐藏文本对象
        if (蓄力文本 != null)
            蓄力文本.gameObject.SetActive(false); // 初始时隐藏文本对象
    }

    public void OnPointerEnter(PointerEventData eventData)//当鼠标进入
    {
        if (是技能按钮)
        {

            if ( B2.Instance.当前行动角色.GetComponent<A4>().技能描述 != null)
            {
                技能文本.text = B2.Instance.当前行动角色.GetComponent<A4>().技能描述; // 设置文本为指定的文本
                技能文本.gameObject.SetActive(true); // 显示文本对象
            }
        }
        if (是普攻按钮)
        {

            if  (B2.Instance.当前行动角色.GetComponent<A4>().普攻描述 != null)
            {
                普攻文本.text = B2.Instance.当前行动角色.GetComponent<A4>().普攻描述; // 设置文本为指定的文本
                普攻文本.gameObject.SetActive(true); // 显示文本对象
            }
        }
        if (是召唤技按钮)
        {
            if (B2.Instance.当前行动角色.GetComponent<A4>().召唤技描述 != null)
            {
                召唤文本.text = B2.Instance.当前行动角色.GetComponent<A4>().召唤技描述; // 设置文本为指定的文本
                召唤文本.gameObject.SetActive(true); // 显示文本对象
            }
        }
        if (是蓄力技按钮)
        {
            if (B2.Instance.当前行动角色.GetComponent<A4>().蓄力技描述 != null)
            {
                蓄力文本.text = B2.Instance.当前行动角色.GetComponent<A4>().蓄力技描述; // 设置文本为指定的文本
                蓄力文本.gameObject.SetActive(true); // 显示文本对象
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)//当鼠标移除
    {
        if (是技能按钮)
        {

            if (B2.Instance.当前行动角色.GetComponent<A4>().技能描述 != null)
            {
                技能文本.gameObject.SetActive(false); // 隐藏文本
            }
        }
        else
        if (是普攻按钮)
        {

            if (B2.Instance.当前行动角色.GetComponent<A4>().普攻描述 != null)
            { 
                普攻文本.gameObject.SetActive(false); // 隐藏文本
            }
        }
        else if (是召唤技按钮)
        {
            if (B2.Instance.当前行动角色.GetComponent<A4>().召唤技描述 != null)
            {
                召唤文本.gameObject.SetActive(false); // 隐藏文本
            }
        }
        else if (是蓄力技按钮)
        {
            if (B2.Instance.当前行动角色.GetComponent<A4>().蓄力技描述 != null)
            {
                蓄力文本.gameObject.SetActive(false); // 隐藏文本
            }
        }
    }
    
}
