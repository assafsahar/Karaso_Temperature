using DG.Tweening;
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
    [SerializeField] SunEnergyStage2 sunEnergyStage2;


    Animator animator;
    float animationLength;
    private Coroutine sequenceCoroutine;

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
        //StopAllSequences();
        sequenceCoroutine = StartCoroutine(PlaySequenceCoroutine());
    }

    public void StopAllSequences()
    {
        if (sequenceCoroutine != null)
        {
            StopCoroutine(sequenceCoroutine);
            sequenceCoroutine = null;
        }
        CancelInvoke();
        DOTween.Kill(upperAtom1.transform);
        DOTween.Kill(upperAtom2.transform);
        DOTween.Kill(lowerAtom1.transform);
        DOTween.Kill(lowerAtom2.transform);
        // Hide all atoms
        if (upperAtom1 != null) upperAtom1.gameObject.SetActive(false);
        if (upperAtom2 != null) upperAtom2.gameObject.SetActive(false);
        if (lowerAtom1 != null) lowerAtom1.gameObject.SetActive(false);
        if (lowerAtom2 != null) lowerAtom2.gameObject.SetActive(false);
        SetAtomsOff();
        if (particleSystem != null) particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
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

        // Reset the whole application
        if (sunEnergyStage2 != null)
            sunEnergyStage2.ResetStage();
    }
    private void SetAtomsOff()
    {
        upperAtom1.gameObject.SetActive(false);
        upperAtom2.gameObject.SetActive(false);
        lowerAtom1.gameObject.SetActive(false);
        lowerAtom2.gameObject.SetActive(false);
    }
}
