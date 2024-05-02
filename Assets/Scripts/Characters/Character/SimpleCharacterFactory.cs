using System;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCharacterFactory
{
    private Define.ECharacterType _id;

    public SimpleCharacterFactory(Define.ECharacterType id)
    {
        _id = id;
    }

    public BaseCharacter Create(string unitName)
    {
        BaseCharacter character = Managers.Resource.Instantiate($"Character/{unitName}").
            GetComponent<BaseCharacter>();
        if (character == null)
        {
            Debug.Log($"Cannot Find Character {unitName}");
        }
        
        Init(character, _id);
        return character;
    }
    
    
    private void Init(BaseCharacter character, Define.ECharacterType id)
    {
        character._id = id;
        BTMachine BTM = null;
        switch (id)
        {
            case Define.ECharacterType.ECT_Player:
                BTM = CreateBTMPlayer(character);
                break;
            case Define.ECharacterType.ECT_Enemy:
                BTM = CreateBTMEnemy(character);
                break;
            case Define.ECharacterType.ECT_Boss:
                BTM = CreateBTMEnemy(character);
                //보스 효과 부여
                break;
        }
    }

    private BTMachine CreateBTMPlayer(BaseCharacter character)
    {
        var rootNode = new SelectorNode(new List<IBTNode> {
            new SequenceNode(new List<IBTNode> { // Check Death
                new ConditionNode(() => character.IsDead()),
                new ActionNode(() => character.Dead())
            }),
            new SequenceNode(new List<IBTNode> { // Handle Attack
                new ConditionNode(() => !character.IsAnimationPlaying("attack")),
                new ConditionNode(() => character.IsAttackRange()),
                new ConditionNode(() => !character.IsAttackCooldown()),
                new ActionNode(() => character.Attack())
            }),
            new SequenceNode(new List<IBTNode> { // Handle Skills
                new ConditionNode(() => !character.IsAnimationPlaying("attack")),
                new ConditionNode(() => character.IsSkillRange()),
                new ConditionNode(() => !character.IsSkillCooldown()),
                new ActionNode(() => character.UseSkill())
            }),
            new SequenceNode(new List<IBTNode> { // Handle Movement
                new ConditionNode(() => !character.IsAnimationPlaying("walk"))
            }),
            new SequenceNode(new List<IBTNode>
            {
                new ConditionNode(() => !character.IsAnimationPlaying("idle")),
                new ActionNode(() => character.Idle())
            })
        });

        return new BTMachine(rootNode);
    }

    private BTMachine CreateBTMEnemy(BaseCharacter character)
    {
        // 적 행동 트리 생성 로직
        List<IBTNode> nodes = new List<IBTNode>()
        {
            //new ActionNode(() => { /* 공격 타이밍 체크 로직 */ }),
            // 추가 노드 구현
        };
        SelectorNode rootNode = new SelectorNode(nodes);
        return new BTMachine(rootNode);
    }
    
}