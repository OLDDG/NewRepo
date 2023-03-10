//------------------------------------------------------------------
// (c) Copywrite Jianzhong Zhang
// This code is under The Code Project Open License
// Please read the attached license document before using this class
//------------------------------------------------------------------

// class for a single color 3d mesh model.
// version 0.1


using System.Windows.Media.Media3D;
using System;

namespace WPFChart3D
{
    public class Mesh3D
    {
        protected Point3D [] m_points;                              // x, y, z coordinate of the vertex
        protected int[] m_vertIndices;                              // indices in the Positions array
        protected System.Windows.Media.Color m_color;               // mesh color
        protected Triangle3D[] m_tris;                              // triangle information

        public double m_xMin, m_xMax, m_yMin, m_yMax, m_zMin, m_zMax;// data range

        // get number of the vertex in this mesh
        public int GetVertexNo()
        {
            if (m_points == null) return 0;
            return m_points.Length;
        }

        // set number of vertex in this mesh
        public virtual void SetVertexNo(int nSize)
        {
            m_points = new Point3D[nSize];
            m_vertIndices = new int[nSize];
        }

        // get number of triangle in this mesh
        public int GetTriangleNo()
        {
            if (m_tris == null) return 0;
            return m_tris.Length;
        }

        // set number of triangle in this mesh
        public void SetTriangleNo(int nSize)
        {
            m_tris = new Triangle3D[nSize];
        }

        // set number of vertex and triangle in this array
        public virtual void SetSize(int nVertexNo, int nTriangleNo)
        {
            SetVertexNo(nVertexNo);
            SetTriangleNo(nTriangleNo);
        }

        // get position of one vertex in this mesh
        public Point3D GetPoint(int n)
        {
            return m_points[n];
        }

        // set position of one vertex in this mesh
        public void SetPoint(int n, Point3D pt)
        {
            m_points[n] = pt;
        }

        public void SetPoint(int n, double x, double y, double z)
        {
            m_points[n] = new Point3D(x, y, z);
        }

        // get a triangle of this mesh
        public Triangle3D GetTriangle(int n)
        {
            return m_tris[n];
        }

        // set a triangle in this mesh
        public void SetTriangle(int n, Triangle3D triangle)
        {
            m_tris[n] = triangle;
        }

        public void SetTriangle(int i, int m0, int m1, int m2)
        {
            m_tris[i] = new Triangle3D(m0, m1, m2);
        }

        // get normal direction of a triangle
        public Vector3D GetTriangleNormal(int n)
        {
            Triangle3D tri = GetTriangle(n);
            Point3D pt0 = GetPoint(tri.n0); 
            Point3D pt1 = GetPoint(tri.n1); 
            Point3D pt2 = GetPoint(tri.n2);

            double dx1 = pt1.X - pt0.X;
            double dy1 = pt1.Y - pt0.Y;
            double dz1 = pt1.Z - pt0.Z;

            double dx2 = pt2.X - pt0.X;
            double dy2 = pt2.Y - pt0.Y;
            double dz2 = pt2.Z - pt0.Z;

            double vx = dy1*dz2 - dz1*dy2;
            double vy = dz1*dx2 - dx1*dz2;
            double vz = dx1*dy2 - dy1*dx2;

            double length = Math.Sqrt(vx*vx + vy*vy + vz*vz);

            return new Vector3D(vx / length, vy / length, vz / length);
        }

        // get the color of a vertex (all the same for this class, but different in child class)
        public virtual System.Windows.Media.Color GetColor(int nV)
        {
            return m_color;
        }

        // set the color of this mesh
        public void SetColor(Byte r, Byte g, Byte b)
        {
            m_color = System.Windows.Media.Color.FromRgb(r, g, b);
        }

        public void SetColor(System.Windows.Media.Color color)
        {
            m_color = color;
        }

        // after we change the location of the mesh, use this function to update the display
        public void UpdatePositions(MeshGeometry3D meshGeometry)
        {
            int nVertNo = GetVertexNo();
            for (int i = 0; i < nVertNo; i++)
            {
                meshGeometry.Positions[i] = m_points[i];
            }
        }

        // Set the test model
        public virtual void SetTestModel()
        {
            double size = 10;
            SetSize(3, 1);
            SetPoint(0, -0.5, 0, 0);
            SetPoint(1, 0.5, 0.5, 0.3);
            SetPoint(2, 0, 0.5, 0);
            SetTriangle(0, 0, 2, 1);
            m_xMin = 0; m_xMax = 2*size;
            m_yMin = 0; m_yMax = size;
            m_zMin = -size; m_zMax = size;
        }
 
    }
}
