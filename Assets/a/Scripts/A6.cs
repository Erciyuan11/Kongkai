using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class A6 : MonoBehaviour//�������ܣ��ٻ�������Ԫ������
{
    [Header("���ڽ�ɫ���ϣ���ɫ�ж�ָ�������󴥷�Ч��")]
    public int �ж�ָ��������;
    public int ��ǰ�ж�����=0;
    public GameObject ��ǰ�ж���ɫ;
    public GameObject �ٻ�����; // �������Ҫ���ɵ���Ϸ���������
    bool �Ƿ���������ж�����;
    public bool �ͷż��������ж�����=false;
    bool �����ж�����=true;
    public GameObject buffͼ��;
    GameObject ���ɵĶ���;
    GameObject ����λ��;
    public GameObject ���ɵ�buffͼ��;
    Image buff����;
    Text buff����;
    public bool �Ƿ����ʹ�þ���ǿ����;
    public GameObject ǿ��������Ч;
    GameObject ���ɵ�ǿ����Ч;
    public bool ����ǿ��״̬��;
    public bool �Ƿ���������ж�;
    //����ȫ������ֵ���ܺ�
    public float totalDamage = 0.0f;
    [Header("����������˭�ı���")]
    public bool ��Ԫ;
    public bool ����;
    void Start()
    {
        ����λ�� = GameObject.Find("���������ɫ��λ��");
    }

    // Update is called once per frame
    void Update()
    {
        ��ǰ�ж���ɫ = B1.Instance.���ϲ�Ķ���.GetComponent<A2>().�ж�����Ӧ��ɫ;
        if (��ǰ�ж���ɫ == transform.gameObject&& �����ж�����)
        {
            �Ƿ���������ж����� = true;
            if (��ǰ�ж���ɫ == transform.gameObject && �Ƿ���������ж�����&&���ɵĶ���==null&& �ͷż��������ж�����&& !����ǿ��״̬�� )
            {
                ��ǰ�ж�����++;
                �Ƿ���������ж����� = false;
                �����ж����� = false;
                �ͷż��������ж����� = false;
                ����buffͼ��();
                buff����.text= ��ǰ�ж�����.ToString();
                //Debug.Log("11");
            }
            if (��ǰ�ж���ɫ == transform.gameObject && �Ƿ���������ж����� && ���� && �Ƿ����ʹ�þ���ǿ����&& ����ǿ��״̬��)
            {
                ��ǰ�ж�����--;
                �Ƿ���������ж����� = false;
                �����ж����� = false;
                buff����.text = null;
                �����ҷ�ȫ������ֵ();
                if(��ǰ�ж����� == �ж�ָ��������)
                GetComponent<A1>().������ += totalDamage;
                if (��ǰ�ж����� == 0)
                {
                    �Ƿ����ʹ�þ���ǿ���� = false;
                    ����ǿ��״̬�� = false;
                    �����ж����� = true;
                    GetComponent<A1>().������ -= totalDamage;
                    Destroy(���ɵ�buffͼ��);
                    //totalDamage= 0;
                }
            }
        }

        if (��ǰ�ж���ɫ != transform.gameObject)
        {
            �����ж����� = true;
            �Ƿ���������ж����� = false;
            �ͷż��������ж����� = false;
        }

        if(��ǰ�ж�����== �ж�ָ��������)
        {
            if (��Ԫ)
            {
                �ٻ�();
                ��ǰ�ж����� = 0;
                buff����.text = ��ǰ�ж�����.ToString();
                Destroy(���ɵ�buffͼ��);
            }
            if (����&& !����ǿ��״̬��)
            {
                �����ҷ�ȫ������ֵ();
                GetComponent<A1>().������ += totalDamage;
                �Ƿ����ʹ�þ���ǿ���� = true;
                ����ǿ��״̬�� = true;
                Invoke("����ǿ����Ч", 1.5f);
                //totalDamage = 0;
            }
            buff����.text = null;
        }
        if (��ǰ�ж����� == 0&&����)//����ɾ��������ǿ����Ч
        {
            if (���ɵ�ǿ����Ч != null)
            {
                totalDamage = 0;
                GetComponent<A1>().������ -= totalDamage;
                Destroy(���ɵ�ǿ����Ч.gameObject);
            }
        }

            if (GetComponent<A1>().�Ƿ�ǿ��)
        {
            if (��Ԫ)
            {
                if (���ɵĶ��� != null)
                    ���ɵĶ���.GetComponent<A1>().�ӳ�ɾ��(1.3f);
                ��ǰ�ж����� = 0;
                buff����.text = ��ǰ�ж�����.ToString();
            }
        }
    }
    private void OnDestroy()
    {
       // if(���ɵĶ���!=null)
       // ���ɵĶ���.GetComponent<A1>().�ӳ�ɾ��(0.5f);
    }
    private void �ٻ�()
    {

        // ѭ��������Ϸ�����������5���Ӽ�
        for (int i = 0; i <1; i++)
        {
            GameObject randomObject = �ٻ�����;
            if (randomObject != null && gameObject.GetComponent<A1>().�Ƿ��ǵз� == false)
            {
                 ���ɵĶ��� = Instantiate(randomObject, ����λ��.transform);

            }
            else if (randomObject != null && gameObject.GetComponent<A1>().�Ƿ��ǵз�)
            {
                 ���ɵĶ��� = Instantiate(randomObject, ����λ��.transform);

            }
            ���ɵĶ���.GetComponent<A1>().������ = GetComponent<A1>().������;
        }
    }

    private void ������ǿ������()//���ͷż��ܵ�ʱ��ʹ��
    {

    }
    void ����ǿ����Ч()
    {
        if (ǿ��������Ч != null && ���ɵ�ǿ����Ч == null)
        {
            ���ɵ�ǿ����Ч = Instantiate(ǿ��������Ч, transform);
            �Ƿ���������ж� = true;
        }

    }
    public void �����ҷ�ȫ������ֵ()
    {
        totalDamage = 0;
        Transform parentTransform = B1.Instance.�ҷ�����;
        foreach (Transform childTransform in parentTransform)
        {
            GameObject childObject = childTransform.gameObject;

            // ��ȡ�Ӷ����ϵ�"A1"�ű�
            A1 script = childObject.GetComponent<A1>();

            // ����ű�����
            if (script != null)
            {
                // ������Ҫ���ٵ�����ֵ
                float maxHealth = script.�������ֵ;
                float damage = maxHealth * 0.1f;

                // ������ٵ�����ֵ�����������ֵ��10%����������Ϊ10%
                //damage = Mathf.Min(damage, maxHealth * 0.1f);

                // ���ٵ�ǰ����ֵ
                if (script.��ǰ����ֵ > damage)
                    script.��ǰ����ֵ -= damage;
                else
                    script.��ǰ����ֵ = 1;

                // ȷ����ǰ����ֵ��С��1
                //script.��ǰ����ֵ = Mathf.Max(script.��ǰ����ֵ, 1.0f);

                // �ۼ����˺�
                totalDamage += damage;
            }
        }
        // ��ӡ���˺�
        //Debug.Log("���˺���" + totalDamage);
    }
    void ����buffͼ��()
    {
        if(���ɵ�buffͼ��==null)
        ���ɵ�buffͼ�� = Instantiate(buffͼ��, GetComponent<A1>().buff����λ��);
        
        buff���� = ���ɵ�buffͼ��.GetComponent<Image>();
        if (��Ԫ)
            buff����.sprite = �ٻ�����.GetComponent<A1>().��ɫͷ��;
        if (����)
            buff����.sprite = GetComponent<A1>().��ɫͷ��;
        var js = buff����.transform.Find("Text");
        buff���� = js.GetComponent<Text>();
        buff����.text = ��ǰ�ж�����.ToString();

    }
}
