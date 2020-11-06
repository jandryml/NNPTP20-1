using System;
using System.Drawing;

namespace INPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //Tuple with resolution width and height
            ResolutionHolder resolutionHolder = new ResolutionHolder(int.Parse(args[0]), int.Parse(args[1]));

            PointF minimum = new PointF(float.Parse(args[2]), float.Parse(args[4]));
            PointF maximum = new PointF(float.Parse(args[3]), float.Parse(args[5]));
            string outputPath = args[6];

            FractalSolver fractalSolver = new FractalSolver(resolutionHolder, minimum, maximum, outputPath);

            fractalSolver.Solve();
        }
    }
}
