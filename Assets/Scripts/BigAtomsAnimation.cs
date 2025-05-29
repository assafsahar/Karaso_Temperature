using System.Xml.Serialization;
using UnityEngine;

public class BigAtomsAnimation : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] SpriteRenderer atom1;
    [SerializeField] SpriteRenderer atom2;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
    }

    public void ShowInitialAtoms()
    {
        animator.Play("BigAtoms", 0, 0f);  
        animator.speed = 0f;
        Color color = atom1.color;
        color.a = 1f;
        atom1.color = color;
        atom2.color = color;

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
}
