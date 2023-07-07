using System;
using static System.Console;
using static System.Math;

public static class main{

	public static void Main(){
	
	WriteLine($"\nExercise vec \nBuild a class called vec to represent eucledian 3D vectors");

	WriteLine("\nThis i have done. \nThe documented code can be found in the vec.cs file associated with this exercise");
	WriteLine("Below is an extensive testing of the 3D-vec class:\n");

	vec u = new vec(1,2,3); // simple new 3D-vectors
	vec v = new vec(2,3,4);

	u.print("u = "); // trying out the print method
	v.print("v = ");

	WriteLine("\n"); // some space between calculations

	(v+u).print("v+u ="); // trying out summation
	(v-u).print("v-u ="); // trying out difference
	(2*v).print("2*v ="); // trying out multiplication (two birds in one stone)
	
	//vec w = u*2;
	//w.print("w = ");

	(-u).print("-u = "); // trying out the - operation

	WriteLine("\n"); // some space between calculations

	WriteLine($"u.dot(v) = {u.dot(v)}"); // trying out the dot-product
	WriteLine($"vec.dot(v,u) = {vec.dot(v,u)}"); // trying out the dot-product
	WriteLine($"vec.dot(u,v) = {vec.dot(u,v)}"); // trying out the dot-product

	// Some doubles from previous exercise to the rescue
	//double d1 = 0.1 + 0.1 + 0.1 + 0.1 + 0.1 + 0.1 + 0.1 + 0.1;
	//double d2 = 8*0.1;

	WriteLine("\n"); // some space between calculations

	WriteLine($"u.approx(v,1) = {u.approx(v,1)}"); // trying out the approx function
	WriteLine($"u.approx(v,1.1) = {u.approx(v,1.1)}"); // trying out the approx function
	WriteLine($"vec.approx(u,v,1) = {vec.approx(u,v,1)}"); // trying out the approx function
	WriteLine($"vec.approx(v,u,1.1) = {vec.approx(v,u,1.1)}"); // trying out the approx function

	} // Main
} //main
