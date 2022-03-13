using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private FeedBackColor feedBackColors;

    private float distance => Vector3.Distance(this.transform.position, DotSpawner.Instance.transform.position);


    public void CheckPosition()
    {
        if (distance > DotSpawner.radius)
        {
            EventSystem.SpawnedOutside();
            spriteRenderer.color = feedBackColors.outside;
        }
        else
        {
            EventSystem.SpawnedInside();
            spriteRenderer.color = feedBackColors.inside;
        }
    }
}

[System.Serializable]
public struct FeedBackColor
{
    public Color inside;
    public Color outside;
}