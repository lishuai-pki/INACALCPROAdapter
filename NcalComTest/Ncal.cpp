#include <iostream>
#include <objbase.h>
#include <atlstr.h>

#include "stdafx.h"

#import "..\INACALCPROLib\bin\Debug\INACALCPROLib.tlb"

using namespace ATL;
using namespace std;
using namespace INACALCPROLib;

extern const TCHAR g_cstrAnd[] = _T("And");
extern const TCHAR g_cstrOr[] = _T("Or");
extern const TCHAR g_cstrNot[] = _T("Not");
extern const TCHAR g_cstrIf[] = _T("logical");
extern const TCHAR g_typeBSTR[] = _T("VT_BSTR");
extern const TCHAR g_typeEMPTY[] = _T("VT_EMPTY");
extern const TCHAR g_typeDATE[] = _T("VT_DATE");
extern const TCHAR g_typeR8[] = _T("VT_R8");
extern const TCHAR g_typeBOOL[] = _T("VT_BOOL");
static const TCHAR g_cstrExt[] = _T("_!?!");
static const TCHAR g_szEqID[] = { _T("{0FCCC1AA-41F0-4ED3-BEA0-A6D61410B645}") };
extern const TCHAR g_cstrBetween[] = _T(" Between ");
extern const TCHAR g_cstrNotBetween[] = _T(" Not Between ");


ATL::CComModule _Module;

class EventReceiver :

	public IDispEventImpl<0, EventReceiver, &(__uuidof(INACALCPROLib::_IInaCalcProEvents_Event)), &(__uuidof(INACALCPROLib::__INACALCPROLib)), 1, 0>
{
public:
	STDMETHOD(CheckAtom)(BSTR strAtom, EInaValueType* valType);
	STDMETHOD(GetAtomValue)(BSTR strAtom, VARIANT* vntValue);
	STDMETHOD(ValueChanged)(BSTR strAtom);
	STDMETHOD(CheckCustomFunction)(BSTR strFunc, IDispatch* argTypes, EInaValueType* valType);
	STDMETHOD(EvalCustomFunction)(BSTR strFunc, IDispatch* argVals, VARIANT* vntValue);
	//STDMETHOD(AtomChanged)(IInaCalcAtom pAtom);

	BEGIN_SINK_MAP(EventReceiver)
		SINK_ENTRY_EX(0, (__uuidof(INACALCPROLib::_IInaCalcProEvents_Event)), 1, CheckAtom)
		SINK_ENTRY_EX(0, (__uuidof(INACALCPROLib::_IInaCalcProEvents_Event)), 2, GetAtomValue)
		SINK_ENTRY_EX(0, (__uuidof(INACALCPROLib::_IInaCalcProEvents_Event)), 3, ValueChanged)
		SINK_ENTRY_EX(0, (__uuidof(INACALCPROLib::_IInaCalcProEvents_Event)), 4, CheckCustomFunction)
		SINK_ENTRY_EX(0, (__uuidof(INACALCPROLib::_IInaCalcProEvents_Event)), 5, EvalCustomFunction)
		//SINK_ENTRY_EX(0, (__uuidof(INACALCPROLib::_IInaCalcProEvents_Event)), 6, AtomChanged)
	END_SINK_MAP()
};


int main()
{
	CoInitialize(NULL);
	_Module.Init(NULL, (HINSTANCE)GetModuleHandle(NULL));

	INACALCPROLib::IInaCalcProPtr m_pInaCalc;

	//m_pInaCalc.CreateInstance(__uuidof(InaCalcPro), NULL, CLSCTX_INPROC_SERVER);
	m_pInaCalc.CreateInstance(__uuidof(INACALCPROLib::InaCalcProClass));
	m_pInaCalc->put_AutoCalc(0);
	m_pInaCalc->put_RestrictVariables(TRUE);

	IInaCalcFuncsPtr  pInaCalcFuncs;
	m_pInaCalc->get_Funcs(&pInaCalcFuncs);

	IInaCalcFuncPtr  spInaCalcFn;
	BSTR bstrName = SysAllocString(_T("getarea")),
		bstrPrototype = SysAllocString(_T("SomeText")),
		bstrDesc = SysAllocString(_T("SomeText"));

	_bstr_t  sbName(bstrName, false);
	_bstr_t  sbPrototype(bstrPrototype, false);
	_bstr_t  sbDesc(bstrDesc, false);
	pInaCalcFuncs->raw_Add(sbName, EInaFuncCategory::EInaFuncCategory_inaFuncCustom, sbPrototype, sbDesc, &spInaCalcFn);


	EventReceiver* pReveiver = new EventReceiver;

	HRESULT hresult = pReveiver->DispEventAdvise(m_pInaCalc);

	IInaCalcAtomsPtr   spAtoms;
	m_pInaCalc->get_Atoms(&spAtoms);

	CComVariant svEquation(_T("{0FCCC1AA-41F0-4ED3-BEA0-A6D61410B645}"));
	IInaCalcAtomPtr   spAtomEq;
	spAtoms->get_Item(svEquation, &spAtomEq);


	//BSTR m_equationFormula = SysAllocString(_T("1+1"));
	//CString  cstrEquation = m_equationFormula;
	//cstrEquation.MakeLower();
	//_bstr_t sbEquation(cstrEquation);

	//spAtomEq->put_Formula(sbEquation);
	//m_pInaCalc->Recalc();

	//_variant_t  svRes;
	//spAtomEq->get_Value(&svRes);
	//cout << "the result is " << svRes.intVal << endl;



	BSTR m_equationFormula = SysAllocString(_T("getArea(3,5)"));
	CString cstrEquation = m_equationFormula;
	cstrEquation.MakeLower();
	_bstr_t sbEquation(cstrEquation);
	spAtomEq->put_Formula(sbEquation);
	m_pInaCalc->Recalc();

	_variant_t  svRes;
	spAtomEq->get_Value(&svRes);
	cout << "the result is " << svRes.intVal << endl;


	pReveiver->DispEventUnadvise(m_pInaCalc);
	_Module.Term();
	CoUninitialize();


}

STDMETHODIMP_(HRESULT __stdcall) EventReceiver::CheckAtom(BSTR strAtom, EInaValueType* valType)
{
	return E_NOTIMPL;
}

STDMETHODIMP_(HRESULT __stdcall) EventReceiver::GetAtomValue(BSTR strAtom, VARIANT* vntValue)
{
	return E_NOTIMPL;
}

STDMETHODIMP_(HRESULT __stdcall) EventReceiver::ValueChanged(BSTR strAtom)
{
	return E_NOTIMPL;
}

STDMETHODIMP_(HRESULT __stdcall) EventReceiver::CheckCustomFunction(BSTR strFunc, IDispatch* argTypes, EInaValueType* valType)
{
	_bstr_t sbFunc(strFunc);

	BSTR  bstrName = SysAllocString(_T("getarea"));
	_bstr_t  sbName(bstrName, false);
	if (_wcsicmp(sbName, sbFunc) == 0)
	{
		IInaCalcFuncArgTypesPtr  pArgTypes(argTypes);
		long  nArgs;
		pArgTypes->get_Count(&nArgs);
		sbFunc += _T("(");
		EInaValueType  type;
		for (long ni = 0; ni < nArgs; ni++)
		{
			if (ni > 0)
				sbFunc += _T(",");
			pArgTypes->get_Item(ni + 1, &type);

			switch (type)
			{
			case EInaValueType::EInaValueType_inaValEmpty:
			case EInaValueType::EInaValueType_inaValError:
				sbFunc += g_typeEMPTY;
				break;
			case EInaValueType::EInaValueType_inaValText:
				sbFunc += g_typeBSTR;
				break;
			case EInaValueType::EInaValueType_inaValNumber:
				sbFunc += g_typeR8;
				break;
			case EInaValueType::EInaValueType_inaValDate:
				sbFunc += g_typeDATE;
				break;
			case EInaValueType::EInaValueType_inaValBool:
				sbFunc += g_typeBOOL;
				break;
			}
		}
		sbFunc += _T(")");
		cout << "checkCustomFunc is " << sbFunc << endl;
		return S_OK;
	}

	return E_NOTIMPL;
}

STDMETHODIMP_(HRESULT __stdcall) EventReceiver::EvalCustomFunction(BSTR strFunc, IDispatch* argVals, VARIANT* vntValue)
{
	_bstr_t sbFunc(strFunc);

	BSTR  bstrName = SysAllocString(_T("getarea"));
	_bstr_t  sbName(bstrName, false);
	if (_wcsicmp(sbName, sbFunc) == 0)
	{
		IInaCalcFuncArgValsPtr  pArgVals(argVals);
		long  nArgs;
		pArgVals->get_Count(&nArgs);
		sbFunc += _T("(");
		TCHAR szBuffer[32];
		for (long ni = 0; ni < nArgs; ni++)
		{
			if (ni > 0)
				sbFunc += _T(",");
			_variant_t  type;
			pArgVals->get_Item(ni + 1, &type);
			szBuffer[0] = _T('\0');
			_bstr_t  sbValue;
			switch (type.vt)
			{
			case VT_R8:
				swprintf_s(szBuffer, _T("%.14g"), type.dblVal);
				break;
			case VT_I4:
				swprintf_s(szBuffer, _T("%d"), type.lVal);
				break;
			case VT_BOOL:
				swprintf_s(szBuffer, _T("%d"), type.boolVal);
				break;
			}
			if (type.vt == VT_BSTR)
				sbFunc += type.bstrVal;
			else
				sbFunc += szBuffer;
		}
		sbFunc += _T(")");

		cout << "evalCustomFunc is " << sbFunc << endl;

		double area = 5 * 6;
		vntValue->dblVal = area;
		return S_OK;
	}
}

