using System;
using static System.Console;
using static System.Math;

public static class sfun{
	
	public static double ucirc(vector y){ // Unit circle ---> function which yields 1 if norm of y is less than 1. Returns 0 if not
					      // Function takes n-dim vectors and is functionally an n-dim unit sphere function

		if(y.norm() > 1) return 0;

		return 1;

	} //ucirc

	public static double asin(vector y, double a, double b){ // area between sine wave and x-axis from a to b (should yield zero if b-a = n*2pi).
	
		if( y[0] > b || a > y[0]) return 0; // checks for interval

		double p = y[1]*Sin(y[0]); // + if same polarity - if not

		if( p < 0) return 0; // checks polarity
		// Proceeds if p > 0
		if( p > Sin(y[0])*Sin(y[0]) ) return 0; // checks if between sine and axis (by polarity change): (Sin(y[0]) - y[1])*Sin(y[0]) > 0 must be true in order to lie between
		// proceeds if p < Sin(y[0])*Sin(y[0])
		
		return Sign(y[1]); // returns -1 or 1 (or 0 if == 0)

	} // asin


	public static double esin(vector y, double c, double a, double b){ // area between elevated sine wave and x-axis from a to b (should yield c if b-a = n*2pi and c > 1).
	
		if( y[0] > b || a > y[0]) return 0; // checks for interval 

		double f = c + Sin(y[0]);		

		double p = y[1]*f; 

		if( p < 0) return 0; // checks polarity
		// Proceeds if p > 0
		if( p > f*f ) return 0; // checks if between f and axis (by polarity change): (f - y[1])*f > 0 must be true in order to lie between
		// proceeds if p < f*f
		
		return Sign(y[1]); // returns -1 or 1 (or 0 if == 0)

	} // esin

	public static double parabola(vector y, vector c, double a, double b){ // area between parabola and x-axis from a to b
	
		if( y[0] > b || a > y[0]) return 0; // checks for interval 

		double f = c[0] + y[0]*(c[1] + y[0]*c[2]); // parabola

		double p = y[1]*f;

		if( p < 0 || f*f < p) return 0; // checks parity and between f and axis
		// Proceeds if p > 0 (same parity) and if p < f*f
		
		return Sign(y[1]); // returns -1 or 1 (or 0 if == 0)

	} // parabola

	public static double integral(vector y, vector a = null, vector b = null){ // A.3 integral

		// We assume y[i] lies in (0,PI) for i = 0,1,2 and y[3] lies in (0,1]

		if(a == null || b == null){ // Lovely boxy interval.

			a = new vector(Pow(2,-4),Pow(2,-4),Pow(2,-4),0);
			b = new vector(PI - Pow(2,-4),PI- Pow(2,-4),PI-Pow(2,-4),200);

		}

		for(int i = 0; i < a.size-1; i++){

			if( y[i] >= b[i] || a[i] >= y[i]) return 0; // checks for interval (we also need to avoid the singularity at cos(x)*cos(y)*cos(z) = 1.

		}

		double f = 1/(1 - Cos(y[0])*Cos(y[1])*Cos(y[2])); // the function is greater than 

		if( f < y[3] ) return 0; //
		
		return 1;

		

	} //int

	public static double corput(int n, int b){ // Takes n (in base 10) and b as desired base for corput
	
		double q = 0, bk = 1.0/b; // The sum (q) starts at 0 and bk is 1/b for the first iteration (k=1)

		while(n>0){ // continues 
			
			q += (n % b)*bk; // % is the modulo operator (It takes the highest natural number j where n > j*b --> The modulo is then n - j*b). 
					 // The result inside the parentheses gives, itteratively, the base representation of the given number (we give a natural number n in base 10)
			n /= b; 	 // division between integers ---> Goes to next number in base (ignores remainder)
			bk /= b;	 // Updates bk for next number in base.
		
		}
		
		return q;

	} // corput

	public static bool isprime(int n){ // checks if an integer is a prime

		// Corner case
		if(n <= 1) return false;
 
        	// Check from 2 to sqrt(n)
		for (int i = 2; i < Math.Sqrt(n); i++){

            		if(n % i == 0) return false;

		}
 
        	return true;

	} // isprime

	public static int[] bprimes(int d){ // Gives the first d primes

		int[] primes = new int[d]; // we use primes as bases for the d-dimensions

		int sumd = 0, j = 2;

		while(sumd < d){ // Fills "prime" with the d first primes. 

			if(isprime(j) == true){ // checks 

				primes[sumd] = j;
				sumd += 1;
			}

			j += 1;
	
		}

		return primes;

	} // isprime

	public static vector Halton(int n, int[] b){ // Halton which takes and outputs vectors

		vector H = new vector(b.Length);

		for(int i = 0; i < b.Length; i++){
			
			H[i] = corput(n,b[i]);

		}

		return H;

	} // Halton 

} //sfun
