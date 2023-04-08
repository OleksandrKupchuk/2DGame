using UnityEngine;
using MathematicalCalculations;

public class FieldOfViewByMonkey : MonoBehaviour {
    Mesh _mesh;
    private Vector3[] _vertices;
    private Vector2[] _uv;
    private int[] _triangles;
    private Vector3 _origin;
    private bool[] _raysDetected;
    [SerializeField]
    private LayerMask _layer;

    [SerializeField]
    private float _viewDistance;

    public bool IsDetectedPlayer { get; private set; }

    private void Start() {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;
        _origin = Vector3.zero;
    }

    private void Update() {
        //_origin = transform.position;
    }

    private void LateUpdate() {

        float _fov = 180f;
        int _rayCount = 5;
        float _angle = 0f;
        float _angleIncrease = _fov / _rayCount;
        _raysDetected = new bool[_rayCount + 2];

        _vertices = new Vector3[_rayCount + 1 + 1];
        _uv = new Vector2[_vertices.Length];
        _triangles = new int[_rayCount * 3];

        _vertices[0] = _origin;

        int _vertexIndex = 1;
        int _triangleIndex = 0;
        for (int i = 0; i <= _rayCount; i++) {
            Vector3 _vertex;
            RaycastHit2D _raycastHit2D = Physics2D.Raycast(_origin, CalculationAngle.GetVector3FromAngle(_angle), _viewDistance, _layer);
            Debug.DrawLine(_origin, CalculationAngle.GetVector3FromAngle(_angle) * _viewDistance, Color.green);

            if (_raycastHit2D.collider == null) {
                _vertex = _origin + CalculationAngle.GetVector3FromAngle(_angle) * _viewDistance;
            }
            else {
                _vertex = _raycastHit2D.point;

                if (_raysDetected.Length < i) {
                    return;
                }
                if (_raycastHit2D.collider.gameObject.CompareTag("Player")) {
                    _raysDetected[i] = true;
                }
                else {
                    _raysDetected[i] = false;
                }
            }

            _vertices[_vertexIndex] = _vertex;

            if (i > 0) {
                _triangles[_triangleIndex + 0] = 0;
                _triangles[_triangleIndex + 1] = _vertexIndex - 1;
                _triangles[_triangleIndex + 2] = _vertexIndex;

                _triangleIndex += 3;
            }

            _vertexIndex++;
            _angle -= _angleIncrease;
        }

        _mesh.vertices = _vertices;
        _mesh.uv = _uv;
        _mesh.triangles = _triangles;

        CheckDetectedPlayer();
    }

    private void CheckDetectedPlayer() {
        foreach (var _rayDetect in _raysDetected) {
            if (_rayDetect) {
                IsDetectedPlayer = true;
            }
            else {
                IsDetectedPlayer = false;
            }
        }
    }

    public void SetStartPoint(Vector3 origin) {
        _origin = origin;
    }
}
