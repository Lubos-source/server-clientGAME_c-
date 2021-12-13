#pragma once

#include "stdafx.h"
#include "tcpcomm.h"
#include <conio.h>
#include "Pozice.h"
#include <vector>


class CMazeClient
{
protected:
	CTCPComm g_TCPComm;
	char mojeID;
	int m_SmerPohybu;

	Pozice m_Pozice;
	Pozice Cil;
	vector <Pozice> cesta;
	
public:
	
	char SetID(int id);
	char GetID();
	bool NastavPozici(char id, char x, char y);
	char ZjistiHodnotu(char id, char x, char y);
	Pozice ZjistiCIL();
	void UkonciSpojeni(char id);
	bool PripojaRegistruj(char* server, int port, char* jmeno);
	
	void ZPETvPuvSmeru();

	Pozice GetPozice();

	CMazeClient(void);
	~CMazeClient(void);

	void UlozDoVektoru();
	void VypisVektroTEST();
	void Zpet();
	char VectorX(int i);
	char VectorY(int i);
	int SizeofVector();

	//int SetSmer();
	int GetSmer();
	void UlozSmer(int sm);
	char Pohni(int s);
	void Krok();
};

