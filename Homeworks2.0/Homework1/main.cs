using System;
using static System.Console;
using static System.Math;
using System.IO;

public static class main{

	public static void Main(){

	/////// A

	matrix A = calcs.RRM(7,5);
	string A_string = calcs.MTS(A, "A = ");

	(matrix Q, matrix R) t1 = QRGS.decomp(A);

	string Q_string = calcs.MTS(t1.Q, "Q = ");
	string R_string = calcs.MTS(t1.R, "R = ");
	
	string Id_string = calcs.MTS(((t1.Q).T)*(t1.Q), "Q^T*Q = ");
	string QR_string = calcs.MTS((t1.Q)*(t1.R), "Q*R = ");

	matrix M = calcs.RRM(5,5);
	string M_string = calcs.MTS(M, "M = ");
	
	vector b = calcs.RRV(5);
	string b_string = calcs.VTS(b, "b = ");

	(matrix Q2, matrix R2) t2 = QRGS.decomp(M);
	vector x = QRGS.solve(t2.Q2, t2.R2, b);
	string x_string = calcs.VTS(x, "x = ");

	string Mx_string = calcs.VTS(M*x, "M*x = ");

	/////// A

	/////// B

	matrix A_new = calcs.RRM(5,5);
	string A2_string = calcs.MTS(A_new, "A = ");

	(matrix Q3, matrix R3) t3 = QRGS.decomp(A_new);

	matrix B = QRGS.inverse(t3.Q3, t3.R3);
	string B_string = calcs.MTS(B, "B = ");

	string AB_string = calcs.MTS(A_new*B, "A*B = ");


	/////// B

	string text = File.ReadAllText(@"Homework1.txt");
	WriteLine(text, A_string, Q_string, R_string, Id_string, QR_string, M_string,
			b_string, x_string, Mx_string, A2_string, B_string, AB_string);
	}
}
