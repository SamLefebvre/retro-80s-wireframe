using UnityEngine;

// From https://github.com/UnityCommunity/UnityLibrary/tree/master/Assets/Shaders/2D/Effects

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{

    Mesh mesh;
    public MeshFilter[] meshFilters;
    Vector3[] vertices;
    int[] triangles;

    public int xSize = 20;
    public int zSize = 20;
    public float yNoise = 2f;
    public float xNoise = 0.3f;
    public float zNoise = 0.3f;

    public Material[] materials;
    private Vector2[] uvs;

    public bool drawGizmo = false;

    void Start()
    {
        mesh = new Mesh
        {
            subMeshCount = 2
        };

        //GetComponent<Renderer>().material.SetColor("_EMISSION", new Color(0.0927F, 0.4852F, 0.2416F, 0.42F));
        //GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

        CreateShape();
        UpdateMesh();

        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;

    }
    void FixedUpdate()
    {
        CreateShape();
        UpdateMesh();
        xNoise += 0.0001f;
        zNoise += 0.0001f;
    }
    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];


        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * xNoise, z * zNoise) * yNoise;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;

            }
            vert++;
        }

        uvs = new Vector2[vertices.Length];
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                uvs[i] = new Vector2((float)x / xSize, (float)z / zSize);
                i++;
            }
        }
    }
    private void LateUpdate()
    {
        gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;
    }
    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
    private void OnDrawGizmos()
    {
        if (drawGizmo)
        {
            if (vertices == null) return;
            for (int i = 0; i < vertices.Length; i++)
            {
                Gizmos.DrawWireSphere(vertices[i], 0.1f);
            }
        }
    }
}
