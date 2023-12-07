using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftbodyController : MonoBehaviour
{
    public float moveSpeed = 5f; // 移動速度

    void Update()
    {
        // 獲取水平和垂直輸入
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 計算移動向量
        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;

        // 移動 Softbody
        MoveSoftbody(moveDirection);
    }

    void MoveSoftbody(Vector2 direction)
    {
        // 獲取 Softbody 的 Rigidbody2D
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // 設定速度
        rb.velocity = direction * moveSpeed;
    }
}
