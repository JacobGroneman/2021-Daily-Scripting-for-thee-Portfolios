#include <iostream>
#include <ctime>

void PlayGameAtDifficulty(int difficulty)
{
	//Stuffs
}

int main()
{
	int difficulty = 2;
	int maxDifficulty = 10;
	
	while (difficulty <= maxDifficulty)
	{
		PlayGameAtDifficulty(difficulty);
		std::cin.clear();
		std::cin.ignore();
		++difficulty;
	}

	std::cout << "WOW - you're a master codebreaker!\n";
	return 0; //<--Exits without an error code
}