using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WireFrame : MonoBehaviour
{
    private GraphicsBuffer _vertexBuffer;
    private GraphicsBuffer _indexBuffer;

    private VisualEffect _vfx;

    private Mesh _mesh;

    private void Start()
    {
        _vfx = GetComponent<VisualEffect>();
        _mesh = GetComponent<MeshFilter>().mesh;
        var vertcies = _mesh.vertices;
        var triangles = _mesh.triangles;

        //_vertexBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, vertcies.Length, 3 * sizeof(float));
        _vertexBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Vertex, vertcies.Length, 3 * sizeof(float));
        _indexBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Index, triangles.Length, sizeof(int));
        _vertexBuffer.SetData(vertcies);
        _indexBuffer.SetData(triangles);

        _vfx.SetGraphicsBuffer("VertexBuffer", _vertexBuffer);
        _vfx.SetGraphicsBuffer("IndexBuffer", _indexBuffer);
    }

    private void OnDisable()
    {
        _vertexBuffer.Release();
        _indexBuffer.Dispose();
    }
}
