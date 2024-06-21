using R3;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ProgressBar
{
    public abstract class ProgressBar : MonoBehaviour
    {
        [Header("UI ELEMENTS")] 
        [SerializeField] private Image _fillArea;

        protected ReactiveProperty<float> ProgressValue = new ReactiveProperty<float>();
        protected ReactiveProperty<int> MaxValue = new ReactiveProperty<int>();

        #region MONO

        private void Awake()
        {
            OnAwake();
        }

        private void OnValidate()
        {
            /*if (_fillArea?.type != Image.Type.Filled)
            {
                Debug.LogError("Progress Bar not correct format");
            }*/
        }

        #endregion

        public void SetProgressValue(int value)
        {
            if (value > 0)
            {
                ProgressValue.Value = value;
            }
        }

        public void SetMaxProgressValue(int value)
        {
            if (value > 0)
            {
                MaxValue.Value = value;
            }
        }

        public void ResetProgress() => ProgressValue.Value = 0;

        #region CALLBACKS

        protected virtual void OnAwake()
        {
            ProgressValue.Subscribe(_ =>
            {
                OnProgressValueChange();
            });

            MaxValue.Subscribe(_ => OnProgressValueChange());
        }

        protected virtual void OnProgressValueChange()
        {
            _fillArea.fillAmount = (float) ProgressValue.Value / MaxValue.Value;
        }

        #endregion
    }
}

