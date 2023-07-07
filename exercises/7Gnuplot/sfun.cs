using System;
using static System.Console;
using static System.Math;

public static class sfun{
	
	public static double erf(double x){ /// single precision error function (Abramowitz and Stegun, from Wikipedia)

		if(x<0) return -erf(-x); // the error function is odd

		double[] a={0.254829592,-0.284496736,1.421413741,-1.453152027,1.061405429}; // some derived coefficients
		double t = 1/(1+0.3275911*x); // associated expansion function

		double sum = t*(a[0]+t*(a[1]+t*(a[2]+t*(a[3]+t*a[4])))); // expansion

		return 1 - sum*Exp(-x*x) ;

	} //erf

	public static double gamma(double x){ ///single precision gamma function (Gergo Nemes, from Wikipedia)

		if(x<0)return PI/Sin(PI*x)/gamma(1-x); // Euler's reflection formula?
		if(x<9)return gamma(x+1)/x; // gamma func. Property --> stirling approx works better for larger x.

		double lngamma=x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2; // stirling approx

		return Exp(lngamma); // return value

	} //gamma

	public static double lngamma(double x){ // I implemented a similar function in an earlier exercise

		if(x<=0) throw new ArgumentException("lngamma: x<=0"); // Avoids negative gamma values --> x>0 => gamma>0
		if(x<9) return lngamma(x+1)-Log(x); // gamma propery together with logarithm property

		return x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2; // stirling approx returns logarithm directly

	} //lngamma

	
	
} //sfun
