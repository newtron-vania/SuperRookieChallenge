using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleCharacterFactory
{
    private readonly Define.ECharacterType _id;

    public SimpleCharacterFactory(Define.ECharacterType id)
    {
        _id = id;
    }
    
    // 캐릭터 생성
    public BaseCharacter Create(string unitName)
    {
        var character = Managers.Resource.Instantiate($"Character/{unitName}", new Vector3(999f,999f,999f)).GetComponent<BaseCharacter>();
        if (character == null)
        {
            Debug.Log($"Cannot Find Character {unitName}");
            return null;
        }

        Init(character, _id);
        return character;
    }

    // 생성 캐릭터의 초기화
    private void Init(BaseCharacter character, Define.ECharacterType id)
    {
        character._id = id;
        BTMachine BTM = null;
        switch (id)
        {
            case Define.ECharacterType.ECT_Player:
                BTM = CreateBTMPlayer(character);
                //파괴되지 않도록 설정
                break;
            case Define.ECharacterType.ECT_Enemy:
                BTM = CreateBTMEnemy(character);
                break;
            case Define.ECharacterType.ECT_Boss:
                BTM = CreateBTMEnemy(character);
                //보스 효과 부여
                character.SetBuff(new BossEffect());
                break;
        }

        character.SetBehaviourTree(BTM);
    }

    private BTMachine CreateBTMPlayer(BaseCharacter character)
    {
        var rootNode = new SelectorNode(new List<IBTNode>
        {
            new SequenceNode(new List<IBTNode>
            {
                // Check Death
                new ConditionNode(() => character.IsDead()),
                new ConditionNode(() => !character.IsAnimationPlaying("die")),
                new ActionNode(() => character.Dead())
            }),
            new SelectorNode(new List<IBTNode>
            {
                // Check Death
                new ConditionNode(() => character.IsDead())
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
                new ConditionNode(() => character.HasMoveTarget()),
                new ActionNode(() => character.Move())
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

    private BTMachine CreateBTMEnemy(BaseCharacter character)
    {
        var rootNode = new SelectorNode(new List<IBTNode>
        {
            new SequenceNode(new List<IBTNode>
            {
                // Check Death
                new ConditionNode(() => character.IsDead()),
                new ConditionNode(() => !character.IsAnimationPlaying("die")),
                new ActionNode(() => character.Dead())
            }),
            new SelectorNode(new List<IBTNode>
            {
                // Check Death
                new ConditionNode(() => character.IsDead())
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
                new ConditionNode(() => character.HasMoveTarget()),
                new ActionNode(() => character.Move())
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