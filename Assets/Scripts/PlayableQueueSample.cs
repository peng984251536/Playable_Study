using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

public class PlayableQueue : PlayableBehaviour
{
    private AnimationMixerPlayable mixer;

    public void Init(PlayableGraph grap, Playable owner, AnimationClip[] clips)
    {
        owner.SetInputCount(1);

        mixer = AnimationMixerPlayable.Create(grap);
        for (int i = 0; i < clips.Length; i++)
        {
            mixer.AddInput(A)
        }
    }
}

public class PlayableQueueSample: MonoBehaviour
{
        
}