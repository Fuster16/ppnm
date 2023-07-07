using System;
using static System.Math;

public static partial class cmath{

public static readonly complex I = new complex(0,1); // imaginary unit

public static double   arg(complex z) => Math.Atan2(z.Im,z.Re); // also found in the complex class

public static complex  exp(complex z)
	=> Math.Exp(z.Re)*(Math.Cos(z.Im)+I*Math.Sin(z.Im)); // defines complex exponential function

// Defines complex functions for arg(z) in the interval [0,2pi)
public static complex  sin(complex z) => (exp(I*z)-exp(-I*z))/2/I;
public static complex  cos(complex z) => (exp(I*z)+exp(-I*z))/2;
public static complex  log(complex z) => Math.Log(abs(z))+I*arg(z);
public static complex sqrt(complex z) => Math.Sqrt(abs(z))*exp(I*arg(z)/2);

public static double   abs(complex z){

	double x=Math.Abs(z.Re),y=Math.Abs(z.Im); //magnitudes

	if(x==0 && y==0) return 0; // length is identical 0

	else if(x>y){ double t=y/x; return x*Math.Sqrt(1+t*t); } // Pythagoras. Changed sqrt to Math.Sqrt?
	else        { double t=x/y; return y*sqrt(t*t+1); } // changed it back due to overload on doubles below

}

public static complex pow(this complex z, double x ) => exp(log(z)*x); // From definition --> a^x = e^(ln(a^x)) = e^(x*ln(a))
public static complex pow(this complex z, complex w) => exp(log(z)*w);

// Overload of operation to usual functions on the reals
public static double exp(double x){return Math.Exp(x);} 
public static double sin(double x){return Math.Sin(x);}
public static double cos(double x){return Math.Cos(x);}
public static double abs(double x){return Math.Abs(x);}
public static double log(double x){return Math.Log(x);}
public static double sqrt(double x){return Math.Sqrt(x);}
public static double pow(this double x, double y){return Math.Pow (x,y);}
public static double pow(this double x, int n   ){return Math.Pow (x,n);}

public static bool approx(this double x, double y, double abserr=1e-9, double relerr=1e-9){

	return complex.approx(x,y,abserr,relerr); // fetches the approx function from "complex.cs"

}

public static bool approx(this double x, complex y, double abserr=1e-9, double relerr=1e-9){

	return complex.approx(x,y,abserr,relerr); // approx is overloaded in the "complex.cs" and thus supports complex in second argument

}

// xtenstions
public static void print(this double x,string s){ Console.WriteLine(s+x); }
public static void print(this double x)         { x.print(""); }
public static void print(this complex z)
	{Console.WriteLine("({0},{1})",z.Re,z.Im);}
public static void print(this complex z, string s)
	{Console.Write(s);z.print();}
public static void printf(this complex z,string s)
	{Console.WriteLine(s,z.Re,z.Im);}

}// cmath