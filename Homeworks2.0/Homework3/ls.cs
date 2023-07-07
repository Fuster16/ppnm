using System;
using static System.Console;
using static System.Math;
using System.IO;

public static class ls{

	public static void Main(){

		char[] split_delimiters = {' ','\t','\n'};
		var split_options = StringSplitOptions.RemoveEmptyEntries;

		vector x = new vector(9);
		vector y = new vector(9);
		vector dy = new vector(9);

		int s = 0;
		for( string line = ReadLine(); line != null; line = ReadLine() ){

			var numbers = line.Split(split_delimiters,split_options);
			x[s] = double.Parse(numbers[0]);
			y[s] = double.Parse(numbers[1]);
			dy[s] = double.Parse(numbers[2]);
			s += 1;
        	}

		
		// Preparing for linear regression.
		vector z = y.map( t => Log(t));
		vector dz = new vector(9);

		for(int i = 0; i<9;i++){

			dz[i] = dy[i]/y[i];

 		}
		
		for(int i = 0; i< 9;i++) WriteLine($"{x[i]} {z[i]} {dz[i]}");
		WriteLine(); WriteLine();

		Func<double,double>[] fs = { t => 1.0, t => t };
		
		(vector c, matrix S) t2 = OLSF.lsfit(fs, x, z, dz);

		double[] x_axis = new double[128];
		double step = 16.0/128.0; // actually 1.0/8.0

		for(int i =0; i<128;i++){
			
			x_axis[i] = step*i;
		}

		for(int k = 0; k< 128;k++){
			
			double fit = 0;
			double fit1 = 0;
			double fit2 = 0;

			for(int j = 0; j<fs.Length;j++){ 

				fit += t2.c[j]*fs[j](x_axis[k]);
				fit1 += (t2.c[j] - t2.S[j,j])*fs[j](x_axis[k]);
				fit2 += (t2.c[j] + t2.S[j,j])*fs[j](x_axis[k]);

			}

			WriteLine($"{x_axis[k]} {fit} {fit1} {fit2}");
		}

}//Main

}//ls
