using System.Collections.Generic;
using UnityEngine;

public class MouseTrail : MonoBehaviour
{

    [SerializeField] LineRenderer m_TrailPrefab ;
    [SerializeField] Camera m_Camera;
    [SerializeField] float m_ClearSpeed = 1;
    [SerializeField] float m_DistanceFromCamera = 1;

    private LineRenderer _CurrentTrail;
    private List<Vector3> _Points = new();

    private void Update()
    {
        InputCheck();

        UpdateTrailPoints();

        ClearTrailPoints();
    }

    private void InputCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DestroyCurrentTrail();
            CreateCurrentTrail();
            AddPoint();
        }

        if (Input.GetMouseButton(0))
        {
            AddPoint();
        }
    }

    private void DestroyCurrentTrail()
    {
        if (_CurrentTrail != null)
        {
            Destroy(_CurrentTrail.gameObject);
            _CurrentTrail = null;
            _Points.Clear();
        }
    }

    private void CreateCurrentTrail()
    {
        _CurrentTrail = Instantiate(m_TrailPrefab);
        _CurrentTrail.transform.SetParent(transform, true);
    }

    private void AddPoint()
    {
        Vector3 mousePosition = Input.mousePosition;
        _Points.Add(m_Camera.ViewportToWorldPoint(new Vector3(mousePosition.x / Screen.width, mousePosition.y / Screen.height, m_DistanceFromCamera)));
    }

    private void UpdateTrailPoints()
    {
        if (_CurrentTrail != null && _Points.Count > 1)
        {
            _CurrentTrail.positionCount = _Points.Count;
            _CurrentTrail.SetPositions(_Points.ToArray());
        }
        else
        {
            DestroyCurrentTrail();
        }
    }

    private void ClearTrailPoints()
    {
        float clearDistance = Time.deltaTime * m_ClearSpeed;
        while (_Points.Count > 1 && clearDistance > 0)
        {
            float distance = (_Points[1] - _Points[0]).magnitude;
            if (clearDistance > distance)
            {
                _Points.RemoveAt(0);
            }
            else
            {
                _Points[0] = Vector3.Lerp(_Points[0], _Points[1], clearDistance / distance);
            }
            clearDistance -= distance;
        }
    }

    void OnDisable()
    {
        DestroyCurrentTrail();
    }

}


