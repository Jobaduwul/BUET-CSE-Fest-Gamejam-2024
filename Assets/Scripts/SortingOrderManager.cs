using UnityEngine;

public class SortingOrderManager : MonoBehaviour
{
    [SerializeField] private Transform playerFootPoint;
    [SerializeField] private Transform bossFootPoint;

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
        if (playerFootPoint != null && bossFootPoint != null)
        {
            if (playerFootPoint.position.y > bossFootPoint.position.y)
            {
                playerRenderer.sortingOrder = 0;
            }
            else
            {
                playerRenderer.sortingOrder = 2;
            }
        }
        else
        {
            //Debug.LogWarning("PlayerFootPoint or BossFootPoint is not assigned!");
        }
    }
}
