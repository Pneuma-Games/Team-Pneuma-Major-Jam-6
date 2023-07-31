using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Life
{
    public class SpecimenRadar : MonoBehaviour
    {
        public Canvas Canvas;
        public GameObject SpecimenIndicatorPrefab;
        public RectTransform SpecimenIndicatorParent;
        public float SpecimenIndicatorDistance = 75f;
        public Camera DroneCam;
        
        private Specimen[] _specimens;
        private List<RectTransform> _indicators = new List<RectTransform>();

        private void OnEnable()
        {
            _specimens = FindObjectsOfType<Specimen>().Where(s => s.DisplayDroneIndicator).ToArray();
        }

        private void Start()
        {
            for (int i = 0; i < _specimens.Length; i++)
            {
                var indicator = Instantiate(SpecimenIndicatorPrefab, SpecimenIndicatorParent).transform as RectTransform;
                _indicators.Add(indicator);
            }
        }

        private void Update()
        {
            for (int i = 0; i < _indicators.Count; i++)
            {
                var item = _indicators[i];
                if (_specimens.Length <= i)
                {
                    _indicators[i].gameObject.SetActive(false);
                    continue;
                }
                
                var specimen = _specimens[i];
                
                if (specimen.DisplayDroneIndicator == false)
                {
                    _indicators[i].gameObject.SetActive(false);
                    continue;
                }
                
                UpdateSpecimenDisplay(specimen, DroneCam, item);
            }
        }

        private List<Vector3> _displayCornerCandidates = new List<Vector3>();
        private void UpdateSpecimenDisplay(Specimen s, Camera cam, RectTransform indicator)
        {
            var dist = Vector3.Distance(s.transform.position, cam.transform.position);
            if (dist > SpecimenIndicatorDistance)
            {
                indicator.gameObject.SetActive(false);
                return;
            }

            Vector2 canvasPos;
            var screenPos = RectTransformUtility.WorldToScreenPoint(cam, s.transform.position);
            indicator.gameObject.SetActive(true);
            //RectTransformUtility.ScreenPointToLocalPointInRectangle(Canvas.transform as RectTransform, screenPos, cam, out canvasPos);
            indicator.anchoredPosition = screenPos;
            


            // _displayCornerCandidates.Clear();
            // var ren = s.GetComponentInChildren<MeshRenderer>();
            // var bds = ren.bounds;
            // var pos = bds.center;
            // var ext = bds.extents;
            //
            // _displayCornerCandidates.Add(cam.WorldToScreenPoint(pos + new Vector3(ext.x, ext.y, ext.z)));
            // _displayCornerCandidates.Add(cam.WorldToScreenPoint(pos + new Vector3(ext.x, ext.y, -ext.z)));
            // _displayCornerCandidates.Add(cam.WorldToScreenPoint(pos + new Vector3(ext.x, -ext.y, ext.z)));
            // _displayCornerCandidates.Add(cam.WorldToScreenPoint(pos + new Vector3(ext.x, -ext.y, -ext.z)));
            // _displayCornerCandidates.Add(cam.WorldToScreenPoint(pos + new Vector3(-ext.x, ext.y, ext.z)));
            // _displayCornerCandidates.Add(cam.WorldToScreenPoint(pos + new Vector3(-ext.x, ext.y, -ext.z)));
            // _displayCornerCandidates.Add(cam.WorldToScreenPoint(pos + new Vector3(-ext.x, -ext.y, ext.z)));
            // _displayCornerCandidates.Add(cam.WorldToScreenPoint(pos + new Vector3(-ext.x, -ext.y, -ext.z)));
            //
            // var camPos = cam.transform.position;
            // var corners = _displayCornerCandidates.OrderByDescending(crn => (crn - camPos).sqrMagnitude).Take(4);

        }
    }
}
