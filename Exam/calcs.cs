using System;
using static System.Console;
using static System.Math;

public static class calcs{
	
	public static string VTS(vector v, string s =""){ // Vector to string

		s += "(";
		for(int i=0;i<v.size;i++){

			s += $" {v[i]} ";

		}
		s += ")";

		return s;
} //VTS

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

		if(n <= 1) return false; // if statement looking for trouble
 
        	// Check from 2 to sqrt(n) (maths)
		for (int i = 2; i < Math.Sqrt(n); i++){

            		if(n % i == 0) return false;

		}
 
        	return true;

} // isprime

	public static vector bprimes(int d){ // Gives the first d primes

		vector primes = new vector(d); // we use primes as bases for the d-dimensions

		int sumd = 0, j = 2;

		while(sumd < d){ // Fills "prime" with the d first primes. 

			if(isprime(j) == true){ // checks if j is prime

				primes[sumd] = j; // j was indeed a prime, and is added to "primes" vector
				sumd += 1; // continues to next index in "primes"
			}

			j += 1; // continues to next integer
	
		}

		return primes;

} // bprimes

	public static vector Halton(int n, vector b){ // Halton which takes and outputs vectors

		vector H = new vector(b.size);

		for(int i = 0; i < b.size; i++){
			
			H[i] = corput(n,(int)b[i]);

		}

		return H;

} // Halton 

} //calcs
