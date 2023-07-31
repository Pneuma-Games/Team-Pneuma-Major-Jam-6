using System;
using DG.Tweening;
using UnityEngine;

namespace Life
{
    public class DispenserUIController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _drillBtnCGroup;
        [SerializeField] private CanvasGroup _dispenseBtnCGroup;
        [SerializeField] private GameObject _idleText;
        [SerializeField] private GameObject _choiceText;
        [SerializeField] private GameObject _buttons;
        
        public void DisplayIdle()
        {
            _idleText.SetActive(true);
            _choiceText.SetActive(false);
            _buttons.SetActive(false);
        }
        
        public void DisplayChoice()
        {
            _idleText.SetActive(false);
            _choiceText.SetActive(true);
            _buttons.SetActive(true);
            _dispenseBtnCGroup.blocksRaycasts = true;
            _drillBtnCGroup.blocksRaycasts = true;
        }
        
        
        public void AnimateDrillClick()
        {
            BlinkButton(_drillBtnCGroup);
            _dispenseBtnCGroup.blocksRaycasts = false;
        }

        public void AnimateDispenseClick()
        {
            BlinkButton(_dispenseBtnCGroup);
            _drillBtnCGroup.blocksRaycasts = false;
        }
        
        private void BlinkButton(CanvasGroup cGroup)
        {
            DOTween.Kill(cGroup, true);
            cGroup.blocksRaycasts = false;
            cGroup.DOFade(0f, 0.15f).SetLoops(12, LoopType.Yoyo).SetEase(Ease.InOutQuint).OnComplete(() =>
            {
                cGroup.DOFade(1.0f, 0f);
                DisplayIdle();
            });
        }

        private void Awake()
        {
            DisplayIdle();
        }
    }
}