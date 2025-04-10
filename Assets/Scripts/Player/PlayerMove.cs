using System.IO.Pipes;
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
    private float _attackTime = 0.1f;
    private float _dashTime = 0.4f;
    private Vector2 _lastDir;

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
                ChangetState(0);
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
                ChangetState(0); _numState = 0;
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
                ChangetState(0);
            }
            else
            {
                _nowSpeed = 10;
            }
        }
        // 공격 반동
        else if (_numState == 4)
        {
            _stateTime += Time.deltaTime;
            if (_stateTime > _attackTime)
            {
                ChangetState(0);
                inputVec = _lastDir;
            }
            else
            {
                inputVec = transform.right * -1;
                _nowSpeed = Mathf.Lerp(_nowSpeed, speed, Time.deltaTime * 30);
            }
        }
        // 대쉬
        else if (_numState == 5)
        {
            _stateTime += Time.deltaTime;
            if (_stateTime > _dashTime)
            {
                ChangetState(0);
                inputVec = _lastDir;
            }
            else
            {
                inputVec = transform.right * -1;
                _nowSpeed = Mathf.Lerp(_nowSpeed, speed, Time.deltaTime * 8);
                if (_nowSpeed - speed  < 1f)
                {
                    _stateTime = _dashTime;
                }
            }
        }


        Vector2 nextVec = inputVec * _nowSpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);

        // 회전 추가
        if (inputVec != Vector2.zero && (_numState != 4 && _numState != 5))
        {
            float angle = Mathf.Atan2(inputVec.y, inputVec.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

            // 부드럽게 회전 (Lerp 또는 Slerp 사용 가능)
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 30f);

        }
    }


    void OnMove(InputValue value)
    {
        if (_numState == 4 || _numState == 5)
        {
            _lastDir = value.Get<Vector2>();
        }
        else if (_numState != 3)
        {
            inputVec = value.Get<Vector2>();
            print("Dd");
        }
        
    }

    public void ChangetState(int num)
    {
        if (_numState == num) return;
        if (_numState >= 1 && _numState <= 3 && (num == 4 || num ==5)) return;
        _stateTime = 0;
        _numState = num;
        if (_numState == 4 || _numState == 5)
        {
            _lastDir = inputVec;
            _nowSpeed = 20;
        }
    }
}
