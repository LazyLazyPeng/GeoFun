// GeoFunC.cpp : Defines the exported functions for the DLL application.
//

#include "geofunc.h"
#include "stdafx.h"
#include <cmath>

#define PAI 4.0*atan(1.0)
#define P0 (3600.0*180.0/PAI)

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


void  compute_xyz(double b, double l, double  h, double a, double  e, double *x, double *y, double *z)
{
	double n;
	n = a / sqrt(1 - e * pow(sin(b), 2));
	(*x) = (n + h)*cos(b);
	(*y) = (*x)*sin(l);
	(*x) *= cos(l);
	(*z) = (n*(1 - e) + h)*sin(b);
	return;
}

void inv(double *p, int i1)
{
	double *pq, p1, pc;
	int i, j, k;
	pq = new  double[i1];
	for (i = 0; i < i1; i++)
		pq[i] = p[i*i1 + i];
	for (i = 0; i < i1 - 1; i++)
	{
		for (j = i + 1; j < i1; j++)
		{
			p1 = -p[j*i1 + i] / p[i*i1 + i];
			for (k = 0; k < i1; k++)
			{
				if (k == i) continue;
				p[j*i1 + k] = p[j*i1 + k] + p1 * p[i*i1 + k];
			}
			p[j*i1 + i] = p1;
		}
	}
	for (i = 0; i < i1; i++)
	{
		for (j = 0; j < i1; j++)
		{
			if (j == i) continue;
			p[i*i1 + j] = p[i*i1 + j] / p[i*i1 + i];
		}
		p[i*i1 + i] = 1 / p[i*i1 + i];
	}
	for (i = i1 - 1; i >= 1; i--)
		for (j = i - 1; j >= 0; j--)
		{
			pc = -p[j*i1 + i];
			for (k = 0; k <= j; k++)
				p[j*i1 + k] = p[j*i1 + k] + pc * p[i*i1 + k];
		}
	for (i = 0; i < i1; i++)
		if (p[i*i1 + i] < 0)
		{
			for (j = 0; j < i1; j++)
				for (k = 0; k < i1; k++)
					if (k == j)
						p[j*i1 + j] = 1.0 / pq[j];
					else
						p[i*i1 + j] = 0.0;
			return;
		}
	for (i = 0; i < i1 - 1; i++)
		for (j = i + 1; j < i1; j++)
			p[i*i1 + j] = p[j*i1 + i];
	delete   pq;
}

double dg1(double d)
{
	int d1, d2;
	d *= 180.0 / PAI;
	d1 = (int)floor(d + 1e-12);
	d2 = (int)floor((d - d1) * 60);
	d = ((d - d1 - d2 / 60.0)*3600.0);
	if (fabs(d - 60.0) < 1e-4)
	{
		d = 0;
		d2++;
	}
	if (fabs(d2 - 60.0) < 1e-4)
	{
		d2 = 0;
		d1++;
	}
	d = d1 + d2 / 100.0 + d / 10000.0;
	return  (d);
}

int FourCal(int n,
	double *x1, double *y1, double *x2, double *y2,
	double *ddx, double *ddy, double *r, double *s)
{
	if (n < 2)return -1;

	double p[16], x[4], l[1500], w[4], v[1500];
	char dh[20];
	double targetx[500], targety[500], sourcex[500], sourcey[500];
	double dx, dy, Q, m;
	double a[1500][4], pvv;
	int i, j,  k;
	int pos[4] = { 2,3 }, num;

	num = 4;
	dx = 0;
	dy = 0;
	Q = 0;
	m = 0;

	for (i = 0; i < n; i++)
	{
		dx += x1[i] - x2[i];
		dy += y1[i] - y2[i];
	}

	dx = dx / n;
	dy = dy / n;
	do
	{
		for (i = 0; i < n; i++)
		{
			a[2 * i + 0][0] = (1 + m)*cos(Q);
			a[2 * i + 0][1] = (1 + m)*sin(Q);
			a[2 * i + 0][pos[0]] = cos(Q)*(sourcex[i] + dx) + sin(Q)*(sourcey[i] + dy);
			a[2 * i + 0][pos[1]] = -(1 + m)*sin(Q)*(sourcex[i] + dx) + (1 + m)*cos(Q)*(sourcey[i] + dy);

			a[2 * i + 1][0] = -(1 + m)*sin(Q);
			a[2 * i + 1][1] = (1 + m)*cos(Q);
			a[2 * i + 1][pos[0]] = -sin(Q)*(sourcex[i] + dx) + cos(Q)*(sourcey[i] + dy);
			a[2 * i + 1][pos[1]] = -(1 + m)*cos(Q)*(sourcex[i] + dx) - (1 + m)*sin(Q)*(sourcey[i] + dy);

			l[2 * i + 0] = (1 + m)*cos(Q)*(sourcex[i] + dx) + (1 + m)*sin(Q)*(sourcey[i] + dy) - targetx[i];
			l[2 * i + 1] = -(1 + m)*sin(Q)*(sourcex[i] + dx) + (1 + m)*cos(Q)*(sourcey[i] + dy) - targety[i];

		}
		for (i = 0; i < num; i++)
		{
			for (j = i; j < num; j++)
			{
				p[i*num + j] = 0;
				for (k = 0; k < 2 * n; k++)
					p[i*num + j] += a[k][j] * a[k][i];
				p[j*num + i] = p[i*num + j];
			}
		}
		for (i = 0; i < num; i++)
		{
			w[i] = 0.0;
			for (j = 0; j < 2 * n; j++)
				w[i] += a[j][i] * l[j];
		}
		inv(p, num);
		for (i = 0; i < num; i++)
		{
			x[i] = 0;
			for (j = 0; j < num; j++)
				x[i] += -p[i*num + j] * w[j];
		}
		pvv = 0;
		for (i = 0; i < 3 * n; i++)
		{
			v[i] = l[i];
			for (j = 0; j < num; j++)
				v[i] += a[i][j] * x[j];
			pvv += v[i] * v[i];
		}

		dx += x[0];
		dy += x[1];
		m += x[2];
		Q += x[3];

	} while (fabs(x[0]) > 1E-10&&fabs(x[1]) > 1E-10&&fabs(x[2]) > 1E-15&&fabs(x[3]) > 1E-15);

	if (Q < 0)
		Q = Q + 2 * PAI;
	Q = dg1(Q);

	*ddx = dx;
	*ddy = dy;
	*r = Q;
	*s = m * 1e6;

	return 0;
}

int SevenCal(int n,
	double a1, double f1, double a2, double f2,
	double *b1, double *l1, double *h1,
	double *b2, double *l2, double *h2,
	double *dx, double *dy, double *dz,
	double *rx, double *ry, double *rz,
	double *s)
{
	double p[49], x[7], l[3000], w[7], v[3000];
	char dh[20];
	double targetx, targety, targetz, sourcex, sourcey, sourcez, targetb, targetl, targeth, sourceb, sourcel, sourceh;
	double a[1500][7], pvv, m[7], m0, targeta, targetf, sourcea, sourcef, targete, sourcee;
	int i, j,  k;
	int pos[4] = { 3,4,5,6 }, num;

	//// 7七参数或者3参数
	num = 7;

	x[0] = x[1] = x[2] = x[3] = x[4] = x[5] = x[6] = 0;
	m[0] = m[1] = m[2] = m[3] = m[4] = m[5] = m[6] = 0;

	sourcea = a1;
	sourcef = f1;
	targeta = a2;
	targetf = f2;

	targete = (2.0 / targetf - 1.0 / targetf / targetf);
	sourcee = (2.0 / sourcef - 1.0 / sourcef / sourcef);

	for (i = 0; i < n; i++)
	{
		compute_xyz(b1[i], l1[i], h1[i], sourcea, sourcee, &sourcex, &sourcey, &sourcez);
		compute_xyz(b1[i], l1[i], h1[i], targeta, targete, &targetx, &targety, &targetz);

		a[3 * i + 0][0] = 1;
		a[3 * i + 0][1] = 0;
		a[3 * i + 0][2] = 0;
		a[3 * i + 0][pos[0]] = sourcex / 1000000.0;
		a[3 * i + 0][pos[1]] = 0;
		a[3 * i + 0][pos[2]] = -sourcez / P0;
		a[3 * i + 0][pos[3]] = sourcey / P0;

		a[3 * i + 1][0] = 0;
		a[3 * i + 1][1] = 1;
		a[3 * i + 1][2] = 0;
		a[3 * i + 1][pos[0]] = sourcey / 1000000.0;
		a[3 * i + 1][pos[1]] = sourcez / P0;
		a[3 * i + 1][pos[2]] = 0;
		a[3 * i + 1][pos[3]] = -sourcex / P0;

		a[3 * i + 2][0] = 0;
		a[3 * i + 2][1] = 0;
		a[3 * i + 2][2] = 1;
		a[3 * i + 2][pos[0]] = sourcez / 1000000.0;
		a[3 * i + 2][pos[1]] = -sourcey / P0;
		a[3 * i + 2][pos[2]] = sourcex / P0;
		a[3 * i + 2][pos[3]] = 0;


		l[3 * i + 0] = -targetx + sourcex;
		l[3 * i + 1] = -targety + sourcey;
		l[3 * i + 2] = -targetz + sourcez;
	}

	for (i = 0; i < num; i++)
	{
		for (j = i; j < num; j++)
		{
			p[i*num + j] = 0;
			for (k = 0; k < 3 * n; k++)
				p[i*num + j] += a[k][j] * a[k][i];
			p[j*num + i] = p[i*num + j];
		}
	}
	for (i = 0; i < num; i++)
	{
		w[i] = 0.0;
		for (j = 0; j < 3 * n; j++)
			w[i] += a[j][i] * l[j];
	}
	inv(p, num);
	for (i = 0; i < num; i++)
	{
		x[i] = 0;
		for (j = 0; j < num; j++)
			x[i] += -p[i*num + j] * w[j];
	}
	pvv = 0;
	for (i = 0; i < 3 * n; i++)
	{
		v[i] = l[i];
		for (j = 0; j < num; j++)
			v[i] += a[i][j] * x[j];
		pvv += v[i] * v[i];
	}
	m0 = sqrt(pvv / (3 * n - num + 0.000000001));
	for (i = 0; i < num; i++)
		m[i] = sqrt(p[i*num + i])*m0;

	*dx = x[0];
	*dy = x[1];
	*dz = x[2];
	*s = x[3];
	*rx = x[4];
	*ry = x[5];
	*rz = x[6];

	return 0;
}
