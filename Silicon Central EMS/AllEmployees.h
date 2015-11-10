#pragma once
#include<string.h>
#include<string>
class Employee
{
public:
	Employee();
	~Employee();

private:

protected:
	std::string firstName;
	std::string lastName;
	std::string socialInsuranceNumber;
	std::string dateOfBirth;
};

Employee::Employee()
{
}

Employee::~Employee()
{
}

class FullTimeEmployee:Employee
{
public:
	FullTimeEmployee();
	~FullTimeEmployee();

private:
	std::string dateOfHire;
	std::string dateOfTermination;
	double salary;
};

FullTimeEmployee::FullTimeEmployee()
{
}

FullTimeEmployee::~FullTimeEmployee()
{
}

class ParttimeEmployee:Employee
{
public:
	ParttimeEmployee();
	~ParttimeEmployee();

private:
	std::string dateOfHire;
	std::string dateOfTermination;
	double hourlyRate;
};

ParttimeEmployee::ParttimeEmployee()
{
}

ParttimeEmployee::~ParttimeEmployee()
{
}

class ContractEmployee:Employee
{
public:
	ContractEmployee();
	~ContractEmployee();

private:
	std::string contractStartDate;
	std::string contractStopDate;
	double fixedContractAmount;//What is this is it a double thats my guess
};

ContractEmployee::ContractEmployee()
{
}

ContractEmployee::~ContractEmployee()
{
}

class SeasonalEmployee: Employee
{
public:
	typedef enum seasons
	{
		summer,
		fall,
		winter,
		spring,
	}seasons;

	SeasonalEmployee();
	~SeasonalEmployee();

private:
	seasons season;
	double piecePay;
};

SeasonalEmployee::SeasonalEmployee()
{
}

SeasonalEmployee::~SeasonalEmployee()
{
}