#include <iostream>
#include <fstream>
#include <string>
#include <errno.h>
#define FILE_NAME "Tmp.txt"  

using namespace std;

int main()
{
	ifstream IFS;
	ofstream OFS;
	string Input;	// Ввод пользователя.
	unsigned WordCount = 0;	// Количество слов.
	OFS.open(FILE_NAME);
	if (OFS.is_open())
	{
		for (; cin >> Input && Input.substr(Input.length() - 1, 1) != "."; WordCount++)
		{
			OFS << Input << endl;
		}
		OFS << Input.substr(0, Input.length() - 1) << endl;	// Последнее слово.
		OFS.close();
	}
	else
	{
		perror("open() in ofstream");
		cout << stderr << endl;
		system("pause");
		return errno;
	}
	IFS.open(FILE_NAME);
	if (IFS.is_open())
	{
		for (unsigned C = 0; !IFS.eof(); C++)
		{
			string Word;
			IFS >> Word;
			cout << Word;
			if (C % 2)
			{
				cout << endl;
			}
			else
			{
				cout << "	";
			}
		}
		IFS.close();
	}
	else
	{
		perror("open() in ifstream");
		cout << stderr << endl;
		system("pause");
		return errno;
	}
	remove(FILE_NAME);	// If errno == 2 File was deleted already.
	// Можно вывести WordCount.
	system("pause");
	return EXIT_SUCCESS;
}
