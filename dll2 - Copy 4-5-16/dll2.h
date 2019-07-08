// dll2.h : main header file for the dll2 DLL
//

#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"		// main symbols


// Cdll2App
// See dll2.cpp for the implementation of this class
//

class Cdll2App : public CWinApp
{
public:
	Cdll2App();

// Overrides
public:
	virtual BOOL InitInstance();

	DECLARE_MESSAGE_MAP()
};
