using System;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using Kvant;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Swarmmanage 管理脚本
/// </summary>

/// <summary>
/// 隐藏脚本文件
/// </summary>
[HideMonoScript]
public class SwarmManager : MonoBehaviour
{

    // Start is called before the first frame update
    #region  基础属性
    [HideInEditorMode]
    public Swarm swarm;
    GameObject targetObject;

    [Title("基础属性",titleAlignment:TitleAlignments.Centered)]
    [HideLabel]
    [LabelText("数量")]
    public int lineCount;

    [HideLabel]
    [LabelText("过去的长度")]
    public int historyLength;

    [HideLabel]
    [LabelText("节流阀")]
    [ProgressBar(0,1,Height =15)]
    public float throttle;

    [HideLabel]
    [LabelText("线条向量")]
    public Vector3 flowVector;

    #endregion

    #region  吸引力属性
    
    [Title("追踪物体",titleAlignment:TitleAlignments.Centered)]
    [ReadOnly]
    public Transform target;

    [Title("吸引力属性",titleAlignment:TitleAlignments.Centered)]
    [HideLabel]
    [LabelText("吸引力半径")]
    [ProgressBar(0,10,Height =15)]
    public float attractorRadius;

    [HideLabel]
    [LabelText("吸引力的距离,数值越高吸引力越强")]
    [ProgressBar(0,10,Height =15)]
    public float forcePerDistance;

    [HideLabel]
    [LabelText("吸引力的随机性")]
    [ProgressBar(0,1,Height =15)]
    public float forceRandom;

    [HideLabel]
    [LabelText("阻力")]
    [ProgressBar(0,6,Height =15)]
    public float drag;

    #endregion

    #region 湍流噪声参数
    [Title("湍流噪声参数",titleAlignment:TitleAlignments.Centered)]
    [HideLabel]
    [LabelText("Noise振幅")]
    [ProgressBar(0,12,Height =15)]
    public float amplitude;

    [HideLabel]
    [LabelText("Noise频率")]
    [ProgressBar(0,1,Height =15)]
    public float frequency;

    [HideLabel]
    [LabelText("噪点的散布")]
    [ProgressBar(0,1,Height =15)]
    public float spread;

    [HideLabel]
    [LabelText("噪点的运动")]
    [ProgressBar(0,4,Height =15)]
    public float motion;

    [HideLabel]
    [LabelText("漩涡振幅")]
    [ProgressBar(0,1,Height =15)]
    public float swirlAmplitude;

    [HideLabel]
    [LabelText("漩涡频率")]
    [ProgressBar(0,1,Height=15)]
    public float swirlFrequency;




    


    #endregion

    #region  渲染设置

    [Title("渲染设置",titleAlignment:TitleAlignments.Centered)]
    [HideLabel]
    [LabelText("线的宽度")]
    [ProgressBar(0,1,Height =15)]
    public float lineWidth;

    [HideLabel]
    [LabelText("线宽的随机性")]
    [ProgressBar(0,1,Height=15)]
    public float lineWidthRandomness;


    [HideLabel]
    [LabelText("颜色模式")]
    [EnumToggleButtons]
    public ColorMode colorMode;

    [HideLabel]
    [LabelText("Color_1")]
    [ColorPalette]
    public Color color_1;

    [HideLabel]
    [LabelText("Color_2")]
    [ColorPalette]
    public Color color_2;

    [HideLabel]
    [LabelText("金属")]
    [ProgressBar(0,1,Height =15)]
    public float metallic;

    [HideLabel]
    [LabelText("光滑")]
    [ProgressBar(0,1,Height =15)]
    public float smoothness;

    [HideLabel]
    [LabelText("阴影模式")]
    [EnumToggleButtons]
    public ShadowCastingMode castingMode;

    [HideLabel,LabelText("是否接受阴影")]
    public bool receiveShadows;

    [Title("混合设置",titleAlignment:TitleAlignments.Centered)]
    [HideLabel]
    [LabelText("修复时间步骤")]
    public bool fixTimeStep;

    [HideLabel,LabelText("每秒的步骤")]
    [ShowIf("fixTimeStep")]
    [ProgressBar(0,100,Height =15)]
    public float stepsPerSecond;


    [HideLabel,LabelText("随机速度")]
    [ProgressBar(0,1000,Height =15)]
    public int randomSeed;



    [Title("配置文件",titleAlignment:TitleAlignments.Centered)]
    [HideLabel,LabelText("配置文件名称(无须后缀.asset)")]
    [InlineButton("SaveSwarmScriptableObjectData","保存")]
    public string assetName;


    [HideLabel,LabelText("加载配置文件")]
    [InlineButton("LoadSwarmScriptableObjectData","加载")]
    [AssetSelector]
    public SwarmScriptableObject loadScriptableObject;

    





    #endregion
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    void Update()
    {

    }


    #if UNITY_EDITOR

    [Button("实例化追踪物体 并添加到Swarm脚本")]
    public void InstanceTargetObjectAddSwarm()
    {
        if(GameObject.Find("TargetObject")==null)
        {
            targetObject=new GameObject("TargetObject");
        }
        else
        {
            targetObject=GameObject.Find("TargetObject");
            LocateTtheHierarchyObject(targetObject);
        }
        targetObject.transform.position=Vector3.zero;
        swarm.attractor=targetObject.transform;
        target=targetObject.transform;
    }


    /// <summary>
    /// 定位Hierarchy面板物体
    /// </summary>
    /// <param name="obj"></param>
	public void LocateTtheHierarchyObject (GameObject obj) 
	{
 
		GameObject go = GameObject.Find(obj.name);
		EditorGUIUtility.PingObject(go);
		Selection.activeGameObject =  go;	
	}


    
    /// <summary>
    /// 定位Project面板物体
    /// </summary>
    /// <param name="obj"></param>
	public void LocateTtheProjectObject (SwarmScriptableObject obj,string assetPath) 
	{
		EditorGUIUtility.PingObject(obj);
		Selection.activeObject  = AssetDatabase.LoadAssetAtPath<SwarmScriptableObject>(assetPath);
	}


    /// <summary>
    /// 改变数值时调用
    /// </summary>
    void OnValidate()
    {
        swarm._lineCount=lineCount;  
        swarm._historyLength=historyLength;
        swarm.throttle=throttle;
        swarm.flow=flowVector;
        swarm.attractorRadius=attractorRadius;
        swarm.forcePerDistance=forcePerDistance;
        swarm.forceRandomness=forceRandom;
        swarm.drag=drag;
        swarm.noiseAmplitude=amplitude;
        swarm.noiseFrequency=frequency;
        swarm.noiseSpread=spread;
        swarm.noiseMotion=motion;
        swarm.swirlAmplitude=swirlAmplitude;
        swarm.swirlFrequency=swirlFrequency;
        swarm.lineWidth=lineWidth;
        swarm.lineWidthRandomness=lineWidthRandomness;
        swarm.colorMode=colorMode;
        swarm.color=color_1;
        swarm.color2=color_2;
        swarm.metallic=metallic;
        swarm.smoothness=smoothness;
        swarm.castShadows=castingMode;
        swarm.receiveShadows=receiveShadows;
        swarm._fixTimeStep=fixTimeStep;
        swarm._stepsPerSecond=stepsPerSecond;
        swarm._randomSeed=randomSeed;
    }

    /// <summary>
    /// 脚本挂载时调用
    /// </summary>
    private void Reset()
    {
        Debug.Log("脚本开始挂载");
        swarm=GameObject.Find("Swarm").GetComponent<Swarm>();
    }




    public void SaveSwarmScriptableObjectData()
    {
        if(assetName=="")
        {
            Debug.LogError("配置文件名称不能为空");
        }
        else
        {
            var level = ScriptableObject.CreateInstance<SwarmScriptableObject>(); 
            AssetDatabase.CreateAsset(level, @"Assets/"+assetName + ".asset");
            AssetDatabase.SaveAssets(); //存储资源
            AssetDatabase.Refresh(); //刷新
            
            level.lineCount=lineCount;  
            level.historyLength=historyLength;
            level.throttle=throttle;
            level.flowVector=flowVector;
            level.attractorRadius=attractorRadius;
            level.forcePerDistance=forcePerDistance;
            level.forceRandom=forceRandom;
            level.drag=drag;
            level.amplitude=amplitude;
            level.frequency=frequency;
            level.spread=spread;
            level.motion=motion;
            level.swirlAmplitude=swirlAmplitude;
            level.swirlFrequency=swirlFrequency;
            level.lineWidth=lineWidth;
            level.lineWidthRandomness=lineWidthRandomness;
            level.colorMode=colorMode;
            level.color_1=color_1;
            level.color_2=color_2;
            level.metallic=metallic;
            level.smoothness=smoothness;
            level.castingMode=castingMode;
            level.receiveShadows=receiveShadows;
            level.fixTimeStep=fixTimeStep;
            level.stepsPerSecond=stepsPerSecond;
            level.randomSeed=randomSeed;
            LocateTtheProjectObject(level,"Assets/"+assetName+".asset");
        }
    }
    

    public void LoadSwarmScriptableObjectData()
    {
        if(loadScriptableObject==null)
        {
            Debug.LogError("配置文件不能为空");
        }
        else
        {
            lineCount=loadScriptableObject.lineCount;  
            historyLength=loadScriptableObject.historyLength;
            throttle=loadScriptableObject.throttle;
            flowVector=loadScriptableObject.flowVector;
            attractorRadius=loadScriptableObject.attractorRadius;
            forcePerDistance=loadScriptableObject.forcePerDistance;
            forceRandom=loadScriptableObject.forceRandom;
            drag=loadScriptableObject.drag;
            amplitude=loadScriptableObject.amplitude;
            frequency=loadScriptableObject.frequency;
            spread=loadScriptableObject.spread;
            motion=loadScriptableObject.motion;
            swirlAmplitude=loadScriptableObject.swirlAmplitude;
            swirlFrequency=loadScriptableObject.swirlFrequency;
            lineWidth=loadScriptableObject.lineWidth;
            lineWidthRandomness=loadScriptableObject.lineWidthRandomness;
            colorMode=loadScriptableObject.colorMode;
            color_1=loadScriptableObject.color_1;
            color_2=loadScriptableObject.color_2;
            metallic=loadScriptableObject.metallic;
            smoothness=loadScriptableObject.smoothness;
            castingMode=loadScriptableObject.castingMode;
            receiveShadows=loadScriptableObject.receiveShadows;
            fixTimeStep=loadScriptableObject.fixTimeStep;
            stepsPerSecond=loadScriptableObject.stepsPerSecond;
            randomSeed=loadScriptableObject.randomSeed;
            Debug.Log("加载"+loadScriptableObject.name+".asset"+"完成");
        }
    }


    #endif


}
