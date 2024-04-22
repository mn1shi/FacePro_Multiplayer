// Copyright (c) Meta Platforms, Inc. and affiliates.

using UnityEngine;
using UnityEngine.Assertions;
using static OVRPlugin;

namespace Oculus.Movement.Utils
{
    /// <summary>
    /// Allows setting body tracking fidelity.
    /// </summary>
    public class BodyTrackingFidelityToggle : MonoBehaviour
    {
        /// <summary>
        /// The current body tracking fidelity set.
        /// </summary>
        [SerializeField]
        [Tooltip(BodyTrackingFidelityToggleTooltips.CurrentFidelity)]
        protected BodyTrackingFidelity2 _currentFidelity = BodyTrackingFidelity2.Low;
        /// <summary>
        /// The text to update after body tracking fidelity is changed.
        /// </summary>
        [SerializeField]
        [Tooltip(BodyTrackingFidelityToggleTooltips.WorldText)]
        protected TMPro.TextMeshPro _worldText;

        private const string _LOW_FIDELITY = "Three Point BT";
        private const string _HIGH_FIDELITY = "IOBT";

        private void Awake()
        {
            Assert.IsNotNull(_worldText);
        }

        private void Start()
        {
            EnforceFidelity();
        }

        /// <summary>
        /// Changes the body tracking fidelity from low to high or vice versa.
        /// </summary>
        public void SwapFidelity()
        {
            _currentFidelity = _currentFidelity == BodyTrackingFidelity2.Low ?
                BodyTrackingFidelity2.High : BodyTrackingFidelity2.Low;
            EnforceFidelity();
        }

        private void EnforceFidelity()
        {
            OVRPlugin.RequestBodyTrackingFidelity(_currentFidelity);
            _worldText.text = _currentFidelity == BodyTrackingFidelity2.Low ?
                _LOW_FIDELITY : _HIGH_FIDELITY;
        }
    }
}
