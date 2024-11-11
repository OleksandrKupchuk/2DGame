using UnityEngine;

namespace MathematicalCalculations {
    public class CalculationAngle {
        public static Vector3 GetVector3FromAngle(float angleInDegree) {
            float _angleInRadian = angleInDegree * (Mathf.PI / 180f);
            return new Vector3(Mathf.Cos(_angleInRadian), Mathf.Sin(_angleInRadian));
        }
    }
}
