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
    private Vector3 ��ʼλ��; // ��ʼλ��
    private Quaternion ��ʼ��ת�Ƕ�; // ��ʼ��ת�Ƕ�
    private GameObject ��Ҷ���; // ����Ѱ����Ҷ���
    private GameObject NPC����;
    public GameObject ��ǰ�ж���ɫ;
    public Transform ��ǰ����Ŀ��; // ��ǰĿ�����
    [HideInInspector]
    public Animator ��ǰ�ж���ɫ����; // ����������
    [HideInInspector]
    public bool isMoving = false; // �Ƿ������ƶ�
    [HideInInspector]
    public bool isAttacking = false; // �Ƿ����ڹ���
    [HideInInspector]
    public bool ��ʼ����;

    public GameObject[] playerCharacters; // �ҷ���ɫ����
    public GameObject[] enemyCharacters; // �з���ɫ����

    [Header("�����")]
    public CinemachineVirtualCamera �ҷ������;
    public CinemachineVirtualCamera NPC�������;
    public CinemachineVirtualCamera �����ҷ��������;
    public CinemachineVirtualCamera ����з���ɫ�ٻ���������;

    [Header("��ť")]
    public GameObject �չ���ť;
    public GameObject ���ܰ�ť;
    public GameObject �ٻ���ť;
    public GameObject ������ť;
    public Transform ��ť����λ��;
    Button ��ͨ������ť;
    Button ���ܹ�����ť;
    Button �ٻ�����ť;
    Button ��������ť;

    [Header("ս������ͼ��")]
    public GameObject ս��ʤ��ͼ��;
    public GameObject ս��ʧ��ͼ��;
    [Header("����Ĳ��ù�")]
        public Text ��ʾ���������ı�;
    public GameObject ָʾ��ͷ;
    public Transform �ж�������;
        [HideInInspector]
    public bool �Ƿ����ʹ���չ� = false;
        [HideInInspector]
    public bool �Ƿ����ʹ�ü��� = false;
        [HideInInspector]
    private bool �Ƿ����ʹ���ٻ��� = false;
        [HideInInspector]
    public bool �Ƿ����ʹ�������� = false;
        [HideInInspector]
    public bool ʹ���չ� = false;
        [HideInInspector]
    private bool ʹ�ü��� = false;
        [HideInInspector]
    public bool ���� = false;
        [HideInInspector]
    public bool ʹ���ٻ� = false;
        [HideInInspector]
    public bool ʹ������ = false;
        [HideInInspector]
    public bool �����ʼλ��;
        [HideInInspector]
    private Vector3 �չ���ť��ʼ��С;
    private Vector3 ���ܰ�ť��ʼ��С;
    private Vector3 �ٻ���ť��ʼ��С;
    private Vector3 ������ť��ʼ��С;
    public int NPCʣ������;
        [HideInInspector]
    public bool NPC�Ƿ�ʹ���չ�;//NPC�������
        [HideInInspector]
    public bool NPC�Ƿ�ʹ�ü���;
        [HideInInspector]
    public bool NPC�Ƿ�ʹ������;
        [HideInInspector]
    public bool NPC�Ƿ�ʹ���ٻ�;
        [HideInInspector]
    public bool �Ƿ����ʹ������;
        [HideInInspector]
    public string �ͷ�������NPC;
    public int ���ʣ������;

        [HideInInspector]
    public GameObject ���ɵ�ָʾ��ͷ;
    [HideInInspector]
    public bool �������ɰ�ť = true;
    bool ѡ��ǰ�ж���ɫ = true;
        [HideInInspector]
    public Transform �ж������еڶ��Ķ���;
        [HideInInspector]
    public bool ��������;
    bool b = true;
    bool ������ʾ��ť = true;
    enum Action { �չ�, ����, ������, �ٻ��� };
    Action NPC��ǰ�ж�;
    private void Start()
    {
        ��ť����λ�� = transform.Find("��ť");
        ��Ҷ��� = GameObject.Find("�ҷ�����"); // �ҵ���Ҷ���
        NPC���� = GameObject.Find("�з�����"); // �ҵ��з�����
    }



    private void Update()
    {
        NPCʣ������ = 0;
        
         ���ʣ������ = 0;
        foreach (Transform child in NPC����.transform)
        {
            A1 script = child.GetComponent<A1>();
            if (script != null && !script.����)
                NPCʣ������++;
        }
        foreach (Transform child in ��Ҷ���.transform)
        {
            A1 script = child.GetComponent<A1>();
            if (script != null && !script.����)
                ���ʣ������++;
        }

        //Debug.Log("NPC�����л����ŵ�NPC������" + NPCʣ������);
        //Debug.Log("��Ҷ����л����ŵ����������" + ���ʣ������);
        if (NPCʣ������ > 0 && ���ʣ������ > 0)
        {
            ѡ���ж���ɫ();
            if (ѡ��ǰ�ж���ɫ == false&&��ǰ�ж���ɫ!=null)
                if (��ǰ�ж���ɫ.GetComponent<A1>().�Ƿ��ǵз�)
                    ����ж����ǵз�();
                else
                    ����ж������ҷ�();
        }

        else if (NPCʣ������ <= 0 || ���ʣ������ <= 0)
        {
            // B3.Instance.����˫��ʣ��������ʾ��Ӯ();
            Invoke("��ʾ��Ӯ", 1.5f);

        }
        
    }
    private void ��ʾ��Ӯ()
    {
        if (NPCʣ������ <= 0)
            ս��ʤ��ͼ��.SetActive(true);

        if (���ʣ������ <= 0)
            ս��ʧ��ͼ��.SetActive(true);
    }
    void ѡ���ж���ɫ()
    {
        if (ѡ��ǰ�ж���ɫ)
        {
            b = true;
            var a = FindTopObject(�ж�������);
            if (a != null)
                ��ǰ�ж���ɫ = a.GetComponent<A2>().�ж�����Ӧ��ɫ;
            ��ǰ�ж���ɫ���� = ��ǰ�ж���ɫ.GetComponent<Animator>(); // ��ȡ�������������
            ��ʼλ�� = ��ǰ�ж���ɫ.transform.position; // ��¼��ʼλ��
            //Debug.Log(��ʼλ��);
            ��ʼ��ת�Ƕ� = ��ǰ�ж���ɫ.transform.rotation; // ��¼��ʼ��ת�Ƕ�

            //Debug.Log(��ʼλ��);
            Cursor.lockState = CursorLockMode.None; // �ɼ�ʱ���������
            if (��ǰ�ж���ɫ != null&&b)
            {
                A1 ��ǰ�ж���ɫ��A1 = ��ǰ�ж���ɫ.GetComponent<A1>();
                if (��ǰ�ж���ɫ��A1.�Ƿ��ǵз�)
                {
                    //Debug.Log("��ǰ�ж�����NPC");
                    ��ǰ����Ŀ�� = null;
                    if ((��ǰ�ж���ɫ��A1.����) || ��ǰ�ж���ɫ��A1.�Ƿ�ǿ��)
                    {
                        �ƶ���ǰ�ж���ɫ���ж�������ײ�();
                        b = false;
                    }
                    else
                    {
                        ��ǰ�ж���ɫ = a.GetComponent<A2>().�ж�����Ӧ��ɫ;
                        ��ǰ�ж���ɫ���� = ��ǰ�ж���ɫ.GetComponent<Animator>(); // ��ȡ�������������

                        if (���ɵ�ָʾ��ͷ != null)
                            Destroy(���ɵ�ָʾ��ͷ);

                        var _A4 = ��ǰ�ж���ɫ.GetComponent<A4>();
                        bool �Ƿ����ʹ���ٻ���;
                        if (NPCʣ������ <= 4 && _A4.�Ƿ����ʹ���ٻ���&& _A4.���ɵ���ɫ�ٻ���==null)
                        {
                            �Ƿ����ʹ���ٻ��� = true;
                        }else
                            �Ƿ����ʹ���ٻ��� = false;
                        bool[] actions = new bool[]
                            {
                         _A4.�Ƿ����ʹ���չ�,
                         _A4.�Ƿ����ʹ�ü���,
                         _A4.�Ƿ����ʹ��������,
                         �Ƿ����ʹ���ٻ���
                            };

                            // ͳ�ƿ����ж�������
                            int trueCount = actions.Count(action => action == true);
                            // ����NPC��ǰ�ж����ö�Ӧ��bool����
                            if (trueCount == 1)
                                NPC��ǰ�ж� = (Action)Array.IndexOf(actions, true);
                            else if (trueCount > 1)
                            {
                                List<int> trueIndices = new List<int>();
                                for (int i = 0; i < actions.Length; i++)
                                {
                                    if (actions[i]) // �������ж��ǿ��õģ�����Ӧ��boolΪtrue��
                                    {
                                        trueIndices.Add(i); // �ͽ���������ӵ��б���
                                    }
                                }
                                int randomIndex = UnityEngine.Random.Range(0, trueIndices.Count); // �����п��õ����������ѡ��һ��
                                NPC��ǰ�ж� = (Action)trueIndices[randomIndex];// ������������������NPC�ĵ�ǰ�ж�
                            }
                        
                        if (��ǰ�ж���ɫ.GetComponent<A4>().������)
                        {
                            NPC�Ƿ�ʹ������ = true;
                        }

                        else
                        {
                            NPC�Ƿ�ʹ���չ� = NPC��ǰ�ж� == Action.�չ�;
                            NPC�Ƿ�ʹ�ü��� = NPC��ǰ�ж� == Action.����;
                            NPC�Ƿ�ʹ���ٻ� = NPC��ǰ�ж� == Action.�ٻ���;
                            NPC�Ƿ�ʹ������ = NPC��ǰ�ж� == Action.������;
                        }
                        if (��ǰ����Ŀ�� == null)
                        {
                            if (���ʣ������ > 0)
                            {
                                if (��ǰ�ж���ɫ.GetComponent<A4>().�����Ƿ���ҷ�ʹ�� == false & NPC�Ƿ�ʹ���չ� == false)
                                {
                                    if (��ǰ�ж���ɫ.GetComponent<A4>().��Ŀ�����ȫ����Ч)
                                    {                // �����м�λ�õ�����
                                        int middleIndex = ���ʣ������ / 2;

                                        // ��ȡ�м�λ�õ��Ӷ���
                                        Transform middleChild = ��Ҷ���.transform.GetChild(middleIndex);

                                        ��ǰ����Ŀ�� = middleChild;
                                    }
                                    else
                                    {
                                        // ���ѡ��һ���Ӷ���
                                        int randomIndex = UnityEngine.Random.Range(0, ���ʣ������);
                                        Transform randomChild = ��Ҷ���.transform.GetChild(randomIndex);
                                        ��ǰ����Ŀ�� = randomChild;
                                    }
                                }
                                else
                                if (��ǰ�ж���ɫ.GetComponent<A4>().�����Ƿ���ҷ�ʹ�� && NPC�Ƿ�ʹ���չ� == false)
                                {
                                    A1[] a1Scripts = NPC����.GetComponentsInChildren<A1>();
                                    float minHealth = float.MaxValue;
                                    GameObject minHealthObject = null;
                                    // �������е�A1�ű�
                                    foreach (A1 a1 in a1Scripts)
                                    {
                                        // ��ȡA1�ű��еĵ�ǰ����ֵ
                                        float currentHealth = a1.��ǰ����ֵ;
                                        float �������ֵ = a1.�������ֵ;
                                        float ��ǰ�����ٷֱ� = currentHealth / �������ֵ;
                                        // ����Ƿ�����С����ֵ
                                        if (��ǰ�����ٷֱ� < minHealth)
                                        {
                                            minHealth = ��ǰ�����ٷֱ�;
                                            minHealthObject = a1.gameObject;
                                        }
                                    }
                                    ��ǰ����Ŀ�� = minHealthObject.transform;
                                }
                                else
                                if (NPC�Ƿ�ʹ���չ�)
                                {
                                    // ���ѡ��һ���Ӷ���
                                    int randomIndex = UnityEngine.Random.Range(0, ���ʣ������);
                                    Transform randomChild = ��Ҷ���.transform.GetChild(randomIndex);
                                    ��ǰ����Ŀ�� = randomChild;
                                }

                            }
                        }
                        if (��ǰ�ж���ɫ.GetComponent<A1>().�Ƿ�����ɫ�ٻ��� == false)
                        {
                            �ҷ������.gameObject.SetActive(false);
                            NPC�������.gameObject.SetActive(true);
                            NPC�������.Follow = ��ǰ����Ŀ��.transform;
                            NPC�������.LookAt = ��ǰ�ж���ɫ.transform;
                            ����з���ɫ�ٻ���������.gameObject.SetActive(false);
                        }
                        else
                        {
                            ����з���ɫ�ٻ���������.gameObject.SetActive(true);
                            ����з���ɫ�ٻ���������.LookAt = ��ǰ�ж���ɫ.transform;
                        }
                    }
                }
                else if (��ǰ�ж���ɫ��A1.�Ƿ��ǵз� == false)
                {
                    //Debug.Log("��ǰ�ж��������");
                    if ((��ǰ�ж���ɫ��A1.���� || ��ǰ�ж���ɫ��A1.�Ƿ�ǿ��))
                    {
                        �ƶ���ǰ�ж���ɫ���ж�������ײ�();
                    }
                    else
                    {
                        ��ǰ�ж���ɫ = a.GetComponent<A2>().�ж�����Ӧ��ɫ;
                        ��ǰ�ж���ɫ���� = ��ǰ�ж���ɫ.GetComponent<Animator>(); // ��ȡ�������������
                        �ҷ������.gameObject.SetActive(true);
                        NPC�������.gameObject.SetActive(false);
                            �ҷ������.Follow = ��ǰ�ж���ɫ.transform;
                        �����ʼλ�� = true;

                        if (��ǰ����Ŀ�� == null)
                        {
                            int childCount = NPC����.transform.childCount;

                            if (childCount > 0)
                            {
                                // ���ѡ��һ���Ӷ���
                                int randomIndex = UnityEngine.Random.Range(0, childCount);
                                Transform randomChild = NPC����.transform.GetChild(randomIndex);
                                ��ǰ����Ŀ�� = randomChild;
                                if (randomChild.transform.Find("��ݽ�ɫ����") != null)
                                {
                                    //Debug.Log("11");
                                    �ҷ������.LookAt = randomChild.transform.Find("��ݽ�ɫ����").transform.Find("���������λ��");
                                    ��ǰ�ж���ɫ.transform.LookAt(��ǰ����Ŀ��.transform);
                                }

                            }
                        }
                        if (!��ǰ����Ŀ��.GetComponent<A1>().�Ƿ��ǵз�)
                            ѡ��Ŀ������м������Ϊ����Ŀ��(NPC����);
                    }
                    if (���ɵ�ָʾ��ͷ == null)
                    {
                        ���ɵ�ָʾ��ͷ = Instantiate(ָʾ��ͷ);
                    }
                    if (�������ɰ�ť)
                    {
                        if (������ʾ��ť)
                        {
                            StartCoroutine(�ӳ����ɰ�ť());
                            ������ʾ��ť = false;
                        }
                        else
                            ���ɰ�ť();
                    }
                }
            }

        }
        ѡ��ǰ�ж���ɫ = false;
    }
    void ���ɰ�ť()
    {
        if (��ǰ�ж���ɫ.GetComponent<A4>().�Ƿ����ʹ���ٻ���)
        {
            var zh = Instantiate(�ٻ���ť, ��ť����λ��);
            �ٻ�����ť = zh.GetComponent<Button>();
        }
        if (��ǰ�ж���ɫ.GetComponent<A4>().�Ƿ����ʹ��������)
        {
            var xl = Instantiate(������ť, ��ť����λ��);
            ��������ť = xl.GetComponent<Button>();
        }
        if (��ǰ�ж���ɫ.GetComponent<A4>().�Ƿ����ʹ�ü���)
        {
            var jn = Instantiate(���ܰ�ť, ��ť����λ��);
            ���ܹ�����ť = jn.GetComponent<Button>();
        }
        if (��ǰ�ж���ɫ.GetComponent<A4>().�Ƿ����ʹ���չ�)
        {
            var pg = Instantiate(�չ���ť, ��ť����λ��);
            ��ͨ������ť = pg.GetComponent<Button>();
        }
        if (��ǰ�ж���ɫ.GetComponent<A4>().������)
        {
            ���ٹ�����ť();
            var xl = Instantiate(������ť, ��ť����λ��);
            ��������ť = xl.GetComponent<Button>();
        }
        ���水ť();
        �������ɰ�ť = false;
    }
    private IEnumerator �ӳ����ɰ�ť()
    {
        yield return new WaitForSeconds(0.9f);
        ���ɰ�ť();
    }

    void ����ж����ǵз�()
    {

        if (NPC�Ƿ�ʹ���չ�)
        {
            if (��ǰ�ж���ɫ.GetComponent<A4>().�չ��Ƿ��ƶ���Ŀ�� == true)
            {
                if (!isMoving && !isAttacking)
                {                 // ����Ŀ�����
                    ��ǰ�ж���ɫ.transform.LookAt(��ǰ����Ŀ��);
                    ��ǰ�ж���ɫ����.SetBool("׼���ͷ��չ�", true);
                    ��ǰ�ж���ɫ����.SetBool("IsMoving", true);
                    Invoke("�ӳ��ƶ���Ŀ��", ��ǰ�ж���ɫ.GetComponent<A4>().����չ��ƶ���Ŀ�겥�Ŷ�����ÿ�ʼ�ƶ�);
                    //isMoving = true;
                }
                if (isMoving && !isAttacking && !��ʼ����)
                {

                    ��ǰ�ж���ɫ.transform.position = Vector3.MoveTowards(��ǰ�ж���ɫ.transform.position, ��ǰ����Ŀ��.position, ��ǰ�ж���ɫ.GetComponent<A1>().�ƶ���з����ٶ� * Time.deltaTime);
                    if (Vector3.Distance(��ǰ�ж���ɫ.transform.position, ��ǰ����Ŀ��.position) <= ��ǰ����Ŀ��.GetComponent<A1>().����ս����ʱ���������Զͣ��)
                    {
                        isMoving = false;
                        isAttacking = true;

                        ��ǰ�ж���ɫ����.SetBool("IsMoving", false);
                        ��ǰ�ж���ɫ����.SetBool("IsAttacking", true);

                        Invoke("����չ��˺�", ��ǰ�ж���ɫ.GetComponent<A4>().��ʼ�����չ�����������ú�����˺�);
                        Invoke("�����չ���Ч", ��ǰ�ж���ɫ.GetComponent<A4>().��ʼ�����չ��������ú�������Ч);
                        Invoke("AttackAnimationComplete", ��ǰ�ж���ɫ.GetComponent<A4>().�չ�����ʱ��);
                        ��ʾ���ܻ��չ������ı�(��ǰ�ж���ɫ.GetComponent<A4>().�չ�����);
                    }
                }
            }
            else if (��ǰ�ж���ɫ.GetComponent<A4>().�չ��Ƿ��ƶ���Ŀ�� == false)
            {
                if (!isMoving && !isAttacking)
                {                 // ����Ŀ�����
                    ��ǰ�ж���ɫ.transform.LookAt(��ǰ����Ŀ��);
                    isMoving = true;
                }
                if (isMoving && !isAttacking && !��ʼ����)
                {
                    isMoving = false;
                    isAttacking = true;
                    ��ǰ�ж���ɫ����.SetBool("IsAttacking", true);
                    Invoke("����չ��˺�", ��ǰ�ж���ɫ.GetComponent<A4>().��ʼ�����չ�����������ú�����˺�);
                    Invoke("�����չ���Ч", ��ǰ�ж���ɫ.GetComponent<A4>().��ʼ�����չ��������ú�������Ч);
                    Invoke("AttackAnimationComplete", ��ǰ�ж���ɫ.GetComponent<A4>().�չ�����ʱ��);
                    ��ʾ���ܻ��չ������ı�(��ǰ�ж���ɫ.GetComponent<A4>().�չ�����);
                }
            }
        }
        // ʹ�ü���
        else if (NPC�Ƿ�ʹ�ü���)
        {
            //��ǰ�ж���ɫ����.SetBool("׼���ͷż���", true);
            if (��ǰ�ж���ɫ.GetComponent<A4>().���ܹ����Ƿ����ƶ���Ŀ�� == true)
            {
                if (!isMoving && !isAttacking)
                {                 // ����Ŀ�����
                    ��ǰ�ж���ɫ.transform.LookAt(��ǰ����Ŀ��);

                    //isMoving = true;
                    ��ǰ�ж���ɫ����.SetBool("IsMoving", true);
                    Invoke("�ӳ��ƶ���Ŀ��", ��ǰ�ж���ɫ.GetComponent<A4>().��������ƶ���Ŀ�겥�Ŷ�����ÿ�ʼ�ƶ�);
                }
                if (isMoving && !isAttacking && !��ʼ����)
                {
                    ��ǰ�ж���ɫ.transform.position = Vector3.MoveTowards(��ǰ�ж���ɫ.transform.position, ��ǰ����Ŀ��.position, ��ǰ�ж���ɫ.GetComponent<A1>().�ƶ���з����ٶ� * Time.deltaTime);
                    if (Vector3.Distance(��ǰ�ж���ɫ.transform.position, ��ǰ����Ŀ��.position) <= ��ǰ����Ŀ��.GetComponent<A1>().����ս����ʱ���������Զͣ��)
                    {
                        isMoving = false;
                        isAttacking = true;
                        ��ǰ�ж���ɫ����.SetBool("׼���ͷż���", true);
                        ��ǰ�ж���ɫ����.SetBool("IsMoving", false);
                        ��ǰ�ж���ɫ����.SetBool("�ͷż���", true);
                        Invoke("���ż�����Ч", ��ǰ�ж���ɫ.GetComponent<A4>().��ʼ���ż��ܶ������ú�������Ч);
                        Invoke("��ɼ����˺�", ��ǰ�ж���ɫ.GetComponent<A4>().��ʼ���ż��ܹ���������ú�����˺�);
                        Invoke("AttackAnimationComplete", ��ǰ�ж���ɫ.GetComponent<A4>().���ܶ���ʱ��);
                        ��ʾ���ܻ��չ������ı�(��ǰ�ж���ɫ.GetComponent<A4>().��������);
                    }
                }
            }
            else if (��ǰ�ж���ɫ.GetComponent<A4>().���ܹ����Ƿ����ƶ���Ŀ�� == false)
            {
                if (!isMoving && !isAttacking)
                {                 // ����Ŀ�����
                    ��ǰ�ж���ɫ.transform.LookAt(��ǰ����Ŀ��);
                    isMoving = true;
                }
                if (isMoving && !isAttacking && !��ʼ���� && ��ǰ�ж���ɫ.GetComponent<A4>().�����Ƿ���ҷ�ʹ�� == false)
                {
                    isAttacking = true;
                    ��ǰ�ж���ɫ����.SetBool("׼���ͷż���", true);
                    ��ǰ�ж���ɫ����.SetBool("�ͷż���", true);
                    Invoke("��ɼ����˺�", ��ǰ�ж���ɫ.GetComponent<A4>().��ʼ���ż��ܹ���������ú�����˺�);
                    Invoke("���ż�����Ч", ��ǰ�ж���ɫ.GetComponent<A4>().��ʼ���ż��ܶ������ú�������Ч);
                    Invoke("AttackAnimationComplete", ��ǰ�ж���ɫ.GetComponent<A4>().���ܶ���ʱ��);
                    ��ʾ���ܻ��չ������ı�(��ǰ�ж���ɫ.GetComponent<A4>().��������);
                }
                if (isMoving && !isAttacking && !��ʼ���� && ��ǰ�ж���ɫ.GetComponent<A4>().�����Ƿ���ҷ�ʹ�� == true)
                {
                    isAttacking = true;
                    ��ǰ�ж���ɫ����.SetBool("�ͷż���", true);
                    Invoke("��ɼ����˺�", ��ǰ�ж���ɫ.GetComponent<A4>().��ʼ���ż��ܹ���������ú�����˺�);
                    Invoke("���ż�����Ч", ��ǰ�ж���ɫ.GetComponent<A4>().��ʼ���ż��ܶ������ú�������Ч);
                    Invoke("AttackAnimationComplete", ��ǰ�ж���ɫ.GetComponent<A4>().���ܶ���ʱ��);
                    ��ʾ���ܻ��չ������ı�(��ǰ�ж���ɫ.GetComponent<A4>().��������);
                }
            }
        }
        else if (NPC�Ƿ�ʹ���ٻ�)
        {
            if (!isMoving && !isAttacking)
            {                 // ����Ŀ�����
                ��ǰ�ж���ɫ.transform.LookAt(��ǰ����Ŀ��);
                isMoving = true;
            }
            if (isMoving && !isAttacking && b )
            {
                isMoving = false;
                ʹ���ٻ�=true;
                ��ǰ�ж���ɫ����.SetBool("�ٻ�", true);
                Invoke("ʹ���ٻ���", ��ǰ�ж���ɫ.GetComponent<A4>().��ʼ�����ٻ��������ú�ʼ�ٻ�);
                Invoke("AttackAnimationComplete", ��ǰ�ж���ɫ.GetComponent<A4>().�ٻ�����ʱ��);
                ��ʾ���ܻ��չ������ı�(��ǰ�ж���ɫ.GetComponent<A4>().�ٻ�������);
                b = false;
            }
        }
        else if (NPC�Ƿ�ʹ������)
        {
            if (!isMoving  && b)
            {                 // ����Ŀ�����
                ��ʼλ�� = ��ǰ�ж���ɫ.transform.position; // ��¼��ʼλ��
                ��ǰ�ж���ɫ.transform.LookAt(��ǰ����Ŀ��);
                Invoke("�ӳ��ƶ���Ŀ��", ��ǰ�ж���ɫ.GetComponent<A4>().��������ƶ���Ŀ�겥�Ŷ�����ÿ�ʼ�ƶ�);
                if (��ǰ�ж���ɫ.GetComponent<A4>().������)
                    ��ʾ���ܻ��չ������ı�(��ǰ�ж���ɫ.GetComponent<A4>().�������������);
                else
                    ��ʾ���ܻ��չ������ı�(��ǰ�ж���ɫ.GetComponent<A4>().����������);
            }
            if (!��ǰ�ж���ɫ.GetComponent<A4>().�������Ƿ��ƶ���Ŀ���ͷ� && isAttacking == false&&isMoving)
            {
                if (��ǰ�ж���ɫ.GetComponent<A4>().������ == false && b)
                {
                    ��ǰ�ж���ɫ����.SetBool("����", true);
                    Invoke("ʹ��������", ��ǰ�ж���ɫ.GetComponent<A4>().��ʼ���������������ú�����˺�);
                    Invoke("AttackAnimationComplete", ��ǰ�ж���ɫ.GetComponent<A4>().��������ʱ��);
                    isAttacking = true;
                    b = false;
                }
                else if (��ǰ�ж���ɫ.GetComponent<A4>().������ && b)
                {
                    ��ǰ�ж���ɫ.GetComponent<A4>().�ͷ�������(��ǰ����Ŀ��.gameObject, ��ǰ�ж���ɫ.GetComponent<A1>().����);
                    Invoke("��������˺�", ��ǰ�ж���ɫ.GetComponent<A4>().��ʼ���������������ú�����˺�);
                    Invoke("AttackAnimationComplete", ��ǰ�ж���ɫ.GetComponent<A4>().��������ʱ��);
                    isAttacking = true;
                    b = false;
                    NPC�Ƿ�ʹ������ = false;
                }
            }
            else if (��ǰ�ж���ɫ.GetComponent<A4>().�������Ƿ��ƶ���Ŀ���ͷ� && isAttacking == false)
            {
                if (��ǰ�ж���ɫ.GetComponent<A4>().������ == false && b)
                {
                    ��ʾ���ܻ��չ������ı�(��ǰ�ж���ɫ.GetComponent<A4>().����������);
                    isAttacking = true;
                    ��ǰ�ж���ɫ����.SetBool("����", true);
                    Invoke("ʹ��������", ��ǰ�ж���ɫ.GetComponent<A4>().��ʼ���������������ú�����˺�);
                    Invoke("AttackAnimationComplete", ��ǰ�ж���ɫ.GetComponent<A4>().��������ʱ��);
                    b = false;
                }
                else if (��ǰ�ж���ɫ.GetComponent<A4>().������ && b)
                {
                    ��ʾ���ܻ��չ������ı�(��ǰ�ж���ɫ.GetComponent<A4>().�������������);
                    ��ǰ�ж���ɫ.transform.position = Vector3.MoveTowards(��ǰ�ж���ɫ.transform.position, ��ǰ����Ŀ��.position, ��ǰ�ж���ɫ.GetComponent<A1>().�ƶ���з����ٶ� * Time.deltaTime);
                    if (Vector3.Distance(��ǰ�ж���ɫ.transform.position, ��ǰ����Ŀ��.position) <= ��ǰ����Ŀ��.GetComponent<A1>().����ս����ʱ���������Զͣ��)
                    {
                        isAttacking = true;
                        ��ǰ�ж���ɫ.GetComponent<A4>().�ͷ�������(��ǰ����Ŀ��.gameObject, ��ǰ�ж���ɫ.GetComponent<A1>().����);
                        Invoke("��������˺�", ��ǰ�ж���ɫ.GetComponent<A4>().��ʼ���������������ú�����˺�);
                        Invoke("AttackAnimationComplete", ��ǰ�ж���ɫ.GetComponent<A4>().��������ʱ��);
                        b = false;
                        NPC�Ƿ�ʹ������ = false;
                    }
                }

            }
        }
    }


    void ����ж������ҷ�()
    {

        // ���ѡ��з�Ŀ��
        if (Input.GetMouseButtonDown(0)&&!isAttacking&&!isMoving) // ������������
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.gameObject.CompareTag("NPC"))
                {
                    GameObject targetObject = hitInfo.collider.gameObject;
                    if (targetObject.transform.Find("��ݽ�ɫ����") != null)
                    {
                        �ҷ������.LookAt = targetObject.transform.Find("��ݽ�ɫ����").transform.Find("���������λ��");
                    }
                    else
                        �ҷ������.LookAt = targetObject.transform;
                    ��ǰ����Ŀ�� = targetObject.transform;
                    ��ǰ�ж���ɫ.transform.LookAt(targetObject.transform);
                    if (���ɵ�ָʾ��ͷ != null)
                        ���ɵ�ָʾ��ͷ.transform.position = ��ǰ����Ŀ��.position + new Vector3(0, 3.5f, 0);
                }
            }
        }
        if (���ɵ�ָʾ��ͷ != null && ��ǰ����Ŀ�� != null)
            ���ɵ�ָʾ��ͷ.transform.position = ��ǰ����Ŀ��.position + new Vector3(0, 3.5f, 0);
        if (ʹ���չ�)
        {
            if (��ǰ�ж���ɫ.GetComponent<A4>().�չ��Ƿ��ƶ���Ŀ�� == true)
            {
                ��ս�չ�();
            }
        }
        if (�Ƿ����ʹ�ü���)
        {
            ��ǰ�ж���ɫ����.SetBool("׼���ͷż���", true);
            ��ǰ�ж���ɫ����.SetBool("׼���ͷ��չ�", false);
            �����ҷ��������.gameObject.SetActive(false);
            �ҷ������.gameObject.SetActive(true);
            ʹ�ü��� = true;

            if (����)
                ��ս����();
            if (��ǰ�ж���ɫ.GetComponent<A4>().�����Ƿ���ҷ�ʹ�� == true && ��ǰ�ж���ɫ.GetComponent<A4>().���ܹ����Ƿ����ƶ���Ŀ�� == false)//����Լ����ͷż���
            {
                �����ҷ��������.gameObject.SetActive(true);
                �ҷ������.gameObject.SetActive(false);
                if (Input.GetMouseButtonDown(0)) // ������������
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hitInfo;
                    if (Physics.Raycast(ray, out hitInfo))
                    {
                        if (hitInfo.collider.gameObject.CompareTag("Player"))
                        {
                            GameObject targetObject = hitInfo.collider.gameObject;
                            �����ҷ��������.LookAt = targetObject.transform;
                            ��ǰ����Ŀ�� = targetObject.transform;
                        }
                    }
                }

                if (�����ҷ��������.LookAt == null)
                {
                    �����ҷ��������.LookAt = ��ǰ�ж���ɫ.transform;
                }

            }
        }
        if (�Ƿ����ʹ���ٻ���)
        {
            ʹ���ٻ� = true;
            �����ҷ��������.gameObject.SetActive(true);
            �ҷ������.gameObject.SetActive(false);
            �����ҷ��������.LookAt = ��ǰ�ж���ɫ.transform;
        }
        if (�Ƿ����ʹ��������)
        {
            �����ҷ��������.gameObject.SetActive(false);
            �ҷ������.gameObject.SetActive(true);
            ʹ������ = true;
        }
    }

    public void AttackAnimationComplete()
    {
        //if(��ǰ�ж���ɫ.GetComponent<A1>().�Ƿ��ǵз�==false){
        //    �ҷ������.Follow=null;
        //    //�ҷ������.LookAt=null;
        //}

        ��ǰ�ж���ɫ����.SetBool("IsAttacking", false);
        ��ǰ�ж���ɫ����.SetBool("IsMoving", false);
        ��ǰ�ж���ɫ����.SetBool("�ͷż���", false);
        ��ǰ�ж���ɫ����.SetBool("׼���ͷż���", false);
        ��ǰ�ж���ɫ����.SetBool("׼���ͷ��չ�", false);
        �Ƿ����ʹ���չ� = false;
        �Ƿ����ʹ�ü��� = false;
        �Ƿ����ʹ���ٻ��� = false;
        ���� = false;
        ʹ���չ� = false;
        ʹ�ü��� = false;

        ��ǰ�ж���ɫ����.SetBool("�ٻ�", false);
        ��ǰ�ж���ɫ����.SetBool("�ͷ�����", false);
        �Ƿ����ʹ�������� = false;
        ʹ������ = false;
        NPC�Ƿ�ʹ���չ� = false;
        NPC�Ƿ�ʹ�ü��� = false;
        isAttacking = false;
        NPC�Ƿ�ʹ���ٻ�=false;
        StartCoroutine(MoveBackToInitialPosition());
        if (��ǰ�ж���ɫ.GetComponent<A4>().�����Ƿ���ҷ�ʹ��)
            ѡ��Ŀ������м������Ϊ����Ŀ��(NPC����);
    }        // ��������������Ϻ�Ļص�����

    // �ƶ��س�ʼλ�õ�Э��
    private IEnumerator MoveBackToInitialPosition()
    {
        var characterA1 = ��ǰ�ж���ɫ.GetComponent<A1>();
        var characterA4 = ��ǰ�ж���ɫ.GetComponent<A4>();
        var characterAnimator = ��ǰ�ж���ɫ����;
        if (characterA1.������ɾ������ && !characterA4.������)
        {
            characterA1.�ӳ�ɾ��(0.01f);
            StartCoroutine(DelayedExecutionCoroutine());

        }
        else
        {
            if (!ʹ���ٻ�)
            {
                while (Vector3.Distance(��ǰ�ж���ɫ.transform.position, ��ʼλ��) > 0.2f)
                {
                    //Debug.Log(��ʼλ��);
                    characterAnimator.SetBool("IsMoving", true);
                    MoveCharacterTowards(��ǰ�ж���ɫ, ��ʼλ��, ��ǰ�ж���ɫ.GetComponent<A1>().���ص��ٶ�);

                    if (characterA1.�����Ƿ�תͷ)
                    {
                        FaceCharacterTowards(��ǰ�ж���ɫ, ��ʼλ��);
                    }
                    yield return null;
                }

                isMoving = false;
                characterAnimator.SetBool("IsMoving", false);
                ResetCharacterState(characterAnimator, ��ǰ�ж���ɫ, ��ʼ��ת�Ƕ�);
            }
            else
            {
                ʹ���ٻ� = false;
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
        character.transform.position = ��ʼλ��;
        character.transform.rotation = initialRotation;
        �����ҷ��������.gameObject.SetActive(false);
    }

    private void ���ٹ�����ť()
    {
        // ���ٹ�����ť���߼�
        if (��ͨ������ť != null)
            Destroy(��ͨ������ť.gameObject);
        if (���ܹ�����ť != null)
            Destroy(���ܹ�����ť.gameObject);
        if (�ٻ�����ť != null)
            Destroy(�ٻ�����ť.gameObject);
        if (��������ť != null)
            Destroy(��������ť.gameObject);
    }
    void �ӳ��ƶ���Ŀ��()
    {
        isMoving = true;
        //Debug.Log("66");
    }

    void ���水ť()
    {
        // �����ʼ��ť��С
        if (��ͨ������ť != null)
        {
            �չ���ť��ʼ��С = ��ͨ������ť.transform.localScale;
            ��ͨ������ť.onClick.AddListener(����ʹ���չ�);
        }
        if (���ܹ�����ť != null)
        {
            ���ܰ�ť��ʼ��С = ���ܹ�����ť.transform.localScale;
            ���ܹ�����ť.onClick.AddListener(����ʹ�ü���);
        }
        if (�ٻ�����ť != null)
        {
            �ٻ���ť��ʼ��С = �ٻ�����ť.transform.localScale;
            �ٻ�����ť.onClick.AddListener(����ʹ���ٻ���);
        }
        if (��������ť != null)
        {
            ������ť��ʼ��С = ��������ť.transform.localScale;
            ��������ť.onClick.AddListener(����ʹ��������);
        }
    }
    public void ��ս�չ�()
    {

        if (!isMoving && !isAttacking)
        {                 // ����Ŀ�����
            ��ʼλ�� = ��ǰ�ж���ɫ.transform.position; // ��¼��ʼλ��
            //Debug.Log(��ʼλ��);
            ��ǰ�ж���ɫ.transform.LookAt(��ǰ����Ŀ��);
            ��ǰ�ж���ɫ����.SetBool("IsMoving", true);
           
            if (��ǰ�ж���ɫ.GetComponent<A4>().�չ��Ƿ�ظ�ս����)
                B1.Instance.����ս����();
            ��ʾ���ܻ��չ������ı�(��ǰ�ж���ɫ.GetComponent<A4>().�չ�����);
            Invoke("�ӳ��ƶ���Ŀ��", ��ǰ�ж���ɫ.GetComponent<A4>().����չ��ƶ���Ŀ�겥�Ŷ�����ÿ�ʼ�ƶ�);
        }
        else
        if (isMoving && !isAttacking && !��ʼ����)
        {
            ��ǰ�ж���ɫ.transform.position = Vector3.MoveTowards(��ǰ�ж���ɫ.transform.position, ��ǰ����Ŀ��.position, ��ǰ�ж���ɫ.GetComponent<A1>().�ƶ���з����ٶ� * Time.deltaTime);
            if (Vector3.Distance(��ǰ�ж���ɫ.transform.position, ��ǰ����Ŀ��.position) <= ��ǰ����Ŀ��.GetComponent<A1>().����ս����ʱ���������Զͣ��)
            {
                isMoving = false;
                isAttacking = true;
                ��ǰ�ж���ɫ����.SetBool("IsMoving", false);
                ��ǰ�ж���ɫ����.SetBool("IsAttacking", true);
                Invoke("����չ��˺�", ��ǰ�ж���ɫ.GetComponent<A4>().��ʼ�����չ�����������ú�����˺�);
                Invoke("�����չ���Ч", ��ǰ�ж���ɫ.GetComponent<A4>().��ʼ�����չ��������ú�������Ч);
                Invoke("AttackAnimationComplete", ��ǰ�ж���ɫ.GetComponent<A4>().�չ�����ʱ��);
            }

        }
    }
    public void Զ���չ�()
    {
        if (!isMoving && !isAttacking)
        {                 // ����Ŀ�����
            ��ʼλ�� = ��ǰ�ж���ɫ.transform.position; // ��¼��ʼλ��
            ��ǰ�ж���ɫ.transform.LookAt(��ǰ����Ŀ��);
            isMoving = true;
            ��ʾ���ܻ��չ������ı�(��ǰ�ж���ɫ.GetComponent<A4>().�չ�����);
            if (��ǰ�ж���ɫ.GetComponent<A4>().�չ��Ƿ�ظ�ս����)
                B1.Instance.����ս����();
        }
        if (isMoving && !isAttacking && !��ʼ����)
        {
            isMoving = false;
            ��ǰ�ж���ɫ����.SetBool("IsAttacking", true);
            isAttacking = true;
            Invoke("����չ��˺�", ��ǰ�ж���ɫ.GetComponent<A4>().��ʼ�����չ�����������ú�����˺�);
            Invoke("�����չ���Ч", ��ǰ�ж���ɫ.GetComponent<A4>().��ʼ�����չ��������ú�������Ч);
            Invoke("AttackAnimationComplete", ��ǰ�ж���ɫ.GetComponent<A4>().�չ�����ʱ��);
        }
    }
    public void ��ս����()
    {
        if (!isMoving && !isAttacking)
        {                 // ����Ŀ�����
            ��ʼλ�� = ��ǰ�ж���ɫ.transform.position; // ��¼��ʼλ��
            ��ǰ�ж���ɫ.transform.LookAt(��ǰ����Ŀ��);
            //isMoving = true;
            Invoke("�ӳ��ƶ���Ŀ��", ��ǰ�ж���ɫ.GetComponent<A4>().��������ƶ���Ŀ�겥�Ŷ�����ÿ�ʼ�ƶ�);
            ��ǰ�ж���ɫ����.SetBool("IsMoving", true);
            ��ʾ���ܻ��չ������ı�(��ǰ�ж���ɫ.GetComponent<A4>().�չ�����);
            if (��ǰ�ж���ɫ.GetComponent<A6>() != null)
                ��ǰ�ж���ɫ.GetComponent<A6>().�ͷż��������ж����� = true;
        }
        if (isMoving && !isAttacking && !��ʼ����)
        {
            // �ƶ���Ŀ�����
            ��ǰ�ж���ɫ.transform.position = Vector3.MoveTowards(��ǰ�ж���ɫ.transform.position, ��ǰ����Ŀ��.position, ��ǰ�ж���ɫ.GetComponent<A1>().�ƶ���з����ٶ� * Time.deltaTime);

            //������Ŀ��һ������ʱ��ͣ�²����Ź�������
            if (Vector3.Distance(��ǰ�ж���ɫ.transform.position, ��ǰ����Ŀ��.position) <= ��ǰ����Ŀ��.GetComponent<A1>().����ս����ʱ���������Զͣ��)
            {
                isMoving = false;
                isAttacking = true;
                ��ǰ�ж���ɫ����.SetBool("IsMoving", false);
                ��ǰ�ж���ɫ����.SetBool("�ͷż���", true);
                Invoke("��ɼ����˺�", ��ǰ�ж���ɫ.GetComponent<A4>().��ʼ���ż��ܹ���������ú�����˺�);
                Invoke("���ż�����Ч", ��ǰ�ж���ɫ.GetComponent<A4>().��ʼ���ż��ܶ������ú�������Ч);
                Invoke("AttackAnimationComplete", ��ǰ�ж���ɫ.GetComponent<A4>().���ܶ���ʱ��);
            }

        }
    }
    public void Զ�̼���()
    {
        if (!isMoving && !isAttacking)
        {                 // ����Ŀ�����
            ��ʼλ�� = ��ǰ�ж���ɫ.transform.position; // ��¼��ʼλ��
            ��ǰ�ж���ɫ.transform.LookAt(��ǰ����Ŀ��);
            isMoving = true;
            ��ʾ���ܻ��չ������ı�(��ǰ�ж���ɫ.GetComponent<A4>().��������);
            if (��ǰ�ж���ɫ.GetComponent<A6>() != null)
                ��ǰ�ж���ɫ.GetComponent<A6>().�ͷż��������ж����� = true;
        }
        if (isMoving && !isAttacking && !��ʼ����)
        {
            isMoving = false;
            isAttacking = true;
            ��ǰ�ж���ɫ����.SetBool("�ͷż���", true);

            if (��ǰ�ж���ɫ.GetComponent<A4>().�����Ƿ���ҷ�ʹ��)
            {
                Invoke("���ż�����Ч", ��ǰ�ж���ɫ.GetComponent<A4>().��ʼ���ż��ܶ������ú�������Ч);
                Invoke("��ɼ����˺�", ��ǰ�ж���ɫ.GetComponent<A4>().��ʼ���ż��ܹ���������ú�����˺�);
            }

            else
            {
                Invoke("��ɼ����˺�", ��ǰ�ж���ɫ.GetComponent<A4>().��ʼ���ż��ܹ���������ú�����˺�);
                Invoke("���ż�����Ч", ��ǰ�ж���ɫ.GetComponent<A4>().��ʼ���ż��ܶ������ú�������Ч);
            }
            Invoke("AttackAnimationComplete", ��ǰ�ж���ɫ.GetComponent<A4>().���ܶ���ʱ��);
        }
    }
    public void �ٻ�����()
    {
        if (!isMoving && !isAttacking)
        {
            isMoving = true;
            ��ʾ���ܻ��չ������ı�(��ǰ�ж���ɫ.GetComponent<A4>().�ٻ�������);
        }
        if (isMoving && !isAttacking && !��ʼ����)
        {
            isMoving = false;
            ��ǰ�ж���ɫ����.SetBool("�ٻ�", true);
            Invoke("ʹ���ٻ���", ��ǰ�ж���ɫ.GetComponent<A4>().��ʼ�����ٻ��������ú�ʼ�ٻ�);
            Invoke("��¼��ʼλ��", 0.5f);
            Invoke("AttackAnimationComplete", ��ǰ�ж���ɫ.GetComponent<A4>().�ٻ�����ʱ��);
        }
    }
    public void ��������()
    {
        if (!isMoving && !isAttacking)
        {                 // ����Ŀ�����
            ��ʼλ�� = ��ǰ�ж���ɫ.transform.position; // ��¼��ʼλ��
            ��ǰ�ж���ɫ.transform.LookAt(��ǰ����Ŀ��);
            isMoving = true;
            if (��ǰ�ж���ɫ.GetComponent<A4>().������ == false)
            {
                ��ʾ���ܻ��չ������ı�(��ǰ�ж���ɫ.GetComponent<A4>().����������);
                B1.Instance.����ս����();
            }
            else
                ��ʾ���ܻ��չ������ı�(��ǰ�ж���ɫ.GetComponent<A4>().�������������);
        }
        if (isMoving && !isAttacking && !��ʼ����)
        {
            isMoving = false;
            isAttacking = true;
            if (��ǰ�ж���ɫ.GetComponent<A4>().������ == false)
            {
                ��ǰ�ж���ɫ����.SetBool("����", true);
                Invoke("ʹ��������", ��ǰ�ж���ɫ.GetComponent<A4>().��ʼ���������������ú�����˺�);
            }
            else
            {
                ��ǰ�ж���ɫ.GetComponent<A4>().�ͷ�������(��ǰ����Ŀ��.gameObject, ��ǰ�ж���ɫ.GetComponent<A1>().����);
                Invoke("��������˺�", ��ǰ�ж���ɫ.GetComponent<A4>().��ʼ���������������ú�����˺�);
            }
            Invoke("AttackAnimationComplete", ��ǰ�ж���ɫ.GetComponent<A4>().��������ʱ��);
        }
        �Ƿ����ʹ�������� = false;
    }
    public void ��ս��������()
    {
        if (!isMoving && !isAttacking)
        {                 // ����Ŀ�����
            ��ʼλ�� = ��ǰ�ж���ɫ.transform.position; // ��¼��ʼλ��
            ��ǰ�ж���ɫ.transform.LookAt(��ǰ����Ŀ��);
            //isMoving = true;
            Invoke("�ӳ��ƶ���Ŀ��", ��ǰ�ж���ɫ.GetComponent<A4>().��������ƶ���Ŀ�겥�Ŷ�����ÿ�ʼ�ƶ�);
            if (��ǰ�ж���ɫ.GetComponent<A4>().������ == false)
            {
                ��ʾ���ܻ��չ������ı�(��ǰ�ж���ɫ.GetComponent<A4>().����������);
                B1.Instance.����ս����();
            }
            else
                ��ʾ���ܻ��չ������ı�(��ǰ�ж���ɫ.GetComponent<A4>().�������������);
            �Ƿ����ʹ�������� = false;
        }
        if (isMoving && !isAttacking && !��ʼ����)
        {
            if (��ǰ�ж���ɫ.GetComponent<A4>().������ == false)
            {
                Debug.Log("11");
                ��ǰ�ж���ɫ����.SetBool("����", true);
                Invoke("ʹ��������", ��ǰ�ж���ɫ.GetComponent<A4>().��ʼ���������������ú�����˺�);
            }
            else
            if (��ǰ�ж���ɫ.GetComponent<A4>().������)
            {
                Debug.Log("22");
                ��ǰ�ж���ɫ.transform.position = Vector3.MoveTowards(��ǰ�ж���ɫ.transform.position, ��ǰ����Ŀ��.position, ��ǰ�ж���ɫ.GetComponent<A1>().�ƶ���з����ٶ� * Time.deltaTime);
                if (Vector3.Distance(��ǰ�ж���ɫ.transform.position, ��ǰ����Ŀ��.position) <= ��ǰ����Ŀ��.GetComponent<A1>().����ս����ʱ���������Զͣ��)
                {
                    isMoving = false;
                    isAttacking = true;
                    ��ǰ�ж���ɫ.GetComponent<A4>().�ͷ�������(��ǰ����Ŀ��.gameObject, ��ǰ�ж���ɫ.GetComponent<A1>().����);
                    Invoke("��������˺�", ��ǰ�ж���ɫ.GetComponent<A4>().��ʼ���������������ú�����˺�);
                }
            }
            Invoke("AttackAnimationComplete", ��ǰ�ж���ɫ.GetComponent<A4>().��������ʱ��);
            �Ƿ����ʹ�������� = false;
        }
    }
    bool �����ж�����б�(GameObject character)// ������߻��е�������б��е�
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
    bool �����жϵз��б�(GameObject character)// ������߻��е���NPC�б��е�
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
    private void ����ʹ���չ�()
    {
        if(�Ƿ����ʹ�ü���&&��ǰ�ж���ɫ.GetComponent<A4>().�����Ƿ���ҷ�ʹ��)
            ѡ��Ŀ������м������Ϊ����Ŀ��(NPC����);
        �Ƿ����ʹ�ü��� = false;
        ʹ�ü��� = false;
        �Ƿ����ʹ���ٻ��� = false;
        �����ҷ��������.gameObject.SetActive(false);
        �ҷ������.gameObject.SetActive(true);
        ��ǰ�ж���ɫ����.SetBool("׼���ͷż���", false);
        ��ǰ�ж���ɫ����.SetBool("׼���ͷ��չ�", true);

        if (�Ƿ����ʹ���չ�)
        {
            ʹ���չ� = true;
            if (��ǰ�ж���ɫ.GetComponent<A4>().�չ��Ƿ��ƶ���Ŀ�� == true)
            {
//
            }
            else if (ʹ���չ�)
            {
                Զ���չ�();
            }
        }
        �Ƿ����ʹ���չ� = true;
        if (��ͨ������ť != null)
            ��ͨ������ť.transform.localScale = �չ���ť��ʼ��С * 1.2f;
        if (���ܹ�����ť != null)
            ���ܹ�����ť.transform.localScale = ���ܰ�ť��ʼ��С;
        if (�ٻ�����ť != null)
            �ٻ�����ť.transform.localScale = �ٻ���ť��ʼ��С;
        if (��������ť != null)
            ��������ť.transform.localScale = ������ť��ʼ��С;
    }

    private void ����ʹ�ü���()
    {
        �Ƿ����ʹ���չ� = false;
        �Ƿ����ʹ�ü��� = true;
        ʹ���չ� = false;
        �Ƿ����ʹ���ٻ��� = false;
        if (ʹ�ü���&& B1.Instance.��ǰս���� > 0)
        {
            if (��ǰ�ж���ɫ.GetComponent<A4>().���ܹ����Ƿ����ƶ���Ŀ�� == true)
            {
                ���� = true;
                if (��ǰ�ж���ɫ.GetComponent<A4>().�����Ƿ�����ս����)
                    B1.Instance.����ս����();
            }
            else
            {
                Զ�̼���();
                if (��ǰ�ж���ɫ.GetComponent<A4>().�����Ƿ�����ս����)
                    B1.Instance.����ս����();
            }
        }
        if (��ǰ�ж���ɫ.GetComponent<A4>().�����Ƿ���ҷ�ʹ�� == true && ��ǰ����Ŀ��.GetComponent<A1>().�Ƿ��ǵз�)
        {
            ��ǰ����Ŀ�� = ��ǰ�ж���ɫ.transform;
        }
        // �ı䰴ť��С
        if (���ܹ�����ť != null)
            ���ܹ�����ť.transform.localScale = ���ܰ�ť��ʼ��С * 1.2f;
        if (��ͨ������ť != null)
            ��ͨ������ť.transform.localScale = �չ���ť��ʼ��С;
        if (�ٻ�����ť != null)
            �ٻ�����ť.transform.localScale = �ٻ���ť��ʼ��С;
        if (��������ť != null)
            ��������ť.transform.localScale = ������ť��ʼ��С;
    }
    private void ����ʹ���ٻ���()
    {
        �Ƿ����ʹ���ٻ��� = true;
        ʹ���չ� = false;
        �Ƿ����ʹ���չ� = false;
        �Ƿ����ʹ�ü��� = false;
        ʹ�ü��� = false;
        if (ʹ���ٻ�&&B1.Instance.��ǰս����>0)
        {
            �ٻ�����();
            B1.Instance.����ս����();
        }
        // �ı䰴ť��С
        if (���ܹ�����ť != null)
            ���ܹ�����ť.transform.localScale = ���ܰ�ť��ʼ��С;
        if (��ͨ������ť != null)
            ��ͨ������ť.transform.localScale = �չ���ť��ʼ��С;
        if (�ٻ�����ť != null)
            �ٻ�����ť.transform.localScale = �ٻ���ť��ʼ��С * 1.2f;
        if (��������ť != null)
            ��������ť.transform.localScale = ������ť��ʼ��С;
    }
    public void ����ʹ�ñ�ɱ��(A1 ˭ʹ�ñ�ɱ��)//�����ж�
    {
        ˭ʹ�ñ�ɱ��.GetComponent<A1>().��ǰ��ɱ������ = 0;
        // ���ϲ���ж���
        Transform targetTransform = B1.Instance.���ϲ�Ķ���.transform;
        // Ŀ����ж���
        Transform objectToMove = ˭ʹ�ñ�ɱ��.GetComponent<A1>().�Լ���Ӧ���ж���.transform;

        if (objectToMove != null && targetTransform != null)
        {
            if (targetTransform != null)
            {
                // ��ȡĿ�����Ĳ㼶����λ��
                int targetSiblingIndex = targetTransform.GetSiblingIndex();

                // ����Ҫ�ƶ��Ķ������õ�Ŀ�����ĺ󷽣����� + 1 ��λ�ã�
                objectToMove.SetSiblingIndex(targetSiblingIndex + 1);
                ˭ʹ�ñ�ɱ��.GetComponent<A1>().����Ч���ı�("�����ж�", Color.red);
            }
        }
    }
    public void ����ʹ��������()
    {
        �Ƿ����ʹ�������� = true;
        if (���ܹ�����ť != null)
            ���ܹ�����ť.transform.localScale = ���ܰ�ť��ʼ��С;
        if (��ͨ������ť != null)
            ��ͨ������ť.transform.localScale = �չ���ť��ʼ��С;
        if (�ٻ�����ť != null)
            �ٻ�����ť.transform.localScale = �ٻ���ť��ʼ��С;
        if (��������ť != null)
            ��������ť.transform.localScale = ������ť��ʼ��С * 1.2f;
        if (ʹ������ && ��ǰ�ж���ɫ.GetComponent<A4>().�������Ƿ��ƶ���Ŀ���ͷ� == false)
        {
            ��������();
        }
        if (ʹ������ && ��ǰ�ж���ɫ.GetComponent<A4>().�������Ƿ��ƶ���Ŀ���ͷ�)
        {
            ��ս��������();
        }
    }
    public void ��ʾ���ܻ��չ������ı�(string textToDisplay)
    {
        if (��ʾ���������ı� != null)
        {
            // �����ı�����
            ��ʾ���������ı�.text = textToDisplay;

            // �����ı�����
            ��ʾ���������ı�.gameObject.SetActive(true);

            // �ӳ�һ��ʱ�������ı�����
            StartCoroutine(DisableTextAfterDelay());
        }
    }

    // Э�����ڽ����ı�����
    private IEnumerator DisableTextAfterDelay()
    {
        yield return new WaitForSeconds(1);//�ı���ʾʱ��

        if (��ʾ���������ı� != null)
        {
            // �����ı�����
            ��ʾ���������ı�.gameObject.SetActive(false);
        }
    }
    void ��¼��ʼλ��()
    {
        ��ʼλ�� = ��ǰ�ж���ɫ.transform.position; // ��¼��ʼλ��
    }
    void ��ɼ����˺�()
    {
        ��ǰ�ж���ɫ.GetComponent<A4>().ʹ�ü���(��ǰ����Ŀ��.gameObject, ��ǰ�ж���ɫ.GetComponent<A1>().����);
    }
    void ����չ��˺�()
    {
        ��ǰ�ж���ɫ.GetComponent<A4>().ʹ���չ�(��ǰ����Ŀ��.gameObject, ��ǰ�ж���ɫ.GetComponent<A1>().����);
    }
    void ʹ���ٻ���()
    {
        ��ǰ�ж���ɫ.GetComponent<A4>().ʹ���ٻ���();
    }
    void ʹ��������()
    {
        if(��ǰ�ж���ɫ!=null&&��ǰ����Ŀ��!=null)
        ��ǰ�ж���ɫ.GetComponent<A4>().ʹ��������(��ǰ����Ŀ��.gameObject, ��ǰ�ж���ɫ.GetComponent<A1>().����);
    }
    void ��������˺�()
    {
        ��ǰ�ж���ɫ.GetComponent<A4>().��������˺�(��ǰ����Ŀ��.gameObject, ��ǰ�ж���ɫ.GetComponent<A1>().����);
    }
    void ���ż�����Ч()
    {
        ��ǰ�ж���ɫ.GetComponent<A4>().����ָ��������Ч(��ǰ����Ŀ��);
    }
    void �����չ���Ч()
    {
        ��ǰ�ж���ɫ.GetComponent<A4>().����ָ���չ���Ч(��ǰ����Ŀ��);
    }
    void �ƶ���ǰ�ж���ɫ���ж�������ײ�()
    {
        isAttacking = false;
        isMoving = false;
        GameObject ���� = B1.Instance.���ϲ�Ķ���.GetComponent<A2>().�ж�����Ӧ��ɫ;
        Transform parentTransform = B1.Instance.�����ж���;
        int childCount = parentTransform.childCount;

        if (childCount > 1)
        {
            Transform topChild = parentTransform.GetChild(0);
            Transform bottomChild = parentTransform.GetChild(childCount - 1);
            ����.GetComponent<A4>().��������Buffʣ��غ�();
            topChild.SetAsLastSibling();
            ����.GetComponent<A1>().�ָ�����();
        }
        StartCoroutine(DelayedExecutionCoroutine());
    }

    private IEnumerator DelayedExecutionCoroutine()
    {
        yield return new WaitForSeconds(0.35f);
        if (��������)
        {
            yield return new WaitForSeconds(0.8f);
            �������� = false;
        }
        ���ٹ�����ť();
        ѡ��ǰ�ж���ɫ = true;
        �������ɰ�ť = true;
    }
    private IEnumerator DelayedExecutionCoroutine2()
    {
        yield return new WaitForSeconds(0.8f);
        ѡ��ǰ�ж���ɫ = false;
    }
    private IEnumerator DelayedExecutionCoroutine3()
    {
        if (��ǰ����Ŀ�� == null&&��ǰ�ж���ɫ.GetComponent<A1>().�Ƿ��ǵз�)
        {
                ѡ��Ŀ������м������Ϊ����Ŀ��(��Ҷ���);
                yield return new WaitForSeconds(0.5f);
                �ƶ���ǰ�ж���ɫ���ж�������ײ�();

        }
        else
        {
            yield return new WaitForSeconds(0.4f);
            �ƶ���ǰ�ж���ɫ���ж�������ײ�(); ;
            isMoving = false;
            ���ٹ�����ť();
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

    public Transform FindSecondSubset(Transform parent)
    {
        Transform secondSubset = null;

        if (parent.childCount >= 2)
        {
            // ��������������Ӷ��󣬻�ȡ�ڶ����Ӷ�����ΪĿ���Ӽ�
            secondSubset = parent.GetChild(1);
        }
        else
        {
            // ����Ӷ������������������ݹ�����Ӷ�����Ӷ���
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
    void ѡ��Ŀ������м������Ϊ����Ŀ��(GameObject ��һ�з�����)
    {
        int childCount = ��һ�з�����.transform.childCount;
        if (childCount > 0)
        {
            // �����м�λ�õ�����
            int middleIndex = childCount / 2;
            // ��ȡ�м�λ�õ��Ӷ���
            Transform middleChild = ��һ�з�����.transform.GetChild(middleIndex);

            ��ǰ����Ŀ�� = middleChild;
        }
        ��ǰ�ж���ɫ.transform.LookAt(��ǰ����Ŀ��.transform);
        if(��һ�з�����==NPC����&&��ǰ����Ŀ��!=null)
        �ҷ������.LookAt = ��ǰ����Ŀ��.transform.Find("��ݽ�ɫ����").transform.Find("���������λ��");
    }

}



