﻿using UnityEngine;
using System.Collections;

namespace Foliage
{
    [RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]
    public class Foliage2D : MonoBehaviour
    {
        #region Private fields
        /// <summary>
        /// Foliage2D_Mesh instance. 
        /// </summary>
        private Foliage2D_Mesh dynamicMesh;
        /// <summary>
        /// The max width and height the mesh can have using the current texture. 
        /// </summary>
        private Vector2 unitsPerUV = Vector2.one;
        /// <summary>
        /// Reference to the mesh component. 
        /// </summary>
        private Mesh mesh;
        /// <summary>
        /// The width of a horizontal segment. 
        /// </summary>
        private float scaleX = 1f;
        /// <summary>
        /// The height of a vertical segment. 
        /// </summary>
        private float scaleY = 1f;
        /// <summary>
        /// The number of horizontal vertices. 
        /// </summary>
        private int hVerts = 1;
        /// <summary>
        /// The number of vertical vertices. 
        /// </summary>
        private int vVerts = 1;
        /// <summary>
        /// Foliage2D_Mesh property. 
        /// </summary>
        private Foliage2D_Mesh DynamicMesh
        {
            get
            {
                if (dynamicMesh == null)
                    dynamicMesh = new Foliage2D_Mesh();
                return dynamicMesh;
            }
        }
        #endregion

        #region Public fields
        /// <summary>
        /// How many pixels should be in one unit of Unity space.
        /// </summary>
        public float pixelsPerUnit = 100f;
        /// <summary>
        /// The width of the foliage mesh. 
        /// </summary>
        public float width = 1.0f;
        /// <summary>
        /// The height of the foliage mesh. 
        /// </summary>
        public float height = 1.0f;
        /// <summary>
        /// The number of columns the mesh has. 
        /// </summary>
        public int widthSegments = 3;
        /// <summary>
        /// The number of rows the mesh has.
        /// </summary>
        public int heightSegments = 3;
        #endregion

        #region Class methods
        /// <summary>
        /// Sets the vertices position and UVs.
        /// </summary>
        public void RebuildMesh()
        {
            DynamicMesh.Clear();

            unitsPerUV.x = GetComponent<Renderer>().sharedMaterial.mainTexture.width / pixelsPerUnit;
            unitsPerUV.y = GetComponent<Renderer>().sharedMaterial.mainTexture.height / pixelsPerUnit;

            width = unitsPerUV.x;
            height = unitsPerUV.y;

            hVerts = widthSegments + 1;
            vVerts = heightSegments + 1;

            scaleX = width / widthSegments;
            scaleY = height / heightSegments;

            float startX = -unitsPerUV.x / 2f;
            float startY = -unitsPerUV.y / 2f;

            for (int y = 0; y < vVerts; y++)
            {
                for (int x = 0; x < hVerts; x++)
                {
                    Vector3 vertPos = new Vector3(x * scaleX + startX, y * scaleY + startY, 0.0f);
                    float U = (vertPos.x / width) + 0.5f;
                    float V = y == 0 ? 0 : (scaleY * y) / height;
                    DynamicMesh.AddVertex(vertPos, 0.0f, new Vector2(U, V));
                }
            }

            DynamicMesh.GenerateTriangles(widthSegments, heightSegments, hVerts);

            mesh = GetComponent<MeshFilter>().sharedMesh;
            string name = GetMeshName();
            if (mesh == null || mesh.name != name)
            {
                mesh = GetComponent<MeshFilter>().sharedMesh = new Mesh();
                mesh.name = name;
            }

            DynamicMesh.Build(ref mesh);
        }

        /// <summary>
        /// Generates a name for the mesh.
        /// </summary>
        private string GetMeshName()
        {
            return "SpriteMesh" + gameObject.GetInstanceID();
        }

        /// <summary>
        /// Sets the initial foliage material.
        /// </summary>
        public void SetDefaultMaterial()
        {
            Renderer rend = GetComponent<Renderer>();
            Material m = Resources.Load("Default_FoliageMaterial", typeof(Material)) as Material;
            if (m != null)
            {
                rend.material = m;

                unitsPerUV.x = GetComponent<Renderer>().sharedMaterial.mainTexture.width / pixelsPerUnit;
                unitsPerUV.y = GetComponent<Renderer>().sharedMaterial.mainTexture.height / pixelsPerUnit;
            }
            else
            {
                Debug.LogWarning("The default material was not found. This happened most likely because you moved Foliage2D from the "
                    + "Assets folder to a subfolder, deleted or renamed the Default_FoliageMaterial from the Resources folder. Click on this "
                    + "message to set the new name of the default material if you renamed it.");
            }
        }
        #endregion
    }
}
