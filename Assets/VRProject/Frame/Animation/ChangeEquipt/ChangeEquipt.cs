using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//蒙皮动画换装
public class ChangeEquipt : MonoBehaviour {

    public Transform idPlayer;//要换装的个体
    public Transform idPartPlayer;//要换的部件


    public void ChangeEquipts(Transform boneobj, Transform rootobj)
    {
        SkinnedMeshRenderer[] skinrenders = boneobj.GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (SkinnedMeshRenderer item in skinrenders)
        {

        }
    }
    /// <summary>
    /// 重新创建一个SkinnedMeshRenderer 然后找骨骼   替换
    /// </summary>
    /// <param name="thisRender">This render.</param>
    /// <param name="rootobj">Rootobj.</param>
    private void ProcessMeshRender(SkinnedMeshRenderer thisRender, Transform rootobj)
    {
        GameObject newwobj = new GameObject(thisRender.gameObject.name);
        newwobj.transform.parent = rootobj.transform;//新物体放在根骨骼下面
        //创建新的并进行替换
        SkinnedMeshRenderer newRender = newwobj.AddComponent<SkinnedMeshRenderer>();
        //替换bones
        Transform[] mybones = new Transform[thisRender.bones.Length ];
        for (int i = 0; i < thisRender .bones .Length ; i++)
        {
            mybones[i] = FindChildByName(thisRender .bones[i].name ,rootobj );
        }
        newRender.rootBone = rootobj;
        newRender.bones = mybones;
        newRender.sharedMesh = thisRender.sharedMesh;
        newRender.materials = thisRender.materials;
    }

    private Transform FindChildByName(string thisName,Transform thisobj)
    {

        Transform resultObj = null;
        if (thisobj .name ==thisName )
        {
            return thisobj.transform;
        }
        for (int i = 0; i < thisobj .childCount ; i++)
        {
            resultObj = FindChildByName(thisName ,thisobj .GetChild(i));
            if (resultObj !=null )
            {
                return resultObj;
            }
        }
        return resultObj;
    }
        // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
