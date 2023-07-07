using System;
using static System.Console;
using static System.Math;

public static class main{

	public static void Main(){
	
	WriteLine($"\nExercise complex \n4.Calculate sqrt(-1),sqrt(i), e^i,e^(i*pi), i^i, ln(i) and sin(i*pi)");

	WriteLine($"\nThe calculations are found to yield:");

	complex a = complex.One;
	complex b = complex.I;

	complex z1 = cmath.sqrt(-a);

	complex z2 = cmath.sqrt(b);

	complex z3 = cmath.exp(b);

	complex z4 = cmath.exp(Math.PI*b);

	complex z5 = cmath.log(b);
	
	complex z6 = cmath.sin(b*Math.PI);

	complex z7 = cmath.pow(b,b);

	double c = -1*(Math.Exp(-1*Math.PI) -  Math.Exp(Math.PI))/2;

	WriteLine($"sqrt(-1) = {z1} and should be equal to i");
	WriteLine($"sqrt(i) = {z2} and should be equal to e^(0.5*ln(i)) = e^(i*pi/4) = cos(pi/4) + i*sin(pi/4) = (1 + i)/sqrt(2)");
	WriteLine($"e^i = {z3} and should be equal to cos(1) + i*sin(1) = 0.54 + i*0.84");
	WriteLine($"e^(pi*i) = {z4} and should be equal to cos(pi) + i*sin(pi) = -1 + 0i");
	WriteLine($"i^i = {z7} should be equal to e^(i*ln(i)) = e^(-pi/2) = 0.208");
	WriteLine($"ln(i) = {z5} and should be equal to ln(e^(ipi/2)) = ipi/2 = i*1.57");
	WriteLine($"sin(i*pi) = {z6} and should be equal to (exp(i*i*pi)-exp(-i*i*pi))/2/i = i(-1)*(e^(-pi) - e^(pi))/2");
	WriteLine($"(-1)*(e^(-pi) - e^(pi))/2 is found from Math on doubles to yield {c}");

	} // Main
} //main
