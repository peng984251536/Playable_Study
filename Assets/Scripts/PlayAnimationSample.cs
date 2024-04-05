using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.Playables;

using UnityEngine.Animations;

[RequireComponent(typeof(Animator))]

public class PlayAnimationSample : MonoBehaviour
{

    public List<AnimationClip>  clipList;
    [Range(0,1)]
    public float weight = 1.0f;
    private PlayableGraph playableGraph;
    private AnimationMixerPlayable mixerPlayable;
    private List<AnimationClipPlayable> playables = new List<AnimationClipPlayable>(4);

    
    void Start()
    {

        CreatePlayableGrap2();
    }

    private void Update()
    {
        
        mixerPlayable.SetInputWeight(0,1.0f-weight);
        mixerPlayable.SetInputWeight(1,weight);
    }

    void OnDisable()
    {

        //销毁该图创建的所有可播放项和 PlayableOutput。

        playableGraph.Destroy();

    }

    #region 处理动画的若干种方法

    private void CreatePlayableGrap1()
    {
        //创建PlayableGraph
        playableGraph = PlayableGraph.Create();
        //
        var playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", GetComponent<Animator>());
        playableGraph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);//设置时间更新
        mixerPlayable = AnimationMixerPlayable.Create(playableGraph, 2);
        playableOutput.SetSourcePlayable(mixerPlayable);//设置播放内容
        
        // 将剪辑包裹在可播放项中
        for (int i = 0; i < clipList.Count; i++)
        {
            var clipPlayable = AnimationClipPlayable.Create(playableGraph, clipList[i]);
            playables.Add(clipPlayable);
            
            // 将可播放项连接到输出
            //playableOutput.SetSourcePlayable(clipPlayable);
        }
        
        //混合输出动作
        playableGraph.Connect(playables[0], 0, mixerPlayable, 0);
        playableGraph.Connect(playables[1], 0, mixerPlayable, 1);
        
        
        

        // 播放该图。
        playableGraph.Play();
    }
    
    private void CreatePlayableGrap2()
    {
        //创建PlayableGraph
        playableGraph = PlayableGraph.Create();
        playableGraph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);//设置时间更新
        //
        mixerPlayable = AnimationMixerPlayable.Create(playableGraph);
        for (int i = 0; i < clipList.Count; i++)
        {
            //创建动画clip
            var clipPlayable = AnimationClipPlayable.Create(playableGraph, clipList[i]);
            playables.Add(clipPlayable);
            
            // 将可播放项连接到输出
            //playableOutput.SetSourcePlayable(clipPlayable);
        }
        mixerPlayable.AddInput(playables[0], 0, 0);
        mixerPlayable.AddInput(playables[1], 0, 1);
        
        
        var playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", GetComponent<Animator>());
        playableOutput.SetSourcePlayable(mixerPlayable);
        

        // 播放该图。
        playableGraph.Play();
    }

    #endregion
    
}

