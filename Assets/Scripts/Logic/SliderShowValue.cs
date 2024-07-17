using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Logic
{
    public class SliderShowValue : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI value;
        [SerializeField] private string format;
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            ShowValue();
        }

        public void ShowValue() => 
            value.text = _slider.value.ToString(format);
    }
}