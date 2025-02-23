using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class B1 : Singleton<B1>//���ƽ�ɫ���ܺ��չ�,���ɽ�ɫ�͵���
{

    [Header("ս����")]
    public int ��ǰս���� = 3;
    public int ���ս���� = 5; // ���ս������
    public Image ս����ͼ��Ԥ����; // ��������ͼ��Ԥ����
    Transform ս���㸸��;

    [Header("�����ж���������Ķ���")]
    public Transform �����ж���; // ָ���ĸ�����
    public Transform ���ϲ�Ķ���;
    [HideInInspector]
    public Transform ���еڶ��Ķ���;

    public GameObject[] �ҷ���ɫ;
    public Transform �ҷ�����;

    public GameObject[] �з���ɫ;
    public Transform �з�����;

    public Transform ��ɫ���Ը���;
    public Transform boss���Ը���;
    public Transform �ж�������;

    public GameObject ��ʾ��ǰ�ж���ɫ���ı�; // Ҫ������Ϸ����

    [Header("buff���")]
    public string �����˺�A���� = "����";
    public Sprite �����˺�ͼ��A;
    public string �����˺�B���� = "����";
    public Sprite �����˺�ͼ��B;
    public Sprite ����������ͼ��;
    public Sprite �������½�ͼ��;
    [Header("ǿ����أ�������Ŀ���޷�����������ָ���غϺ����")]
    public string ǿ������ = "����";
    public Sprite ǿ��ͼ��;
    [Header("����Buffʱ��Ŀ�����ϲ�������Ч")]
    public GameObject ͣ����Ŀ�����ϵ�ǿ����Ч;
    public GameObject ��ɳ����˺�Aʱ����Ч;
    public GameObject ��ɳ����˺�Bʱ����Ч;

    private void OnEnable()
    {
        ���ɽ�ɫ();
        ���ϲ�Ķ��� = FindTopObject(�ж�������);
    }

    private void Start()
    {

        //���ϲ�Ķ��� = FindTopObject(�ж�������);
        ��ʾ��ǰ�ж���ɫ���ı�.gameObject.SetActive(false);

        //���ɽ�ɫ();

        ս���㸸�� = transform.Find("ս����UI");
        CreateImages(��ǰս����);
    }

    private void Update()
    {
        ���ϲ�Ķ��� = FindTopObject(�ж�������);
        //Debug.Log("���ϲ�Ķ�����" + ���ϲ�Ķ���.GetComponent<A2>().�ж�����Ӧ��ɫ.name);
        ��ʾ��ǰ�ж���ɫ�ı�();

        ���еڶ��Ķ��� = GetSecondChildFromTop(�����ж���);

    }


    private void CreateImages(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Image newImageObj = Instantiate(ս����ͼ��Ԥ����, ս���㸸��);
            Image newImage = newImageObj.GetComponent<Image>();
            newImage.transform.SetParent(ս���㸸��, false);
        }
    }

    // ���ô˺���������ͼ������
    public void ����ս����()
    {
        if (��ǰս���� < ���ս����)
        {
            ��ǰս����++;
            CreateImages(1);
        }
    }

    // ���ô˺����Լ���ͼ������
    public void ����ս����()
    {
        if (��ǰս���� > 0)
        {
            ��ǰս����--;
            // ��ȡ�������µ��������Ӷ���ɾ��
            Transform topChild = ս���㸸��.GetChild(0);
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

            // �ݹ����Ӷ�����Ӷ���
            Transform childTop = FindTopObject(child);
            if (childTop != null && childTop.position.y > highestY)
            {
                highestY = childTop.position.y;
                topObject = childTop;
            }
        }

        return topObject;
    }//�����ж������ϲ�Ķ���

    public void �ͷű�ɱ��()
    {

    }
    public void ���ɽ�ɫ()
    {
        foreach (GameObject objPrefab in �ҷ���ɫ)
        {
            GameObject spawnedObject = Instantiate(objPrefab, �ҷ�����);
        }
        foreach (GameObject objPrefab in �з���ɫ)
        {
            GameObject spawnedObject = Instantiate(objPrefab, �з�����);
        }
    }


    public void ��ʾ��ǰ�ж���ɫ�ı�()
    {
        // ����Ӽ��Ƿ�Ϊ��
        if (�����Ϸ������Ӽ��Ƿ�Ϊ��(�����ж���.gameObject))
        {
            ��ʾ��ǰ�ж���ɫ���ı�.SetActive(true); // ������Ϸ����
        }
        else
        {
            ��ʾ��ǰ�ж���ɫ���ı�.SetActive(false); // �ر���Ϸ����
        }
    }
    // �����Ϸ������Ӽ��Ƿ�Ϊ��
    private bool �����Ϸ������Ӽ��Ƿ�Ϊ��(GameObject obj)
    {
        int childCount = obj.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = obj.transform.GetChild(i);
            if (child.gameObject.activeSelf) // ֻ���Ǵ��ڼ���״̬���Ӷ���
            {
                return true; // ���ڷǿ��Ӽ�
            }
        }
        return false; // �Ӽ�Ϊ��
    }
    //�����ж������еڶ��Ķ���
    Transform GetSecondChildFromTop(Transform parent)
    {
        if (parent == null || parent.childCount < 2)
        {
            return null; // ���������Ϊ�ջ��Ӷ������������򷵻�null
        }

        Transform secondChild = parent.GetChild(1); // ��ȡ�ڶ����Ӷ��󣨴�����������

        return secondChild;
    }
    public void QuitGame()
    {
        // ��ӡһ����Ϣ������̨����һ����ʵ����Ϸ���ǿ�ѡ�ģ�
        Debug.Log("Exiting Game");

        // �˳���Ϸ
        Application.Quit();
    }
}
