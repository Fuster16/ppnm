using System;
using System.IO;
using System.Linq;
using static System.Math;
using static System.Console;

public static class montecarlo{

	public static (double,double) plainmc(Func<vector,double> f,vector a,vector b,int N){ // Spits out (double, double) ---> (Value of integral, statistical uncertainty in integral)
											      // plainmc takes "Func<vector,double> f". The given function has to be pre-made to yield zero if outside of intergration region
											      // "vector a" and "vector b" are Domain vectors ---> simple n-dim cube which must encompas the integration region. 
											      // Int N is the number of samples.

        	int dim = a.size; double V = 1; // Dimension and volume are defined.

		for(int i=0; i < dim; i++){ 

			V *= b[i]-a[i]; // The n-volume of the problem is defined
		}

        	double sum = 0, sum2 = 0; // One for integral value, another for error estimate

		var x = new vector(dim); // Initializes new vector of same length as a and b
		var rnd=new Random(); // random variable

        	for(int i = 0; i < N; i++){ // loops through nr. of given samples

                	for(int k = 0; k < dim; k++){ //loops through each dimension

				x[k] = a[k] + rnd.NextDouble()*(b[k]-a[k]); // For k'th dimension, sets x[k] to a random value within k'th interval

			}

                	double fx = f(x); sum += fx; sum2 += fx*fx; // Function evaluation in rnd point and is given to sum, sum2.

                }

        	double mean = sum/N, sigma = Sqrt(sum2/N - mean*mean); // Plain monte carlo evaluates with even weigth
        	var result = (mean*V,sigma*V/Sqrt(N)); // returns intergral value and eror estimate according to the CLT

        	return result;

} //plain monte carlo

	public static (double,double) quasimc(Func<vector,double> f,vector a,vector b,int N){ // Spits out (double, double) ---> (Value of integral, statistical uncertainty in integral)
											      // montecarlo takes "Func<vector,double> f". The given function has to be pre-made to yield zero if outside of intergration region
											      // "vector a" and "vector b" are Domain vectors ---> simple n-dim cube which must encompas the integration region. 
											      // Int N is the number of samples.

        	int dim = a.size; double V = 1; // Dimension and volume are defined.

		for(int i=0; i < dim; i++){ 

			V *= b[i]-a[i]; // The n-volume of the problem is defined
		}

        	double sum = 0, sum2 = 0; // One for integral value, another for error estimate

		var x1 = new vector(dim); // Initializes new vector of same length as a and b
		var x2 = new vector(dim); // Initializes new vector of same length as a and b

		vector ab = b-a; // creating vector containing the differences for looping over later

		int[] primes = sfun.bprimes(dim); // Bases for the Halton
		int[] primes2 = new int[dim]; // Basis for comparison Halton
		
		var rnd = new Random(); // random variable 
		primes2 = primes.OrderBy(x => rnd.Next()).ToArray(); // Randomly orders the already found primes for a new basis --> new sequence

        	for(int i = 1; i <= N; i++){ // loops through nr. of given samples
			
			vector H1 = sfun.Halton(i,primes); // the Halton grid is generated
			vector H2 = sfun.Halton(i,primes2);

			for(int k=0; k<dim; k++){

				x1[k] = a[k] + H1[k]*ab[k]; // sets x to a quasi-random value within volume
                		x2[k] = a[k] + H2[k]*ab[k];
			}

			double fx1 = f(x1), fx2 = f(x2); 
			sum += fx1; sum2 += fx2; // Function evaluation in quasi-rnd point and is given to sum, sum2.
			
                }

        	double mean = sum/N, mean2 = sum2/N; // Quasi monte carlo evaluates integral with even weight and uncertainty as difference in two quasi-evaluations
        	var result = (mean*V,Abs(mean2 - mean)*V); // returns intergral value and eror estimate

        	return result;

} //quasi-random monte carlo



} //adaptive