using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>    // 范型单例,导入这个代码，不需要放入场景
{             //导入后，其他代码想要把自己变成单例就只需要把类的继承改为 public class WuPinGuanLi : Singleton<WuPinGuanLi>
    private static T instance;

    public static T Instance
    {
        get { return instance; }
    }
    protected virtual void Awake()//只运行在子类继承并改写
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = (T)this;
    }
    public static bool IsInitialzed//确定当前单例是否已经生成
    {
        get { return instance != null; }
    }
    protected virtual void OnDestroy()//当前如果被销毁，那么设置为空
    {
        if(instance == this)
        {
            instance=null;
        }
    }

}
