using UnityEngine;

public class CloseToObj : MonoBehaviour
{
    //public GameObject fanObject;  // 電風扇的 GameObject，請將電風扇的 SpriteRenderer 放在這個 GameObject 上
    public Transform obj;
    public Transform player;
    public float glowIntensity = 2.0f;
    public float glowDistance = 3.0f;

    private SpriteRenderer fanSpriteRenderer;
    private Color originalColor;

    private void Start()
    {
        // 獲取電風扇的 SpriteRenderer
        fanSpriteRenderer = obj.GetComponent<SpriteRenderer>();
        

        if (fanSpriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on fanObject. Make sure to assign the correct GameObject.");
        }

        // 保存原始顏色，以便稍後重置
        originalColor = fanSpriteRenderer.color;
    }

    private void Update()
    {
        // 檢測角色是否靠近電風扇，這裡使用了一個簡單的距離檢測，你可以根據實際需求修改
        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        BoxCollider2D playerCollider = player.GetComponent<BoxCollider2D>();
        BoxCollider2D objCollider = obj.GetComponent<BoxCollider2D>();

        if (player != null)
        {
            // 使用 Vector2.Distance 來計算 2D 空間中的距離
            float distance =  CalculateDistance(playerCollider, objCollider);
            //float distance = (obj.position - player.position).magnitude;
            // Debug.Log(distance);
            // Debug.Log(obj.position);
            // Debug.Log(player.position);
            if (distance < glowDistance)
            {
                // 計算亮度
                float glow = glowIntensity / (1.0f + glowDistance * distance);

                // 調整 SpriteRenderer 的顏色
                // Color color = fanSpriteRenderer.color;
                // Color.RGBToHSV(color, out float h, out float s, out float v);
                // fanSpriteRenderer.color = Color.HSVToRGB(h, s * 0.7f, v);
                //fanSpriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);
                Color tmp = new Color(0.1f, 0.2f, 0.3f, 1);
                //float saturationDelta = 0.3f;  // 這裡可以調整飽和度的增量

                //Color modifiedColor = ChangeSaturation(originalColor , saturationDelta);
                fanSpriteRenderer.color = tmp;

                //  = Color.white * glow;
                // Debug.Log("Player come");
            }
            else
            {
                // 如果角色離開，將 SpriteRenderer 的顏色重置
                fanSpriteRenderer.color = originalColor;
                // Debug.Log("Player leave");
            }
        }
        else
        {
            Debug.LogWarning("Player not found. Make sure the player has the 'Player' tag.");
        }
    }

    float CalculateDistance(BoxCollider2D collider1, BoxCollider2D collider2)
    {
        // 获取碰撞框中心点的世界坐标
        Vector2 center1 = collider1.bounds.center;
        Vector2 center2 = collider2.bounds.center;

        // 计算距离
        float distance = Vector2.Distance(center1, center2);

        return distance;
    }

    public static Color ChangeSaturation(Color originalColor, float saturationDelta)
    {
        // Convert RGB to HSL
        Color.RGBToHSV(originalColor, out float h, out float s, out float v);

        // Modify saturation
        s = Mathf.Clamp01(s + saturationDelta);

        // Convert back to RGB
        Color modifiedColor = Color.HSVToRGB(h, s, v);

        return modifiedColor;
    }
}
