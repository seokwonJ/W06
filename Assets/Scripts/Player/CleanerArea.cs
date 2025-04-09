using System.Collections.Generic;
using UnityEngine;

public class CleanerArea : MonoBehaviour
{
    private float _maxSuctionSpeed = 4f;
    private float _suctionRange = 3f;
    private PlayerController _playerController;
    private Transform _playerTransform;
    

    private List<Rigidbody2D> trashInRange = new List<Rigidbody2D>();

    private void Start()
    {
        _playerTransform = transform.parent;
        _playerController = _playerTransform.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Rigidbody2D rb = other.attachedRigidbody;
            if (rb != null && !trashInRange.Contains(rb))
                trashInRange.Add(rb);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Rigidbody2D rb = other.attachedRigidbody;
            if (rb != null && trashInRange.Contains(rb))
                trashInRange.Remove(rb);
        }
    }

    private void FixedUpdate()
    {
        for (int i = trashInRange.Count - 1; i >= 0; i--)
        {
            var rb = trashInRange[i];
            if (rb == null)
            {
                trashInRange.RemoveAt(i);
                continue;
            }

            Vector2 direction = (_playerTransform.position - rb.transform.position);
            float distance = direction.magnitude;

            if (distance < 0.5f && _playerController.trashList.Count < 5)
            {
                _playerController.trashList.Add(rb.gameObject.GetComponent<Obstacle>().trashId);
                trashInRange.RemoveAt(i);
                Destroy(rb.gameObject);
                continue;
            }

            float t = Mathf.Clamp01(1f - (distance / _suctionRange));
            float suctionSpeed = _maxSuctionSpeed * t;

            rb.linearVelocity = direction.normalized * suctionSpeed;
        }
    }
}
