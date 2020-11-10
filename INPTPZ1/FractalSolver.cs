using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using INPTPZ1.Mathematics;

namespace INPTPZ1
{
	class FractalSolver
	{
		private const int MAX_ITERATIONS = 30;
		private const double MIN_VALUE = 0.0001;
		private const double ROOT_TOLERANCE = 0.01;
		private const double PRECISION = 0.5;
		private readonly string outputPath;
		private PointF minimum;
		private PointF maximum;
		private readonly ImageHandler imageHandler;
		private readonly ResolutionHolder resolutionHolder;
		private Complex initialComplex;

		public FractalSolver(ResolutionHolder resolutionHolder, PointF minimum, PointF maximum, string outputPath)
		{
			this.resolutionHolder = resolutionHolder;
			imageHandler = new ImageHandler(resolutionHolder);
			this.minimum = minimum;
			this.maximum = maximum;
			this.outputPath = outputPath;
		}

		internal void Solve()
		{
			List<Complex> rootList = new List<Complex>();

			Polynom originalPolynom = InitOriginalPolynom();
			Polynom derivedPolynom = originalPolynom.Derive();

			double xstep = (maximum.X - minimum.X) / resolutionHolder.Width;
			double ystep = (maximum.Y - minimum.Y) / resolutionHolder.Height;

			for (int xIndex = 0; xIndex < resolutionHolder.Width; xIndex++)
			{
				for (int yIndex = 0; yIndex < resolutionHolder.Height; yIndex++)
				{
					double y = minimum.Y + xIndex * ystep;
					double x = minimum.X + yIndex * xstep;

					x = x == 0 ? MIN_VALUE : x;
					y = y == 0 ? MIN_VALUE : y;

					initialComplex = new Complex(x, y);

					int iteration = FindSolutionByNewtonIteration(originalPolynom, derivedPolynom);

					imageHandler.SetPixel(xIndex, yIndex, ImageHandler.GetColorByParams(iteration, GetRootCount(rootList)));
				}
			}
			imageHandler.SaveImage(outputPath);
		}

		private int FindSolutionByNewtonIteration(Polynom originalPolynom, Polynom derivedPolynom )
		{
			int iteration = 0;
			for (int i = 0; i < MAX_ITERATIONS; i++)
			{
				var diff = originalPolynom.Evaluate(initialComplex) / (derivedPolynom.Evaluate(initialComplex));
				initialComplex -= diff;

				if (Math.Pow(diff.Real, 2) + Math.Pow(diff.Imaginary, 2) >= PRECISION)
				{
					i--;
				}
				iteration++;
			}
			return iteration;
		}

		private int GetRootCount(List<Complex> rootList)
		{
			var known = false;
			int rootCount = 0;
			for (int i = 0; i < rootList.Count; i++)
			{
				if (IsKnownRoot(rootList[i]))
				{
					known = true;
					rootCount = i;
				}
			}
			if (!known)
			{
				rootList.Add(initialComplex);
				rootCount = rootList.Count;
			}

			return rootCount;
		}

		private bool IsKnownRoot(Complex koren)
		{
			return Math.Pow(initialComplex.Real - koren.Real, 2) + Math.Pow(initialComplex.Imaginary - koren.Imaginary, 2) <= ROOT_TOLERANCE;
		}

		private Polynom InitOriginalPolynom()
		{
			Polynom originalPolynom = new Polynom();
			originalPolynom.ComplexNumberList.Add(new Complex(1, 0));
			originalPolynom.ComplexNumberList.Add(Complex.Zero);
			originalPolynom.ComplexNumberList.Add(Complex.Zero);
			originalPolynom.ComplexNumberList.Add(new Complex(1, 0));
			return originalPolynom;
		}
	}
}
