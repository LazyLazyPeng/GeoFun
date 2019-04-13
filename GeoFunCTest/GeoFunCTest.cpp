// GeoFunCTest.cpp : This file contains the 'main' function. Program execution begins and ends there.
//
#include "pch.h"
#include <iostream>
#include "geofunc.h"
using namespace std;

int main()
{
    std::cout << "Hello World!\n"; 

	double x1[3] = { 0,500,500 };
	double y1[3] = { 0,0,500 };
	double x2[3], y2[3];

	double dx1 = 100, dy1 = 200, r1 = 2.33 * 4 * atan(1.0) / 180.0, s1 = 3.712;
	double dx2, dy2, r2, s2;

	FourCal(3, x1, y1, x2, y2, &dx2, &dy2, &r2, &s2);

	double dx = dx2 - dx1;
	double dy = dy2 - dy1;
	double r = r2 - r1;
	double s = s2 - s1;
	double a = 0;
}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
