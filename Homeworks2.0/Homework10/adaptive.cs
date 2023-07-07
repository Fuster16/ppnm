using System;
using System.IO;
using static System.Math;
using static System.Console;

public static class adaptive{

	public static double integrate(
		Func<double,double> f, // integrand
		double a, double b, // start value a and end value b
		vector c = null,
		double acc=0.001, double eps=0.001, // absolute and relative accuracy goals
		double f2=Double.NaN, double f3=Double.NaN // 
		){

		if(c != null) c[0] +=1;

		double h=b-a; // interval length

		if(Double.IsNaN(f2)){ f2=f(a+2*h/6); f3=f(a+4*h/6); } // first call, no points to reuse

		double f1=f(a+h/6), f4=f(a+5*h/6);
		double Q = (2*f1+f2+f3+2*f4)/6*h; // higher order rule ---> (f1 + (f2 + f3)/2 + f4)*(b-a)/3 retains riemann end intervals and makes a trapez of center interval
		double q = (f1+f2+f3+f4)/4*h; // lower order rule --> classic riemann sum with re-used function calls.
		double err = Abs(Q-q);

		if (err <= acc+eps*Abs(Q)) return Q;
		else return integrate(f,a,(a+b)/2,c,acc/Math.Sqrt(2),eps,f1,f2) +
            		    integrate(f,(a+b)/2,b,c,acc/Math.Sqrt(2),eps,f3,f4); // if error is not up to par with tolerance ---> divide interval into two. 
								 // First half goes from a to (a+b)/2. 
								 // Since h ---> h/2, f1 and f2 carried over according to : f1 = f(a+h/6) --> f(a+h/3) = f(a+2*h/6) 
								 // and f2 = f(a+2*h/6) --> f(a+4*h/6). Thus, f1 --> f2 and f2 --> f3 in the new interval.
								 // Second half goes from (a+b)/2 to b. We thus have h ---> h/2 AND a --> (a+b)/2. 
								 // All in all: f3 = f(a+4*h/6) --->  f(a+8*h/6) = f(a+ h + 2*h/6) = f(a+ (b-a)/2 + 2*h/6) = f((a+b)/2 + 2*h/6)
								 // and: f4=f(a+5*h/6) ---> f(a+10*h/6) = f(a+ h +4*h/6) = f((a+b)/2 + 4*h/6). 
								 // Thus, f3 --> f2 and f4 --> f3 in the new interval.
} //integrate

	public static double erf(double z){

		if(z<0) return -1*erf(-z);

		Func<double,double> f;

		if(1<z){

			 f = t => Math.Exp(-Pow(z+(1-t)/t,2))/t/t;

			return 1 - 2*integrate(f,0,1)/Math.Sqrt(Math.PI);
		}

		f = x => Math.Exp(-x*x);
		
		return 2*integrate(f,0,z)/Math.Sqrt(Math.PI);

} //erf

	public static double erf2(double x){
		
		if(x<0) return -erf(-x);

		double[] a={0.254829592,-0.284496736,1.421413741,-1.453152027,1.061405429};
		double t=1/(1+0.3275911*x);
		double sum=t*(a[0]+t*(a[1]+t*(a[2]+t*(a[3]+t*a[4]))));/* the right thing */
		return 1-sum*Exp(-x*x);

} //erf2	

	public static double CCintegrate(
		Func<double,double> f, // integrand
		double a, double b, // start value a and end value b
		vector c = null,
		double acc=0.001, double eps=0.001, // absolute and relative accuracy goals
		double f2=Double.NaN, double f3=Double.NaN // 
		){

		Func<double,double> f_new = t => f((a+b)/2 + (b-a)*Math.Cos(t)/2)*Math.Sin(t)*(b-a)/2;
		return integrate(f_new,0,Math.PI,c,acc,eps,f2,f3);

} //CCintegrate

} //adaptive