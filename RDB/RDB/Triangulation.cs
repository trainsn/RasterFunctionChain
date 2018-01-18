using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDB
{
    using System;
    using ESRI.ArcGIS.Geometry;

    public partial class Triangulation
    {
        /// <summary>
        /// 根据输入的一系列点计算voronoi图
        /// </summary>
        /// <param name="points">一系列点</param>
        /// <returns>一系列多变形</returns>
        public static IList<IGeometry> GeometryVoronoi(List<IPoint> points)
        {
            // 判断点的数量是否合法
            if (points.Count < 3)
            {
                throw new ArgumentException("Input must be a MultiPoint containing at least three points");
            }

            // 初始化顶点列表
            List<SimplePoint> vertices = new List<SimplePoint>();

            // 加入所有初始提供的点
            for (int i = 0; i < points.Count; i++)
            {
                SimplePoint point = new SimplePoint(points[i].X, points[i].Y);

                // 除掉所有的多点，因为三角剖分算法不支持多点的引入
                if (!vertices.Contains(point))
                {
                    vertices.Add(point);
                }
            }

            // 计算点集的数量，此时应该已经除掉了所有的重复点
            int numPoints = vertices.Count;

            // 判断点的数量是否合法
            if (numPoints < 3)
            {
                throw new ArgumentException("Input must be a list of points containing at least three points");
            }

            // 基于vertices中的顶点x坐标对indices进行sort
            vertices.Sort();

            IPointCollection pointCollection = new MultipointClass();
            foreach (SimplePoint p in vertices)
            {
                pointCollection.AddPoint(new PointClass() { X = p.X, Y = p.Y });
            }

           
            IEnvelope envelope = (pointCollection as IGeometry).Envelope;

            // Width
            double dx = envelope.Width;

            // Height 
            double dy = envelope.Height;

            // Maximum dimension
            double dmax = (dx > dy) ? dx : dy;

            // Centre
            double avgx = ((envelope.XMax - envelope.XMin) / 2) + envelope.XMin;
            double avgy = ((envelope.YMax - envelope.YMin) / 2) + envelope.YMin;

            // Create the points at corners of the supertriangle
            SimplePoint a = new SimplePoint(avgx - (2 * dmax), avgy - dmax);
            SimplePoint b = new SimplePoint(avgx + (2 * dmax), avgy - dmax);
            SimplePoint c = new SimplePoint(avgx, avgy + (2 * dmax));

            // Add the supertriangle vertices to the end of the vertex array
            vertices.Add(a);
            vertices.Add(b);
            vertices.Add(c);

            double radius;
            SimplePoint circumcentre;
            Triangulation.CalculateCircumcircle(a, b, c, out circumcentre, out radius);

            // 确定超级三角形，这个三角形应该包含所有点
            SimpleTriangle superTriangle = new SimpleTriangle(numPoints, numPoints + 1, numPoints + 2, circumcentre, radius);

            // 将超级三角形push到triangles列表
            List<SimpleTriangle> triangles = new List<SimpleTriangle>();
            triangles.Add(superTriangle);

            List<SimpleTriangle> completedTriangles = new List<SimpleTriangle>();

            // 遍历基于indecies顺序的vertices中的每一个点
            for (int i = 0; i < numPoints; i++)
            {
                // 初始化边缓存数组
                List<int[]> edges = new List<int[]>();

                // 遍历temp triangles中的每一个三角形
                for (int j = triangles.Count - 1; j >= 0; j--)
                {
                    // 如果该点在外接圆内
                    if (Distance(triangles[j].CircumCentre, vertices[i]) < triangles[j].Radius)
                    {
                        // 则该三角形不为Delaunay三角形，将三边保存至edge buffer
                        edges.Add(new int[] { triangles[j].A, triangles[j].B });
                        edges.Add(new int[] { triangles[j].B, triangles[j].C });
                        edges.Add(new int[] { triangles[j].C, triangles[j].A });

                        // 在temp中除掉该三角形
                        triangles.RemoveAt(j);
                    }
                    else if (vertices[i].X > triangles[j].CircumCentre.X + triangles[j].Radius)
                    {
                        {
                            completedTriangles.Add(triangles[j]);
                        }

                        triangles.RemoveAt(j);
                    }
                }

                // 对edgebuffer进行去重
                for (int j = edges.Count - 1; j > 0; j--)
                {
                    for (int k = j - 1; k >= 0; k--)
                    {
                        // Compare if this edge match in either direction
                        if (edges[j][0].Equals(edges[k][1]) && edges[j][1].Equals(edges[k][0]))
                        {
                            // 去重
                            edges.RemoveAt(j);
                            edges.RemoveAt(k);

                            // We've removed an item from lower down the list than where j is now, so update j
                            j--;
                            break;
                        }
                    }
                }

                // Create new triangles for the current point
                for (int j = 0; j < edges.Count; j++)
                {
                    Triangulation.CalculateCircumcircle(vertices[edges[j][0]], vertices[edges[j][1]], vertices[i], out circumcentre, out radius);
                    SimpleTriangle t = new SimpleTriangle(edges[j][0], edges[j][1], i, circumcentre, radius);
                    triangles.Add(t);
                }
            }

            // 我们已经完成了三角剖分部分，接下来就是要完成构建voronoi图的过程
            completedTriangles.AddRange(triangles);

            IList<IGeometry> voronoiPolygon = new List<IGeometry>();
            for (var i = 0; i < vertices.Count; i++)
            {
                // 新建一个IGeometry来存放voronoi图
                IPointCollection mp = new MultipointClass();

                // 遍历所有三角形
                foreach (SimpleTriangle tri in completedTriangles)
                {
                    // If the triangle intersects this point
                    if (tri.A == i || tri.B == i || tri.C == i)
                    {
                        mp.AddPoint(new PointClass() { X = tri.CircumCentre.X, Y = tri.CircumCentre.Y });
                    }
                }

                // Create the voronoi polygon from the convex hull of the circumcentres of intersecting triangles
                ITopologicalOperator topologicalOperator = mp as ITopologicalOperator;
                IGeometry polygon = topologicalOperator.ConvexHull();
                topologicalOperator = polygon as ITopologicalOperator;
                IGeometry result = topologicalOperator.Intersect(envelope, esriGeometryDimension.esriGeometry2Dimension);
                if ((result != null) && (!result.IsEmpty))
                {
                    voronoiPolygon.Add(result);
                }
            }

            return voronoiPolygon;
        }
    }
}
