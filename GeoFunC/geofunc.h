#pragma once

#ifdef _cpluscplus
#if _cplusplus
extern "C" {
#endif
#endif

	_declspec(dllexport) int Sum(int a, int b);

	//// ��������
	void inv(double *p, int i1);

	//// blhתxyz
	void  compute_xyz(double b, double l, double  h, double a, double  e, double *x, double *y, double *z);

	//// �����Ĳ���
	_declspec(dllexport) int FourCal(int num,
		double *x1, double *y1, double *x2, double *y2,
		double *dx, double *dy, double *r, double *s);

	//// �����߲���
	_declspec(dllexport) int SevenCal(int num,
		double *b1, double *l1, double *h1,
		double *b2, double *l2, double *h2,
		double *dx, double *dy, double *dz,
		double *rx, double *ry, double *rz,
		double *s);

	_declspec(dllexport) void FourTrans(int count,double *x, double *y,
		double dx, double dy, double r, double s);

#ifdef _cplusplus
#if _cplusplus
}
#endif
#endif
