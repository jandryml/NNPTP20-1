using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace INPTPZ1
{
	namespace Mathematics
	{
		class Polynom
		{
			public List<Complex> ComplexNumberList { get; private set; } = new List<Complex>();

			public Polynom Derive()
			{
				Polynom polynom = new Polynom();
				for (int i = 1; i < ComplexNumberList.Count; i++)
				{
					polynom.ComplexNumberList.Add(ComplexNumberList[i] * new Complex(i, 0));
				}
				return polynom;
			}

			public Complex Evaluate(Complex number)
			{
				Complex resultValue = Complex.Zero;
				for (int i = 0; i < ComplexNumberList.Count; i++)
				{
					Complex coeficient = ComplexNumberList[i];

					if (i > 0)
					{
						coeficient *= Complex.Pow(number, i);
					}
					resultValue += coeficient;
				}
				return resultValue;
			}

			public override string ToString()
			{
				StringBuilder sb = new StringBuilder();
				for (int i = 0; i < ComplexNumberList.Count; i++)
				{
					if (i > 0)
					{
						sb.Append(" + ");
					}
					sb.Append(ComplexNumberList[i]);
					sb.Append("x^" + i);
				}
				return sb.ToString();
			}
		}
	}
}
