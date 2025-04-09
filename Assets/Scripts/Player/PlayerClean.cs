using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerClean : MonoBehaviour
{
    private GameObject _cleanerArea;
    private PlayerInput _playerInput;
    private InputAction _interactAction;
    private PlayerController _playerController;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        // ���� Action Map���� Interact �׼��� ������
        _interactAction = _playerInput.actions["Interact"];

        _interactAction.performed += OnInteractPerformed;
        _interactAction.canceled += OnInteractCanceled;
    }

    private void Start()
    {
        _cleanerArea = GetComponentInChildren<CleanerArea>().gameObject;
        _cleanerArea.SetActive(false);
    }
    void OnEnable()
    {
        _interactAction?.Enable();
    }

    void OnDisable()
    {
        _interactAction?.Disable();

        // �޸� ���� ������ ���� �ݹ� ����
        _interactAction.performed -= OnInteractPerformed;
        _interactAction.canceled -= OnInteractCanceled;
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        _cleanerArea?.SetActive(true);
    }

    private void OnInteractCanceled(InputAction.CallbackContext context)
    {
        _cleanerArea?.SetActive(false);
    }
}
