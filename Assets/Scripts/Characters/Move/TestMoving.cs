using System.Collections.Generic;
using UnityEngine;

public class TestMoving : MonoBehaviour
{
    public Transform _target;

    public float speed = 5f; // 이동 속도

    private BaseCharacter _character;
    private AbstractPathFind _pathFind;
    private List<Vector3> path; // 이동할 경로

    private void Awake()
    {
        if (!_character) _character = GetComponent<BaseCharacter>();
        _character.SetBehaviourTree(CreateBTMPlayer(_character));
    }

    private BTMachine CreateBTMPlayer(BaseCharacter character)
    {
        var rootNode = new SelectorNode(new List<IBTNode>
        {
            new SequenceNode(new List<IBTNode>
            {
                // Check Death
                new ConditionNode(() => character.IsDead()),
                new ActionNode(() => character.Dead())
            }),
            new SequenceNode(new List<IBTNode>
            {
                // Handle Attack
                new ConditionNode(() => !character.IsAnimationPlaying("attack")),
                new ConditionNode(() => character.IsAttackRange()),
                new ConditionNode(() => !character.IsAttackCooldown()),
                new ActionNode(() => character.Attack())
            }),
            new SequenceNode(new List<IBTNode>
            {
                // Handle Skills
                new ConditionNode(() => !character.IsAnimationPlaying("attack")),
                new ConditionNode(() => character.IsSkillRange()),
                new ConditionNode(() => !character.IsSkillCooldown()),
                new ActionNode(() => character.UseSkill())
            }),
            new SequenceNode(new List<IBTNode>
            {
                // Handle Movement
                new ConditionNode(() => character.IsOnlyWalkOrIdleAnimationPlaying()),
                new ConditionNode(() => !character.IsAttackRange()),
                new ConditionNode(() => character.HasMoveTarget())
            }),
            new SequenceNode(new List<IBTNode>
            {
                new ConditionNode(() => character.IsOnlyWalkOrIdleAnimationPlaying()),
                new ConditionNode(() => !character.IsAnimationPlaying("idle")),
                new ActionNode(() => character.Idle())
            })
        });

        return new BTMachine(rootNode);
    }
}