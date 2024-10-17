using UnityEngine;

public class SortingOrderManager : MonoBehaviour
{
    public Transform playerFootPoint;
    public Transform bossFootPoint;

    private SpriteRenderer playerRenderer;

    void Start()
    {
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        UpdateSortingOrder();
    }

    void UpdateSortingOrder()
    {
        if (playerFootPoint.position.y > bossFootPoint.position.y)
        {
            playerRenderer.sortingOrder = -1; 
        }
        else
        {
            playerRenderer.sortingOrder = 1;  
        }
    }
}
