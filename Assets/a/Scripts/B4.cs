using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class B4 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler//�������չ��ͼ��ܰ�ť��ʾ����
{
    [Header("���ڰ�ť��")]
    public Text �����ı�; //�ı�
    public Text �չ��ı�; //�ı�
    public Text �ٻ��ı�; //�ı�;
    public Text �����ı�; //�ı�;

    public bool �Ǽ��ܰ�ť;
    public bool ���չ���ť;
    public bool ���ٻ�����ť;
    public bool ����������ť;
    private void Start()
    {
        if(�����ı�!=null)
            �����ı�.gameObject.SetActive(false); // ��ʼʱ�����ı�����
        if (�չ��ı� != null)
            �չ��ı�.gameObject.SetActive(false); // ��ʼʱ�����ı�����
        if (�ٻ��ı� != null)
            �ٻ��ı�.gameObject.SetActive(false); // ��ʼʱ�����ı�����
        if (�����ı� != null)
            �����ı�.gameObject.SetActive(false); // ��ʼʱ�����ı�����
    }

    public void OnPointerEnter(PointerEventData eventData)//��������
    {
        if (�Ǽ��ܰ�ť)
        {

            if ( B2.Instance.��ǰ�ж���ɫ.GetComponent<A4>().�������� != null)
            {
                �����ı�.text = B2.Instance.��ǰ�ж���ɫ.GetComponent<A4>().��������; // �����ı�Ϊָ�����ı�
                �����ı�.gameObject.SetActive(true); // ��ʾ�ı�����
            }
        }
        if (���չ���ť)
        {

            if  (B2.Instance.��ǰ�ж���ɫ.GetComponent<A4>().�չ����� != null)
            {
                �չ��ı�.text = B2.Instance.��ǰ�ж���ɫ.GetComponent<A4>().�չ�����; // �����ı�Ϊָ�����ı�
                �չ��ı�.gameObject.SetActive(true); // ��ʾ�ı�����
            }
        }
        if (���ٻ�����ť)
        {
            if (B2.Instance.��ǰ�ж���ɫ.GetComponent<A4>().�ٻ������� != null)
            {
                �ٻ��ı�.text = B2.Instance.��ǰ�ж���ɫ.GetComponent<A4>().�ٻ�������; // �����ı�Ϊָ�����ı�
                �ٻ��ı�.gameObject.SetActive(true); // ��ʾ�ı�����
            }
        }
        if (����������ť)
        {
            if (B2.Instance.��ǰ�ж���ɫ.GetComponent<A4>().���������� != null)
            {
                �����ı�.text = B2.Instance.��ǰ�ж���ɫ.GetComponent<A4>().����������; // �����ı�Ϊָ�����ı�
                �����ı�.gameObject.SetActive(true); // ��ʾ�ı�����
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)//������Ƴ�
    {
        if (�Ǽ��ܰ�ť)
        {

            if (B2.Instance.��ǰ�ж���ɫ.GetComponent<A4>().�������� != null)
            {
                �����ı�.gameObject.SetActive(false); // �����ı�
            }
        }
        else
        if (���չ���ť)
        {

            if (B2.Instance.��ǰ�ж���ɫ.GetComponent<A4>().�չ����� != null)
            { 
                �չ��ı�.gameObject.SetActive(false); // �����ı�
            }
        }
        else if (���ٻ�����ť)
        {
            if (B2.Instance.��ǰ�ж���ɫ.GetComponent<A4>().�ٻ������� != null)
            {
                �ٻ��ı�.gameObject.SetActive(false); // �����ı�
            }
        }
        else if (����������ť)
        {
            if (B2.Instance.��ǰ�ж���ɫ.GetComponent<A4>().���������� != null)
            {
                �����ı�.gameObject.SetActive(false); // �����ı�
            }
        }
    }
    
}
