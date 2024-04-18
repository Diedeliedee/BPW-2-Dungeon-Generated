using UnityEngine;
using Joeri.Tools.AI.BehaviorTree;

public class IsAnimationPlaying : LeafNode
{
    private readonly string m_animation = null;

    public IsAnimationPlaying(string _animationName)
    {
        m_animation = _animationName;
    }

    public override State OnUpdate()
    {
        if (board.Get<Animator>().GetCurrentAnimatorStateInfo(0).IsName(m_animation)) return State.Succes;
        return State.Failure;
    }
}
