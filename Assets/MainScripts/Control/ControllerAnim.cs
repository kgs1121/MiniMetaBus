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
        
        // Ű�� �Ķ���� �̸� ����
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
        // ��� �Ķ���͸� false�� �ʱ�ȭ
        foreach (var param in keyToParameterMap.Values)
        {
            // �ִϸ����Ϳ� �ش� �Ķ���Ͱ� �ִ� ��츸 false�� ����
            if (animator.HasParameter(param))
            {
                animator.SetBool(param, false);
            }
        }

        // �� Ű�� �ش��ϴ� �Ķ���͸� true�� ����
        foreach (var entry in keyToParameterMap)
        {
            if (Input.GetKey(entry.Key)) // �ش� Ű�� ������ ��
            {
                // �ִϸ����Ϳ� �ش� �Ķ���Ͱ� �ִ� ��츸 true�� ����
                if (animator.HasParameter(entry.Value))
                {
                    animator.SetBool(entry.Value, true);
                }
            }
        }
    }
}

// �ִϸ����� �Ķ������ �̸��� Ȯ���ϴ� Ȯ�� �޼���
public static class AnimatorExtensions
{
    public static bool HasParameter(this Animator animator, string parameterName)
    {
        // �ִϸ������� ��� �Ķ���� �̸��� ��������
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
    