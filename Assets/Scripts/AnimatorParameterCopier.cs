using UnityEngine;

public class AnimatorParameterCopier : MonoBehaviour
{
    public Animator modelAnimator;    
    public Animator[] peerAnimators;  

    void Update()
    {
        if (modelAnimator != null && peerAnimators.Length > 0)
        {
            foreach (Animator playerAnimator in peerAnimators)
            {
                if (playerAnimator != null)
                {
                    CopyAnimatorParameters(modelAnimator, playerAnimator);
                }
            }
        }
    }

    void CopyAnimatorParameters(Animator sourceAnimator, Animator targetAnimator)
    {
        for (int i = 0; i < sourceAnimator.parameterCount; i++)
        {
            AnimatorControllerParameter parameter = sourceAnimator.parameters[i];

            switch (parameter.type)
            {
                case AnimatorControllerParameterType.Float:
                    targetAnimator.SetFloat(parameter.name, sourceAnimator.GetFloat(parameter.name));
                    break;
                case AnimatorControllerParameterType.Int:
                    targetAnimator.SetInteger(parameter.name, sourceAnimator.GetInteger(parameter.name));
                    break;
                case AnimatorControllerParameterType.Bool:
                    targetAnimator.SetBool(parameter.name, sourceAnimator.GetBool(parameter.name));
                    break;
                case AnimatorControllerParameterType.Trigger:
                    if (sourceAnimator.GetBool(parameter.name))
                    {
                        targetAnimator.SetTrigger(parameter.name);
                    }
                    break;
            }
        }
    }
}
