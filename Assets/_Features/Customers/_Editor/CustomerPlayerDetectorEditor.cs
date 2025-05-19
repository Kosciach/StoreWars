using System;
using UnityEditor;
using UnityEngine;

namespace Kosciach.StoreWars.Customers.Editor
{
    [CustomEditor(typeof(CustomerPlayerDetector))]
    public class CustomerPlayerDetectorEditor : UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            CustomerPlayerDetector detector = (CustomerPlayerDetector)target;
            
            //Draw Range
            Handles.color = Color.white;
            Handles.DrawWireArc(detector.transform.position, Vector3.up, Vector3.forward, 360, detector.Range);
            
            //Draw Angle
            Handles.color = Color.yellow;
            float halfAngle = detector.Angle / 2f;
            DrawAngleLine(detector, -halfAngle);
            DrawAngleLine(detector, halfAngle);

            if (detector.PlayerTransform != null)
            {
                Handles.color = Color.green;
                Handles.DrawLine(detector.transform.position, detector.PlayerTransform.position);
            }
        }

        private void DrawAngleLine(CustomerPlayerDetector p_detector, float p_rotationAngle)
        {
            Quaternion rotation = Quaternion.AngleAxis(p_rotationAngle, Vector3.up);
            Vector3 direction = rotation * p_detector.transform.forward;
            Vector3 targetPos = p_detector.transform.position + direction * p_detector.Range;
            Handles.DrawLine(p_detector.transform.position, targetPos);
        }
    }
}
