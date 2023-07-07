using System;
using static System.Console;
using static System.Math;
using System.IO;

public static class Erf{

	public static void Main(){

		// Taking data from Wiki-table

		char[] split_delimiters = {' ','\t','\n'};
		var split_options = StringSplitOptions.RemoveEmptyEntries;

		vector x = new vector(32);
		vector y1 = new vector(32);
		vector y2 = new vector(32);

		int s = 0;
		for( string line = ReadLine(); line != null; line = ReadLine() ){

			var numbers = line.Split(split_delimiters,split_options);
			x[s] = double.Parse(numbers[0]);
			y1[s] = double.Parse(numbers[1]);
			y2[s] = double.Parse(numbers[2]);
			s += 1;
        	}

		// Taking data from Wiki-table

		double erf;
		double erf2;

		for(int i = 0; i< 32;i++){

			erf = adaptive.erf(x[i]);
			erf2 = adaptive.erf2(x[i]);

			WriteLine($"{x[i]} {y1[i]} {erf} {erf2} {Abs(y1[i] - erf)} {Abs(y1[i] - erf2)}");

		}

	}//Main
}//Erf
