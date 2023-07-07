using System;
using static System.Console;
using static System.Math;
using System.IO;

public static class main{
	
	public static void Main(){

	// Math + checking

	double sqrt2 = Math.Sqrt(2.0);

	string String1 = $"sqrt2 is {sqrt2} using Math.Sqrt()";
	string String2 = $"2^(1/5) is {Pow(2,0.2)} using Math.Pow(2,0.2)";
	string String3 = $"e^(pi) is {Exp(PI)} using Math.Exp(Math.PI)";
	string String4 = $"pi^(e) is {Pow(PI,E)} using Math.Pow(Math.PI,Math.E)";

	string String5 = $"sqrt2^2 is {sqrt2*sqrt2} by multiplication with itself";
	string String6 = $"(2^(1/5))^5 is {Pow(Pow(2,0.2),5)} using Math.Pow(Math.Pow(2,0.2),5)";
	string String7 = $"ln(e^(pi)) is {Log(Exp(PI))} using Math.Log(Math.Exp(Math.PI))";
	string String8 = $"(pi^(e))^(1/e) is {Pow(Pow(PI,E),1/E)} using Math.Pow(Math.Pow(Math.PI,Math.E), 1/Math.E)";
	
	// Math + checking

	// Gamma function + checking

	double gamma1 = sfuns.gamma(1);
	double gamma2 = sfuns.gamma(2);
	double gamma3 = sfuns.gamma(3);
	double gamma10 = sfuns.gamma(10);

	string Gamma1 = $"Γ(1) = {gamma1} should be equal to 1";
	string Gamma2 = $"Γ(2) = {gamma2} should be equal to 1";
	string Gamma3 = $"Γ(3) = {gamma3} should be equal to 2";
	string Gamma10 =$"Γ(10) = {gamma10} should be equal to 9! = 362880";

	// Gamma function + checking

	// Lngamma function + checking

	double lngamma1 = sfuns.lngamma(1);
	double lngamma2 = sfuns.lngamma(2);
	double lngamma3 = sfuns.lngamma(3);
	double lngamma10 = sfuns.lngamma(10);

	string Lngamma1 = $"Log(Γ(1)) = {lngamma1} should be equal to 0";
	string Lngamma2 = $"Log(Γ(2)) = {lngamma2} should be equal to 0";
	string Lngamma3 = $"Log(Γ(3)) = {lngamma3} should be equal to ln(2)";
	string Lngamma10 =$"Log(Γ(10)) = {lngamma10} should be equal to ln(9!)";

	string E_Lngamma1 = $"Exp(Log(Γ(1))) = Γ(1) = {Exp(lngamma1)} should be equal to 1";
	string E_Lngamma2 = $"Exp(Log(Γ(1))) = Γ(2) = {Exp(lngamma2)} should be equal to 1";
	string E_Lngamma3 = $"Exp(Log(Γ(1))) = Γ(3) = {Exp(lngamma3)} should be equal to 2";
	string E_Lngamma10 =$"Exp(Log(Γ(10))) = Γ(10) = {Exp(lngamma10)} should be equal to 362880";

	// Lngamma function + checking


	string text = File.ReadAllText(@"exercise2.txt");
	WriteLine(text, String1, String2, String3, String4, String5, String6, String7, 
			String8, Gamma1, Gamma2, Gamma3, Gamma10, Lngamma1, Lngamma2,
			Lngamma3, Lngamma10, E_Lngamma1, E_Lngamma2, E_Lngamma3, 
			E_Lngamma10);

}//Main

}//main
	
