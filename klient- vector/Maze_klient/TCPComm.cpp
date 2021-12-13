#include "TCPComm.h"
#include "stdafx.h"

CTCPComm::CTCPComm(void)
{
	WORD wVersionRequested;
	WSADATA wsaData;
	int err;
 
	// Inicializujeme WinSock DLL	
	wVersionRequested = MAKEWORD( 2, 2 );
 	err = WSAStartup( wVersionRequested, &wsaData );

}

CTCPComm::~CTCPComm(void)
{

	shutdown(m_Soket,SD_SEND);
	closesocket(m_Soket);

	WSACleanup();
}


int CTCPComm::PripojSeNaServer(char * server_name,int server_port) {
	SOCKADDR_IN server;
    int err;
	struct hostent *hp;
	unsigned int addr;

	printf("Pripojuji se na: %s, port: %d\n",server_name,server_port);
   
    
    m_Soket = socket( AF_INET, SOCK_STREAM, 0 );
    if ( m_Soket== INVALID_SOCKET ) {
	 
        return FALSE;
    }

 
	// jestli je adresa slovne tak ji zkusime prevest na ciselnou
		
	if (isalpha(server_name[0])) {   // serverova adresa je jmeno
		hp = gethostbyname(server_name);
	}
	else  { // Konvertujeme nnn.nnn addresu na pouzitelnou
		addr = inet_addr(server_name);
		hp = gethostbyaddr((char *)&addr,4,AF_INET);
	}
	if (hp == NULL ) {
		printf("Client: Nemuzu prelozit adresu [%s]: Error %d\n",server_name,WSAGetLastError());
 		return(-1);
	}

	//
	// Zkopirujeme prelozenou adresu do sockaddr_in struktury
	//
	memset(&server,0,sizeof(server));
	memcpy(&(server.sin_addr),hp->h_addr,hp->h_length);
 
	server.sin_port = htons(server_port);
	server.sin_family = AF_INET; 

	err=connect(m_Soket,(const struct sockaddr*)&server,sizeof(server));
	if (err!=0) {
		printf("Nepodarilo se pripojit.\n");
		return(-1);
	}

	printf("Pripojeno ... \n");

return(0);  
}



int CTCPComm::OdesliData(char *buff, int size) {
int err;

err=send(m_Soket,(char *)buff,size,0);
 if (err==SOCKET_ERROR) {
	 closesocket(m_Soket);
	 return(-1);
 }

return(0);
}

int CTCPComm::CekejaPrijmiData(char *buff, int size) {
int err=0;

	do {
		err=recv(m_Soket,buff+err,size-err,0);	
	
	if (err==SOCKET_ERROR) {
		 closesocket(m_Soket);
		return(-1);
	 }
	} while(err<size);

return(0);

}