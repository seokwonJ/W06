using TMPro;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public TMP_Text numText;
    public int trashId;
    public int speed;
    public Vector2 dir; 
    public bool isAttack;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        trashId = Random.Range(1, 10);
        numText.text = trashId.ToString();
    }

    void FixedUpdate()
    {
        if (isAttack)
        {
            rb.linearVelocity = dir.normalized * speed;
        }
        else
        {
            //rb.linearVelocity = Vector2.zero;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("collision");
        if ((collision.gameObject.layer == LayerMask.NameToLayer("Wall") || collision.gameObject.layer == LayerMask.NameToLayer("Player")) && transform.tag != "Obstacle")
        {

            ContactPoint2D contact = collision.GetContact(0);
            Vector2 reflectDir = Vector2.Reflect(dir.normalized, contact.normal);
            Vector2 bounce = reflectDir * 3f; // 튕김 강도

            // 튕기는 효과 적용
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.AddForce(bounce, ForceMode2D.Impulse);
            }

            // 플레이어와 충돌한 경우 추가 처리
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                print("collision with");
                ChangePlayerState(collision.gameObject);
            }

            // 튕김 상태와 이동 상태 전환
            isAttack = false;
            transform.tag = "Obstacle";
        }
    }

    public virtual void ChangePlayerState(GameObject collisonPlayer)
    {
        Debug.Log("Hello from Parent!");
    }
}
