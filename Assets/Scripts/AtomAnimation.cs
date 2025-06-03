using DG.Tweening;
using UnityEngine;

public class AtomAnimation : MonoBehaviour
{
    [SerializeField] float minDuration = 1.5f;
    [SerializeField] float maxDuration = 3.5f;

    BoxCollider2D areaCollider;
    bool isFloating = false;

    public void StartFloating()
    {
        if (!isFloating)
        {
            isFloating = true;
            if (areaCollider == null)
            {
                GameObject areaObj = GameObject.Find("AnimationArea");
                if (areaObj != null)
                    areaCollider = areaObj.GetComponent<BoxCollider2D>();
                else
                {
                    Debug.LogError("No GameObject named 'AnimationArea' with a BoxCollider2D found in the scene.");
                    return;
                }
            }
            MoveToRandomPosition();
        }
    }

    public void StopFloating()
    {
        isFloating = false;
        DOTween.Kill(transform); // Stop any running tweens on this atom
    }

    void MoveToRandomPosition()
    {
        if (!isFloating || areaCollider == null)
            return;

        Vector2 areaCenter = (Vector2)areaCollider.transform.position + areaCollider.offset;
        Vector2 areaSize = areaCollider.size * 0.5f;

        float x = Random.Range(areaCenter.x - areaSize.x, areaCenter.x + areaSize.x);
        float y = Random.Range(areaCenter.y - areaSize.y, areaCenter.y + areaSize.y);
        float z = transform.position.z;

        Vector3 target = new Vector3(x, y, z);
        float duration = Random.Range(minDuration, maxDuration);

        transform.DOMove(target, duration)
            .SetEase(Ease.Linear) // Linear movement
            .OnComplete(MoveToRandomPosition);
    }
}
