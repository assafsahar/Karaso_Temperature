using System.Xml.Serialization;
using UnityEngine;

public class BigAtomsAnimation : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSystem;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
    }
    void Start()
    {
        
    }
    public void PlayAnimation()
    {
        animator.Play("BigAtoms");
    }
    public void PlayEffect()
    {
        particleSystem.Play();
    }
}
