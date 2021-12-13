#include "Player.h"

#define SmerNahoru 1
#define SmerDoprava 2
#define SmerDolu 3
#define SmerDoleva 4


char CMazeClient::SetID(int id)
{
	mojeID = id;
	return id;
}

char CMazeClient::GetID()
{
	return mojeID;
}

bool CMazeClient::NastavPozici(char id, char x, char y)
{
	char prikaz = 1;
	g_TCPComm.OdesliData(&prikaz, 1); // odesleme prikaz
	g_TCPComm.OdesliData(&id, 1); //odesleme nase ID				

	g_TCPComm.OdesliData(&x, 1); //odesleme souradnice
	g_TCPComm.OdesliData(&y, 1);

	char hodnota;
	g_TCPComm.CekejaPrijmiData(&hodnota, 1); //pockejme na vysledek

	if (hodnota > 0) return(true); //pokud je prijata hodnota vetsi nez 0 tak bylo presunuti uspesne
	else return(false); //presunuti neuspesne - asi na souradnicich uz neco je (zed)
}

char CMazeClient::ZjistiHodnotu(char id, char x, char y)
{
	char prikaz = 2;
	g_TCPComm.OdesliData(&prikaz, 1);
	g_TCPComm.OdesliData(&id, 1);

	g_TCPComm.OdesliData(&x, 1);
	g_TCPComm.OdesliData(&y, 1);

	char hodnota;
	g_TCPComm.CekejaPrijmiData(&hodnota, 1);

	return(hodnota);	//pùvodnì--> (hodnota)
}

Pozice CMazeClient::ZjistiCIL()
{
	char prikaz = 3;
	g_TCPComm.OdesliData(&prikaz, 1);
	
	Pozice hodnota;
	g_TCPComm.CekejaPrijmiData(&hodnota.x, 1);
	g_TCPComm.CekejaPrijmiData(&hodnota.y, 1);

	return(hodnota);	//pùvodnì--> (hodnota)
}

void CMazeClient::UkonciSpojeni(char id)
{
	char prikaz = 99;
	g_TCPComm.OdesliData(&prikaz, 1);
	g_TCPComm.OdesliData(&id, 1);

	char hodnota;
	g_TCPComm.CekejaPrijmiData(&hodnota, 1); //pockame zda probehlo v poradku

}

bool CMazeClient::PripojaRegistruj(char* server, int port, char* jmeno)
{
	if (g_TCPComm.PripojSeNaServer(server, port) != 0) return(false); //pripojeni se nezdarilo

	char prikaz = 0;
	g_TCPComm.OdesliData(&prikaz, 1);
	g_TCPComm.OdesliData(jmeno, strlen(jmeno));
	g_TCPComm.CekejaPrijmiData(&mojeID, 1); //protoze promenna id uz je ukazatel (abychom v ni mohli vratit nazpet hodnotu), tak tady nemusime davat hvezdicku
	return(true);
	
}

void CMazeClient::ZPETvPuvSmeru()
{
	switch (m_SmerPohybu)			
	{
	case 1:
		NastavPozici(mojeID, m_Pozice.x, m_Pozice.y+1);			//vzdy na druhou stranu nez je smer pohybu ! nahoru--> tak dolu ! atd...
		break;
	case 2:
		NastavPozici(mojeID, m_Pozice.x-1, m_Pozice.y);
		break;
	case 3:
		NastavPozici(mojeID, m_Pozice.x, m_Pozice.y-1);
		break;
	case 4:
		NastavPozici(mojeID, m_Pozice.x+1, m_Pozice.y);
		break;
	}
}

void CMazeClient::UlozDoVektoru()
{
	cesta.push_back(GetPozice());
}

void CMazeClient::VypisVektroTEST()
{
	for(int i=0;i<cesta.size();i++)
	cout << "x: " <<(int)cesta[i].x << "	y: " << (int)cesta[i].y << endl;
	cesta.clear();
}

void CMazeClient::Zpet()
{

	//if ((Pohni(1)==8||Pohni(1)==1)&& (Pohni(2) == 8 || Pohni(2) == 1)&& (Pohni(3) == 8 || Pohni(3) == 1)&& (Pohni(4) == 8 || Pohni(4) == 1))
	//{
	//	do
	//	{
			for (int i = cesta.size()-1; i >=0 ; i--)
			{
				m_Pozice.x = VectorX(i);
				m_Pozice.y = VectorY(i);
				NastavPozici(mojeID, m_Pozice.x, m_Pozice.y);
				//Sleep(200);	

				for (int j = 0; j <= 4; j++)
				{
					UlozSmer(GetSmer() + 1);
					if ((Pohni(GetSmer()) != 1) && (Pohni(GetSmer()) != 8))
					{
						break;
					}
				}
				if ((Pohni(GetSmer()) != 1) && (Pohni(GetSmer()) != 8))
				{
					break;
				}
			}
	//	} while (!((Pohni(GetSmer()) != 1) && (Pohni(GetSmer()) != 8)));		//((Pohni(1) != 8 && Pohni(1) != 1) || (Pohni(2) != 8 && Pohni(2) != 1) || (Pohni(3) != 8 && Pohni(3) != 1) || (Pohni(4) != 8 && Pohni(4) != 1))
		
	//}
	/*else if ((Pohni(1) == 8) || (Pohni(1) == 1) || (Pohni(2) == 8) || (Pohni(2) == 1) || (Pohni(3) == 8) || (Pohni(3) == 1) || (Pohni(4) == 8) || (Pohni(4) == 1))
	{
		UlozSmer(GetSmer() + 1);
	}
	*/
}

char CMazeClient::VectorX(int i)
{
	return cesta[i].x;
}

char CMazeClient::VectorY(int i)
{
	return cesta[i].y;
}

int CMazeClient::SizeofVector()
{
	return cesta.size();
}

Pozice CMazeClient::GetPozice()
{
	m_Pozice.x;
	m_Pozice.y;
	return m_Pozice;
}


CMazeClient::CMazeClient(void)
{	
	mojeID = 0;
	m_Pozice.y = 20;
	m_Pozice.x = 10;
		
	m_SmerPohybu = SmerDoprava;	
}

CMazeClient::~CMazeClient(void)
{
}


int CMazeClient::GetSmer()
{
	return m_SmerPohybu;
}

void CMazeClient::UlozSmer(int sm)
{
	if (sm > 4)
	{
		sm = 1;
	}
	else if(sm < 1) 
	{
		sm = 4;
	}
	m_SmerPohybu = sm;
}

char CMazeClient::Pohni(int s)
{
	//m_SmerPohybu = s;
	if ((s > 4)) s -= s;
	if (s < 1) s = 4;

	switch (s)
	{
	case 1:	//1
		for (int i = 0; i < cesta.size(); i++)
		{
			if ((m_Pozice.x == cesta[i].x) && (m_Pozice.y - 1 == cesta[i].y))
				return 8;
		}
		return ZjistiHodnotu(mojeID, m_Pozice.x, m_Pozice.y - 1);
		break;
	case 2:	//2
		for (int i = 0; i < cesta.size(); i++)
		{
			if ((m_Pozice.x + 1 == cesta[i].x) && (m_Pozice.y == cesta[i].y))
				return 8;
		}
		return ZjistiHodnotu(mojeID, m_Pozice.x + 1, m_Pozice.y);
		break;
	case 3:		//3
		for (int i = 0; i < cesta.size(); i++)
		{
			if ((m_Pozice.x == cesta[i].x) && (m_Pozice.y + 1 == cesta[i].y))
				return 8;
		}
		return ZjistiHodnotu(mojeID, m_Pozice.x, m_Pozice.y + 1);
		break;
	case 4:	//4
		for (int i = 0; i < cesta.size(); i++)
		{
			if ((m_Pozice.x - 1 == cesta[i].x) && (m_Pozice.y == cesta[i].y))
				return 8;
		}
		return ZjistiHodnotu(mojeID, m_Pozice.x - 1, m_Pozice.y);
		break;
	}
	
	
}

void CMazeClient::Krok()
{	
		if (m_SmerPohybu == SmerDoleva)m_Pozice.x--;
		if (m_SmerPohybu == SmerDoprava)m_Pozice.x++;
		if (m_SmerPohybu == SmerNahoru)m_Pozice.y--;
		if (m_SmerPohybu == SmerDolu)m_Pozice.y++;

		NastavPozici(mojeID, m_Pozice.x, m_Pozice.y);	

}
