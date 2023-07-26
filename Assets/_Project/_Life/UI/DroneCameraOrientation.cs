using System;
using System.Collections;
using System.Collections.Generic;
using Life.MovementControllers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Life.UI
{
    public class DroneCameraOrientation : MonoBehaviour
    {
        [SerializeField] private float _horizIndicatorSpan;
        [SerializeField] private float _vertInidcatorSpan;
        [SerializeField] private RectTransform _horizIndicator;
        [SerializeField] private RectTransform _vertIndicator;
        [SerializeField] private RectTransform _vertCenter;
        [SerializeField] private DroneCameraController _cam;

        private float _horizIndicatorY; 
        private float _vertIndicatorX;

        private void Awake()
        {
            _horizIndicatorY = _horizIndicator.anchoredPosition.y;
            _vertIndicatorX = _vertIndicator.anchoredPosition.x;
            var vertSpan = Mathf.Abs(_cam.VerticalClamp.y) + Mathf.Abs(_cam.VerticalClamp.x);
            var centerPoint = (vertSpan - _cam.VerticalClamp.y) / vertSpan;
            var x = _vertCenter.anchoredPosition.x;
            _vertCenter.anchoredPosition = new Vector2(x,  _vertInidcatorSpan/2f - (_vertInidcatorSpan * centerPoint));

        }

        void Update()
        {
            var c = _cam;
            var horizSpan = Mathf.Abs(c.HorizontalClamp.y) + Mathf.Abs(c.HorizontalClamp.x);
            var vertSpan = Mathf.Abs(c.VerticalClamp.y) + Mathf.Abs(c.VerticalClamp.x);
            var horizPerc = (c.CamHorizontalAngle + Mathf.Abs(c.HorizontalClamp.x)) / horizSpan;
            var vertPerc = (c.CamVerticalAngle + Mathf.Abs(c.VerticalClamp.x)) / vertSpan;
            var horizPos = _horizIndicatorSpan * horizPerc;
            var vertPos = _vertInidcatorSpan * vertPerc;
            
            _horizIndicator.anchoredPosition = new Vector2(horizPos - _horizIndicatorSpan/2f, _horizIndicatorY);
            _vertIndicator.anchoredPosition = new Vector2(_vertIndicatorX,  _vertInidcatorSpan/2f - vertPos);

        }
    }
}
