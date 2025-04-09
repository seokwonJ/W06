using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private GameObject _cleanerArea;
    private PlayerInput _playerInput;
    private InputAction _interactAction;

    private PlayerController _playerController;

    public GameObject trash;
    public GameObject ice;
    public GameObject banana;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        // 현재 Action Map에서 Interact 액션을 가져옴
        _interactAction = _playerInput.actions["Attack"];

        _interactAction.performed += OnInteractPerformed;
        _interactAction.canceled += OnInteractCanceled;
    }

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    void OnEnable()
    {
        _interactAction?.Enable();
    }

    void OnDisable()
    {
        _interactAction?.Disable();

        _interactAction.performed -= OnInteractPerformed;
        _interactAction.canceled -= OnInteractCanceled;
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
 

        for (int i=0;i < _playerController.trashList.Count;i++)
        {
            GameObject shootObject = null ;
            switch(_playerController.trashList[i])
            {
               
                case 1:
                    shootObject = Instantiate(trash, transform.position + transform.right + transform.up * 0.2f * i, Quaternion.identity);
                    shootObject.tag = "Trash";
                    break;
                case 2:
                    shootObject = Instantiate(ice, transform.position + transform.right + transform.up * 0.2f * i, Quaternion.identity);
                    shootObject.tag = "Ice";
                    break;
                case 3:
                    shootObject = Instantiate(banana, transform.position + transform.right + transform.up * 0.2f * i, Quaternion.identity);
                    shootObject.tag = "Banana";
                    break;
                default:
                    break;
            }
            Obstacle obstacle = shootObject.GetComponent<Obstacle>();
            obstacle.isAttack = true;
            obstacle.dir = transform.right + transform.up * 0.2f * i;
        }
        _playerController.trashList.Clear();
    } 

    private void OnInteractCanceled(InputAction.CallbackContext context)
    {
        //Debug.Log("Attack 취소됨!");
    }
}
