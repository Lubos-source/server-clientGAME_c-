#include "Player.h"



int _tmain(int argc, _TCHAR* argv[])
{
	CMazeClient Client;

	char jmeno[15];



	strcpy_s(jmeno, 15, "lubos");	//nickname v serveru			"localhost"	, 192.168.43.230	

	if (Client.PripojaRegistruj("localhost", 6000, jmeno) == false) {		//localhost=adresa serveru,6000=cislo portu,jmeno=nicname,mojeID=prideli server proto &!!!
		printf("Nepodarilo se pripojit na server.\n");
		return(-1);
	}

	printf("Moje ID je: %d\n", Client.GetID());

	/*	//test volani souradnic cile
	int Cx,Cy;
	Cx = Client.ZjistiCIL().x;
	Cy = Client.ZjistiCIL().y;
	cout << "cil	x: " << Cx << "	y: " << Cy << endl;
	*/

	while ((Client.Pohni(Client.GetSmer()) != 1))
	{
		Client.Krok();
		Client.UlozDoVektoru();

		if (Client.NastavPozici(Client.GetID(),Client.GetPozice().x, Client.GetPozice().y) == false) 
		{
			Client.ZPETvPuvSmeru();
		}

		//Sleep(1000);
		cout << " x: " << (int)Client.GetPozice().x << "	y: " << (int)Client.GetPozice().y << endl;
		cout << "Hodnota na pozici hrace:" << (int)Client.ZjistiHodnotu(Client.GetID(), Client.GetPozice().x, Client.GetPozice().y) << endl;
		cout << "id hrace: " << (int)Client.GetID() << endl;
	}
	
	while (!(Client.GetPozice().x == (int)Client.ZjistiCIL().x && Client.GetPozice().y == (int)Client.ZjistiCIL().y)) {	// k!=20	// !(Client.GetPozice().x == 62 && Client.GetPozice().y == 62)
		if (Client.Pohni(Client.GetSmer()) == 8)		// 8 = uz ZDE byl !
		{
			Client.Zpet();						
		}
		else if (Client.Pohni(Client.GetSmer()) == 1) {
			Client.UlozSmer(Client.GetSmer() + 1);
		}

		else if (((Client.Pohni(Client.GetSmer() - 1)) != 1) && ((Client.Pohni(Client.GetSmer() - 1)) != 8)) {
			Client.UlozSmer(Client.GetSmer() - 1);
		}

			Client.Krok();
			Client.UlozDoVektoru();
			if (Client.NastavPozici(Client.GetID(), Client.GetPozice().x, Client.GetPozice().y) == false) 
			{
				Client.ZPETvPuvSmeru();
			}
			//Sleep(1000);
			cout << " x2: " << (int)Client.GetPozice().x << "	y2: " << (int)Client.GetPozice().y << endl;
			cout << "Hodnota na pozici hrace y+1:" << (int)Client.ZjistiHodnotu(Client.GetID(), Client.GetPozice().x, Client.GetPozice().y+1) << endl;
			cout << "id hrace: " << (int)Client.GetID() << endl;
	}
		Client.VypisVektroTEST();
		cout << endl << endl << endl;	
		printf("Hodnota bludiste na pozici hrace je : %d\n", Client.ZjistiHodnotu(Client.GetID(), Client.GetPozice().x, Client.GetPozice().y));
		//printf("Hodnota ctverecku cile je : %d\n", Client.ZjistiHodnotu(Client.GetID(), Client.GetPozice().x, Client.GetPozice().y+1));
		cout << "cil	x: " << (int)Client.ZjistiCIL().x << "	y: " << (int)Client.ZjistiCIL().y << endl;		//kontorla pro polohu cile :)
		printf("Stiskem ENTER ukoncis spojeni.\n ");

		while (_kbhit() == false) {
			Sleep(500);
		};
		
		Client.UkonciSpojeni(Client.GetID());

		return 0;
	}
