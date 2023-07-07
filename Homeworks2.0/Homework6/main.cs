using System;
using static System.Console;
using static System.Math;
using System.IO;

public static class main{

	public static void Main(){

	Func<double,double> f1 = x => Math.Sqrt(x);
	Func<double,double> f2 = x => 1/Math.Sqrt(x);
	Func<double,double> f3 = x => 4*Math.Sqrt(1-x*x);
	Func<double,double> f4 = x => Math.Log(x)/Math.Sqrt(x);

	vector a = new vector(1); // vector has built in "set" function
	vector b= new vector(1);

	var F1 = adaptive.integrate(f1,0,1);
	var F2 = adaptive.integrate(f2,0,1,a);
	var F3 = adaptive.integrate(f3,0,1);
	var F4 = adaptive.integrate(f4,0,1,b);

	vector c= new vector(1);
	vector d= new vector(1);

	var G2 = adaptive.CCintegrate(f2,0,1,c);
	var G4 = adaptive.CCintegrate(f4,0,1,d);

	string text = File.ReadAllText(@"Homework6.txt");
	WriteLine(text,F1,F2,F3,F4,F2,a[0],G2,c[0],F4,b[0],G4,d[0]);

	}
}
