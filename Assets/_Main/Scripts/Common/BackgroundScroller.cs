using UnityEngine;

/// <summary>
/// 背景をスクロールさせるクラス
/// </summary>
public class BackgroundScroller : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer[] sprites;

    [SerializeField]
    private float scrollSpeed = 0.5f;
    
    [SerializeField]
    private float tileSizeY = 40f;

    private void Awake()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.transform.position += Vector3.down * scrollSpeed * Time.deltaTime;
            if (sprite.transform.position.y <= -tileSizeY)
            {
                Vector3 newPosition = sprite.transform.position;
                newPosition.y = tileSizeY * (sprites.Length - 1);
                sprite.transform.position = newPosition;
            }
        }
    }
}
