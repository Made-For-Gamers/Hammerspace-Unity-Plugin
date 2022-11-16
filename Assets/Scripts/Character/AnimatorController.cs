using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public Animator animator;
    private PlayerInputHandler playerInputHandler;
    [SerializeField] AudioClip[] FootstepAudioClips;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
    }

    public void MovementAnimation(float xMove, float yMove)
    {      
        if (!playerInputHandler.run)
        {
            if (yMove > 0.5f)
            {
                yMove = 0.5f;
            }
        }
        animator.SetFloat("xMove", xMove, 0.1f, Time.deltaTime);
        animator.SetFloat("yMove", yMove, 0.1f, Time.deltaTime);
    }

    public void JumpingAnimation(bool jump)
    {
        if (jump)
        {
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }
    }

    public void FallingAnimation(bool fall)
    {
        if (fall)
        {
            animator.SetBool("Fall", true);
        }
        else
        {
            animator.SetBool("Fall", false);
        }
    }

    public void MP_Move(float xMove, float yMove, bool isRun)
    {
        if (!isRun)
        {
            if (yMove > 0.5f)
            {
                yMove = 0.5f;
            }
        }
        animator.SetFloat("xMove", xMove, 0.1f, Time.deltaTime);
        animator.SetFloat("yMove", yMove, 0.1f, Time.deltaTime);
    }

    public void MP_Jump(bool isJump)
    {
        if (isJump)
        {
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }
    }

    public void MP_Fall(bool isFalling)
    {
        if (isFalling)
        {
            animator.SetBool("Fall", true);
        }
        else
        {
            animator.SetBool("Fall", false);
        }
    }


    //Animation Events
    private void OnFootstep(AnimationEvent animationEvent) //walking & running animation event
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            if (FootstepAudioClips.Length > 0)
            {
                var index = Random.Range(1, FootstepAudioClips.Length);
                AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.position, 1f);
            }
        }
    }

    private void OnLand(AnimationEvent animationEvent) //jump animation event
    {
        AudioSource.PlayClipAtPoint(FootstepAudioClips[0], transform.position, 1f);
    }
}
