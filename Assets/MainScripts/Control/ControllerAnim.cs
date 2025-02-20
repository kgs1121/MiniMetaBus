using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAnim : MonoBehaviour
{
    protected Animator animator;

    private Dictionary<KeyCode, string> keyToParameterMap;


    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        
        // 키와 파라미터 이름 매핑
        keyToParameterMap = new Dictionary<KeyCode, string>()
        {
            { KeyCode.W, "isUp" },
            { KeyCode.S, "isDown" },
            { KeyCode.A, "isLeft" },
            { KeyCode.D, "isRight" },
            { KeyCode.E, "isE" }
        };
    }

    private void Update()
    {
        ControllerAnimation();
    }
    
    private void ControllerAnimation()
    {
        // 모든 파라미터를 false로 초기화
        foreach (var param in keyToParameterMap.Values)
        {
            // 애니메이터에 해당 파라미터가 있는 경우만 false로 설정
            if (animator.HasParameter(param))
            {
                animator.SetBool(param, false);
            }
        }

        // 각 키에 해당하는 파라미터를 true로 설정
        foreach (var entry in keyToParameterMap)
        {
            if (Input.GetKey(entry.Key)) // 해당 키가 눌렸을 때
            {
                // 애니메이터에 해당 파라미터가 있는 경우만 true로 설정
                if (animator.HasParameter(entry.Value))
                {
                    animator.SetBool(entry.Value, true);
                }
            }
        }
    }
}

// 애니메이터 파라미터의 이름을 확인하는 확장 메서드
public static class AnimatorExtensions
{
    public static bool HasParameter(this Animator animator, string parameterName)
    {
        // 애니메이터의 모든 파라미터 이름을 가져오기
        foreach (var param in animator.parameters)
        {
            if (param.name == parameterName)
            {
                return true;
            }
        }
        return false;
    }
}
    