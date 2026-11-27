using UnityEngine;
using System.Collections.Generic;
using System;

public class FloorCreator : MonoBehaviour
{
    public float height = 5;
    public float width = 10;
    public int followingFloors;
    public int floorDivisions = 10;
    public Material floor;
    public Material deadly;
    private BallController ball;
    public int currentFloor = 0;
    void Start()
    {
        ball = BallController.instance;
        ball.height = height - 1;
        for (int i = 0; i <= followingFloors; i++)
        {
            CreateFloor(i * height);
        }
    }
    void Update()
    {
        if (ball.transform.position.y < -currentFloor * height)
        {
            currentFloor++;
            CreateFloor((currentFloor + followingFloors) * height);
        }
    }


    private void CreateFloor(float y)
    {
        int displacement = UnityEngine.Random.Range(0, floorDivisions) * 360 / floorDivisions;
        int floorDivisionsLeft = floorDivisions;
        List<Tuple<FloorTypes, int>> listOfFloors = new();
        while (floorDivisionsLeft > 0)
        {
            int size = UnityEngine.Random.Range(1, Math.Min(4, floorDivisionsLeft));
            FloorTypes type = (FloorTypes)UnityEngine.Random.Range(0, 3);
            listOfFloors.Add(new(type, size));
            floorDivisionsLeft -= size;
        }
        if (y == 0)
        {
            listOfFloors[0] = new(FloorTypes.Floor, listOfFloors[0].Item2);
            listOfFloors[listOfFloors.Count/2] = new(FloorTypes.Void, listOfFloors[listOfFloors.Count / 2].Item2);
            displacement = (int)((2.5f - listOfFloors[0].Item2 * 0.5f) * 360 / floorDivisions);
        }
        else
        {
            listOfFloors[0] = new(FloorTypes.Void, listOfFloors[0].Item2);
        }
        int placement = 0;
        for (int i = 0; i < listOfFloors.Count; i++)
        {
            GameObject mesh = null;
            switch (listOfFloors[i].Item1)
            {
                case FloorTypes.Void:
                    break;
                case FloorTypes.Floor:
                    {
                        mesh = new("mesh", typeof(MeshRenderer), typeof(MeshFilter), typeof(MeshCollider))
                        {
                            tag = "Floor"
                        };
                        mesh.GetComponent<MeshRenderer>().SetMaterials(new() { floor });
                    }
                    break;
                case FloorTypes.Deadly:
                    {
                        mesh = new("mesh", typeof(MeshRenderer), typeof(MeshFilter), typeof(MeshCollider))
                        {
                            tag = "Deadly"
                        };
                        mesh.GetComponent<MeshRenderer>().SetMaterials(new() { deadly });
                    }
                    break;
                default:
                    break;
            }
            if (mesh != null)
            {
                mesh.transform.SetParent(transform);
                mesh.transform.position = y * Vector3.down;
                mesh.transform.eulerAngles = (displacement + 360 * placement / floorDivisions) * Vector3.up + transform.eulerAngles;
                mesh.transform.localScale = new Vector3(width, 1, width);
                mesh.GetComponent<MeshFilter>().mesh = CreateShape((float)listOfFloors[i].Item2 / floorDivisions);
                mesh.GetComponent<MeshCollider>().sharedMesh = mesh.GetComponent<MeshFilter>().mesh;
            }
            placement += listOfFloors[i].Item2;
        }
    }

    private Mesh CreateShape(float radius)
    {
        int radiusDivision = (int)(radius * 60);
        float radiusRadiants = radius * Mathf.Deg2Rad * 360;

        Mesh mesh = new();

        Vector3[] vertices = new Vector3[radiusDivision + 2];
        Vector2[] uvs = new Vector2[radiusDivision + 2];
        vertices[0] = new(0f, 1f, 0f);
        uvs[0] = new(0f, 0f);
        for (int i = 1; i < radiusDivision + 2; i++)
        {
            float sin = Mathf.Sin((i - 1) * radiusRadiants / radiusDivision);
            float cos = Mathf.Cos((i - 1) * radiusRadiants / radiusDivision);
            vertices[i] = new(sin, 1f, cos);
            uvs[i] = new(sin, cos);
        }

        int[] triangles = new int[radiusDivision * 3];

        int triangleIndex = 0;
        for (int i = 1; i < vertices.Length - 1; i++)
        {
            triangles[triangleIndex++] = 0;
            triangles[triangleIndex++] = i;
            triangles[triangleIndex++] = i + 1;
        }

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        return mesh;
    }
}

public enum FloorTypes
{
    Void,
    Floor,
    Deadly
}