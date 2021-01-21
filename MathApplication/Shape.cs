using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathApplication
{
    public abstract class Shape
    {
        public abstract string Name { get; }
        public virtual double Perimeter() { return 0; }
        public virtual double SurfaceArea() { return 0; }
    }

    public class Circle : Shape
    {
        public static int Count { get; private set; }
        public Circle()
        {
            Count++;
        }
        ~Circle()
        {
            Count--;
        }

        const float Pi = 3.14f;
        public override string Name { get { return "Circle"; } }
        public double Radius { get; set; }
        public override double Perimeter()
        {
            return 2 * Radius * Pi;
        }
        public override double SurfaceArea()
        {
            return Radius * Radius * Pi;
        }
    }

    public class Triangle : Shape
    {
        public static int Count { get; private set; }
        public Triangle()
        {
            Count++;
        }
        ~Triangle()
        {
            Count--;
        }
        public override string Name
        {
            get
            {
                if (ASideLength == BSideLength && BSideLength == CSideLength)
                    return "Equilateral";
                else if (ASideLength != BSideLength && ASideLength != CSideLength && BSideLength != CSideLength)
                    return "Scalene";
                else
                    return "Isosceles";
            }
        }
        public double ASideLength { get; set; }
        public double BSideLength { get; set; }
        public double CSideLength { get; set; }
        public override double Perimeter()
        {
            return ASideLength + BSideLength + CSideLength;
        }
        public override double SurfaceArea()
        {
            return Math.Sqrt(Perimeter() / 2 * (Perimeter() / 2 - ASideLength) * (Perimeter() / 2 - BSideLength) * (Perimeter() / 2 - CSideLength));
        }
    }

    public class Quadrilateral : Shape
    {
        public static int Count { get; private set; }
        public Quadrilateral()
        {
            Count++;
        }
        ~Quadrilateral()
        {
            Count--;
        }
        public override string Name
        {
            get
            {
                if (Width == Height)
                    return "Square";
                else
                    return "Rectangle";
            }
        }
        public double Width { get; set; }
        public double Height { get; set; }
        public override double Perimeter()
        {
            return (Width + Height) * 2;
        }
        public override double SurfaceArea()
        {
            return Width * Height;
        }
    }
}
