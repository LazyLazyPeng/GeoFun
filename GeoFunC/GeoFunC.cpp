// GeoFunC.cpp : Defines the exported functions for the DLL application.
//

#include "geofunc.h"
#include "stdafx.h"
#include <cmath>

int Sum(int a, int b)
{
	return a + b;
}
void FourTrans(int count, double *x, double *y,
	double dx, double dy, double r, double s)
{
	double xx = 0, yy = 0;
	for (int i = 0; i < count; i++)
	{
		xx = *(x + i);
		yy = *(y + i);

		*(x + i) = (1 + s * 1e-6)*(cos(r)*xx + sin(r)*yy) + dx;
		*(y + i) = (1 + s * 1e-6)*(-sin(r)*xx + cos(r)*yy) + dy;
	}
}

