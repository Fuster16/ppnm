using System;
using System.IO;
using static System.Math;
using static System.Console;

public static class test{

	public static void Main(){
	
		///////////// Fetching data from Makfile

		double[] x = new double[11];
		double[] y = new double[11];
	
		char[] split_delimiters = {' ','\t','\n'};
		var split_options = StringSplitOptions.RemoveEmptyEntries;

		int s = 0;
		for( string line = ReadLine(); line != null; line = ReadLine() ){

			var numbers = line.Split(split_delimiters,split_options);
			x[s] = double.Parse(numbers[0]);
			y[s] = double.Parse(numbers[1]);
			s += 1;
        	}		

		for(int i = 0; i<11; i++){
			
			WriteLine($"{x[i]} {y[i]}");

		}
		WriteLine("\n");

		///////////// Fetching data from Makfile

		///////////// A.3 

		double[] z = new double[256];
		double step = 8.0/256.0;
		for(int j = 0; j<z.Length;j++) z[j] = 1 + step*(j+1);

		double[] linterp = spline.linterp(x,y,z);
		double[] lint = spline.linterpInteg(x,y,z);
		
		for(int i = 0; i<256; i++){
			
			WriteLine($"{z[i]} {linterp[i]}");

		}
		WriteLine("\n");

		for(int i = 0; i<256; i++){
			
			WriteLine($"{z[i]} {lint[i]}");

		}
		WriteLine("\n");

		///////////// A.3 

		///////////// B

		double[] qinterp = spline.qinterp(x,y,z);
		double[] qint = spline.qinterpInteg(x,y,z);
		double[] qdiff = spline.qinterpDiff(x,y,z);

		for(int i = 0; i<256; i++){
			
			WriteLine($"{z[i]} {qinterp[i]}");

		}
		WriteLine("\n");

		for(int i = 0; i<256; i++){
			
			WriteLine($"{z[i]} {qint[i]}");

		}
		WriteLine("\n");

		for(int i = 0; i<256; i++){
			
			WriteLine($"{z[i]} {qdiff[i]}");

		}
		WriteLine("\n");

		///////////// B

}//Main

}//test