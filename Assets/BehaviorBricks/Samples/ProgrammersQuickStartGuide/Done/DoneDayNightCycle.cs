using System;
using UnityEngine;

namespace BBSamples.PQSG // Programmers Quick Start Guide
{
	/// <summary>
	///     Behavior DoneDayNightCycle component. Add it to your directional light to control the brightness,colorand time to
	///     simulate day and night.
	/// </summary>
	public class DoneDayNightCycle : MonoBehaviour
    {
        /// <value>Complete day-night cycle duration (in seconds).</value>
        // Complete day-night cycle duration (in seconds).
        public float dayDuration = 10.0f;

        // Private field with the day color. It is set to the initial light color.
        private Color dayColor;

        // Reference to the Light component
        private Light lightComponent;

        // Private field with the hard-coded night color.
        private readonly Color nightColor = Color.white * 0.1f;

        /// <value>Read-only property that informs if it is currently night time.</value>
        // Read-only property that informs if it is currently night time.
        public bool isNight { get; private set; }

        /// <summary>DoneDayNightCycle Initialization Method.</summary>
        /// <remarks>Search the light component and set color light.</remarks>
        private void Start()
        {
            lightComponent = GetComponent<Light>();
            dayColor = lightComponent.color;
        } // Start

        /// <summary>DoneDayNightCycle Update Method.</summary>
        /// <remarks>
        ///     Calculate the intensity of the light with the time elapsed and
        ///     depending on it do a lerp between the color of day and night, also register of EventHandler of this class.
        /// </remarks>
        private void Update()
        {
            var lightIntensity = 0.5f +
                                 Mathf.Sin(Time.time * 2.0f * Mathf.PI / dayDuration) / 2.0f;
            if (isNight != lightIntensity < 0.3)
            {
                isNight = !isNight;
                if (OnChanged != null)
                    OnChanged(this, EventArgs.Empty);
            }

            lightComponent.color = Color.Lerp(nightColor, dayColor, lightIntensity);
        } // Update

        /// <value>Event raised when sun rises or sets.</value>
        // Event raised when sun rises or sets.
        public event EventHandler OnChanged;
    } // class DoneDayNightCycle
} // namespace