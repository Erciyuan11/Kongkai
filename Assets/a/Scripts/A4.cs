using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class A4 : MonoBehaviour//���ڽ�ɫ�������ڿ��ƽ�ɫ����
{
    [Header("                                              �չ�Ч��")]
    public string �չ�����;
    public string �չ�����;
    public bool �Ƿ����ʹ���չ� = true;
    public bool �չ��Ƿ�ظ�ս���� = true;
    public int �չ�����˺����� = 1;
    public float �չ���ɶ���˺���� = 0.1f;
    public bool �չ��Ƿ��ƶ���Ŀ��;
    [HideInInspector]
    public float ����չ��ƶ���Ŀ�겥�Ŷ�����ÿ�ʼ�ƶ� = 0.1f;
    public float �չ��˺����� = 1f;
    public int �չ�����ֵ = 1;
    public float �չ�����ʱ�� = 1.5f;
    public float ��ʼ�����չ�����������ú�����˺� = 0.2f;
    public float ��ʼ�����չ��������ú�������Ч = 0.2f;
    public GameObject �չ�������Ч;
    public float �չ���Ч����ʱ�� = 0.6f;
    public AudioClip[] �չ�������Ч;
    AudioSource ��Ч������;
    [Header("                                              ����Ч��")]
    public string ��������;
    public string ��������;
    public bool �Ƿ����ʹ�ü���;
    public bool ���ܹ����Ƿ����ƶ���Ŀ��;
    [HideInInspector]
    public float ��������ƶ���Ŀ�겥�Ŷ�����ÿ�ʼ�ƶ� = 0.1f;
    public float ���ܱ��� = 2;
    public bool �����Ƿ���ҷ�ʹ��; // �Ƿ���ҷ�ʹ��
    public bool �����Ƿ�����ս����;
    public int ��������˺����� = 1;
    public float ������ɶ���˺������ = 0.2f;
    public int ���ܹ�������ֵ = 2;
    public float ���ܶ���ʱ�� = 1.5f;
    public float ��ʼ���ż��ܹ���������ú�����˺�;
    public float ��ʼ���ż��ܶ������ú�������Ч = 0;
    public GameObject ����������Ч;
    public float ������Ч����ʱ�� = 1;
    public AudioClip[] ���ܻ�����Ч;

    [Header("������Ч����ѡ��һ��")]
    public bool �Ե�����Ч = true;
    public bool ��Ŀ�����ȫ����Ч;
    public bool ��Ŀ������Ҷ�����Ч;
    public float ����Ŀ���ܵ��˺����� = 0.5f;
    public bool ��Ŀ����������������Ч;

    [Header("���ܶԵз���Ч��")]
    public bool �Եз�����˺� = true;

    public bool �õз������ܵ��˺�A;
    //public string �����˺�A����="����";
    public bool �õз������ܵ��˺�B;
    //public string �����˺�B���� = "����";
    public int �����ܵ��˺��غ� = 3;
    public float �����˺����� = 0.2f;
    public int �����˺������� = 0;

    public bool �õз�����������;
    public int ���������Ͷ��� = 30;
    public int ���������ͳ����غ� = 1;

    public bool �õз��ж�����;
    public int �ж����Ͷ��� = 1;

    public bool ����ǿ�صз�;
    public float ���� = 0.6f;
    //public GameObject ͣ����Ŀ�����ϵ�ǿ����Ч;
    public int ǿ�س����غ� = 1;

    [Header("���Լ�����Ч��")]
    public bool ���ѷ��ظ�����;
    public bool ���ѷ������ӻ���;
    public bool ���ѷ������ж�;
    public bool ���ѷ��������;
    public bool ���ѷ����ӹ�����;
    public int �������������� = 30;
    public int ���ӹ����������غ� = 1;
    public bool ���ѷ��ж�����;
    public int �ж��������� = 1;

    [Header("                                              ������Ч��")]
    public string ����������;
    public string �������������;
    public string ����������;
    public bool �Ƿ����ʹ��������;
    public float ������Ч������ = 3;
    public bool ������ = false;
    public GameObject ������Ч;
    public GameObject �ͷ�������Ч;
    public int ������������ֵ = 1;
    public bool �������Ե�����Ч = true;
    public bool �������Ƿ��Ŀ�����ȫ����Ч;
    public bool �������Ƿ��Ŀ������Ҷ�����Ч;
    public float ����������Ŀ���ܵ��˺����� = 0.5f;
    public int ��������˺����� = 1;
    public float ������ɶ���˺������ = 0.2f;
    public bool �������Ƿ��ƶ���Ŀ���ͷ�;
    [HideInInspector]
    public float ��������ƶ���Ŀ�겥�Ŷ�����ÿ�ʼ�ƶ� = 0;
    public float ��ʼ���������������ú�����˺� = 0;
    public float ������Ч����ʱ�� = 0.6f;
    public float ��������ʱ�� = 1.5f;

    [Header("                                              �ٻ���Ч��")]
    public string �ٻ�������;
    public string �ٻ�������;
    public bool �Ƿ����ʹ���ٻ���;//�����Ƿ���ʾ�ٻ���ť
    public GameObject[] �ٻ��б�; // �������Ҫ���ɵ���Ϸ���������
    public int ���һ���ٻ�����Ŀ��;   // ��Ҫ���ɵ���Ϸ��������
    public float ��ʼ�����ٻ��������ú�ʼ�ٻ� = 0;
    public float �ٻ�����ʱ�� = 1.5f;
    public bool �ٻ����Ƿ�Ϊ��ɫ�ٻ���;
    public GameObject ��ɫ�ٻ���;

    [Header("��Ч")]
    public GameObject Ŀ��������Ч;
    public float ������Ч����ʱ��=0.2f;
    public bool �չ���Ч�Ƿ�ֱ����Ŀ���ͷ� = false;
    public bool ������Ч�Ƿ�ֱ����Ŀ���ͷ� = false;
    public bool ������Ч�Ƿ�ֱ����Ŀ���ͷ� = false;
            [HideInInspector]
    public Transform ��Ч����λ��;

    [Header("buff���")]
    public GameObject buffͼ��Ԥ����;
    Sprite ��ɫ�ٻ���ͼ��;


    //[Header("����Ĳ��ù�")]
        [HideInInspector]
    public GameObject ���ɵ�������Ч;
        [HideInInspector]
    public GameObject ���ɵ���ɫ�ٻ���buffͼ��;
        [HideInInspector]
    public GameObject ���ɵ�ǿ��buffͼ��;
        [HideInInspector]
    public int ʣ�౻ǿ�ػغ���;
        [HideInInspector]
    public GameObject ���ɵĹ���������buffͼ��;
        [HideInInspector]
    public int ʣ�๥���������غ���;
        [HideInInspector]
    public float �����Ĺ�����;
        [HideInInspector]
    public GameObject ���ɵķ���������buffͼ��;
        [HideInInspector]
    public int ʣ����������ͻغ���;
        [HideInInspector]
    public float ���͵ķ�����;
        [HideInInspector]
    public GameObject ���ɵĳ��������˺�buffͼ��;
        [HideInInspector]
    public GameObject ���ɵĳ��������˺�buffͼ��;
        [HideInInspector]
    public int ʣ������˺�A�غ���;
        [HideInInspector]
    public int ʣ������˺�B�غ���;
        [HideInInspector]
    public float �����ܵ��˺���;
        [HideInInspector]
    public GameObject ���ɵ���ɫ�ٻ���;
    int callCount = 0;//��������

     Camera ��ʼ�����;
    Image buff����;
    Transform Ŀ�����;
    int ִ�д��� = 0;
    int ���ִ�д��� = 2; // ��Ҫִ�е�������
    GameObject ����Ŀ��;
    string ���ܹ�������;

    void Start()
    {
        var a = transform.Find("��ݽ�ɫ����");
        ��Ч����λ�� = a.transform.Find("��Ч����λ��");
        ��Ч������ = GetComponent<AudioSource>();
        ��ʼ����� = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        ������buffʣ��غ�ˢ��();

        if (GetComponent<A1>().�Ƿ�ǿ�� && ʣ�౻ǿ�ػغ��� == 0)

            GetComponent<A1>().�ָ�����();

        if (GetComponent<A1>().�Ƿ��ܵ����������˺� && ʣ������˺�A�غ��� == 0)
        {
            GetComponent<A1>().�ָ��ܵ������˺�();
        }
        if (GetComponent<A1>().�Ƿ��ܵ����������˺� && ʣ������˺�B�غ��� == 0)
        {
            GetComponent<A1>().�ָ��ܵ������˺�();
        }
        if (GetComponent<A1>().�Ƿ����������� && ʣ�๥���������غ��� == 0)
        {
            GetComponent<A1>().�ָ�����������();
        }
        if (GetComponent<A1>().�Ƿ񽵵ͷ����� && ʣ����������ͻغ��� == 0)
        {
            GetComponent<A1>().�ָ�������������();
        }
        if (GetComponent<A1>().��ǰ����ֵ <= 0)
        {
            if (���ɵ���ɫ�ٻ��� != null)
            {
                ���ɵ���ɫ�ٻ���.GetComponent<A1>().�ӳ�ɾ��(0.8f);
            }
        }
    }
    public void ʹ�ü���(GameObject Ŀ��, string ��������)
    {
        ���ż�����Ч();
        ����Ŀ�� = Ŀ��;
        ���ܹ������� = ��������;
        StartCoroutine(���������˺����());
    }
    public void ʹ���չ�(GameObject Ŀ��, string ��������)
    {
        ����Ŀ�� = Ŀ��;
        �����չ���Ч();
        StartCoroutine(�����չ��˺����(Ŀ��, ��������));
    }
    public void ʹ���ٻ���()
    {
        �ٻ�();
    }

    public void ʹ��������(GameObject Ŀ��, string ��������)
    {
        ����Ŀ�� = Ŀ��;
        callCount++;
        if (���ɵ�������Ч == null && ������Ч != null)
            ���ɵ�������Ч = Instantiate(������Ч, transform);

        ������ = true;
        if (callCount >= 2)
        {
            // ���ü�����
            callCount = 0;
        }
    }

    public void �ͷ�������(GameObject Ŀ��, string ��������)
    {
        Animator animator = GetComponent<Animator>();
        ������ = false;
        animator.SetBool("����", false);
        animator.SetBool("�ͷ�����", true);
        Destroy(���ɵ�������Ч);
    }
    public void ��������˺�(GameObject Ŀ��, string ��������)
    {
        //Debug.Log(Ŀ��.name);
        if (������Ч�Ƿ�ֱ����Ŀ���ͷ� && �ͷ�������Ч != null)
        {
            GameObject sf = Instantiate(�ͷ�������Ч, Ŀ��.transform);
            Destroy(sf, ������Ч����ʱ��);
        }
        else if (!������Ч�Ƿ�ֱ����Ŀ���ͷ� && �ͷ�������Ч != null)
        {
            GameObject sf = Instantiate(�ͷ�������Ч, ��Ч����λ��);
            Destroy(sf, ������Ч����ʱ��);
        }
        float ����˺� = GetComponent<A1>().������ * ������Ч������;
        Transform parentTransform = Ŀ��.transform.parent;

        // ʹ��Э�̴��������˺�
        StartCoroutine(�����������˺����(Ŀ��, ��������, ��������˺�����, ������ɶ���˺������, ����˺�));
    }
    private void �ٻ�()
    {
        if (�ٻ����Ƿ�Ϊ��ɫ�ٻ��� == false)
        {
            bool �������Ϸ� = true;
            int ʵ���ٻ����� = ���һ���ٻ�����Ŀ��;
            if (gameObject.GetComponent<A1>().�Ƿ��ǵз� == false)
            {
                Debug.Log("222");
                if (B2.Instance.���ʣ������ + ���һ���ٻ�����Ŀ�� >= 4)
                {
                    ʵ���ٻ����� = 4 - B2.Instance.���ʣ������;
                }
            }
            for (int i = 0; i < ʵ���ٻ�����; i++)
            {
                GameObject randomObject = GetRandomObject();

                if (randomObject != null && gameObject.GetComponent<A1>().�Ƿ��ǵз� == false)
                    {

                        GameObject spawnedObject = Instantiate(randomObject, B1.Instance.�ҷ�����);
                        Debug.Log("222");
                        // ���ݷ������Ϸ��Ĳ���������ѡ�����λ��
                        if (�������Ϸ�)
                        {
                            spawnedObject.transform.SetAsFirstSibling(); // �������Ϸ�
                        }
                        else
                        {
                            spawnedObject.transform.SetAsLastSibling(); // �������·�
                        }

                        // �л�����������ֵ
                        �������Ϸ� = !�������Ϸ�;
                }
                else if (randomObject != null && gameObject.GetComponent<A1>().�Ƿ��ǵз�)
                {
                    GameObject spawnedObject = Instantiate(randomObject, B1.Instance.�з�����);

                    // ���ݷ������Ϸ��Ĳ���������ѡ�����λ��
                    if (�������Ϸ�)
                    {
                        spawnedObject.transform.SetAsFirstSibling(); // �������Ϸ�
                    }
                    else
                    {
                        spawnedObject.transform.SetAsLastSibling(); // �������·�
                    }

                    // �л�����������ֵ
                    �������Ϸ� = !�������Ϸ�;
                }

            }
        }
        else
        {
            if (B2.Instance.��ǰ�ж���ɫ.GetComponent<A1>().�Ƿ��ǵз� == false)
            {
                GameObject ����λ��;
                ����λ�� = GameObject.Find("2���������ɫ��λ��");
                if (���ɵ���ɫ�ٻ��� == null)
                {
                    ���ɵ���ɫ�ٻ��� = Instantiate(��ɫ�ٻ���, ����λ��.transform);
                    ��ɫ�ٻ���ͼ�� = ��ɫ�ٻ���.GetComponent<A1>().��ɫͷ��;
                    ���ɵ���ɫ�ٻ���buffͼ�� = Instantiate(buffͼ��Ԥ����, GetComponent<A1>().buff����λ��);
                }
            }
            if (B2.Instance.��ǰ�ж���ɫ.GetComponent<A1>().�Ƿ��ǵз� == true)
            {
                GameObject ����λ��;
                ����λ�� = GameObject.Find("2���������ɫ��λ��(�з�)");
                if (���ɵ���ɫ�ٻ��� == null)
                {
                    ���ɵ���ɫ�ٻ��� = Instantiate(��ɫ�ٻ���, ����λ��.transform);
                    ��ɫ�ٻ���ͼ�� = ��ɫ�ٻ���.GetComponent<A1>().��ɫͷ��;
                    ���ɵ���ɫ�ٻ���buffͼ�� = Instantiate(buffͼ��Ԥ����, GetComponent<A1>().buff����λ��);
                }
            }

            buff���� = ���ɵ���ɫ�ٻ���buffͼ��.GetComponent<Image>();
            buff����.sprite = ��ɫ�ٻ���ͼ��;
            ���ɵ���ɫ�ٻ���.GetComponent<A1>().��ɫ�ٻ���ʣ��غ� = ���ɵ���ɫ�ٻ���.GetComponent<A1>().��ɫ�ٻ�����ڻغ�;
        }
    }

    private GameObject GetRandomObject()//���ѡ���ٻ��б��һ��
    {
        int randomIndex = Random.Range(0, �ٻ��б�.Length);
        return �ٻ��б�[randomIndex];
    }



    public void ����ָ���չ���Ч(Transform transform)
    {
        //Debug.Log("�չ���Ч");
        if (�չ��Ƿ��ƶ���Ŀ��)
        {
            // ��ս����
            �չ���Ч(transform.transform);
        }
        else
        {
            // Զ�̹���
            if (�չ���Ч�Ƿ�ֱ����Ŀ���ͷ�)
            {
                �չ���Ч(transform.transform);
            }
            else
            {
                �ƶ��չ���Ч(transform.transform);
            }
        }

    }
    public void ����ָ��������Ч(Transform transform)
    {
        //Debug.Log("������Ч");
        if (���ܹ����Ƿ����ƶ���Ŀ��)
        {
            // ��ս����
            ���ƶ�������Ч(transform.transform);
        }
        else
        {

            // Զ�̹���
            if (������Ч�Ƿ�ֱ����Ŀ���ͷ�)
            {
                ���ƶ�������Ч(transform.transform);
            }
            else
            {
                �ƶ�������Ч(transform.transform);
            }
        }
    }
    private void �չ���Ч(Transform Ŀ��λ��)
    {
        // ������ս������Ч
        if (�չ��Ƿ��ƶ���Ŀ��)
        {
            if (�չ�������Ч != null && ��Ч����λ�� != null)
            {
                GameObject meleeEffect = Instantiate(�չ�������Ч, Ŀ��λ��.position, Quaternion.identity);
                Destroy(meleeEffect.gameObject, �չ���Ч����ʱ��);
            }
        }
    }
    private void �ƶ��չ���Ч(Transform Ŀ��λ��)
    {
        // ������ս������Ч
        if (�չ�������Ч != null && ��Ч����λ�� != null)
        {
            GameObject meleeEffect = Instantiate(�չ�������Ч, ��Ч����λ��.position, Quaternion.identity);
            if (Ŀ��λ��.Find("��ݽ�ɫ����").transform.Find("���������λ��") != null)
                meleeEffect.transform.LookAt(Ŀ��λ��.Find("��ݽ�ɫ����").transform.Find("���������λ��").position);
            else
                meleeEffect.transform.LookAt(Ŀ��λ��.position);
            meleeEffect.transform.position = Vector3.MoveTowards(meleeEffect.transform.position, Ŀ��λ��.Find("��ݽ�ɫ����").transform.Find("���������λ��").position, 10 * Time.deltaTime);
            Destroy(meleeEffect.gameObject, �չ���Ч����ʱ��);
        }
    }

    private void ���ƶ�������Ч(Transform Ŀ��λ��)
    {
        // ����Զ��������Ч
        if (����������Ч != null)
        {
            GameObject meleeEffect = Instantiate(����������Ч, Ŀ��λ��.position, Quaternion.identity);
            Destroy(meleeEffect.gameObject, ������Ч����ʱ��);
        }
    }

    private void �ƶ�������Ч(Transform Ŀ��λ��)
    {
        if (����������Ч != null)
        {
            GameObject meleeEffect = Instantiate(����������Ч, ��Ч����λ��.position, Quaternion.identity);
            if (Ŀ��λ��.Find("��ݽ�ɫ����").transform.Find("���������λ��") != null)
                meleeEffect.transform.LookAt(Ŀ��λ��.Find("��ݽ�ɫ����").transform.Find("���������λ��").position);
            else
                meleeEffect.transform.LookAt(Ŀ��λ��.position);
            meleeEffect.transform.position = Vector3.MoveTowards(meleeEffect.transform.position, Ŀ��λ��.Find("��ݽ�ɫ����").transform.Find("���������λ��").position, 10 * Time.deltaTime);
            Destroy(meleeEffect.gameObject, ������Ч����ʱ��);

        }
    }

    void ��Ŀ���ж����˶���(GameObject Ŀ��, int ��ǰ���Ӻ�)
    {
        Transform objectToMove = Ŀ��.GetComponent<A1>().�Լ���Ӧ���ж���.transform;
        if (objectToMove != null)
        {
            // ��ȡĿ�����Ĳ㼶����λ��
            int targetSiblingIndex = objectToMove.GetSiblingIndex();
            // �ƶ��Ķ������õ�Ŀ�����ĺ󷽾��ǣ����� + 1 ��
            objectToMove.SetSiblingIndex(targetSiblingIndex + ��ǰ���Ӻ�);
        }
    }
    void ��Ŀ�����ӹ���buff(GameObject Ŀ��)
    {
        if (Ŀ��.GetComponent<A4>().���ɵĹ���������buffͼ�� == null)
        {
            Ŀ��.GetComponent<A4>().�����Ĺ����� = ��������������;
            Ŀ��.GetComponent<A1>().������ += ��������������;
            Ŀ��.GetComponent<A4>().ʣ�๥���������غ��� = ���ӹ����������غ�;
            Ŀ��.GetComponent<A4>().���ɵĹ���������buffͼ�� = Instantiate(buffͼ��Ԥ����, Ŀ��.GetComponent<A1>().buff����λ��);
            buff���� = ����Ŀ��.GetComponent<A4>().���ɵĹ���������buffͼ��.GetComponent<Image>();
            buff����.sprite = B1.Instance. ����������ͼ��;
        }
        else
        {
            Ŀ��.GetComponent<A4>().ʣ����������ͻغ��� = ���������ͳ����غ�;
        }
    }
    void ��Ŀ�꽵�ͷ�����(GameObject Ŀ��)
    {

        if (Ŀ��.GetComponent<A4>().���ɵķ���������buffͼ�� == null)
        {
            Ŀ��.GetComponent<A4>().���͵ķ����� = ���������Ͷ���;
            Ŀ��.GetComponent<A1>().������ -= ���������Ͷ���;
            Ŀ��.GetComponent<A4>().ʣ����������ͻغ��� = ���������ͳ����غ�;
            Ŀ��.GetComponent<A4>().���ɵķ���������buffͼ�� = Instantiate(buffͼ��Ԥ����, Ŀ��.GetComponent<A1>().buff����λ��);
            buff���� = ����Ŀ��.GetComponent<A4>().���ɵķ���������buffͼ��.GetComponent<Image>();
            buff����.sprite = B1.Instance.�������½�ͼ��;
        }
        else
        {
            Ŀ��.GetComponent<A4>().ʣ����������ͻغ��� = ���������ͳ����غ�;
        }
    }
    void ��Ŀ�����ǿ��buff(GameObject Ŀ��)
    {
        Ŀ��.GetComponent<A1>().�Ƿ�ǿ�� = true;
        Ŀ��.GetComponent<Animator>().SetBool("����", true);
        if (Ŀ��.GetComponent<A4>().���ɵ�ǿ��buffͼ�� == null)
        {
            Ŀ��.GetComponent<A1>().���ɵĶ�����Ч = Instantiate(B1.Instance. ͣ����Ŀ�����ϵ�ǿ����Ч, Ŀ��.transform);
            Ŀ��.GetComponent<A4>().ʣ�౻ǿ�ػغ��� = ǿ�س����غ�;
            Ŀ��.GetComponent<A4>().���ɵ�ǿ��buffͼ�� = Instantiate(buffͼ��Ԥ����, Ŀ��.GetComponent<A1>().buff����λ��);
            buff���� = ����Ŀ��.GetComponent<A4>().���ɵ�ǿ��buffͼ��.GetComponent<Image>();
            buff����.sprite = B1.Instance.ǿ��ͼ��;

        }
        else
        {
            Ŀ��.GetComponent<A4>().ʣ�౻ǿ�ػغ��� = ǿ�س����غ�;
        }
    }
    void ��Ŀ����ӳ����˺�A(GameObject Ŀ��, int �����˺�������)
    {
        Ŀ��.GetComponent<A1>().�Ƿ��ܵ����������˺� = true;
        if (Ŀ��.GetComponent<A4>().���ɵĳ��������˺�buffͼ�� == null)
        {
            Ŀ��.GetComponent<A4>().�����ܵ��˺��� = GetComponent<A1>().������ * �����˺�����;
            //Ŀ��.GetComponent<A1>().�ܵ������˺�(Ŀ��.GetComponent<A4>().�����ܵ��˺���, GetComponent<A1>().����, �����˺�������);
            Ŀ��.GetComponent<A1>().����Ч���ı�(B1.Instance.�����˺�A����, Ŀ��.GetComponent<A1>().�����˺�A��ɫ);
            Ŀ��.GetComponent<A4>().ʣ������˺�A�غ��� = �����ܵ��˺��غ�;
            Ŀ��.GetComponent<A4>().���ɵĳ��������˺�buffͼ�� = Instantiate(buffͼ��Ԥ����, Ŀ��.GetComponent<A1>().buff����λ��);
            Ŀ��.GetComponent<A1>().�ܵ��ĳ����˺������� = �����˺�������;
            buff���� = ����Ŀ��.GetComponent<A4>().���ɵĳ��������˺�buffͼ��.GetComponent<Image>();
            buff����.sprite =B1.Instance. �����˺�ͼ��A;
            
        }
        else
        {
            Ŀ��.GetComponent<A4>().ʣ������˺�A�غ��� = �����ܵ��˺��غ�;
        }
    }
    void ��Ŀ����ӳ����˺�B(GameObject Ŀ��, int �����˺�������)
    {
        Ŀ��.GetComponent<A1>().�Ƿ��ܵ����������˺� = true;
        if (Ŀ��.GetComponent<A4>().���ɵĳ��������˺�buffͼ�� == null)
        {
            Ŀ��.GetComponent<A4>().�����ܵ��˺��� = GetComponent<A1>().������ * �����˺�����;
            //Ŀ��.GetComponent<A1>().�ܵ������˺�(Ŀ��.GetComponent<A4>().�����ܵ��˺���, GetComponent<A1>().����, �����˺�������);
            Ŀ��.GetComponent<A1>().����Ч���ı�(B1.Instance.�����˺�B����, Ŀ��.GetComponent<A1>().�����˺�B��ɫ);
            Ŀ��.GetComponent<A4>().ʣ������˺�B�غ��� = �����ܵ��˺��غ�;
            Ŀ��.GetComponent<A4>().���ɵĳ��������˺�buffͼ�� = Instantiate(buffͼ��Ԥ����, Ŀ��.GetComponent<A1>().buff����λ��);
            Ŀ��.GetComponent<A1>().�ܵ��ĳ����˺������� = �����˺�������;
            buff���� = ����Ŀ��.GetComponent<A4>().���ɵĳ��������˺�buffͼ��.GetComponent<Image>();
            buff����.sprite = B1.Instance. �����˺�ͼ��B;
        }
        else
        {
            Ŀ��.GetComponent<A4>().ʣ������˺�B�غ��� = �����ܵ��˺��غ�;
        }
    }


    private IEnumerator �ӳٻظ�(float �ظ���)            // ���������Ŀ��ظ�����
    {
        ִ�д��� = 0;
        while (ִ�д��� < ���ִ�д���)
        {
            yield return new WaitForSeconds(0.2f);

            // ��ȡ�з����е������Ӽ�
            A1[] �Ӽ� = Ŀ�����.GetComponentsInChildren<A1>();
            if (�Ӽ�.Length > 0)
            {
                int ������� = Random.Range(0, �Ӽ�.Length);
                A1 ���Ŀ�� = �Ӽ�[�������];

                // �����Ŀ��ظ�����
                ���Ŀ��.�ظ�����(�ظ���);
                ���Ŀ��.����Ч���ı�("�ظ�����", Color.green);
                �ظ��� = �ظ��� * 0.7f;

                ִ�д���++;
            }
        }
    }
    private IEnumerator �ӳ��˺�(GameObject Ŀ��, float �˺�, string ����)            // �������������˺�
    {
        ִ�д��� = 0;
        while (ִ�д��� < ���ִ�д���)
        {
            yield return new WaitForSeconds(0.2f);

            // ��ȡ�з����е������Ӽ�
            A1[] �Ӽ� = Ŀ�����.GetComponentsInChildren<A1>();
            if (�Ӽ�.Length > 0)
            {
                int ������� = Random.Range(0, �Ӽ�.Length);
                A1 ���Ŀ�� = �Ӽ�[�������];

                // �����Ŀ��ظ�����
                ���Ŀ��.��������(���Ŀ��.gameObject, �˺�, ����);
                // Debug.Log("11");
                if (Ŀ��������Ч != null)
                {
                    GameObject meleeEffect = Instantiate(Ŀ��������Ч, ���Ŀ��.transform.position, Quaternion.identity);
                    Destroy(meleeEffect.gameObject, ������Ч����ʱ��);
                }
                ִ�д���++;
            }
        }
    }
    public void ���������˺�()
    {
        if (�����Ƿ���ҷ�ʹ��)
        {
            if (���ѷ��ظ�����)
            {
                if (�Ե�����Ч)
                {
                    float ����˺� = GetComponent<A1>().������ * ���ܱ���;
                    this.����Ŀ��.GetComponent<A1>().�ظ�����(����˺�);
                    this.����Ŀ��.GetComponent<A1>().����Ч���ı�("�ظ�����", Color.green);
                }
                else
                if (��Ŀ����������������Ч)
                {
                    float ����˺� = GetComponent<A1>().������ * ���ܱ���;
                    this.����Ŀ��.GetComponent<A1>().�ظ�����(����˺�);
                    this.����Ŀ��.GetComponent<A1>().����Ч���ı�("�ظ�����", Color.green);

                    if (this.����Ŀ��.GetComponent<A1>().�Ƿ��ǵз�)
                    {
                        Ŀ����� = B1.Instance.�з�����;
                        StartCoroutine(�ӳٻظ�(����˺� * 0.7f));
                    }
                    if (this.����Ŀ��.GetComponent<A1>().�Ƿ��ǵз� == false)
                    {
                        Ŀ����� = B1.Instance.�ҷ�����;
                        StartCoroutine(�ӳٻظ�(����˺� * 0.7f));
                    }
                }
                else               
                if (��Ŀ�����ȫ����Ч)
                {
                    float ����˺� = GetComponent<A1>().������ * ���ܱ���;
                    Transform parentTransform = this.����Ŀ��.transform.parent;
                    // ���������Ӽ���Ϸ����
                    foreach (Transform childTransform in parentTransform)
                    {
                        GameObject childObject = childTransform.gameObject;
                        childObject.GetComponent<A1>().�ظ�����(����˺�);
                        childObject.GetComponent<A1>().����Ч���ı�("�ظ�����", Color.green);
                        if (Ŀ��������Ч != null)
                        {
                            GameObject meleeEffect = Instantiate(Ŀ��������Ч, childObject.transform.position, Quaternion.identity);
                            Destroy(meleeEffect.gameObject, ������Ч����ʱ��);
                        }
                    }
                }
            }
            if (���ѷ������ж�)
            {
                if (�Ե�����Ч && ����Ŀ�� != this.gameObject)
                {
                    // �Լ����ж���
                    Transform targetTransform = gameObject.GetComponent<A1>().�Լ���Ӧ���ж���.transform;
                    // Ŀ����ж���
                    Transform objectToMove = ����Ŀ��.GetComponent<A1>().�Լ���Ӧ���ж���.transform;

                    if (objectToMove != null && targetTransform != null)
                    {
                        if (targetTransform != null)
                        {
                            // ��ȡĿ�����Ĳ㼶����λ��
                            int targetSiblingIndex = targetTransform.GetSiblingIndex();

                            // ����Ҫ�ƶ��Ķ������õ�Ŀ�����ĺ󷽣����� + 1 ��λ�ã�
                            objectToMove.SetSiblingIndex(targetSiblingIndex + 1);
                            this.����Ŀ��.GetComponent<A1>().����Ч���ı�("�����ж�", Color.red);
                        }
                    }
                }
            }
            if (���ѷ������ӻ���)
            {
                if (�Ե�����Ч)
                {
                    float ����˺� = GetComponent<A1>().������ * ���ܱ���;
                    this.����Ŀ��.GetComponent<A1>().���ӻ���(����˺�);
                    this.����Ŀ��.GetComponent<A1>().����Ч���ı�("���ӻ���", Color.yellow);
                }
            }
            if (���ѷ��������)
            {
                if (�Ե�����Ч)
                {
                    this.����Ŀ��.GetComponent<A1>().�ָ�����();
                    this.����Ŀ��.GetComponent<A1>().����Ч���ı�("�������", Color.red);
                    this.����Ŀ��.GetComponent<A4>().ʣ�౻ǿ�ػغ��� = 0;
                }
            }
            if (���ѷ����ӹ�����)
            {
                if (�Ե�����Ч)
                {
                    this.����Ŀ��.GetComponent<A1>().����Ч���ı�("����������", Color.red);
                    ��Ŀ�����ӹ���buff(this.����Ŀ��);
                }
            }
            if (���ѷ��ж�����)
            {
                Transform objectToMove = this.����Ŀ��.GetComponent<A1>().�Լ���Ӧ���ж���.transform;
                if (objectToMove != null)
                {
                    // ��ȡĿ�����Ĳ㼶����λ��
                    int targetSiblingIndex = objectToMove.GetSiblingIndex();
                    // ��Ŀ����������ƶ�ָ���Ĳ㼶λ��
                    if(targetSiblingIndex-�ж���������>1)
                    objectToMove.SetSiblingIndex(targetSiblingIndex - �ж���������);
                    else
                    objectToMove.SetSiblingIndex(1);
                    this.����Ŀ��.GetComponent<A1>().����Ч���ı�("�ж��ӿ�", Color.red);
                }
            }
        }
        else
        {
            if (�Եз�����˺�)
            {
                if (�Ե�����Ч)
                {
                    float ����˺� = GetComponent<A1>().������ * ���ܱ���;
                    this.����Ŀ��.GetComponent<A1>().�������ܹ���(this.����Ŀ��, ����˺�, ���ܹ�������);
                    if (Ŀ��������Ч != null)
                    {
                        GameObject meleeEffect = Instantiate(Ŀ��������Ч, this.����Ŀ��.transform.position, Quaternion.identity);
                        Destroy(meleeEffect.gameObject, ������Ч����ʱ��);
                    }
                }
                else
                if (��Ŀ�����ȫ����Ч)
                {
                    float ����˺� = GetComponent<A1>().������ * ���ܱ���;
                    Transform parentTransform = this.����Ŀ��.transform.parent;
                    // ���������Ӽ���Ϸ����
                    foreach (Transform childTransform in parentTransform)
                    {
                        GameObject childObject = childTransform.gameObject;
                        childObject.GetComponent<A1>().�������ܹ���(childObject, ����˺�, ���ܹ�������);
                        if (Ŀ��������Ч != null)
                        {
                            GameObject meleeEffect = Instantiate(Ŀ��������Ч, childObject.transform.position, Quaternion.identity);
                            Destroy(meleeEffect.gameObject, ������Ч����ʱ��);
                        }
                    }
                }
                else
                if (��Ŀ������Ҷ�����Ч)
                {
                    float ����˺� = GetComponent<A1>().������ * ���ܱ���;
                    Transform parentTransform = this.����Ŀ��.transform.parent;
                    this.����Ŀ��.GetComponent<A1>().�������ܹ���(this.����Ŀ��, ����˺�, ���ܹ�������);
                    if (Ŀ��������Ч != null)
                    {
                        GameObject meleeEffect = Instantiate(Ŀ��������Ч, this.����Ŀ��.transform.position, Quaternion.identity);
                        Destroy(meleeEffect.gameObject, ������Ч����ʱ��);
                    }
                    if (parentTransform != null)
                    {
                        int siblingIndex = this.����Ŀ��.transform.GetSiblingIndex();

                        int aboveIndex = siblingIndex - 1;
                        int belowIndex = siblingIndex + 1;

                        Transform aboveObject = aboveIndex >= 0 ? parentTransform.GetChild(aboveIndex) : null;
                        Transform belowObject = belowIndex < parentTransform.childCount ? parentTransform.GetChild(belowIndex) : null;
                        if (aboveObject != null)
                        {
                            aboveObject.GetComponent<A1>().��������(aboveObject.gameObject, ����˺� * ����Ŀ���ܵ��˺�����, ���ܹ�������);
                            if (Ŀ��������Ч != null)
                            {
                                GameObject meleeEffect1 = Instantiate(Ŀ��������Ч, aboveObject.transform.position, Quaternion.identity);
                                Destroy(meleeEffect1.gameObject, ������Ч����ʱ��);
                            }
                        }
                        if (belowObject != null)
                        {
                            belowObject.GetComponent<A1>().��������(belowObject.gameObject, ����˺� * ����Ŀ���ܵ��˺�����, ���ܹ�������);
                            if (Ŀ��������Ч != null)
                            {
                                GameObject meleeEffect2 = Instantiate(Ŀ��������Ч, belowObject.transform.position, Quaternion.identity);
                                Destroy(meleeEffect2.gameObject, ������Ч����ʱ��);
                            }
                        }

                    }
                }
                else
                if (��Ŀ����������������Ч)
                {
                    float ����˺� = GetComponent<A1>().������ * ���ܱ���;
                    this.����Ŀ��.GetComponent<A1>().�������ܹ���(����Ŀ��, ����˺�, ���ܹ�������);
                    �ӳ��˺�(this.����Ŀ��, ����˺� * 0.5f, ���ܹ�������);
                    if (Ŀ��������Ч != null)
                    {
                        GameObject meleeEffect = Instantiate(Ŀ��������Ч, this.����Ŀ��.transform.position, Quaternion.identity);
                        Destroy(meleeEffect.gameObject, ������Ч����ʱ��);
                    }
                    if (this.����Ŀ��.GetComponent<A1>().�Ƿ��ǵз�)
                    {
                        Ŀ����� = B1.Instance.�з�����;
                        StartCoroutine(�ӳ��˺�(this.����Ŀ��, ����˺� * 0.5f, ���ܹ�������));
                    }
                    if (this.����Ŀ��.GetComponent<A1>().�Ƿ��ǵз� == false)
                    {
                        Ŀ����� = B1.Instance.�ҷ�����;
                        StartCoroutine(�ӳ��˺�(this.����Ŀ��, ����˺� * 0.5f, ���ܹ�������));
                    }
                }
            }

        }
    }
    void ��Ŀ�����Ч��()
    {
       if(�����Ƿ���ҷ�ʹ��==false){
            if (�õз�����������)
            {
                this.����Ŀ��.GetComponent<A1>().����Ч���ı�("��������", Color.red);
                ��Ŀ�꽵�ͷ�����(this.����Ŀ��);
            }
            if (�õз��ж�����)
            {
                ��Ŀ���ж����˶���(this.����Ŀ��, �ж����Ͷ���);
                this.����Ŀ��.GetComponent<A1>().����Ч���ı�("�ж��ӳ�", Color.red);
            }
            if (����ǿ�صз�)
            {
                if (Random.Range(0f, 1f) < ����)
                {
                    this.����Ŀ��.GetComponent<A1>().����Ч���ı�(B1.Instance.ǿ������, new Color(0.5f, 0.5f, 1f));
                    //��Ŀ���ж����˶���(this.����Ŀ��, 4);
                    ��Ŀ�����ǿ��buff(����Ŀ��);
                }
            }
            if (�õз������ܵ��˺�A)
            {
                //Debug.Log("1");
                ��Ŀ����ӳ����˺�A(����Ŀ��, �����˺�������);
                this.����Ŀ��.GetComponent<A1>().�����˺�A��ɫ = this.����Ŀ��.GetComponent<A1>().�ı���ɫ;
                this.����Ŀ��.GetComponent<A1>().�Լ��ܵ��ĳ����˺����� = GetComponent<A1>().����;
                this.����Ŀ��.GetComponent<A1>().����Ч���ı�(B1.Instance.�����˺�A����, this.����Ŀ��.GetComponent<A1>().�����˺�A��ɫ);
            }
            if (�õз������ܵ��˺�B)
            {
                //Debug.Log("2");
                ��Ŀ����ӳ����˺�B(����Ŀ��, �����˺�������);
                this.����Ŀ��.GetComponent<A1>().�����˺�B��ɫ = this.����Ŀ��.GetComponent<A1>().�ı���ɫ;
                this.����Ŀ��.GetComponent<A1>().�Լ��ܵ��ĳ����˺����� = GetComponent<A1>().����;
                this.����Ŀ��.GetComponent<A1>().����Ч���ı�(B1.Instance.�����˺�B����, this.����Ŀ��.GetComponent<A1>().�����˺�B��ɫ);
            }
            if (GetComponent<A6>() != null)
            {
                if (GetComponent<A6>().����ǿ��״̬��)
                    GetComponent<A6>().�����ҷ�ȫ������ֵ();
            }
       }
    }
    IEnumerator ���������˺����()
    {
        B2.Instance.��ǰ����Ŀ��.GetComponent<A1>().��ǰ��ɱ������ += 10;
        GetComponent<A1>().��ǰ��ɱ������ += 20;
        //Debug.Log("1");
        for (int i = 0; i < ��������˺�����; i++)
        {
            yield return new WaitForSeconds(������ɶ���˺������);
            ���������˺�();
        }
        ��Ŀ�����Ч��();
    }
    IEnumerator �����չ��˺����(GameObject Ŀ��, string ��������)
    {
        Ŀ��.GetComponent<A1>().��ǰ��ɱ������ += 5;
        GetComponent<A1>().��ǰ��ɱ������ += 10;
        for (int i = 0; i < �չ�����˺�����; i++)
        {
            yield return new WaitForSeconds(i * �չ���ɶ���˺����);
            float ����˺� = GetComponent<A1>().������ * �չ��˺�����;
            Ŀ��.GetComponent<A1>().��������(Ŀ��, ����˺�, ��������);
            if (Ŀ��������Ч != null)
            {
                GameObject meleeEffect = Instantiate(Ŀ��������Ч, Ŀ��.transform.position, Quaternion.identity);
                Destroy(meleeEffect.gameObject, ������Ч����ʱ��);
            }
        }
    }
    private IEnumerator �����������˺����(GameObject Ŀ��, string ��������, int ����, float ���ʱ��, float ����˺�)
    {
        Ŀ��.GetComponent<A1>().��ǰ��ɱ������ += 10;
        GetComponent<A1>().��ǰ��ɱ������ += 20;
        for (int i = 0; i < ����; i++)
        {
            yield return new WaitForSeconds(���ʱ��);
            if (�������Ե�����Ч)
            {
                Ŀ��.GetComponent<A1>().������������(Ŀ��, ����˺�, ��������);
            }
            if (�������Ƿ��Ŀ�����ȫ����Ч)
            {
                //Debug.Log("1");
                Transform parentTransform = Ŀ��.transform.parent;

                foreach (Transform childTransform in parentTransform)
                {
                    GameObject childObject = childTransform.gameObject;
                    childObject.GetComponent<A1>().������������(childObject, ����˺�, ��������);
                }
            }
            if (�������Ƿ��Ŀ������Ҷ�����Ч)
            {
                Transform parentTransform = Ŀ��.transform.parent;
                Ŀ��.GetComponent<A1>().������������(Ŀ��, ����˺�, ��������);

                if (parentTransform != null)
                {
                    int siblingIndex = this.����Ŀ��.transform.GetSiblingIndex();

                    int aboveIndex = siblingIndex - 1;
                    int belowIndex = siblingIndex + 1;

                    Transform aboveObject = aboveIndex >= 0 ? parentTransform.GetChild(aboveIndex) : null;
                    Transform belowObject = belowIndex < parentTransform.childCount ? parentTransform.GetChild(belowIndex) : null;
                    if (aboveObject != null)
                        aboveObject.GetComponent<A1>().������������(aboveObject.gameObject, ����˺� * ����������Ŀ���ܵ��˺�����, ��������);
                    if (belowObject != null)
                        belowObject.GetComponent<A1>().������������(belowObject.gameObject, ����˺� * ����������Ŀ���ܵ��˺�����, ��������);
                }
            }
            if (Ŀ��������Ч != null)
            {
                GameObject meleeEffect = Instantiate(Ŀ��������Ч, this.����Ŀ��.transform.position, Quaternion.identity);
                Destroy(meleeEffect.gameObject, ������Ч����ʱ��);
            }
        }
        // �ӳ�0.2���������
        //yield return new WaitForSeconds(0.2f);

        ������ = false;
        // �������������Ĳ���...
    }
    public void ���ż�����Ч()
    {
        if (���ܻ�����Ч.Length > 0)
        {
            int ������� = UnityEngine.Random.Range(0, ���ܻ�����Ч.Length);
            AudioClip �����Ч = ���ܻ�����Ч[�������];
            ��Ч������.clip = �����Ч;
            ��Ч������.Play();
        }
    }

    // ����������չ���Ч
    public void �����չ���Ч()
    {
        if (�չ�������Ч.Length > 0)
        {
            int ������� = UnityEngine.Random.Range(0, �չ�������Ч.Length);
            AudioClip �����Ч = �չ�������Ч[�������];
            ��Ч������.clip = �����Ч;
            ��Ч������.Play();
        }
    }
    public void ��������Buffʣ��غ�()
    {
        //Debug.Log("11");
        if (ʣ�౻ǿ�ػغ��� > 0)
            ʣ�౻ǿ�ػغ��� -= 1;
        if (ʣ������˺�A�غ��� > 0 && !GetComponent<A1>().����)
        {
            ʣ������˺�A�غ��� -= 1;
            GetComponent<A1>().�ܵ������˺�(�����ܵ��˺���, GetComponent<A1>().�Լ��ܵ��ĳ����˺�����, GetComponent<A1>().�ܵ��ĳ����˺�������);
            GetComponent<A1>().����Ч���ı�(B1.Instance.�����˺�A����, GetComponent<A1>().�����˺�A��ɫ);
            if (B1.Instance.��ɳ����˺�Aʱ����Ч != null)
            {
                GameObject meleeEffect = Instantiate(B1.Instance.��ɳ����˺�Aʱ����Ч, transform.position, Quaternion.identity);
                Destroy(meleeEffect.gameObject, 0.2f);
            }
        }
        if (ʣ������˺�B�غ��� > 0 && !GetComponent<A1>().����)
        {
            ʣ������˺�B�غ��� -= 1;
            GetComponent<A1>().�ܵ������˺�(�����ܵ��˺���, GetComponent<A1>().�Լ��ܵ��ĳ����˺�����, GetComponent<A1>().�ܵ��ĳ����˺�������);
            GetComponent<A1>().����Ч���ı�(B1.Instance.�����˺�B����, GetComponent<A1>().�����˺�B��ɫ);
            if (B1.Instance.��ɳ����˺�Bʱ����Ч != null)
            {
                GameObject meleeEffect = Instantiate(B1.Instance.��ɳ����˺�Bʱ����Ч, transform.position, Quaternion.identity);
                Destroy(meleeEffect.gameObject, 0.2f);
            }
        }
        if (ʣ�๥���������غ��� > 0)
            ʣ�๥���������غ��� -= 1;
        if (ʣ����������ͻغ��� > 0)
            ʣ����������ͻغ��� -= 1;
        if (GetComponent<A1>().�Ƿ�����ɫ�ٻ��� && GetComponent<A1>().��ɫ�ٻ���ʣ��غ� > 0)
            GetComponent<A1>().��ɫ�ٻ���ʣ��غ� -= 1;

    }
    void ������buffʣ��غ�ˢ��()
    {
        if (���ɵ�ǿ��buffͼ�� != null)
        {
            Image ǿ��buff���� = ���ɵ�ǿ��buffͼ��.GetComponent<Image>();
            var js = ǿ��buff����.transform.Find("Text");
            Text ǿ��ʣ�� = js.GetComponent<Text>();
            ǿ��ʣ��.text = ʣ�౻ǿ�ػغ���.ToString();
        }
        if (���ɵĹ���������buffͼ�� != null)
        {
            Image ����������buff���� = ���ɵĹ���������buffͼ��.GetComponent<Image>();
            var js = ����������buff����.transform.Find("Text");
            Text ����������ʣ�� = js.GetComponent<Text>();
            ����������ʣ��.text = ʣ�๥���������غ���.ToString();

        }
        if (���ɵķ���������buffͼ�� != null)
        {
            Image ����������buff���� = ���ɵķ���������buffͼ��.GetComponent<Image>();
            var js = ����������buff����.transform.Find("Text");
            Text ����������ʣ�� = js.GetComponent<Text>();
            ����������ʣ��.text = ʣ����������ͻغ���.ToString();

        }
        if (���ɵĳ��������˺�buffͼ�� != null)
        {
            Image ��������buff���� = ���ɵĳ��������˺�buffͼ��.GetComponent<Image>();
            var js = ��������buff����.transform.Find("Text");
            Text ��������ʣ�� = js.GetComponent<Text>();
            ��������ʣ��.text = ʣ������˺�A�غ���.ToString();
        }
        if (���ɵĳ��������˺�buffͼ�� != null)
        {
            Image ��������buff���� = ���ɵĳ��������˺�buffͼ��.GetComponent<Image>();
            var js = ��������buff����.transform.Find("Text");
            Text ��������ʣ�� = js.GetComponent<Text>();
            ��������ʣ��.text = ʣ������˺�B�غ���.ToString();
        }
        if (���ɵ���ɫ�ٻ���buffͼ�� != null)
        {
            Image ��ɫ�ٻ���buff���� = ���ɵ���ɫ�ٻ���buffͼ��.GetComponent<Image>();
            var js = ��ɫ�ٻ���buff����.transform.Find("Text");
            Text ��ɫ�ٻ���ʣ�� = js.GetComponent<Text>();
            ��ɫ�ٻ���ʣ��.text = ���ɵ���ɫ�ٻ���.GetComponent<A1>().��ɫ�ٻ���ʣ��غ�.ToString();
            if (���ɵ���ɫ�ٻ���.GetComponent<A1>().��ɫ�ٻ���ʣ��غ� == 0)
            {
                Destroy(���ɵ���ɫ�ٻ���buffͼ��);
                ���ɵ���ɫ�ٻ���.GetComponent<A1>().�ӳ�ɾ��(0.8f);
            }
        }
    }
}
