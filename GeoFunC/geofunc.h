#pragma once
#ifdef _cpluscplus
#if _cplusplus
extern "C" {
#endif
#endif

	_declspec(dllexport) int Sum(int a, int b);

	_declspec(dllexport) void FourTrans(int count,double *x, double *y,
		double dx, double dy, double r, double s);

#ifdef _cplusplus
#if _cplusplus
}
#endif
#endif
