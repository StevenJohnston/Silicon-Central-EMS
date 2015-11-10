#include <String>
#include <string.h>
#include <list>
struct FileIoMessage
{
	int errorCode;
	std::string errorMessage;
};
static class FileIO
{
public:
	FileIO();
	~FileIO();

private:
	std::string path;
	std::string file;
	bool openFile();
	bool closeFile();
	std::list<std::string[]> readFile();
	bool write(std::string[]);
	template<typename T>
	bool readAllTo(std::list<T> toList);
};

FileIO::FileIO()
{
}

FileIO::~FileIO()
{
}

template<typename T>
static bool FileIO::readAllTo(std::list<T> toList)
{
	toList.insert(new T());
	return false;
}
