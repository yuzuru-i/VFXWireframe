using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WireFrameLineTopo : MonoBehaviour
{
    private GraphicsBuffer _vertexBuffer;
    private GraphicsBuffer _lineBuffer;

    private VisualEffect _vfx;

    private Mesh _mesh;

    private void Start()
    {
        _vfx = GetComponent<VisualEffect>();
        _mesh = GetComponent<MeshFilter>().mesh;
        var vertcies = _mesh.vertices;

        var lines = GetLines(_mesh.GetIndices(0));

        _vertexBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Vertex, vertcies.Length, 3 * sizeof(float));
        _lineBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, lines.Count, 2 * sizeof(int));
        _vertexBuffer.SetData(vertcies);
        _lineBuffer.SetData(lines);

        _vfx.SetGraphicsBuffer("VertexBuffer", _vertexBuffer);
        _vfx.SetGraphicsBuffer("LineBuffer", _lineBuffer);

        //foreach(Vector2 line in lines)
        //{
        //    Debug.Log(line.ToString());
        //}

    }

    private void OnDisable()
    {
        _vertexBuffer.Dispose();
        _lineBuffer.Dispose();
    }

    private List<Vector2> GetLines(int[] triangles)
    {
        var set = new HashSet<Vector2>();
        for (int i = 0; i < triangles.Length / 3; i++)
        {
            var tri = new List<int>() { triangles[3*i], triangles[3*i + 1], triangles[3*i + 2] };
            tri.Sort();
            set.Add(new Vector2(tri[0], tri[1]));
            set.Add(new Vector2(tri[1], tri[2]));
            set.Add(new Vector2(tri[0], tri[2]));
        }
        return new List<Vector2>(set);
    }
}
