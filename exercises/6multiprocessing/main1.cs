using System;
using static System.Console;
using static System.Math;
using System.Threading.Tasks;

public static class main1{
	
	public static void Main(){ // The main executable takes a string denoted "args" as argument from the command line

		int N = (int)Math.Pow(10,8);
		double sum=0; 
		Parallel.For( 1, N+1, delegate(int i){sum+=1.0/i;} );
	
		WriteLine($"{sum} with Parallel.For");

} //Main
	
} //main1
