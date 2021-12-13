// Maze_klient.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "tcpcomm.h"
#include <conio.h>

CTCPComm g_TCPComm;	//singlton = objekt pro celou aplikaci			
					//trida CMAZEcomunication - metody NastavPozici,ZjistiHodnotu atd.....

bool NastavPozici(char id,char x,char y) {

	char prikaz = 1;
	g_TCPComm.OdesliData(&prikaz,1); // odesleme prikaz
	g_TCPComm.OdesliData(&id,1); //odesleme nase ID		

	g_TCPComm.OdesliData(&x,1); //odesleme souradnice
	g_TCPComm.OdesliData(&y,1);
	
	char hodnota;
	g_TCPComm.CekejaPrijmiData(&hodnota,1); //pockejme na vysledek

	if (hodnota>0) return(true); //pokud je prijata hodnota vetsi nez 0 tak bylo presunuti uspesne
	else return(false); //presunuti neuspesne - asi na souradnicich uz neco je (zed)
}


char ZjistiHodnotu(char x,char y) {
	char prikaz = 2;
	g_TCPComm.OdesliData(&prikaz,1);
	g_TCPComm.OdesliData(&x,1);
	g_TCPComm.OdesliData(&y,1);

	char hodnota;
	g_TCPComm.CekejaPrijmiData(&hodnota,1);

	return(hodnota);
}

void UkonciSpojeni(char id) {
	char prikaz = 99;
	g_TCPComm.OdesliData(&prikaz,1);
	g_TCPComm.OdesliData(&id,1);

	char hodnota;
	g_TCPComm.CekejaPrijmiData(&hodnota,1); //pockame zda probehlo v poradku

}

bool PripojaRegistruj(char *server,int port,char *jmeno,char *id) {

	if (g_TCPComm.PripojSeNaServer(server,port)!=0) return(false); //pripojeni se nezdarilo

	char prikaz = 0;
	g_TCPComm.OdesliData(&prikaz,1);
	g_TCPComm.OdesliData(jmeno,strlen(jmeno));
	g_TCPComm.CekejaPrijmiData(id,1); //protoze promenna id uz je ukazatel (abychom v ni mohli vratit nazpet hodnotu), tak tady nemusime davat hvezdicku
	return(true);
}

int _tmain(int argc, _TCHAR* argv[])
{
	char mojeID = 1;
	char jmeno[15];
												//169.254.110.176
	strcpy_s(jmeno,15,"petr");	//nickname v serveru			eduroam: 160.216.18.47	,	"localhost"		,	JirkaCompany: 192.168.43.230	ether: 160.216.229.115
	
	if (PripojaRegistruj("local" ,6000,jmeno,&mojeID)==false) {		//localhost=adresa serveru,6000=cislo portu,jmeno=nicname,mojeID=prideli server proto &!!!
		printf("Nepodarilo se pripojit na server.\n");
		return(-1);
	}

	printf("Moje ID je: %d\n",mojeID);

	char x,y;

	for (char x=10;x<20;x++) {	
		y=10; //nastavime souradnice nove pozice
		if (NastavPozici(mojeID,x,y)==false) { //posleme na server pozadavek na presun
			printf("Presunuti na pozici nebylo uspesne!\n");
		}
		Sleep(1000); // pockame sekundu pred dalsim presunem
	}

	printf("Hodnota bludiste na pozici 0,0 je : %d\n",ZjistiHodnotu(0,0));	//0,0 levy horni roh

	printf("Stiskem ENTER ukoncis spojeni.\n ");
	
	while (_kbhit()==false) {
		Sleep(100);
	};
	
	UkonciSpojeni(mojeID);
	
	return 0;
}

