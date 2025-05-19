using UnityEngine;

public class ButtonsBehavior : MonoBehaviour
{
    [SerializeField] GameObject circle;
    Animator animator;

    private void Awake()
    {
        //animator = GameObject.Find("RayRoot").GetComponent<Animator>();

    }
    public void StartAnim()
    {
        Instantiate(circle);
    }

}
