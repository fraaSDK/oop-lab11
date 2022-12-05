using System.Text;

namespace ComplexAlgebra
{
    using System;
    /// <summary>
    /// A type for representing Complex numbers.
    /// </summary>
    ///
    /// TODO: Model Complex numbers in an object-oriented way and implement this class.
    /// TODO: In other words, you must provide a means for:
    /// TODO: * instantiating complex numbers
    /// TODO: * accessing a complex number's real, and imaginary parts
    /// TODO: * accessing a complex number's modulus, and phase
    /// TODO: * complementing a complex number
    /// TODO: * summing up or subtracting two complex numbers
    /// TODO: * representing a complex number as a string or the form Re +/- iIm
    /// TODO:     - e.g. via the ToString() method
    /// TODO: * checking whether two complex numbers are equal or not
    /// TODO:     - e.g. via the Equals(object) method
    public class Complex
    {
        public double Real { get; }
        public double Imaginary { get; }

        public Complex(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public double Modulus => Math.Sqrt(Math.Pow(Real, 2) + Math.Pow(Imaginary, 2));

        public double Phase => Math.Atan2(Imaginary, Real);

        public Complex Complement() => new Complex(Real, -Imaginary);

        public Complex Plus(Complex n) => new Complex(Real + n.Real, Imaginary + n.Imaginary);

        public Complex Minus(Complex n) => new Complex(Real - n.Real, Imaginary - n.Imaginary);

        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != GetType())
            {
                return false;
            }

            var testObj = obj as Complex;
            return Real.CompareTo(testObj?.Real) == 0 && Imaginary.CompareTo(testObj?.Imaginary) == 0;
        }

        public override int GetHashCode() => HashCode.Combine(Real, Imaginary);
        
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            double absImaginary = Math.Abs(Imaginary);

            if (Equals(new Complex(0, 0)))
            {
                return "0";
            }
            
            if (!Real.Equals(0))
            {
                stringBuilder.Append($"{Real}");
            }

            if (!Imaginary.Equals(0))
            {
                stringBuilder.Append($" {GetComplexOperationSign} {absImaginary}");
                
                // Removing excessive white-spaces or redundant operators.
                if (Real.Equals(0))
                {
                    stringBuilder.Replace(" - ", "-");
                    stringBuilder.Replace(" + ", "");
                }
            }
            
            return stringBuilder.ToString();
        }

        private string GetComplexOperationSign => Imaginary > 0 ? "+" : "-";
    }
}