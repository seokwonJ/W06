using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public float slowSpeed;
    private float _nowSpeed;
    

    Rigidbody2D rigid;

    private int _numState = 0;
    private float _stateTime = 0;
    private float _stunTime = 1f;
    private float _iceTime = 3f;
    private float _bananaTime = 0.7f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // 기본 상태
        if (_numState == 0)
        {
            _nowSpeed = speed;
        }
        // 잠깐 스턴 및 아이템 떨구기 (action으로 invoke하면 좋을 듯)
        else if (_numState == 1)
        {
            _stateTime += Time.deltaTime;
            if (_stateTime > _stunTime)
            {
                _stateTime = 0;
                _numState = 0;
            }
            else
            {
                _nowSpeed = 0;
            } 
        }
        // 일정시간 느려지기
        else if (_numState == 2)
        {
            _stateTime += Time.deltaTime;
            if (_stateTime > _iceTime)
            {
                _stateTime = 0;
                _numState = 0;
            }
            else
            {
                _nowSpeed = slowSpeed;
            }
        }
        // 미끄러지기
        else if (_numState == 3)
        {
            _stateTime += Time.deltaTime;
            if (_stateTime > _bananaTime)
            {
                _stateTime = 0;
                _numState = 0;
            }
            else
            {
                _nowSpeed = 10;
            }
        }

        Vector2 nextVec = inputVec * _nowSpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);

        // 회전 추가
        if (inputVec != Vector2.zero)
        {
            float angle = Mathf.Atan2(inputVec.y, inputVec.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }


    void OnMove(InputValue value)
    {
        if (_numState != 3)
        {
            inputVec = value.Get<Vector2>();
        }
    }

    public void ChangetState(int num)
    {
        _numState = num;
    }
}
