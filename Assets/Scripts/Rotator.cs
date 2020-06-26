using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Rotator : MonoBehaviour
{

    [SerializeField]
    int _speed = 12;
    [SerializeField]
    float _friction = 0.5f;
    [SerializeField]
    float _lerpSpeed = 1.5f;
    [SerializeField]
    float _lerpZoomSpeed = 1.5f;
    [SerializeField]
    float _zoomDelta = 0.5f;
    [SerializeField]
    GameObject PanelMaterial;
    public static float _minZoom = 4.4f;
    public static float _maxZoom = 8;

    bool _executeRotation = false;
    bool _executeScale = false;
    bool _firstClick = false;
    bool _firstScroll = false;
    bool _allowDrag = false;
    bool _pointerDown = false;
    bool _allowInertialDrag = false;
    bool touchSupported = true;

    float _degNumber = 0;
    float _positionZ = 5.8f;
    float _xDeg = 205f;
    float _yDeg;
    float _inertiaCoef;

    Vector3 _toPosition;


    public void OnEnable()
    {
        DragControlls.OnDragAction += OnDragAction;
        DragControlls.OnPointerAction += OnPointerAction;
    }


    private void OnDisable()
    {
        DragControlls.OnDragAction -= OnDragAction;
        DragControlls.OnPointerAction -= OnPointerAction;
    }


    public void OnDragAction(bool executeRotation)
    {
        _executeRotation = executeRotation;
    }


    public void OnPointerAction(bool allow)
    {
        _executeScale = allow;
    }


    private void Awake()
    {
        if (SystemInfo.operatingSystem.Contains("Windows") || SystemInfo.operatingSystem.Contains("Mac"))
        {
            touchSupported = false;
        }
        else
        {
           // Vector3 pos = PanelMaterial.transform.position;
            //PanelMaterial.transform.position = new Vector3(pos.x, pos.y + 100, pos.z);
        }
    }


    void Start()
    {
        //chairCollider = GetComponent<SphereCollider>();
        //chairCollider.radius = Vector3.Distance(transform.position, Camera.main.transform.position) - 1;
        //center = ChairTransform.GetComponent<MeshRenderer>().bounds.center;
    }


    void Update()
    {
        RotateModel(Time.deltaTime);
        ZoomModel(Time.deltaTime, Input.mouseScrollDelta.y);
    }

    Vector2 firstpoint, secondpoint;

    private void RotateModel(float deltaTime)
    {
        if (touchSupported)
        {
            //Count touches
            if (Input.touchCount == 1 || _allowInertialDrag)
            {
                if (Input.touchCount == 1 && _executeRotation)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        firstpoint = Input.GetTouch(0).position;
                    }
                    //Move finger
                    if (Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        secondpoint = Input.GetTouch(0).position;
                        _xDeg = -(secondpoint.x - firstpoint.x) * _speed * _friction * 0.005f;
                        _yDeg = (secondpoint.y - firstpoint.y) * _speed * _friction * 0.005f;
                        _firstClick = true;
                        //firstpoint = secondpoint;
                    }
                    else if (Input.GetTouch(0).phase == TouchPhase.Stationary)
                    {
                        //firstpoint = Input.GetTouch(0).position;
                    }
                    else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        //firstpoint = Input.GetTouch(0).position;
                    }
                }
            }
        }
        else
        {
            if (Input.GetMouseButton(0) || _allowInertialDrag)
            {
                if (Input.GetMouseButton(0) && _executeRotation)
                {
                    _xDeg = -Mathf.Clamp(Input.GetAxis("Mouse X"), -5, 5) * _speed * _friction;
                    _yDeg = Mathf.Clamp(Input.GetAxis("Mouse Y"), -5, 5) * _speed * _friction;
                    _firstClick = true;
                }
            }
        }

        if (_firstClick)
        {
            _xDeg *= 0.95f;

            transform.Rotate(Vector3.up, _xDeg, Space.Self);

            _yDeg *= 0.95f;
            _degNumber += _yDeg;

            if (_degNumber > -70 && _degNumber < 5)
            {
                transform.Rotate(Vector3.right, _yDeg, Space.World);
            }
            else
            {
                _degNumber = Mathf.Clamp(_degNumber, -70, 5);
            }

            if (Mathf.Abs(_xDeg) <= 0.1f && Mathf.Abs(_yDeg) <= 0.1f)
            {
                _allowInertialDrag = false;
            }
            else
            {
                _allowInertialDrag = true;
            }                   
        }
    }


    private void ZoomModel(float deltaTime, float scroolPosition)
    {
        if (touchSupported)
        {
            if (_executeScale)
            {
                //Pinch zoom
                if (Input.touchCount == 2)
                {
                    Touch touchZero = Input.GetTouch(0);
                    Touch touchOne = Input.GetTouch(1);

                    Vector2 touchZeroPreviousPosition = touchZero.position - touchZero.deltaPosition;
                    Vector2 touchOnePreviousPosition = touchOne.position - touchOne.deltaPosition;

                    float prevTouchDeltaMag = (touchZeroPreviousPosition - touchOnePreviousPosition).magnitude;
                    float TouchDeltaMag = (touchZero.position - touchOne.position).magnitude;
                    float deltaMagDiff = prevTouchDeltaMag - TouchDeltaMag;

                    if (deltaMagDiff > 1)
                    {
                        _positionZ = Mathf.Clamp(_positionZ + _zoomDelta * 88, _minZoom, _maxZoom);
                        _firstScroll = true;
                    }
                    else if (deltaMagDiff < -1)
                    {
                        _positionZ = Mathf.Clamp(_positionZ - _zoomDelta * 88, _minZoom, _maxZoom);
                        _firstScroll = true;
                    }

                    if (_firstScroll)
                    {
                        _toPosition = transform.localPosition;
                        _toPosition.z = _positionZ;
                        transform.localPosition = Vector3.Lerp(transform.localPosition, _toPosition, deltaTime * _lerpZoomSpeed);
                    }
                }               
            }         
        }
        else
        {
            if (_executeScale)
            {
                if (scroolPosition >= 1)
                {
                    _positionZ = Mathf.Clamp(_positionZ - _zoomDelta, _minZoom, _maxZoom);
                    _firstScroll = true;
                }
                else if (scroolPosition <= -1)
                {
                    _positionZ = Mathf.Clamp(_positionZ + _zoomDelta, _minZoom, _maxZoom);
                    _firstScroll = true;
                }

                if (_firstScroll)
                {
                    _toPosition = transform.localPosition;
                    _toPosition.z = _positionZ;
                    transform.localPosition = Vector3.Lerp(transform.localPosition, _toPosition, deltaTime * _lerpZoomSpeed);
                }
            }
        }     
    }

}