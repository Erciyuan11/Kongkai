using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>    // ���͵���,����������룬����Ҫ���볡��
{             //���������������Ҫ���Լ���ɵ�����ֻ��Ҫ����ļ̳и�Ϊ public class WuPinGuanLi : Singleton<WuPinGuanLi>
    private static T instance;

    public static T Instance
    {
        get { return instance; }
    }
    protected virtual void Awake()//ֻ����������̳в���д
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = (T)this;
    }
    public static bool IsInitialzed//ȷ����ǰ�����Ƿ��Ѿ�����
    {
        get { return instance != null; }
    }
    protected virtual void OnDestroy()//��ǰ��������٣���ô����Ϊ��
    {
        if(instance == this)
        {
            instance=null;
        }
    }

}
