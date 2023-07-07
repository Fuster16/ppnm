using System;
using static System.Console;
using static System.Math;
using System.IO;

public static class main{
	
	public static void Main(){

	// task 1
	
	int whilemax = sfuns.whilemax(1);
	int systemmax = int.MaxValue;
	
	int whilemin = sfuns.whilemin(1);
	int systemmin = int.MinValue; 

	// task 1 

	// task 2
	
	double x = 1.0;
	while( (1.0 + x) != 1.0){

		x/=2;
	}

	x *= 2;

	float y = 1F;
	while((float)(1F + y) != 1F){

		y/=2F;

	}

       	y*=2;
	
	double systemdouble = Pow(2,-52);
	double systemfloat = Pow(2,-23);
	
	int n = (int)1e6;
	double epsilon = Pow(2,-52);
	double tiny = epsilon/2;
	double sumA =0, sumB = 0;

	sumA += 1; // +=1 before itteration 
	for(int i=0;i<n;i++){sumA+= tiny;}
	
	for(int i=0; i<n;i++){sumB += tiny;} 
	sumB += 1; // +=1 after itteration
	
	string S_sumA = $"sumA -1 = {sumA - 1:e} should be {n*tiny:e}";
	string S_sumB = $"sumB -1 = {sumB-1:e} should be {n*tiny:e}";

	double d1 = 0.1 + 0.1 + 0.1 + 0.1 + 0.1 + 0.1 + 0.1 + 0.1;
	double d2 = 8*0.1;

	string Bool_com = $"d1 == d2 ? => {d1 == d2}";	
	
	string Approx_com = $"approx(d1,d2) => {sfuns.approx(d1,d2)}";

	string text = File.ReadAllText(@"exercise3.txt");
	WriteLine(text, whilemax, whilemin, systemmax, systemmin,x,y,systemdouble,
			systemfloat, S_sumA, S_sumB, Bool_com, Approx_com);

} //Main

} //main
