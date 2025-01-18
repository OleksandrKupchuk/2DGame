using UnityEngine;
using MathematicalCalculations;

public class FieldOfView : MonoBehaviour {
    private Player _player;
    private readonly int _numberOfSidesOfTriangle = 3;
    private Mesh _mesh;
    private Vector3[] _vertices;
    private int[] _verticesOfTriangles;
    private int[] _countTriangles;
    private float _additionalAngleInDegree;
    private Vector3 _startPoint;
    private bool[] _raysDetected;

    [SerializeField]
    private float _angleOfView;
    [SerializeField]
    private int _rayCount;
    [SerializeField]
    private float _viewDistance;
    [SerializeField]
    private LayerMask _layer;
    [SerializeField]
    private PlayerHealthController _healthController;

    public Player Target { get; private set; }

    private void Start() {
        _player = FindObjectOfType<Player>();
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;

        _vertices = new Vector3[_rayCount + 3];
        _countTriangles = new int[_rayCount + 1];
        _verticesOfTriangles = new int[_countTriangles.Length * _numberOfSidesOfTriangle];
        _additionalAngleInDegree = _angleOfView / _countTriangles.Length;
        _raysDetected = new bool[_rayCount + 2];
    }

    private void Update() {
        float _currentAngleInDegree = 5f;

        CalculationVertices(_currentAngleInDegree);
        CalculationVerticesOfTriangles();
        SetDataMesh();
        CheckDetectedPlayer();
    }

    private void CalculationVertices(float angleInDegree) {
        _vertices[0] = _startPoint;

        for (int i = 1; i < _vertices.Length; i++) {
            Vector3 _vertex;
            RaycastHit2D _raycastHit2D = Physics2D.Raycast(_startPoint, CalculationAngle.GetVector3FromAngle(angleInDegree), _viewDistance, _layer);

            if (_raycastHit2D.collider == null) {
                Debug.DrawRay(_startPoint, CalculationAngle.GetVector3FromAngle(angleInDegree) * _viewDistance, Color.green);
                _vertex = _startPoint + CalculationAngle.GetVector3FromAngle(angleInDegree) * _viewDistance;

                _raysDetected[i - 1] = false;
            }
            else {
                _vertex = _raycastHit2D.point;
                Debug.DrawRay(_startPoint, CalculationAngle.GetVector3FromAngle(angleInDegree) * _viewDistance, Color.yellow);

                if (_raycastHit2D.collider.gameObject.CompareTag(Tags.Player) && !_healthController.IsDead) {
                    Debug.DrawRay(_startPoint, CalculationAngle.GetVector3FromAngle(angleInDegree) * _viewDistance, Color.red);
                    _raysDetected[i - 1] = true;
                }
                else {
                    _raysDetected[i - 1] = false;
                }
            }
            if (_vertices.Length > i) {
                _vertices[i] = _vertex;
                angleInDegree += _additionalAngleInDegree;
            }

        }
    }

    private void CalculationVerticesOfTriangles() {
        int _trianglesIndex = 0;

        for (int i = 0; i < _countTriangles.Length; i++) {
            _verticesOfTriangles[_trianglesIndex] = 0;
            _verticesOfTriangles[_trianglesIndex + 1] = i + 2;
            _verticesOfTriangles[_trianglesIndex + 2] = i + 1;

            _trianglesIndex += 3;
        }
    }

    private void SetDataMesh() {

        _mesh.vertices = _vertices;
        _mesh.triangles = _verticesOfTriangles;
    }

    private void CheckDetectedPlayer() {
        foreach (var _rayDetect in _raysDetected) {
            if (_rayDetect == true) {
                Target = _player;
                return;
            }
        }

        Target = null;
    }

    public void SetStartPoint(Vector3 startPoint) {
        _startPoint = startPoint;
    }
}
