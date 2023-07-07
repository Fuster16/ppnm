using System;
using static System.Console;
using static System.Math;
using System.IO;

public static class B2{

	public static void Main(string[] args){ // Main function takes -rmax:# -dr:#
						// and spits out associated eigenvalue and dr
	
		vector R = calcs.cmdread(args);
		
		(vector e, matrix f) t = Hatom.Rdiff(R[0],R[1]);

		vector f1 = ((t.f)[0])/Sqrt(R[1]);
		vector f2 = ((t.f)[1])/Sqrt(R[1]);
		vector f3 = ((t.f)[2])/Sqrt(R[1]);

		vector s1 = Hatom.Swave(1,R[0],R[1]);
		vector s2 = Hatom.Swave(2,R[0],R[1]);
		vector s3 = Hatom.Swave(3,R[0],R[1]);
	
		vector r = Hatom.rvalues(R[0],R[1]);

		for(int j = 0; j<r.size;j++){
		
			WriteLine($"{r[j]} {f1[j]} {f2[j]} {f3[j]} {s1[j]} {s2[j]} {s3[j]}");

		}		
		
}//main

}//B2
