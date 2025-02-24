using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class CharactorBasic : MonoBehaviour
{
    public string charactername;
    public float maxhp;
    public float currenthp;
    public float speed;
    public float maxmagic;//战技点数
    public float currentmagic;
    public float maxultskill;//大招
    public float currentskill;
    public float maxshield;
    public float currentshield;
    public float attack;

    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {

    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
    }
}
