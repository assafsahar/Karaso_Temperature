using System.Collections;
using System.Xml.Serialization;
using UnityEngine;

public class BigAtomsAnimation : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] SpriteRenderer upperAtom1;
    [SerializeField] SpriteRenderer upperAtom2;
    [SerializeField] SpriteRenderer lowerAtom1;
    [SerializeField] SpriteRenderer lowerAtom2;
    [SerializeField] float fistPauseDuration = 10f; // Duration of the first pause in seconds
    [SerializeField] float secondPauseDuration = 5f; // Duration of the second pause in seconds

    Animator animator;
    float animationLength;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (var clip in clips)
        {
            if (clip.name == "BigAtoms")
            {
                animationLength = clip.length;
                break;
            }
        }
    }

    public void ShowInitialAtoms()
    {
        animator.Play("BigAtoms", 0, 0f);  
        animator.speed = 0f;
        upperAtom1.gameObject.SetActive(true);
        upperAtom2.gameObject.SetActive(true);
        lowerAtom1.gameObject.SetActive(true);
        lowerAtom2.gameObject.SetActive(true);

    }
    public void PlayAnimation()
    {
        animator.Play("BigAtoms", 0, 0f);
        animator.speed = 1f;
       
    }
    public void PlayEffect()
    {
        particleSystem.Play();
    }
    public void PlaySequence()
    {
        StartCoroutine(PlaySequenceCoroutine());
    }

    IEnumerator PlaySequenceCoroutine()
    {
        // First play
        PlayAnimation();
        yield return new WaitForSeconds(animationLength);

        // Pause 10 seconds
        animator.speed = 0f;
        yield return new WaitForSeconds(fistPauseDuration);

        // Second play
        PlayAnimation();
        yield return new WaitForSeconds(animationLength);

        // Pause 5 seconds
        animator.speed = 0f;
        yield return new WaitForSeconds(secondPauseDuration);

        // Back to initial state
        SetAtomsOff();
    }
    private void SetAtomsOff()
    {
        upperAtom1.gameObject.SetActive(false);
        upperAtom2.gameObject.SetActive(false);
        lowerAtom1.gameObject.SetActive(false);
        lowerAtom2.gameObject.SetActive(false);
    }
}
