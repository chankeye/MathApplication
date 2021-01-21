using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MathApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var circle = new Circle { Radius = 5 };
            Console.WriteLine($"Name:{circle.Name},Area:{circle.SurfaceArea()},Perimeter:{circle.Perimeter()}");
            var triangle = new Triangle { ASideLength = 3, BSideLength = 4, CSideLength = 5 };
            Console.WriteLine($"Name:{triangle.Name},Area:{triangle.SurfaceArea()},Perimeter:{triangle.Perimeter()}");
            var square = new Quadrilateral { Width = 5, Height = 5 };
            Console.WriteLine($"Name:{square.Name},Area:{square.SurfaceArea()},Perimeter:{square.Perimeter()}");
            var rectangle = new Quadrilateral { Width = 5, Height = 6 };
            Console.WriteLine($"Name:{rectangle.Name},Area:{rectangle.SurfaceArea()},Perimeter:{rectangle.Perimeter()}");

            var shapes = new Shape[] { circle, triangle, square, rectangle };
            shapes = ShapesSortByArea(shapes);
            var sortedShapges = string.Empty;
            foreach (var item in shapes)
            {
                sortedShapges = sortedShapges + item.Name + ",";
            }
            Console.WriteLine($"ShapeSortByArea:{sortedShapges}");

            shapes = ShapesSortByPerimeter(shapes);
            sortedShapges = string.Empty;
            foreach (var item in shapes)
            {
                sortedShapges = sortedShapges + item.Name + ",";
            }
            Console.WriteLine($"ShapeSortByArea:{sortedShapges}");

            var jsonFilePath = @"D:\MathTest.json";
            ShapesSerialize(shapes, jsonFilePath);
            Console.WriteLine($"Export Shapes to {jsonFilePath}");

            var shapeCount = CulShapeCount();
            Console.WriteLine($"Circle:{shapeCount.CircleCount},Triangle:{shapeCount.TriangleCount},Quadrilateral:{shapeCount.QuadrilateralCount}");
        }

        public static Shape[] ShapesSortByArea(Shape[] shapes)
        {
            for (var i = 0; i < shapes.Count(); i++)
            {
                for (int j = 0; j < shapes.Count() - 1 - i; j++)
                {
                    if (shapes[j].SurfaceArea() > shapes[j + 1].SurfaceArea())
                    {
                        var temp = shapes[j + 1];
                        shapes[j + 1] = shapes[j];
                        shapes[j] = temp;
                    }
                }
            }

            return shapes;
        }

        public static Shape[] ShapesSortByPerimeter(Shape[] shapes)
        {
            for (var i = 0; i < shapes.Count(); i++)
            {
                for (int j = 0; j < shapes.Count() - 1 - i; j++)
                {
                    if (shapes[j].Perimeter() > shapes[j + 1].Perimeter())
                    {
                        var temp = shapes[j + 1];
                        shapes[j + 1] = shapes[j];
                        shapes[j] = temp;
                    }
                }
            }

            return shapes;
        }

        public static bool ShapesSerialize(Shape[] shapes, string filePath)
        {
            var jsonString = string.Empty;
            foreach (var item in shapes)
            {
                var objectType = item.GetType();
                if (objectType == typeof(Triangle))
                    jsonString = jsonString + JsonSerializer.Serialize(item as Triangle) + ",";
                else if (objectType == typeof(Circle))
                    jsonString = jsonString + JsonSerializer.Serialize(item as Circle) + ",";
                if (objectType == typeof(Quadrilateral))
                    jsonString = jsonString + JsonSerializer.Serialize(item as Quadrilateral) + ",";
            }
            try
            {
                jsonString = "[" + jsonString.TrimEnd(',') + "]";
                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Save file to disk failed!!");
                return false;
            }

            return true;
        }

        public class ShapeCount
        {
            public int CircleCount { get; set; }
            public int TriangleCount { get; set; }
            public int QuadrilateralCount { get; set; }
        }

        public static ShapeCount CulShapeCount()
        {
            return new ShapeCount
            {
                CircleCount = Circle.instanceCount,
                TriangleCount = Triangle.Count,
                QuadrilateralCount = Quadrilateral.Count
            };
        }
    }
}
