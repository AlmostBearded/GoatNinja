using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cutter : MonoBehaviour
{ 
  //public GameObject planeCutter;
  private MeshFilter meshFilter;
  private Mesh mesh;
  private Plane plane;

  public void Cut(Plane cuttingPlane)
  {
    plane = cuttingPlane;

    // Cache variables.
    this.meshFilter = GetComponent<MeshFilter>();
    this.mesh = meshFilter.mesh;

    // Sort triangles by the side of the plane they are on.
    List<int> trianglesPos, trianglesNeg, trianglesCut;
    SortTrianglesBySide(out trianglesPos, out trianglesNeg, out trianglesCut);

    // Cut triangles.
    List<int> cutTrianglesPos, cutTrianglesNeg;
    List<Vector3> cutVerticesPos, cutVerticesNeg;
    CutTriangles(trianglesCut, out cutTrianglesPos, out cutVerticesPos,
      out cutTrianglesNeg, out cutVerticesNeg);

    // Copy triangles.
    CopyTriangles(trianglesPos, mesh.vertices, cutTrianglesPos, cutVerticesPos);
    CopyTriangles(trianglesNeg, mesh.vertices, cutTrianglesNeg, cutVerticesNeg);

    // Clone the gameobject and destroy the cutter script to not end in an endless loop for now.
    GameObject otherObj = Instantiate(gameObject) as GameObject;
    //Destroy(otherObj.GetComponent<Cutter>());
    MeshFilter otherMeshFilter = otherObj.GetComponent<MeshFilter>();

    // Create both meshes.
    meshFilter.mesh = CreateMesh(cutTrianglesPos, cutVerticesPos);
    otherMeshFilter.mesh = CreateMesh(cutTrianglesNeg, cutVerticesNeg);

    // Push both objects apart.
    Rigidbody rb = GetComponent<Rigidbody>();
    Rigidbody otherRb = otherObj.GetComponent<Rigidbody>();
    rb.AddForce(plane.normal * 10);
    otherRb.AddForce(plane.normal * -10);
  }

  public Plane CreateCuttingPlane(Vector3 slicePlaneP0, Vector3 slicePlaneP1, Vector3 slicePlaneP2)
  {
    // Transform plane vertices into other object space.
    Vector3 aPlane = transform.InverseTransformPoint(slicePlaneP0);
    Vector3 bPlane = transform.InverseTransformPoint(slicePlaneP1);
    Vector3 cPlane = transform.InverseTransformPoint(slicePlaneP2);

    // Create a plane.
    Plane cuttingPlane = new Plane(aPlane, bPlane, cPlane);
    return cuttingPlane;
  }

  private void SortTrianglesBySide(out List<int> trianglesPos,
    out List<int> trianglesNeg, out List<int> trianglesCut)
  {
    // Initialize out variables.
    trianglesPos = new List<int>();
    trianglesNeg = new List<int>();
    trianglesCut = new List<int>();

    // Cache mesh variables.
    Vector3[] vertices = mesh.vertices;
    int[] triangles = mesh.triangles;

    // Find the triangles we need to cut and the ones that don't need to be cut.
    for (int triangleIdx = 0; triangleIdx < triangles.Length; triangleIdx += 3)
    {
      // Cache the triangle and the vertices.
      int[] t = new[] { triangles[triangleIdx], triangles[triangleIdx + 1],
        triangles[triangleIdx + 2] };
      Vector3[] tVerts = new[] { vertices[t[0]], vertices[t[1]], vertices[t[2]] };
      bool[] sides = new[] { plane.GetSide(tVerts[0]), plane.GetSide(tVerts[1]),
        plane.GetSide(tVerts[2]) };

      // If all vertices are on the same side of the cutting plane.
      if (sides[0] == sides[1] && sides[0] == sides[2])
      {
        if (sides[0])
          trianglesPos.AddRange(t);
        else
          trianglesNeg.AddRange(t);
      }
      // If not all vertices are on the same side of the cutting plane.
      else
      {
        trianglesCut.AddRange(t);
      }
    }
  }

  private void CutTriangles(List<int> triangles, out List<int> trianglesPos,
    out List<Vector3> verticesPos, out List<int> trianglesNeg,
    out List<Vector3> verticesNeg)
  {
    // Initialize out variables.
    trianglesPos = new List<int>();
    verticesPos = new List<Vector3>();
    trianglesNeg = new List<int>();
    verticesNeg = new List<Vector3>();

    // Cache mesh vertices.
    Vector3[] vertices = mesh.vertices;

    // Cut triangles.
    for (int tIdx = 0; tIdx < triangles.Count; tIdx += 3)
    {
      // Cache the triangle indices.
      int[] t = new[] { triangles[tIdx], triangles[tIdx + 1], triangles[tIdx + 2] };

      // Cache the triangle vertices.
      Vector3[] tVerts = new[] { vertices[t[0]], vertices[t[1]], vertices[t[2]] };

      // Cache the vertex sides regarding the plane.
      bool[] sides = new[] {plane.GetSide(tVerts[0]),
        plane.GetSide(tVerts[1]),
        plane.GetSide(tVerts[2]) };

      // Calculate the vertices.
      List<Vector3> currVerticesPos = new List<Vector3>();
      List<Vector3> currVerticesNeg = new List<Vector3>();
      for (int i = 0; i < 3; i++)
      {
        int i2 = (i + 1) % 3;
        Vector3 v0 = tVerts[i], v1 = tVerts[i2];

        if (sides[i] == true)
          currVerticesPos.Add(v0);
        else
          currVerticesNeg.Add(v0);

        if (sides[i] != sides[i2])
        {
          // Calculate intersection point of triangle edge with plane.
          Vector3 dir = (v1 - v0).normalized;
          Ray r = new Ray(v0, dir);
          float d;
          plane.Raycast(r, out d);
          Vector3 intersection = v0 + dir * d;

          currVerticesPos.Add(intersection);
          currVerticesNeg.Add(intersection);
        }
      }

      // Calculate the triangles.
      trianglesPos.AddRange(CalculateTriangles(currVerticesPos.Count,
        verticesPos.Count));
      trianglesNeg.AddRange(CalculateTriangles(currVerticesNeg.Count,
        verticesNeg.Count));

      // Insert the vertices.
      verticesPos.AddRange(currVerticesPos);
      verticesNeg.AddRange(currVerticesNeg);
    }
  }

  private int[] CalculateTriangles(int numVertices, int startIndex)
  {
    if (numVertices == 3)
    {
      return new[] { startIndex, startIndex + 1, startIndex + 2 };
    }
    else if (numVertices == 4)
    {
      return new[] { startIndex, startIndex + 1, startIndex + 2,
        startIndex, startIndex + 2, startIndex + 3};
    }
    return null;
  }

  private void CopyTriangles(IEnumerable<int> trianglesToAdd, 
    IList<Vector3> verticesToAdd, IList<int> triangles, IList<Vector3> vertices)
  {
    Dictionary<int, int> indexMapping = new Dictionary<int, int>();
    foreach (int idx in trianglesToAdd)
    {
      int newIdx;
      if (!indexMapping.TryGetValue(idx, out newIdx))
      {
        newIdx = vertices.Count;
        indexMapping.Add(idx, newIdx);
        vertices.Add(verticesToAdd[idx]);
      }
      triangles.Add(newIdx);
    }
  }

  private Mesh CreateMesh(List<int> triangles, List<Vector3> vertices)
  {
    Mesh m = new Mesh();

    int[] trianglesArray = new int[triangles.Count];
    triangles.CopyTo(trianglesArray);

    Vector3[] verticesArray = new Vector3[vertices.Count];
    vertices.CopyTo(verticesArray);

    //m.Clear();
    m.vertices = verticesArray;
    m.triangles = trianglesArray;
    m.RecalculateBounds();
    m.RecalculateNormals();

    return m;
  }
}
