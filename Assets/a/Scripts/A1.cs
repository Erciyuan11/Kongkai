
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class A1 : MonoBehaviour//����������������
{
    [Header("��ɫ��Ҫ����")]
    public GameObject �ҷ���ɫ����Ԥ����;
    public float �ƶ���з����ٶ� = 7;
    public float ���ص��ٶ� = 12;
    public bool �����Ƿ�תͷ;
    public float �������ֵ = 100;
    public float ��ǰ����ֵ = 100;
    public int �ٶ� = 100;
    public float ������;
    public float ����;
    public float ������ = 10;
    public string ���� = "��";
    public Image �ж���ͼ��Ԥ����;
    public Sprite ��ɫͷ��;
    public Sprite ��ѡ�ж�����ɫͷ��;
    public float ����ɱ������ = 140;
    public float ������� = 5;
    public float ����ս����ʱ���������Զͣ�� = 2f;
    public GameObject ������Ч;
    public bool ������ɾ������;
        [HideInInspector]
    public Transform �ܵ��˺��������ı�����λ��;
    public GameObject �ı�Ԥ����;
    public GameObject Ч���ı�Ԥ����;
    public Image ����ͼ��Ԥ����;// ��Ҫ���Ƶ�ͼ��Ԥ����
    public string[] ��������; // ͼ�����������

    [Header("�з�����")]
    public bool �Ƿ��ǵз�;
    public bool �Ƿ�ӵ�������� = true;
    public GameObject �з���ɫ����Ԥ����;
            [HideInInspector]
    public Transform ��BOSS�з���ɫ��������λ��;
    public bool �Ƿ���boss;//����Ѫ���Ƿ�����Ļ�м�
    public bool �Ƿ��������ж���;
    public GameObject BOSS��ɫ����Ԥ����;

    [Header("�Ƿ�����ɫ��ɫ")]
    public bool �Ƿ�����ɫ�ٻ���;//�ٻ����ڹ������Զ����٣��Ҳ����ڽ�ɫ������
    public int ��ɫ�ٻ�����ڻغ�;
    public bool ���ٻ�ʱ�Ƿ������ж�;

    Transform boss��������λ��;
    Transform �������Ը���;
    Animator ����;
    float ��ǰ�����ٷֱ�;
    EasySlider ������;

    EasySlider Ѫ��;
    EasySlider ������;
    EasySlider ��ɱ������;
    UnityEngine.UI.Button ��ɱ����ť;
    Transform �ж�������;
    Image ����ͼ��;
    Text ��������;
    Transform �ҷ���ɫ�������;
    GameObject ���ɵĽ�ɫ����;//���ɵĽ�ɫ����
    float ��ǰ������;

    [Header("����ͼ�񣬿����ڽű����Զ���")]
    public Sprite ��;
    public Sprite ��;
    public Sprite ��;
    public Sprite ��;
    public Sprite ����;
    public Sprite ����;
    public Sprite ˮ;

    //[Header("����Ĳ��ù�")]
    [HideInInspector]
    public Transform buff����λ��;
            [HideInInspector]
    public bool ����;
        [HideInInspector]
    public bool ����;
            [HideInInspector]
    public float ��ǰ��ɱ������;
            [HideInInspector]
    public Image �Լ���Ӧ���ж���;
            [HideInInspector]
    public Image �Լ���Ӧ���ж���1;
            [HideInInspector]
    public GameObject ���ɵĶ�����Ч;
            [HideInInspector]
    public int ��ɫ�ٻ���ʣ��غ�;
    [Header("��ɫ�ܵ�Ч��")]
            [HideInInspector]
    public bool �Ƿ�ǿ��;
            [HideInInspector]
    public bool �Ƿ��ܵ����������˺�;
            [HideInInspector]
    public bool �Ƿ��ܵ����������˺�;
            [HideInInspector]
    public bool �Ƿ�����������;
            [HideInInspector]
    public bool �Ƿ񽵵ͷ�����;
            [HideInInspector]
    public string �Լ��ܵ��ĳ����˺�����;
            [HideInInspector]
    public int �ܵ��ĳ����˺�������;
            [HideInInspector]
    public Color �ı���ɫ;
            [HideInInspector]
    public Color �����˺�A��ɫ;
            [HideInInspector]
    public Color �����˺�B��ɫ;
    // ��ʼ��
    private void OnEnable()
    {
        if (������Ч != null)
        {
            var ���ɳ�����Ч = Instantiate(������Ч, transform);
            Destroy(���ɳ�����Ч, 1);
        }
    }
    private void Start()
    {
        var a = transform.Find("��ݽ�ɫ����");
        �ܵ��˺��������ı�����λ�� = a.transform.Find("�ܵ��˺��������ı�����λ��");
        ��BOSS�з���ɫ��������λ�� = a.transform.Find("�з���ɫ��������λ��");

        GameObject ��Ч���Ŷ��� = GameObject.Find("��ɫ��Ч����λ��");

        //��ǰ��ɱ������ = 30;
        �ҷ���ɫ������� = B1.Instance.��ɫ���Ը���;
        boss��������λ�� = B1.Instance.boss���Ը���;
        �ж������� = B1.Instance.�ж�������;

        Image newImage = Instantiate(�ж���ͼ��Ԥ����, �ж�������);
        if (��ѡ�ж�����ɫͷ�� != null)
            newImage.sprite = ��ѡ�ж�����ɫͷ��;
        else
            newImage.sprite = ��ɫͷ��;
        newImage.GetComponent<A2>().�ж�����Ӧ��ɫ = this.gameObject;
        �Լ���Ӧ���ж��� = newImage;
        if (�Ƿ��������ж���)
        {
            Image newImage1 = Instantiate(�ж���ͼ��Ԥ����, �ж�������);
            if (��ѡ�ж�����ɫͷ�� != null)
                newImage1.sprite = ��ѡ�ж�����ɫͷ��;
            else
                newImage1.sprite = ��ɫͷ��;
            newImage1.GetComponent<A2>().�ж�����Ӧ��ɫ = this.gameObject;
            �Լ���Ӧ���ж���1 = newImage;
        }
        if (�Ƿ�����ɫ�ٻ��� == false)
        {
            if (!�Ƿ��ǵз�)//������ҷ���ɫ
            {
                ���ɵĽ�ɫ���� = Instantiate(�ҷ���ɫ����Ԥ����);
                ���ɵĽ�ɫ����.transform.SetParent(�ҷ���ɫ�������, false);

                var sx = ���ɵĽ�ɫ����.transform.Find("����ͼ��");
                ����ͼ�� = sx.gameObject.GetComponent<Image>();
                var xt = ���ɵĽ�ɫ����.transform.Find("Ѫ��");
                Ѫ�� = xt.gameObject.GetComponent<EasySlider>();
                var hd = ���ɵĽ�ɫ����.transform.Find("������");
                ������ = hd.gameObject.GetComponent<EasySlider>();
                if (�Ƿ�ӵ��������)
                {
                    var jd = ���ɵĽ�ɫ����.transform.Find("��ɱ������");
                    ��ɱ������ = jd.gameObject.GetComponent<EasySlider>();
                    var jd1 = ��ɱ������.transform.Find("fill");
                    jd1.gameObject.GetComponent<Image>().sprite = ��ɫͷ��;
                    var jd2 = ��ɱ������.transform.Find("outline");
                    jd2.gameObject.GetComponent<Image>().sprite = ��ɫͷ��;
                    var ��ť = ���ɵĽ�ɫ����.transform.Find("��ɱ����ť");
                    ��ɱ����ť = ��ť.GetComponent<UnityEngine.UI.Button>();
                    ��ɱ����ť.gameObject.GetComponent<Image>().sprite = ��ɫͷ��;
                }
                var b2 = transform.Find("��սUI");
                ��ɱ����ť.onClick.AddListener(�������Լ�ʹ�ñ�ɱ��);
                float ��ǰ���ܰٷֱ� = ���� / �������ֵ;
                ������.SetValue(��ǰ���ܰٷֱ�);
                var ���� = ���ɵĽ�ɫ����.transform.Find("������");
                ������ = ����.GetComponent<EasySlider>();
                ��ǰ������ = �������;
                float ��ǰ�������ٷֱ� = ��ǰ������ / �������;
                ������.SetValue(��ǰ�������ٷֱ�);
                var wz = ���ɵĽ�ɫ����.transform.Find("��������λ��");
                �������Ը��� = wz.gameObject.transform;
                if (��������.Length > 0)
                {
                    ��������ͼ��();
                }
            }
            else//����ǵз�
            {
                if (�Ƿ���boss == false)
                {

                    ���ɵĽ�ɫ���� = Instantiate(�з���ɫ����Ԥ����);
                    ���ɵĽ�ɫ����.transform.SetParent(��BOSS�з���ɫ��������λ��, false);
                    var wz = ���ɵĽ�ɫ����.transform.Find("��������λ��");
                    �������Ը��� = wz.gameObject.transform;
                    var xt = ���ɵĽ�ɫ����.transform.Find("Ѫ��");
                    Ѫ�� = xt.gameObject.GetComponent<EasySlider>();
                    var hd = ���ɵĽ�ɫ����.transform.Find("������");
                    ������ = hd.gameObject.GetComponent<EasySlider>();
                    var ���� = ���ɵĽ�ɫ����.transform.Find("������");
                    var sx = ���ɵĽ�ɫ����.transform.Find("����ͼ��");
                    ����ͼ�� = sx.gameObject.GetComponent<Image>();
                    ������ = ����.GetComponent<EasySlider>();
                    ��ǰ������ = �������;
                    float ��ǰ�������ٷֱ� = ��ǰ������ / �������;
                    ������.SetValue(��ǰ�������ٷֱ�);
                    if (�Ƿ�ӵ��������)
                    {
                        var jd = ���ɵĽ�ɫ����.transform.Find("��ɱ������");
                        ��ɱ������ = jd.gameObject.GetComponent<EasySlider>();
                        var jd1 = ��ɱ������.transform.Find("fill");
                        jd1.gameObject.GetComponent<Image>().sprite = ��ɫͷ��;
                        var jd2 = ��ɱ������.transform.Find("outline");
                        jd2.gameObject.GetComponent<Image>().sprite = ��ɫͷ��;
                    }
                }
                else//�����boos
                {
                    ���ɵĽ�ɫ���� = Instantiate(BOSS��ɫ����Ԥ����);
                    // ��ʵ������UI����ĸ���������ΪĿ����壬ʹ���ΪĿ�������Ӷ���
                    ���ɵĽ�ɫ����.transform.SetParent(boss��������λ��, false);

                    var sx = ���ɵĽ�ɫ����.transform.Find("����ͼ��");
                    ����ͼ�� = sx.gameObject.GetComponent<Image>();
                    var xt = ���ɵĽ�ɫ����.transform.Find("Ѫ��");
                    Ѫ�� = xt.gameObject.GetComponent<EasySlider>();
                    var hd = ���ɵĽ�ɫ����.transform.Find("������");
                    ������ = hd.gameObject.GetComponent<EasySlider>();
                    if (�Ƿ�ӵ��������)
                    {
                        var jd = ���ɵĽ�ɫ����.transform.Find("��ɱ������");
                        ��ɱ������ = jd.gameObject.GetComponent<EasySlider>();
                        var jd1 = ��ɱ������.transform.Find("fill");
                        jd1.gameObject.GetComponent<Image>().sprite = ��ɫͷ��;
                        var jd2 = ��ɱ������.transform.Find("outline");
                        jd2.gameObject.GetComponent<Image>().sprite = ��ɫͷ��;
                        var ��ť = ���ɵĽ�ɫ����.transform.Find("��ɱ����ť");
                    }
                    float ���ܰٷֱ� = ���� / �������ֵ;
                    ������.SetValue(���ܰٷֱ�);
                    var ���� = ���ɵĽ�ɫ����.transform.Find("������");
                    ������ = ����.GetComponent<EasySlider>();
                    ��ǰ������ = �������;
                    float ��ǰ�������ٷֱ� = ��ǰ������ / �������;
                    ������.SetValue(��ǰ�������ٷֱ�);
                    var wz = ���ɵĽ�ɫ����.transform.Find("��������λ��");
                    �������Ը��� = wz.gameObject.transform;
                }

                if (��������.Length > 0)
                {
                    ��������ͼ��();
                }
                float ��ǰ���ܰٷֱ� = ���� / �������ֵ;
                ������.SetValue(��ǰ���ܰٷֱ�);
                buff����λ�� = ���ɵĽ�ɫ����.transform.Find("buff����λ��");
            }



            UpdateImageSprite();
            var smz = ���ɵĽ�ɫ����.transform.Find("����ֵ");
            �������� = smz.GetComponent<Text>();
            ��������.text = ��ǰ����ֵ.ToString();
            buff����λ�� = ���ɵĽ�ɫ����.transform.Find("buff����λ��");
        }
        if (���ٻ�ʱ�Ƿ������ж�)
        {
            //�����ж�();
            B2.Instance.����ʹ�ñ�ɱ��(this);
        }
    }
    private void Update()
    {
        if (�Ƿ�����ɫ�ٻ��� == false&&��ǰ����ֵ>0)
        {
            ��������.text = ��ǰ����ֵ.ToString();
            ���ñ�ɱ��();
            ˢ��������();
            float ��ǰ���ܰٷֱ� = ���� / �������ֵ;
            ������.SetValue(��ǰ���ܰٷֱ�);
            float ��ǰ�����ٷֱ� = ��ǰ����ֵ / �������ֵ;
            if (��ǰ����ֵ > �������ֵ)
                ��ǰ����ֵ = �������ֵ;
            Ѫ��.SetValue(��ǰ�����ٷֱ�);
        }
        if (�Ƿ��ǵз�)
        {
            if (��ǰ��ɱ������ >= ����ɱ������)
            {
                �����ж�();
            }
        }
        if (��ǰ����ֵ <= 0)
        {
            ���� = true;
            ��ǰ����ֵ = 0;

            if (B2.Instance.isAttacking == false)
                �ӳ�ɾ��(1f);
        }
    }
    // �ܵ��˺�
    public void NPC�ܵ��˺�(float damage, string ��������, int ������)
    {
        ���� = GetComponent<Animator>();
        �޸Ĺ����ı���ɫ(��������);
        if (���ڼ��з�����(��������, ��������))
        {
            ��ǰ������ -= ������;
            float ��ǰ�������ٷֱ� = ��ǰ������ / �������;
            ������.SetValue(��ǰ�������ٷֱ�, true);
        }
        if (��ǰ������ <= 0)
        {
            GetComponent<A4>().������ = false;
            GetComponent<A4>().���ɵ�������Ч = null;
            ���� = true;
            ����.SetBool("����", ����);
            �����˺��ı�(damage, �ı���ɫ);

            ����.SetTrigger("����");
            if (���� > damage)//���ֿܵ��˺�
            {
                ���� -= damage;
                float ��ǰ���ܰٷֱ� = ���� / �������ֵ;
                ������.SetValue(��ǰ���ܰٷֱ�);
            }
            else if (���� <= damage)
            {
                float ʣ���˺� = damage - ����;
                ���� = 0;
                float ��ǰ���ܰٷֱ� = ���� / �������ֵ;
                ������.SetValue(��ǰ���ܰٷֱ�);

                ��ǰ����ֵ -= ʣ���˺�;
                float ��ǰ�����ٷֱ� = ��ǰ����ֵ / �������ֵ;
                Ѫ��.SetValue(��ǰ�����ٷֱ�, true);
            }
            else if (���� <= 0)
            {
                ��ǰ����ֵ -= damage;
                float ��ǰ�����ٷֱ� = ��ǰ����ֵ / �������ֵ;
                Ѫ��.SetValue(��ǰ�����ٷֱ�, true);
            }
        }
        else
        {
            // ���Ƿ��������˺�����
            float ʵ���ܵ��˺� = Mathf.Max(damage - ������, 0);
            �����˺��ı�(ʵ���ܵ��˺�, �ı���ɫ);
            ����.SetTrigger("����");
            if (���� > ʵ���ܵ��˺�)//���ֿܵ��˺�
            {
                ���� -= ʵ���ܵ��˺�;
                float ��ǰ���ܰٷֱ� = ���� / �������ֵ;
                ������.SetValue(��ǰ���ܰٷֱ�);
            }
            else if (���� <= ʵ���ܵ��˺�)
            {
                float ʣ���˺� = ʵ���ܵ��˺� - ����;
                ���� = 0;
                float ��ǰ���ܰٷֱ� = ���� / �������ֵ;
                ������.SetValue(��ǰ���ܰٷֱ�);

                ��ǰ����ֵ -= ʣ���˺�;
                float ��ǰ�����ٷֱ� = ��ǰ����ֵ / �������ֵ;
                Ѫ��.SetValue(��ǰ�����ٷֱ�, true);
            }
            else if (���� <= 0)
            {
                ��ǰ����ֵ -= ʵ���ܵ��˺�;
                float ��ǰ�����ٷֱ� = ��ǰ����ֵ / �������ֵ;
                Ѫ��.SetValue(��ǰ�����ٷֱ�, true);
            }


        }

        if (��ǰ����ֵ <= 0)
        {
            ���� = true;
            //��ָ��ͼ������ƵĶ����������
            �ӳ�ɾ��(1f);
        }
        //���ñ�ɱ��();
        ��������.text = ��ǰ����ֵ.ToString();
    }
    public void �ָ�����()
    {
        if (����)
        {
            ���� = false;
            ����.SetBool("����", ����);
            ��ǰ������ = �������;
            float ��ǰ�������ٷֱ� = ��ǰ������ / �������;
            ������.SetValue(��ǰ�������ٷֱ�, true);
        }
    }
    public void �ָ�����()
    {
        if (�Ƿ�ǿ��)
        {
            if (GetComponent<A4>().ʣ�౻ǿ�ػغ��� == 0)
            {
                �Ƿ�ǿ�� = false;
                ����.SetBool("����", �Ƿ�ǿ��);
                if (���ɵĶ�����Ч != null)
                {
                    Destroy(���ɵĶ�����Ч);
                }
                if (GetComponent<A4>().���ɵ�ǿ��buffͼ�� != null)
                {
                    Destroy(GetComponent<A4>().���ɵ�ǿ��buffͼ��.gameObject);
                }
            }
        }
    }
    public void �ָ�����������()
    {
        if (�Ƿ�����������)
        {
            if (GetComponent<A4>().ʣ�๥���������غ��� == 0)
            {
                ������ -= GetComponent<A4>().�����Ĺ�����;
                �Ƿ����������� = false;
                if (GetComponent<A4>().���ɵĹ���������buffͼ�� != null)
                    Destroy(GetComponent<A4>().���ɵĹ���������buffͼ��.gameObject);

            }
        }
    }
    public void �ָ�������������()
    {
        if (�Ƿ񽵵ͷ�����)
        {
            if (GetComponent<A4>().ʣ����������ͻغ��� == 0)
            {
                ������ += GetComponent<A4>().���͵ķ�����;
                �Ƿ񽵵ͷ����� = false;
                if (GetComponent<A4>().���ɵķ���������buffͼ�� != null)
                    Destroy(GetComponent<A4>().���ɵķ���������buffͼ��.gameObject);

            }
        }
    }
    public void �ָ��ܵ������˺�()
    {
        if (�Ƿ��ܵ����������˺�)
        {
            if (GetComponent<A4>().ʣ������˺�A�غ��� == 0)
            {
                �Ƿ��ܵ����������˺� = false;
                if (GetComponent<A4>().���ɵĳ��������˺�buffͼ�� != null)
                    Destroy(GetComponent<A4>().���ɵĳ��������˺�buffͼ��.gameObject);

            }
        }
        if (�Ƿ��ܵ����������˺�)
        {
            if (GetComponent<A4>().ʣ������˺�B�غ��� == 0)
            {
                �Ƿ��ܵ����������˺� = false;
                if (GetComponent<A4>().���ɵĳ��������˺�buffͼ�� != null)
                    Destroy(GetComponent<A4>().���ɵĳ��������˺�buffͼ��.gameObject);

            }
        }
    }

    public void �ܵ��˺�(float damage, string ��������, int ������)
    {
        ���� = GetComponent<Animator>();
        �޸Ĺ����ı���ɫ(��������);
        ����.SetTrigger("����");
        float ʵ���ܵ��˺� = Mathf.Max(damage - ������, 0);
        �����˺��ı�(ʵ���ܵ��˺�, �ı���ɫ);
        // ���Ƿ��������˺�����
        if (���� > ʵ���ܵ��˺�)//���ֿܵ��˺�
        {
            ���� -= ʵ���ܵ��˺�;
            float ��ǰ���ܰٷֱ� = ���� / �������ֵ;
            ������.SetValue(��ǰ���ܰٷֱ�);
        }
        else if (���� <= ʵ���ܵ��˺�)
        {
            float ʣ���˺� = ʵ���ܵ��˺� - ����;
            ���� = 0;
            float ��ǰ���ܰٷֱ� = ���� / �������ֵ;
            ������.SetValue(��ǰ���ܰٷֱ�);

            ��ǰ����ֵ -= ʣ���˺�;
            float ��ǰ�����ٷֱ� = ��ǰ����ֵ / �������ֵ;
            Ѫ��.SetValue(��ǰ�����ٷֱ�, true);
        }
        else if (���� <= 0)
        {
            ��ǰ����ֵ -= ʵ���ܵ��˺�;
            float ��ǰ�����ٷֱ� = ��ǰ����ֵ / �������ֵ;
            Ѫ��.SetValue(��ǰ�����ٷֱ�, true);

        }
        if (��ǰ����ֵ <= 0)
        {
            ���� = true;
            ��ǰ����ֵ = 0;
            B2.Instance.�������� = true;
            if (B2.Instance.isAttacking == false)
                �ӳ�ɾ��(1f);
        }

        if (��������.Length > 0)
        {

            if (���ڼ��з�����(��������, ��������))
            {
                ��ǰ������ -= ������;
                float ��ǰ�������ٷֱ� = ��ǰ������ / �������;
                ������.SetValue(��ǰ�������ٷֱ�, true);
            }
            if (��ǰ������ <= 0)
            {
                GetComponent<A4>().������ = false;
                ���� = true;
                ����.SetBool("����", ����);
                GetComponent<A4>().���ɵ�������Ч = null;
            }
        }

        ��������.text = ��ǰ����ֵ.ToString();
    }
    public void �ܵ������˺�(float damage, string ��������, int ������)
    {
        ���� = GetComponent<Animator>();
        �޸Ĺ����ı���ɫ(��������);
        if (GetComponent<A4>().�õз������ܵ��˺�A)
            ����Ч���ı�(B1.Instance.�����˺�A����, �ı���ɫ);
        else if (GetComponent<A4>().�õз������ܵ��˺�B)
            ����Ч���ı�(B1.Instance.�����˺�B����, �ı���ɫ);
        ����.SetTrigger("����");
        �����˺��ı�(damage, �ı���ɫ);
        // �����Ƿ�����
            ��ǰ����ֵ -= damage;
            float ��ǰ�����ٷֱ� = ��ǰ����ֵ / �������ֵ;
            Ѫ��.SetValue(��ǰ�����ٷֱ�, true);


        if (��ǰ����ֵ <= 0)
        {
            ���� = true;
            ��ǰ����ֵ = 0;
            B2.Instance.�������� = true;
            if (B2.Instance.isAttacking == false)
                �ӳ�ɾ��(1f);
        }

        if (��������.Length > 0)
        {

            if (���ڼ��з�����(��������, ��������))
            {
                ��ǰ������ -= ������;
                float ��ǰ�������ٷֱ� = ��ǰ������ / �������;
                ������.SetValue(��ǰ�������ٷֱ�, true);
            }
            if (��ǰ������ <= 0)
            {
                GetComponent<A4>().������ = false;
                ���� = true;
                ����.SetBool("����", ����);
                GetComponent<A4>().���ɵ�������Ч = null;
            }
        }

        ��������.text = ��ǰ����ֵ.ToString();

    }
    public void �������ܹ���(GameObject game, float damage, string ��������)
    {
        //Debug.Log(��������);
        if (game.GetComponent<A1>() != null && game.GetComponent<A1>().�Ƿ��ǵз�)
            game.GetComponent<A1>().�ܵ��˺�(damage, ��������, GetComponent<A4>().���ܹ�������ֵ);
        if (game.GetComponent<A1>() != null && game.GetComponent<A1>().�Ƿ��ǵз� == false)
            game.GetComponent<A1>().�ܵ��˺�(damage, ��������, GetComponent<A4>().���ܹ�������ֵ);
        ���ñ�ɱ��();
    }//ֻ��Ҫʹ�÷������������Զ���Ŀ���ܵ��˺�
    public void ��������(GameObject game, float damage, string ��������)
    {
        //Debug.Log(��������);
        if (game.GetComponent<A1>() != null && game.GetComponent<A1>().�Ƿ��ǵз�)
            game.GetComponent<A1>().�ܵ��˺�(damage, ��������, GetComponent<A4>().�չ�����ֵ);
        if (game.GetComponent<A1>() != null && game.GetComponent<A1>().�Ƿ��ǵз� == false)
            game.GetComponent<A1>().�ܵ��˺�(damage, ��������, GetComponent<A4>().�չ�����ֵ);

        ���ñ�ɱ��();
    }//ֻ��Ҫʹ�÷������������Զ���Ŀ���ܵ��˺�
    public void ������������(GameObject game, float damage, string ��������)
    {
        //��ǰ��ɱ������ += 20;
        if (game.GetComponent<A1>() != null && game.GetComponent<A1>().�Ƿ��ǵз�)
            game.GetComponent<A1>().�ܵ��˺�(damage, ��������, GetComponent<A4>().������������ֵ);
        if (game.GetComponent<A1>() != null && game.GetComponent<A1>().�Ƿ��ǵз� == false)
            game.GetComponent<A1>().�ܵ��˺�(damage, ��������, GetComponent<A4>().������������ֵ);
    }//ֻ��Ҫʹ�÷������������Զ���Ŀ���ܵ��˺�
    public void �ظ�����(float �ظ�����ֵ)
    {

        Vector3 textSpawnPosition = �ܵ��˺��������ı�����λ��.position;

        // ��XY���������������0.3
        textSpawnPosition.x += UnityEngine.Random.Range(-0.3f, 0.3f);
        textSpawnPosition.y += UnityEngine.Random.Range(-0.2f, 0.2f);
        // �����ı�
        GameObject damageTextInstance = Instantiate(�ı�Ԥ����, textSpawnPosition, Quaternion.identity);
        Text damageTextMesh = damageTextInstance.GetComponentInChildren<Text>();
        // �����ı����ݺ���ɫ
        damageTextMesh.text = �ظ�����ֵ.ToString();
        damageTextMesh.color = Color.green;
        if (��ǰ����ֵ < �������ֵ)
        {
            ��ǰ����ֵ += �ظ�����ֵ;
            float ��ǰ�����ٷֱ� = ��ǰ����ֵ / �������ֵ;
            Ѫ��.SetValue(��ǰ�����ٷֱ�, true);
        }
        else if (��ǰ����ֵ >= �������ֵ)
            ��ǰ����ֵ = �������ֵ;

        ��������.text = ��ǰ����ֵ.ToString();
        Destroy(damageTextInstance, 0.7f);
    }

    public void ���ӻ���(float ���ӻ�����)
    {
        Vector3 textSpawnPosition = �ܵ��˺��������ı�����λ��.position;

        // ��XY���������������0.3
        textSpawnPosition.x += UnityEngine.Random.Range(-0.3f, 0.3f);
        textSpawnPosition.y += UnityEngine.Random.Range(-0.2f, 0.2f);
        // �����ı�
        GameObject damageTextInstance = Instantiate(�ı�Ԥ����, textSpawnPosition, Quaternion.identity);
        Text damageTextMesh = damageTextInstance.GetComponentInChildren<Text>();
        // �����ı����ݺ���ɫ
        damageTextMesh.text = ���ӻ�����.ToString();
        damageTextMesh.color = Color.yellow;
        if (���� < �������ֵ)
        {
            ���� += ���ӻ�����;
        }
        else
            ���� = �������ֵ;

        float ��ǰ���ܰٷֱ� = ���� / �������ֵ;
        Ѫ��.SetValue(��ǰ���ܰٷֱ�);
        //Debug.Log(��ǰ���ܰٷֱ�);
        Destroy(damageTextInstance, 0.7f);
    }
    // �����Ա����ı�ʱ����ͼ����
    public void ˢ��ͼ��()
    {
        UpdateImageSprite();
    }
    // �����������Ƹ���ͼ����
    private void UpdateImageSprite()
    {
        if (����ͼ�� != null)
            ����ͼ��.sprite = GetAttributeSprite(����);

    }

    // �����������ƻ�ȡ��Ӧ������ͼ����
    private Sprite GetAttributeSprite(string attributeName)
    {
        switch (attributeName)
        {
            case "��":
                return ��;
            case "��":
                return ��;
            case "��":
                return ��;
            case "��":
                return ��;
            case "����":
                return ����;
            case "����":
                return ����;
            case "ˮ":
                return ˮ;
            default:
                return null;
        }
    }
    // �������������л���Ӧ�Ĺ�����ɫ
    private void �޸Ĺ����ı���ɫ(string ��������)
    {

        switch (��������)
        {
            case "��":
                �ı���ɫ = Color.red;
                break;
            case "��":
                �ı���ɫ = new Color(0.5f, 0.5f, 1f); // ����ɫ
                break;
            case "ˮ":
                �ı���ɫ = Color.blue;
                break;
            case "��":
                �ı���ɫ = Color.magenta; // ��ɫ
                break;
            case "��":
                �ı���ɫ = Color.green;
                break;
            case "����":
                �ı���ɫ = Color.gray; // ��ɫ
                break;
            case "����":
                �ı���ɫ = Color.yellow; // ��ɫ
                break;
        }
    }
    // ����
    private void ��������ͼ��()
    {
        foreach (string �������� in ��������)
        {
            if (����ͼ��Ԥ���� != null)
            {
                // ����һ���µ�ͼ����������侫��
                Image newImage = Instantiate(����ͼ��Ԥ����, �������Ը���);
                newImage.gameObject.SetActive(true);
                newImage.sprite = GetAttributeSprite(��������);
                //Debug.Log("��������");
            }

        }
    }

    public void ��������()
    {
        ��ɱ����ť.onClick.AddListener(B1.Instance.�ͷű�ɱ��);
    }
    public void �ӳ�ɾ��(float �ӳ�ʱ��)
    {
        GetComponent<Animator>().SetTrigger("����");

        if (�Լ���Ӧ���ж��� != null)
            Destroy(�Լ���Ӧ���ж���.gameObject);
        if (���ɵĽ�ɫ���� != null)
            Destroy(���ɵĽ�ɫ����.gameObject);
        Invoke("Die", �ӳ�ʱ��);

    }
    public void Die()
    {
        Destroy(this.gameObject);
    }

    bool ���ڼ��з�����(string[] array, string target)//���ڼ��з�����
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

    public void ���ñ�ɱ��()
    {
        if (��ɱ����ť != null)
        {
            if (��ǰ�����ٷֱ� >= 1)
            {
                ��ǰ�����ٷֱ� = 1;
                ��ɱ����ť.gameObject.SetActive(true);
            }
            else
                ��ɱ����ť.gameObject.SetActive(false);
        }

    }
    public void �������Լ�ʹ�ñ�ɱ��()
    {
        B2.Instance.����ʹ�ñ�ɱ��(this);
    }
    void ˢ��������()
    {
        if (��ɱ������ != null)
        {
            ��ǰ�����ٷֱ� = ��ǰ��ɱ������ / ����ɱ������;
            ��ɱ������.SetValue(��ǰ�����ٷֱ�);
            //Debug.Log("111");
        }
    }

    void �����ж�()
    {
        //Debug.Log("11");
        if (B2.Instance.��ǰ�ж���ɫ.GetComponent<A1>().�Լ���Ӧ���ж��� != null)
        {
            //Debug.Log("22");
            Transform targetTransform = B2.Instance.��ǰ�ж���ɫ.GetComponent<A1>().�Լ���Ӧ���ж���.transform;
            // Ŀ����ж���
            Transform objectToMove = �Լ���Ӧ���ж���.transform;

            if (objectToMove != null && targetTransform != null && B1.Instance.���еڶ��Ķ��� != objectToMove && targetTransform != objectToMove)
            {
                //Debug.Log("33");
                if (targetTransform != null)
                {
                    Debug.Log("44");
                    // ��ȡĿ�����Ĳ㼶����λ��
                    int targetSiblingIndex = targetTransform.GetSiblingIndex();

                    // ����Ҫ�ƶ��Ķ������õ�Ŀ�����ĺ󷽣����� + 1 ��λ�ã�
                    objectToMove.SetSiblingIndex(targetSiblingIndex + 1);
                    ����Ч���ı�("�ж�����ӿ�", Color.red);
                }
                ��ǰ��ɱ������ = 0;
            }
        }

    }
    public void �����˺��ı�(float damageValue, Color textColor)
    {

        Vector3 textSpawnPosition = �ܵ��˺��������ı�����λ��.position;

        // ��XY���������������0.3
        textSpawnPosition.x += UnityEngine.Random.Range(-0.3f, 0.3f);
        textSpawnPosition.y += UnityEngine.Random.Range(-0.2f, 0.2f);

        // �����ı�
        GameObject damageTextInstance = Instantiate(�ı�Ԥ����, textSpawnPosition, Quaternion.identity);
        Text damageTextMesh = damageTextInstance.GetComponentInChildren<Text>();
        // �����ı����ݺ���ɫ
        damageTextMesh.text = damageValue.ToString();
        damageTextMesh.color = textColor;
        // ����Э������һ��������˺��ı�
        // StartCoroutine(DestroyDamageText(damageTextInstance));
        Destroy(damageTextInstance.gameObject, 0.7f);
    }
    public void ����Ч���ı�(string damageValue, Color textColor)
    {
        //Debug.Log("22");
        Vector3 textSpawnPosition = �ܵ��˺��������ı�����λ��.position;

        // ��XY���������������0.3
        textSpawnPosition.x += UnityEngine.Random.Range(-0.3f, 0.3f);
        textSpawnPosition.y += UnityEngine.Random.Range(-0.3f, 0.3f);
        GameObject damageTextInstance = Instantiate(Ч���ı�Ԥ����, textSpawnPosition + new Vector3(0, 1, 0), Quaternion.identity);
        Text damageTextMesh = damageTextInstance.GetComponentInChildren<Text>();
        // �����ı����ݺ���ɫ
        damageTextMesh.text = damageValue;
        damageTextMesh.color = textColor;
        // ����Э������һ��������˺��ı�
        Destroy(damageTextInstance.gameObject, 0.7f);
    }
}
