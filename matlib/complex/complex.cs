// (C) 2020 Dmitri Fedorov; License: GNU GPL v3+; no warranty.

using System;
using SM=System.Math;
using System.Globalization;

public partial struct complex{

// data
private double re, im; // defines the two doubles (real and imaginary) unique to each instance of the class

// getters
public double Re {get{return re;}} // method for a complex number z. Yields the double re by the syntax "z.Re" f.eks
public double Im {get{return im;}}

// constructors
public complex(double x){ this.re = x; this.im = 0; } // "maps" a double x to the complex x (im value is 0)
public complex(double x, double y){ this.re=x; this.im=y; } // "maps" a double x to the reals and a double y to the imagainary axis

// cast
public static implicit operator complex(double x){return new complex(x);} // instead of writing "complex z = new complex(x)" this allows for "complex z = complex(x)" ?
public static implicit operator complex(int x){return new complex((double)x);} // Same as above. However the implicit casting (double) makes sure that the integer is treated without any roundings

public static double real(complex z) => z.Re; // complex function looking for trouble. (Another way of getting the real value of z)
public static double imag(complex z) => z.Im;

// useful numbers
public static readonly complex Zero = new complex(0,0); // defines what Zero means in terms of the complex class
public static readonly complex One  = new complex(1,0); // similar to the above
public static readonly complex I    = new complex(0,1);
public static readonly complex NaN  = new complex(double.NaN,double.NaN);

// operator~
public static complex operator ~(complex a){

	return new complex(a.Re,-a.Im); // complex conjugate operator

}

public complex conj(){ return new complex(this.Re,-this.Im);} // complex conjugate method

// operator+
public static complex operator +(complex a){return a;} // lol, wat. Overloading just bcuz?

public static complex operator +(complex a, double b){ // defines what it means to add a complex valued number to a double valued number

	return new complex(a.Re+b,a.Im);

}

public static complex operator +(double a, complex b){ // Defines double + complex

	return new complex(a+b.Re,b.Im);

}

public static complex operator +(complex a, complex b){ // defines addition of two complex valued numbers

	return new complex(a.Re+b.Re,a.Im+b.Im);

}

// operator-
public static complex operator-(complex a){ // Defines the - operation on the complex class ---> sets a complex z re and im values to -re and -im 

	return new complex(-a.Re,-a.Im); 

}

public static complex operator-(complex a, double b){ // defines substraction of a complex a with a double b

	return new complex(a.Re-b, a.Im); 

}

public static complex operator-(double a, complex b){ // defines subtraction of a double a with a complex b
	
	return new complex(a-b.Re, -b.Im); 

}

public static complex operator-(complex a, complex b){ // Defines subtraction of two complex numbers

	return new complex(a.Re-b.Re, a.Im-b.Im); 

}

// operator*
public static complex operator*(complex a, double b){ // Defines multiplication of a complex a with a double b 

	return new complex(a.Re*b, a.Im*b); 

}

public static complex operator*(double a, complex b){ // Same as above, but for mirrored syntax

	return new complex(a*b.Re, a*b.Im); 

}

public static complex operator*(complex a, complex b){ // Defines multiplication for two complex numbers

	return new complex(a.Re*b.Re-a.Im*b.Im, a.Re*b.Im+a.Im*b.Re); 

}

// argument
public static double argument(complex z){ // returns the angle (polar coordinates) of the complex number

	return Math.Atan2(z.Im,z.Re); // Atan2(y,x) correctly handles 0 values in either arguments

}

// magnitude
public static double magnitude(complex z){ 

	double zr=Math.Abs(z.Re),zi=Math.Abs(z.Im),r,t; // defines 4 doubles?

	if(zr>zi){t=zi/zr; r=zr*Math.Sqrt(1+t*t);} // since t<1, the square root is faster/more precise?
	else     {t=zr/zi; r=zi*Math.Sqrt(1+t*t);}
	return r;

}

// operator/
public static complex operator / (complex a, complex b){ // Division of two complex numbers

	double ar=a.Re, ai=a.Im, br=b.Re, bi=b.Im; // new doubles to keep track of real and imaginary values

	double s = 1.0/magnitude(b); 

	double sbr = s*br, sbi=s*bi; // Why is this easier? a/b = (1.0/|b|^2)*(a*b.conj())

	double zr = (ar * sbr + ai * sbi) * s;
	double zi = (ai * sbr - ar * sbi) * s;

	return new complex(zr,zi); // correct but should be built off of known operations

}

public static complex operator / (complex a, double x){ // what it means to divide a complex by a double

	return new complex(a.Re/x,a.Im/x); 

}

// Maybe also make a / operation for double divided by complex?

// ToString

static readonly string cformat="{0:g3}+{1:g3}i"; // string writing format

public override string ToString(){ // Is used when a complex class number is called for writing (else it will simply write "complex")

	return String.Format(
		CultureInfo.CurrentCulture,
		cformat, this.Re, this.Im
	);

}

public string ToString(string format){ // change format on the real and imaginary numbers?

	return String.Format(
		CultureInfo.CurrentCulture,
		cformat,
		this.Re.ToString(format, CultureInfo.CurrentCulture),
		this.Im.ToString(format, CultureInfo.CurrentCulture)
	);

}
public string ToString(IFormatProvider provider){ // Similar to ToString(), exept "CultureInfo.CurrentCulture" can be changed in the argument

	return String.Format(
		provider,
		cformat, this.Re, this.Im
	);

}

public string ToString(string format, IFormatProvider provider){ // combination of the two just above ToString() functions

	return String.Format(
		provider,
		cformat,
		this.Re.ToString(format, provider),
		this.Im.ToString(format, provider)
	);

}

// bool

public static bool approx(double a, double b, double abserr=1e-9, double relerr=1e-9){

	double d = Math.Abs(a-b), s = Math.Abs(a) + Math.Abs(b); // absolute distrance between doubles, d, and combined length, s.

	if( d< abserr ) return true; // checks for flat uncertainty
	else if ( d/s < relerr/2 ) return true; // NOT a relative error per. Say. A "true" relative error check can be found in f.Eks the "matrices" file from matlib in this course. 
	else return false; 			// the relerr is used to check for if the average (s/2) of the absolute values is within ( 100% +- eps ) of a or b.

}

public static bool approx(double a, complex b, double abserr=1e-9, double relerr=1e-9){ // approx function for complex and double

	return approx(a,b.Re) && approx(0,b.Im); // runs the approx piecewise 

}

public bool approx(complex b, double abserr=1e-9, double relerr=1e-9){ // method to be run on a complex z with another complex number: z.approx(c)

	return approx(this.Re,b.Re) && approx(this.Im,b.Im); //piecewise comparison

	}
public bool Equals(complex b){

	return this.Re.Equals(b.Re) && this.Im.Equals(b.Im);

}
public override bool Equals(System.Object obj){ // Making implicit conversion to complex?

      if (obj is complex)
      {
         complex b = (complex)obj;
         return this.Equals(b);
      }
      else { return false; }
}

public override int GetHashCode(){

   throw new System.NotImplementedException("complex.GetHashCode() is not implemented." );

}

}// complex