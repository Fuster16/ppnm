using System;
using static System.Console;
using static System.Math;
using System.IO;

public static class B1{

	public static void Main(string[] args){ // Main function takes -rmax:# -dr:#
						// and spits out associated eigenvalue and dr
	
		vector R = calcs.cmdread(args);
		
		(vector e, matrix f) t = Hatom.Rdiff(R[0],R[1]);
		
		WriteLine($"{(t.e)[0]} {R[0]} {R[1]}");
		
		
}//B1

}//b1
