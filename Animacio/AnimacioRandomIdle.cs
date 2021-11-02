using UnityEngine;

public class AnimacioRandomIdle : StateMachineBehaviour
{
    public string nom;
    public Vector2Int rang;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.SetInteger(nom, Random.Range(rang.x, rang.y));
    }
}
