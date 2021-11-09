using System.Collections;
using System.Collections.Generic;
using Kvant;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;

[HideMonoScript]
[CreateAssetMenu(menuName ="Swarm/创建配置文件",fileName ="SwarmConfigurationFiles")]
public class SwarmScriptableObject:ScriptableObject
{
    // Start is called before the first frame update
    #region  基础属性
    
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
    #endregion
}