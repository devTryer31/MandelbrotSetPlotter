using System;

namespace MandelbrotSetPlotter.Models {
	internal struct ComplexNumber {

		public double A { get; set; }

		public double B { get; set; } //Imaginary

		public ComplexNumber(double a, double b) {
			A = a;
			B = b;
		}

		public ComplexNumber Square() => new ComplexNumber(A * A - B * B, 2 * B * A);

		public double Abs() => Math.Sqrt(A * A + B * B);

		public static ComplexNumber operator +(ComplexNumber first, ComplexNumber second) =>
			new ComplexNumber(first.A + second.A, first.B + second.B);

	}
}
