using UnityEngine;

public class EndOfAttack : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<PlayerController>().ActionAfterAttack();
    }
}
