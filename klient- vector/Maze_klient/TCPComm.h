#pragma once

#include <winsock2.h>

class CTCPComm
{
private:
	SOCKET m_Soket;
public:
	int PripojSeNaServer(char * server_name,int server_port);
	int CekejaPrijmiData(char *buff,int size);
	int OdesliData(char *buff,int size);
	CTCPComm(void);
	~CTCPComm(void);
};
